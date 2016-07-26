$(function() {          
       
	$("#semanticsubmit").click(function(){
    	var item=$("#search").val();
        $.ajax({
	    	      type: "GET",
	    	      dataType: "json",
	    	      data:{searchterm:$("#search").val()},
	    	      url: "SemanticSearchCode",	    	   	    	    
	    	      success: function(data) {	
	    	    	  $("#results").empty();
	     	          $("#results").append("Results for <b>" + item + "</b>");
	     	          $.each(data, function(i,item1){	     	        	 
	     	          $("#results").append("<div class='col-md-12' style='font-family:'Times New Roman'><font size='3'><a href='Results.html?postid="+item1.postid+"'>" + item1.title + "</a></font></div><br/><br/>");
	     	          });
	    	    
	    	      }

	    	 });   
       
      });  
});
 