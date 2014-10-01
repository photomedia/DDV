            var PRECISION = 10;      // number of decimal places  
            var viewer = null;            
            var pointerStatus = "-";
            var ColumnNumber = 0;
            var ColumnRemainder = "-";
            var PositionInColumn = "-";
            var ColumnWidthNoPadding= iLineLength * pixelSize;
            var iNucleotidesPerColumn = iLineLength * originalImageHeight / pixelSize;
            var ColumnWidth = ColumnPadding + ColumnWidthNoPadding;
            var originalAspectRatio = originalImageHeight/originalImageWidth;
            var Nucleotide = "-";
            var NucleotideY = "-";
            var nucNumX = 0;
            var nucNumY = 0;
            
            var mySequence;
            var theSequenceSplit="";
            var theSequence="";
            var fragmentid="";
            var sequence_data_loaded=0;
            var sequence_data_viewer_initialized=0;
            
            function init() {
                viewer = OpenSeadragon({
								        id: "container",
								        prefixUrl: "img/",
								        showNavigator: true,
								        tileSources: ["GeneratedImages/dzc_output.xml" ],
								        maxZoomPixelRatio: 6
								    });
								viewer.scalebar({
		                type: OpenSeadragon.ScalebarType.MAP,
		                pixelsPerMeter:2,
		                minWidth: "70px",
		                location: OpenSeadragon.ScalebarLocation.BOTTOM_LEFT,
		                xOffset: 5,
		                yOffset: 10,
		                stayInsideImage: false,
		                color: "rgb(30, 30, 30)",
		                fontColor: "rgb(10, 10, 10)",
		                backgroundColor: "rgba(255, 255, 255, 0.5)",
		                fontSize: "normal",
		                barThickness: 1,
		                sizeAndTextRenderer: OpenSeadragon.ScalebarSizeAndTextRenderer.BASEPAIR_LENGTH
		            });

            		OpenSeadragon.addEvent(viewer.element, "mousemove", showNucleotideNumber);
            		
            		//copy content of pointed at sequence fragment to result log
            		  $('body').keyup(function (event) {
						    	if (theSequence){
								    if (event.keyCode == 67) {
								      $("#outfile").prepend("<div class='sequenceFragment'><div style='background-color:#f0f0f0;'>"+fragmentid+"</div>"+theSequence+"</div>");
								    }
								  }
								  });
            		
            			
            		
            		
            }
            
            function showNucleotideNumber(event) {
            	
      					
                // getMousePosition() returns position relative to page,
                // while we want the position relative to the viewer
                // element. so subtract the difference.
                var pixel = OpenSeadragon.getMousePosition(event).minus
                    (OpenSeadragon.getElementPosition(viewer.element));

                document.getElementById("mousePixels").innerHTML 
                    = toString(pixel, true);
                                    
                if (!viewer.isOpen()) {
                    return;
                }
                
                var point = viewer.viewport.pointFromPixel(pixel);
                
                document.getElementById("mousePoints").innerHTML 
                    = toString(point, true);
                    
                document.getElementById("nucleotideNumberX").innerHTML 
                    = point.x;
                document.getElementById("nucleotideNumberY").innerHTML 
                    = point.y;
                
                
                if ((point.x < 0) || (point.x > 1)) {
                	nucNumX="-";
                	Nucleotide = "-";
                	pointerStatus = "Outside of Image (X)";
                	
                }
                else {
                	nucNumX=(point.x * originalImageWidth).toFixed(0);
                }
                
                if ((point.y < 0) || (point.y > originalAspectRatio)){
                	nucNumY="-";
                	Nucleotide = "-";
                	pointerStatus = "Outside of Image (Y)";
                }
                else {
                	nucNumY=(point.y * originalImageWidth).toFixed(0);
                }
                
                if ((nucNumX != "-")&&(nucNumY != "-")){
                	ColumnNumber = Math.floor(nucNumX/ColumnWidth);
                	ColumnRemainder = nucNumX % ColumnWidth;
                	
                	PositionInColumn = Math.floor(ColumnRemainder / pixelSize) + 1;
                	NucleotideY = iLineLength * Math.floor(nucNumY/pixelSize);
                	
									if ((ColumnRemainder <= ColumnWidth) && (ColumnRemainder >= ColumnWidthNoPadding )){
										ColumnNumber = "-";
										Nucleotide="-";
										PositionInColumn="-";
										pointerStatus = "Outside of Image (Inbetween Columns)";
									}
									else {
										Nucleotide = iNucleotidesPerColumn * ColumnNumber + NucleotideY + PositionInColumn;
										if (Nucleotide > ipTotal) {
											//End of Sequence
											Nucleotide = "-";
										}
						
								}
              	}
               
                document.getElementById("Nucleotide").innerHTML = Nucleotide; 
                
                //show sequence fragment
                if (sequence_data_viewer_initialized){
                    var lineNumber="-";
                    if ($.isNumeric(Nucleotide)){
                    	 lineNumber=Math.floor (Nucleotide  /iLineLength);
                    	 remainder=Nucleotide % iLineLength;
                    	 if (lineNumber>0){
                    	 	theSequence=theSequenceSplit[lineNumber-1]+theSequenceSplit[lineNumber]+theSequenceSplit[lineNumber+1];
                    	 	tempTo=((lineNumber+2)*iLineLength);
                    	 	if (ipTotal < tempTo){tempTo=ipTotal;}
                    	 	fragmentid= "Sequence fragment at ["+Nucleotide+"], showing: ("+((lineNumber-1)*iLineLength+1)+" - "+tempTo+")";
                    	 	mySequence.setSequence(theSequence,fragmentid);
                    	 	mySequence.setSelection(remainder+iLineLength, remainder+iLineLength);
                    		}
                    		else{
                    		theSequence=theSequenceSplit[lineNumber]+theSequenceSplit[lineNumber+1];
                    		fragmentid= "Sequence fragment at ["+Nucleotide+"], showing: "+((lineNumber)*iLineLength+1)+" - "+((lineNumber+2)*iLineLength)+")";
                    	 	mySequence.setSequence(theSequence,fragmentid);
                    		 mySequence.setSelection(remainder, remainder);
                    		}
                    		
                    		$('#SequenceFragmentInstruction').show();
                    	 
                    }
                    else{
  												mySequence.clearSequence("");
  												theSequence="";
  												fragmentid="";
  												$('#SequenceFragmentInstruction').hide();
                    }
                  }
                    
            }
            
            function toString(point, useParens) {
                var x = point.x;
                var y = point.y;
                
                if (x % 1 || y % 1) {         // if not an integer,
                    x = x.toFixed(PRECISION); // then restrict number of
                    y = y.toFixed(PRECISION); // decimal places
                }
                
                if (useParens) {
                    return "(" + x + ", " + y + ")";
                } else {
                    return x + " x " + y;
                }
            }
            
            function addLoadEvent(func) {

						    var oldonload = window.onload;
						    if (typeof window.onload != 'function') {
						        window.onload = func;
						    }
						    else {
						        window.onload = function () {
						            if (oldonload) {
						                oldonload();
						            }
						            func();
						        }
						
						    }
						
						}
            
            
           function getSequence() {
           		 
           	
           	$.ajax({xhr: function()
    {
      var xhr = new window.XMLHttpRequest();
     //Download progress
     xhr.addEventListener("progress", function(evt){
       if (evt.lengthComputable) {
         var percentComplete = (evt.loaded / evt.total)*100;
         //Do something with download progress
         if (percentComplete < 100){
         	$("#status").html("<img src='../../loading.gif' /> Loading sequence data: "+parseFloat(percentComplete).toFixed(2) + "% complete");
         	}
         else {
         	$("#status").html("Sequence data loaded.");
         	sequence_data_loaded=1;
         }
       }
       else {
       $("#status").html("<img src='../../loading.gif' />Loading sequence data  ... [ "+parseFloat(evt.loaded / 1048576).toFixed(2) +" MB loaded ]");
       }
     }, false);
    return xhr;
   },
                    type: "GET",
                    url: direct_data_file,
                    contentType: "text/html",
                    success: initSequence,
                    error: processInitSequenceError
                });
						}
            
            function initSequence (theSequence) {
           		theSequenceSplit=theSequence.split("\n");
           		theSequenceSplit.splice(0,1);
           		mySequence = new Biojs.Sequence({
													sequence : "",
													target : "SequenceFragmentFASTA",
													format : 'FASTA',
													columns : {size:70,spacedEach:0} ,
													formatSelectorVisible: false,
													fontSize: '11px',
											}); 
							sequence_data_viewer_initialized=1;
							mySequence.clearSequence("");
							$('#SequenceFragmentInstruction').hide();
							
						}
						
						function processInitSequenceError() {
							//do nothing
						};
            
            addLoadEvent(init);
            addLoadEvent(getSequence);
            
            function outputTable (){      
            	outputTable1 = '<table id="output" style="border: 1px solid #000000;"><tr><th>Nucleotide Number</th><td id="Nucleotide">-</td></tr></table><div id="base"></div><div id="SequenceFragmentFASTA" style="height:80px;"><div id="SequenceFragmentInstruction">press "c" to copy fragment to Result Log</div></div>';
            	outputTable21 = '<table class="output" style="border: 1px solid #000000;visibility:hidden;display:none;"><tr><th class="name"> </th><th class="value">Pixels</th><th class="value">Points</th></tr>';
            	outputTable22 = '<tr><th>Mouse position</th><td id="mousePixels">-</td><td id="mousePoints">-</td></tr><tr><th>X, Y</th><td id="nucleotideNumberX">-</td><td id="nucleotideNumberY">-</td><td></td></tr>';
            	outputTable23 = '<tr><th>(X, Y)</th><td id="NucleotideNumberX">-</td><td id="NucleotideNumberY">-</td></tr><tr><th>Column Number</th><td id="ColumnNumber">-</td><td id="ColumnRemainder">-</td></tr>';
            	outputTable24 = '<tr><th>Nucleotide Number</th><td id="Nucleotide">-</td><td>-</td></tr><tr><th>Nucleotides in Local Column</th>   <td id="NucleotideY">-</td><td>-</td></tr>';
            	outputTable25 = '<tr><th>Position in Column</th><td id="PositionInColumn">-</td><td></td></tr><tr><th>Nucleotides Per Column</th><td id="iNucleotidesPerColumn">-</td><td></td></tr>';
            	outputTable26 = '<tr><th>Aspect Ratio</th><td id="aspectRatio">-</td><td></td></tr><tr><th>Viewport dimensions</th><td id="viewportSizePixels">-</td><td id="viewportSizePoints">-</td></tr></table>';        	
            	document.write(outputTable1);
            	document.write(outputTable21);
            	document.write(outputTable22);
            	document.write(outputTable23);
            	document.write(outputTable24);
            	document.write(outputTable25);
            	document.write(outputTable26);
            }