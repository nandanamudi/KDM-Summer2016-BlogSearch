$(window).load(function() {          
       
        loadProfile();      
       
      });     
    
     
     function loadProfile()
     {  
    	
        $.ajax({
	    	      type: "GET",
	    	      url: "GetProfileCode",
	    	      dataType: "json",	    	    
	    	      success: function(data) {	
	    	    	  console.log(data);
	     	          $.each(data, function(i,item1){	     	            
	     	            $("#name").html(item1.name123);
	     	            $("#username").html(item1.username123);
	     	           $("#emailid").html(item1.emailid123);
	     	           $("#address").html(item1.address123);	     	             
	     	            	               	        	 
	     	         });
	    	      }

	    	 });
     }   
     