


function outputEmbossGeecee(){
	
	outputEmbossGeecee='<h3> \
        SOAP Services \
    </h3> \
    <div id="soapsequenceid">\
    <input type="radio" name="soap_sequence_id" value="sendsequence" id="sendsequence" checked="checked">Send sequence with request</input> <br />\
		<input type="radio" name="soap_sequence_id" value="byrefseq" id="byrefseq"/>Identify sequence by </input> <input type="text" id="refseq" name="refseq" size="25" />  \
		</div>\
	<div style="margin-bottom:10px;"> \
    start: <input id="sbegin" type="text" /> press left-arrow on keyboard to set<br /> \
    end: <input id="send" type="text" /> press right-arrow on keyboard to set<br /> \
	length: <span id="sequenceLength"></span> nucleotides\
	</div> \
    <input id="btnCallWebService" value="Compute G+C" type="button" /> \
	<input id="btnCallWebServiceDensity" value="Compute A, T, G, C" type="button" /> \
    <div style="margin-top:20px;border:1px solid #202020;font-family:Courier New, Courier, Monospace; font-size:9pt;" id="result"> \
    <div id="status" style="min-height:20px;"></div> \
    Result Log: \
    <div id="outfile"></div> \
  	</div> \
	';
	
	document.write(outputEmbossGeecee);
}

function otherCredits(){
	
	outputOtherCredits='<br />Compute  A, T, G, C: <a href="http://emboss.sourceforge.net/apps/release/6.0/emboss/apps/density.html">EMBOSS nucleic composition density</a> SOAP service povider: <a href="http://wsembnet.vital-it.ch">Swiss Institute of Bioinformatics (SIB)</a>\'s <a href="http://wsembnet.vital-it.ch/soaplab2/typed/services/nucleic_composition.density?wsdl">web service</a>. <br /> \
Compute G+C: <a href="http://emboss.sourceforge.net/apps/cvs/emboss/apps/geecee.html">EMBOSS geecee</a> SOAP service povider: <a href="http://wsembnet.vital-it.ch">Swiss Institute of Bioinformatics (SIB)</a>\'s <a href="http://wsembnet.vital-it.ch/soaplab2/typed/services/nucleic_cpg_islands.geecee?wsdl">web service</a>. ';

document.write(outputOtherCredits);
	
}
 
        $(document).ready(function () {
									
				$('#sbegin').change(function() {
  						updateSequenceLength();
				});			
				$('#send').change(function() {
  						updateSequenceLength();
				});	
									
        	 	$("#refseq").val(usa);
                $("#sbegin").val(sbegin);
                $("#send").val(send);
				
				sequenceLength=parseInt(send)-parseInt(sbegin)+1;
				$("#sequenceLength").html(sequenceLength);
                
                
                $('body').keyup(function (event) {
						    var direction = null;
						    // handle cursor keys
						    if (event.keyCode == 37) {
						      // set start
						      direction=$("#Nucleotide").html();
						      $("#sbegin").val(direction);
							  updateSequenceLength();
						    } else if (event.keyCode == 39) {
						      // set end
						      direction=$("#Nucleotide").html();
						      $("#send").val(direction);
							  updateSequenceLength();
						    }
						  	});
                
                
                
            $("#btnCallWebService").click(function (event) {
            	 	//if identify by refseq
            	 	if ($('#byrefseq').is(':checked')) {
            	sbegin=$("#sbegin").val();
                send=$("#send").val();
                if (parseInt(sbegin)>=parseInt(send)) {
            	  	alert('invalid input: start of sequence should be smaller than end');
            	  	return;
            	  	}
            		uiLoading ("Fetching result.");
            		sbegin=$("#sbegin").val();
            		send=$("#send").val();
                var wsUrl = "../../ajax-proxy.php";
                var soapRequest =
'<?xml version="1.0" encoding="utf-8"?> \
<soap:Envelope xmlns:xsi="http://www.w3.org/2001/XMLSchema-instance" \
		xmlns:geecee="http://soaplab.org/geecee" \
    xmlns:xsd="http://www.w3.org/2001/XMLSchema" \
    xmlns:soap="http://schemas.xmlsoap.org/soap/envelope/"> \
  <soap:Body> \
    <geecee:runAndWaitFor> \
	    	  <sequence> \
			      				<usa>'+$("#refseq").val()+'</usa> \
			      				<sbegin>'+sbegin+'</sbegin> \
			      				<send>'+send+'</send> \
		      </sequence> \
    </geecee:runAndWaitFor> \
  </soap:Body> \
</soap:Envelope>';
                $.ajax({
                    type: "POST",
                    url: wsUrl,
                    contentType: "text/xml",
                    dataType: "xml",
                    data: soapRequest,
                    success: processSuccess,
                    error: processError
                });
						}//else if send entire sequence
						else {
            		sbegin=$("#sbegin").val();
                send=$("#send").val();
                if (parseInt(sbegin)>=parseInt(send)) {
            	  	alert('invalid input: start of sequence should be smaller than end');
            	  	return;
            	  	}
            		
            		sbegin=$("#sbegin").val();
            		send=$("#send").val();
                var wsUrl = "../../ajax-proxy.php";
				
								uiLoading ("Sending sequence..");
				
								$.get(direct_data_file, function(sequence) {
												 
                var soapRequest ='<?xml version="1.0" encoding="utf-8"?> \
									<soap:Envelope xmlns:xsi="http://www.w3.org/2001/XMLSchema-instance" \
													xmlns:geecee="http://soaplab.org/geecee" \
    											xmlns:xsd="http://www.w3.org/2001/XMLSchema" \
    											xmlns:soap="http://schemas.xmlsoap.org/soap/envelope/"> \
  								<soap:Body> \
    							<geecee:runAndWaitFor> \
	    	  				<sequence> \
			      				<direct_data>'+sequence+'</direct_data> \
										<format>fasta</format>\
			      				<sbegin>'+sbegin+'</sbegin> \
			      				<send>'+send+'</send> \
		      				</sequence> \
    							</geecee:runAndWaitFor> \
  								</soap:Body> \
									</soap:Envelope>';
								
									//now send the request
								uiLoading ('Fetching result ...');
								
                $.ajax({
                    type: "POST",
                    url: wsUrl,
                    contentType: "text/xml",
                    dataType: "xml",
                    data: soapRequest,
                    success: processSuccess,
                    error: processError
                });

            });
          };
        });
			
			
			
			
			    $("#btnCallWebServiceDensity").click(function (event) {
			    		//if identify by refseq
            	 	if ($('#byrefseq').is(':checked')) {
            	 		//if identify by refseq
            	sbegin=$("#sbegin").val();
                send=$("#send").val();
                if (parseInt(sbegin)>=parseInt(send)) {
            	  	alert('invalid input: start of sequence should be smaller than end');
            	  	return;
            	  	}
            		uiLoading ("Fetching result");
            		sbegin=$("#sbegin").val();
            		send=$("#send").val();
					sequenceLength=$("#sequenceLength").html();
                var wsUrl = "../../ajax-proxy-density.php";
                var soapRequest =
'<?xml version="1.0" encoding="utf-8"?> \
<soapenv:Envelope xmlns:den="http://soaplab.org/density" xmlns:soapenv="http://schemas.xmlsoap.org/soap/envelope/">\
   <soapenv:Body>\
      <den:runAndWaitFor>\
         <seqall>\
			      				<usa>'+$("#refseq").val()+'</usa> \
			      				<sbegin>'+sbegin+'</sbegin> \
			      				<send>'+send+'</send> \
		</seqall>\
         <graph_format>png</graph_format> \
         <window>'+sequenceLength+'</window> \
         <display>D</display> \
	 </den:runAndWaitFor>\
   </soapenv:Body>\
</soapenv:Envelope>';
								//else if send entire sequence
								
								//send request
                $.ajax({
                    type: "POST",
                    url: wsUrl,
                    contentType: "text/xml",
                    dataType: "xml",
                    data: soapRequest,
                    success: processSuccessDensity,
                    error: processError
                });
            	 	}
            	 	else{
            	sbegin=$("#sbegin").val();
                send=$("#send").val();
                if (parseInt(sbegin)>=parseInt(send)) {
            	  	alert('invalid input: start of sequence should be smaller than end');
            	  	return;
            	  	}
            		
            		sbegin=$("#sbegin").val();
            		send=$("#send").val();
								sequenceLength=$("#sequenceLength").html();
                var wsUrl = "../../ajax-proxy-density.php";
				
								uiLoading ("Sending sequence");
				
								$.get(direct_data_file, function(sequence) {
												 
                var soapRequest ='<?xml version="1.0" encoding="utf-8"?> \
								<soapenv:Envelope xmlns:den="http://soaplab.org/density" xmlns:soapenv="http://schemas.xmlsoap.org/soap/envelope/">\
   							<soapenv:Body>\
      					<den:runAndWaitFor>\
         				<seqall>\
			      			<direct_data>'+sequence+'</direct_data> \
									<format>fasta</format>\
			      			<sbegin>'+sbegin+'</sbegin> \
			      			<send>'+send+'</send> \
								</seqall>\
         				<graph_format>png</graph_format> \
         				<window>'+sequenceLength+'</window> \
         				<display>D</display> \
	 							</den:runAndWaitFor>\
   							</soapenv:Body>\
								</soapenv:Envelope>';
								uiLoading ('Fetching result.');
                $.ajax({
                    type: "POST",
                    url: wsUrl,
                    contentType: "text/xml",
                    dataType: "xml",
                    data: soapRequest,
                    success: processSuccessDensity,
                    error: processError
                });
								
            });
          };
        });
		});
		
		function cleanoutput (s) {
			
				var cleanoutput="";
				var lines = s.split('\n');
				for (var i = 0; i < lines.length; i++) {
					if ((lines[i].substring(0, 1) != "#")&&(lines[i].length>2)){
						temp=lines[i];
						temp=temp.replace(/\sgc/g, "g+c");
						temp=temp.replace(/\sat\s/g, "a+t ");
						temp=temp.replace(/\s/g,"&nbsp;");
						cleanoutput=cleanoutput+"<br />"+temp;
					}
				}
				return cleanoutput;
		}

		function cleanoutputGeecee (s) {
			
				var cleanoutput="";
				var lines = s.split('\n');
				for (var i = 0; i < lines.length; i++) {
					
						temp=lines[i];
						temp=temp.replace(/\sGC\s/g, " G+C ");
						temp=temp.replace(/\s/g,"&nbsp;");
						cleanoutput=cleanoutput+"<br />"+temp;
						
					
				}
				return cleanoutput;
		}
		
		function updateSequenceLength(){
			sbegin=$("#sbegin").val();
            send=$("#send").val();
			sequenceLength=parseInt(send)-parseInt(sbegin)+1;
			$("#sequenceLength").html(sequenceLength);
		}
        
        
        function uiLoading (message) {
        	//var bufferOutFile=$("#outfile").val();
        	$("#status").html(" <img src='../../loading.gif' style='float:left;' /><div style='font-size:11pt;padding-top:10px;padding-bottom:15px;'>"+message+"</div>" );
        	
        }

        function processSuccess(data, status, req) {
        	    
            if (status == "success"){
             
                // Get a jQuery-ized version of the response.
							  var xml = $(req.responseXML);
								
								var resultOutfile = xml.find( "outfile" ).text();
								//var bufferOutFile=$("#outfile").val();
								var detailedStatus = xml.find("detailed_status").text();
								if (detailedStatus == "0"){
									$("#outfile").append("<br />OK. nucleotides "+sbegin+"-"+send+" "+cleanoutputGeecee(resultOutfile)+"<div style='border-top:1px solid #a0a0a0;'></div>");
								}
								else {
									resultOutfile = xml.find( "report" ).text();
									$("#outfile").append("<br />Failed.  nucleotides "+sbegin+"-"+send+" Error Report:"+resultOutfile+"<div style='border-top:1px solid #a0a0a0;'></div>");
								}
								$("#status").html("" );
              }
        }
		
		 function processSuccessDensity(data, status, req) {
        	    
            if (status == "success"){
             
                // Get a jQuery-ized version of the response.
							  var xml = $(req.responseXML);
								
								var resultOutfile = xml.find( "outfile" ).text();
								//remove all comments from result and format as HTML
								//var bufferOutFile=$("#outfile").val();
								var detailedStatus = xml.find("detailed_status").text();
								
								if (detailedStatus == "0"){
									$("#outfile").append("<br />OK. nucleotides "+sbegin+"-"+send+" "+cleanoutput(resultOutfile)+"<div style='border-top:1px solid #a0a0a0;'></div>");
								}
								else {
									resultOutfile = xml.find( "report" ).text();
									$("#outfile").append("<br />Failed.  nucleotides "+sbegin+"-"+send+" Error Report:"+resultOutfile+"<div style='border-top:1px solid #a0a0a0;'></div>");
								}
								$("#status").html("" );
              }
        }

        function processError(data, status, req) {
            var error = req.responseText + " " + status;
            $("#outfile").append( error );
            $("#status").html("" );
        }  

