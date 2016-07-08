 $(window).load(function() {  
       var postid = getUrlVars()["postid"];    
        $("#postid").val(postid); 
        loadPost();       
        DeletePost();   
      });
     
     function getUrlVars() 
     {
       var vars = {};
       var parts = window.location.href.replace(/[?&]+([^=&]+)=([^&#]*)/gi, function(m,key,value) {
       vars[key] = value;
       });
      return vars;
     }
     
     function loadPost()
     {    
       var postid = getUrlVars()["postid"];         
        $.ajax({
	    	      type: "GET",
	    	      url: "GetPostCode",
	    	      dataType: "json",
	    	      data:{postid:postid},
	    	      success: function(data) {		    	            	    
	     	          $.each(data, function(i,item1){	     	            
	     	             $("#post").html(item1.post1);
	     	            
	     	            	               	        	 
	     	         });
	    	      }

	    	 });
     }   
     
     
     function DeletePost()
     {     
    	 
           $('#deletesubmit').click(function(e) {   
            e.preventDefault();  	   
    	       $.ajax({
    	       type: "GET",
    	       url: "DeletePostCode",
    	       data:{postid:$("#postid").val()},
    	       success: function(data) {
    	       alert("Your post has been Deleted");
    	       window.location.href="EditPost.html"
    	         
    	    }
    	});     
    });
  } 