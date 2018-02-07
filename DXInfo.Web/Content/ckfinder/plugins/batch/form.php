<div style=\"padding: 20px \"> 
	<h2>Upload Multiple New Files</h2>
	<p>&nbsp; &nbsp; Requires HTML5 enabled browser</p>
		
	<form action='/ckfinder/core/connector/php/connector.php?command=FileUpload&type=<?=$_GET['type'] ?>&currentFolder=<?=$_GET['currentFolder'] ?>' method='post' enctype='multipart/form-data'> 
	 <input type='hidden' name='selected_folder' value='"+ currentFolder +"'> 
	 <input name='upload[]' size='60' type=file multiple > &nbsp; &nbsp; 
	 <input type='submit'>
	</form>
    
    <div>&nbsp; &nbsp; Uploading multiple large files may take several minutes.<br />
    	 &nbsp; &nbsp; Please note: server configuration allows for up to <?=ini_get('max_file_uploads') ?> files per upload.<br />
   		 &nbsp; &nbsp; view other <a href="?<?=rand(1,99999) ?>" onClick="document.getElementById('restrictions').style.display = 'block'; return false">configuration restrictions</a>.	
    </div>
    <ul id="restrictions" style="display: none; list-style-position: inside; margin-left: 20px">
     	<li>Largest single file size: <?=ini_get('upload_max_filesize') ?> </li>
        <li>Total size of uploaded files: <?=ini_get('post_max_size') ?> </li>
        <li>Allocated memory: <?=ini_get('memory_limit') ?> (note: processing an RBG images takes 4 &times; the file's size, on average) </li>
        <li>Server times out after <?=ini_get('max_execution_time') ?> seconds</li>
    </ul>
</div>