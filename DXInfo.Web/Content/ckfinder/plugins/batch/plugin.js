CKFinder.addPlugin( 'batch', function( api ) {

	api.addFolderContextMenuOption( { 'label' : 'Upload Multiple Files', 'command' : "Batch" } , function( api, file )
	{
		var currentFolder = api.getSelectedFolder();
		var html = CKFinder.ajax.load( '/ckfinder/plugins/batch/form.php?type='+ currentFolder.type +'&currentFolder='+ currentFolder); 
		form = api.replaceUploadForm( html );
		
		CKFinder.tools.callFunction(2,this);
	
	});
	
});