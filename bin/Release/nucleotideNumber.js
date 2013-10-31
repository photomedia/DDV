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
            
            function init() {
                viewer = OpenSeadragon({
								        id: "container",
								        prefixUrl: "img/",
								        showNavigator: true,
								        tileSources: ["GeneratedImages/dzc_output.xml" ],
								        maxZoomPixelRatio: 6
								    });
            		OpenSeadragon.addEvent(viewer.element, "mousemove", showNucleotideNumber);
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
               
                document.getElementById("Nucleotide").innerHTML 
                    = Nucleotide; 
				/*
                document.getElementById("NucleotideY").innerHTML 
                    = NucleotideY; 
                document.getElementById("PositionInColumn").innerHTML 
                    = PositionInColumn;
                document.getElementById("NucleotideNumberX").innerHTML 
                    = nucNumX;  
                document.getElementById("NucleotideNumberY").innerHTML 
                    = nucNumY; 
                document.getElementById("ColumnNumber").innerHTML 
                    = ColumnNumber;
                document.getElementById("ColumnRemainder").innerHTML 
                    = ColumnRemainder;
				*/
            }
            
			/*
            function showViewport(viewer) {
                if (!viewer.isOpen()) {
                    return;
                }
                
                var sizePoints = viewer.viewport.getBounds().getSize();
                var sizePixels = viewer.viewport.getContainerSize();
                var aspectRatio = viewer.viewport.getAspectRatio();
                
				
                document.getElementById("viewportSizePoints").innerHTML 
                    = toString(sizePoints, false);
                    
                document.getElementById("viewportSizePixels").innerHTML 
                    = toString(sizePixels, false);
                    
                 document.getElementById("aspectRatio").innerHTML 
                    = aspectRatio;
                
                 document.getElementById("iNucleotidesPerColumn").innerHTML 
                    = iNucleotidesPerColumn;
                        
            }
			*/
            
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
            
            addLoadEvent(init);
            
            
            function outputTable (){      
            	outputTable1 = '<table id="output" style="border: 1px solid #000000;"><tr><th>Nucleotide Number</th><td id="Nucleotide">-</td></tr></table>';
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