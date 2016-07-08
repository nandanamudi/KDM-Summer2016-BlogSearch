 $(window).load(function() {  
       var postid = getUrlVars()["postid"];    
        $("#postid").val(postid); 
        loadPost();       
        UpdatePost();   
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
     
     
     function UpdatePost()
     {     
    	 
           $('#updatesubmit').click(function(e) {        	   
       	    e.preventDefault();  	   
    	       $.ajax({
    	       type: "GET",
    	       url: "UpdatePostCode",
    	       data:{postid:$("#postid").val(),post:$("#post").val()},
    	       success: function(data) {
    	       alert("Your post has been updated");
    	       window.location.href="EditPost.html"
    	         
    	    }
    	});     
    });
  } 