$(function() {          
       
	$("#ontsubmit").click(function(){
    	
        $.ajax({
	    	      type: "GET",
	    	      url: "PostFilesCode",	    	   	    	    
	    	      success: function(data) {	
	    	    	alert("Files have been located");
	    	    
	    	      }

	    	 });   
       
      });  
});
 