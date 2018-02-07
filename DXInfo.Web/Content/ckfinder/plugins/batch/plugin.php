<?php if (!defined('IN_CKFINDER')) exit;

require_once CKFINDER_CONNECTOR_LIB_DIR . "/CommandHandler/FileUpload.php";
class Batch extends CKFinder_Connector_CommandHandler_FileUpload {

	public function uploadFiles(&$command){
		
		if($command != "FileUpload"){ return true; }  // if given command wasn't to upload files, don't use this plug in.
				
		if(!is_array($_FILES['upload']['tmp_name'])){ return true; } // if only one file was uploaded through normal upload form, use regulard method for handling.

		$this->_errorHandler->setCatchAllErros(true); // turn off error handling in FileUpload->sendResponse() function	
		
		$uploaded_files = $_FILES; // make a copy of the $_FILES array for use below

		for($i=0; $i<count($uploaded_files['upload']['tmp_name']); $i++){
			unset($_FILES);
			$_FILES['upload']['name'] = $uploaded_files['upload']['name'][$i];
			$_FILES['upload']['type'] = $uploaded_files['upload']['type'][$i];
			$_FILES['upload']['tmp_name'] = $uploaded_files['upload']['tmp_name'][$i];
			$_FILES['upload']['error'] = $uploaded_files['upload']['error'][$i];
			$_FILES['upload']['size'] = $uploaded_files['upload']['size'][$i];
			
			// run the normal FileUpload function to process a single file:
			$this->sendResponse();	
		}
	
		echo '<style type="text/css"> * { background-color: #E0E0E0; } </style>';
		echo '<h2>Upload Complete!</h2>';
		echo '<script>window.parent.location.reload(true);</script>'; //refresh ckFinder
		return false; //stop
    }	
}

//Register the javascript plugin named "batch"
$config['Plugins'][] = 'batch';

$batch = new Batch();
$config['Hooks']['BeforeExecuteCommand'][] = array($batch, 'uploadFiles');