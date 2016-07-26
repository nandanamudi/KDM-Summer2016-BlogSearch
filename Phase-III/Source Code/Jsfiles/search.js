$(function() {	
	 $('input:radio').change(function(e) {
		  e.preventDefault();
	    var option=$('input:radio[name=searchradio]:checked').val();
      	  if(option=="wiki"){ 
      		$('#button1').show(); 
      		$('#titlelbl').show();
      		$('#titlelbl1').hide();
      		$('#fieldlbl').hide();
      		$("#searchterm").show();
      		$("#searchterm1").hide();
      		$("#field").hide();
      		$('#searchterm').val('');
      		$('#searchterm1').val('');
      		$('#field').val('');
      	    $("#searchterm").on('input',function(e){ 
      	    	  e.preventDefault();	
 	            var q = $("#searchterm").val();
 	           $.getJSON("http://en.wikipedia.org/w/api.php?callback=?",
 	          {
 	           srsearch: q,
 	           action: "query",
 	           list: "search",
 	           format: "json"
 	          },
 	          function(data) {       
 	          $("#results").empty();
 	          $("#results").append("Results for <b>" + q + "</b>");
 	          $.each(data.query.search, function(i,item){
 	          $("#results").append("<div class='col-md-12' style='font-family:'Times New Roman'><font size='3'><a href='http://en.wikipedia.org/wiki/" + encodeURIComponent(item.title) + "' target='_blank'>" + item.title + "</a></font>" +"<br/>"+ item.snippet + "</div><br/><br/>");
 	          });
 	        }); 	
      	 });
      	 }
     	 else if(option=="global"){ 
     		
      		$('#button1').hide(); 
      		$('#titlelbl1').hide();
      		$('#titlelbl').hide();
      		$('#fieldlbl').show();
      		$("#searchterm").hide();
      		$("#searchterm1").hide();
      		$("#field").show();
      		$('#searchterm').val('');
      		$('#searchterm1').val('');
      		$('#field').val('');
	     }
     	
     	  else if(option=="blog"){ 
     		
       		$('#button1').hide(); 
       		 $('#titlelbl1').show();
       		 $('#titlelbl').hide();
       		 $('#fieldlbl').hide();
       		 $("#searchterm1").show();
       		 $("#searchterm").hide();
       		 $("#field").hide();
       		$('#searchterm').val('');
      		$('#searchterm1').val('');
      		$('#field').val('');
     		 $("#searchterm1").keyup(function(e){ 
       		    e.preventDefault();
	  			var item=$("#searchterm1").val();
                 $.ajax({
	    	      type: "POST",
	    	      url: "SearchPostCode",
	    	      dataType: "json",
	    	      data:{option:$('input:radio[name=data]:checked').val(),searchterm:$("#searchterm1").val()},
	    	      success: function(data) {	
	    	    	  $("#results").empty();
	     	          $("#results").append("Results for <b>" + item + "</b>");
	     	          $.each(data, function(i,item1){	     	        	 
	     	          $("#results").append("<div class='col-md-12' style='font-family:'Times New Roman'><font size='3'><a href='Results.html?postid="+item1.postid+"'>" + item1.title + "</a></font></div><br/><br/>");
	     	          });
	    	      }

	    	   });
     	   });
  
   	    } 
	});
     
});
