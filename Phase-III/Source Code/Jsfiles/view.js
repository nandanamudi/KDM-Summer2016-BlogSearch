 $(window).load(function() {  
       var postid = getUrlVars()["postid"];    
        $("#postid").val(postid); 
        loadPost();  
        CancelPost();
        loadComments();
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
	     	             $("#date").html(item1.date1);
	     	             $("#email").html(item1.email);
	     	             $("#name").html(item1.name);
	     	             $("#post").html(item1.post1);
	     	            	               	        	 
	     	         });
	    	      }

	    	 });
     }   
     
     function loadComments()
     {
       var postid = getUrlVars()["postid"];     
        $.ajax({
	    	      type: "GET",
	    	      url: "GetCommentCode",
	    	      dataType: "json",
	    	      data:{postid:postid},
	    	      success: function(data) {		
	    	         $("#results").empty();    	    		     	         
	     	          $.each(data, function(i,item1){
                        $("#results").append(" <div class='hr-line-dashed'></div><div class='form-group'></strong><p id='comment11' class='col-md-11'>"+item1.sno+ ". "+item1.comment+"</p><div class='col-md-12'> <label class='col-md-5' id='name11'>"+item1.name1+"</label><label class='col-md-5' id='date11'>"+item1.date+"</label></div></div>");	
	     	          });
	    	      }

	    	 });
     }   
     function CancelPost()
     {
     $('#cancelsubmit').click(function(e1) { 
  	   e1.preventDefault();
  	   window.location.href="PublicSearch.html";
     });
     }
     
    