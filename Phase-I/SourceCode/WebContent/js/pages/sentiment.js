$(function() {
    $('input:submit').click(function(e) {
    	e.preventDefault();
    	$.ajax({
    	    type: "POST",
    	    url: "CreatePostCode",
    	    data:{option:$('input:radio[name=data]:checked').val(),post:$("#post").val(),file:$('#upload').prop('files')},
    	     success: function(data) {
    	     $('#res').show();
    	     $('#result').show();
    	     $('#result').html(data);
    	    }
    	});
     
    });
});