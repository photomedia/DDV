<?php
ini_set('max_execution_time', '300');
// Allowed hostname
//define ('HOSTNAME', 'http://www.ebi.ac.uk/soaplab/typed/services/nucleic_composition.density');
define ('HOSTNAME', 'http://wsembnet.vital-it.ch:80/soaplab2-axis/typed/services/nucleic_composition.density');

//returns the headers as an array
function getHeaders()
{
    $headers = array();
    foreach ($_SERVER as $k => $v)
    {
        if (substr($k, 0, 5) == "HTTP_")
        {
            $k = str_replace('_', ' ', substr($k, 5));
            $k = str_replace(' ', '-', ucwords(strtolower($k)));
            $headers[$k] = $v;
        }
    }
    return $headers;
} 

// Get the REST call path from the AJAX application
//$path = $_REQUEST['path'];
$url = HOSTNAME;//.$path;

// Open the Curl session
$session = curl_init($url);

// If it's a POST, put the POST data in the body
/*
if ($_POST['path']) {
 $postvars = '';
 while ($element = current($_POST)) {
  $postvars .= key($_POST).'='.$element.'&';
  next($_POST);
 }
 $postvars = substr($postvars, 0, -1);  // removing trailing &
 $postvars = str_replace('xml_version', 'xml version', $postvars);  // fix xml declaration bug?
 $postvars = stripslashes($postvars);

 curl_setopt ($session, CURLOPT_POST, true);
 curl_setopt ($session, CURLOPT_POSTFIELDS, $postvars);
}
else {
	*/
 //If it's a post, but not a form post (like a SOAP request)
 if ($_SERVER['REQUEST_METHOD']==='POST') {
  curl_setopt ($session, CURLOPT_POST, true);
  curl_setopt ($session, CURLOPT_POSTFIELDS, $HTTP_RAW_POST_DATA);

  $headers = getHeaders();
  //$header_array = Array( "Content-Type: text/xml", "SOAPAction: " . $headers['SOAPAction']);
  $header_array = Array( "Content-Type: text/xml");
  curl_setopt ($session, CURLOPT_HTTPHEADER, $header_array);
  curl_setopt ($session, CURLOPT_CUSTOMREQUEST, "POST");
 }
//}

// Don't return HTTP headers. Do return the contents of the call
curl_setopt($session, CURLOPT_HEADER, false);
curl_setopt($session, CURLOPT_RETURNTRANSFER, true);

// Make the call
$xml = curl_exec($session);

// The web service returns XML. Set the Content-Type appropriately
header("Content-Type: text/xml");

echo $xml;
curl_close($session);

?>