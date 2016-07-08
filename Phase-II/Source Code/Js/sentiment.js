$(function() {
    $('#submit').click(function(e) {    	
    	  	e.preventDefault();
    	 	$.ajax({
    	    type: "POST",
    	    url: "CreatePostCode",
    	    data:{option:$('input:radio[name=data]:checked').val(),post:$("#post").val(),filepath:$('input[type=file]').val()},
    	     success: function(data) {
    	     $('#res').show();
    	     $('#result').show();
    	     $('#result').html(data);
    	     $('#acceptsubmit').show();
    	     $('#rejectsubmit').show();
    	    }
    	});     
    });
    
    $('#acceptsubmit').click(function(e) {
    	e.preventDefault();
    	$.ajax({
    	    type: "GET",
    	    url: "CreatePostCode",
    	    data:{option:$('input:radio[name=data]:checked').val(),title:$("#title").val(),post:$("#post").val(),filepath:$('input[type=file]').val()},
    	     success: function(data) {
    	      alert("Your post has been posted to Blogger Locale");
    	      window.location.href='UserHome.html';
    	    }
    	});     
    });
    
    $('#rejectsubmit').click(function(e) {
    	e.preventDefault();
    	window.location.href='UserHome.html';
    	 
    });
    
});