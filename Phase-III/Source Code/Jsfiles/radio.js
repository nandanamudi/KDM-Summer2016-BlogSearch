$(function() {
    $('input:radio').change(function() {
      if ($(this).val() == "file12") {  
    	  $('#titlelbl').show();
    	  $('#title').show();
    	  $('#file').show();
    	  $('#uplbl').show();
    	  $('#text').hide();
    	  $('#post').hide();
    	  $('#submit').show();
    	  $('#res').hide();
 	     $('#result').hide();
      }
      else{
    	  $('#titlelbl').show();
    	  $('#title').show();
    	  $('#text').show();
    	  $('#post').show();
    	  $('#file').hide();
    	  $('#uplbl').hide();
    	  $('#submit').show();
    	  $('#res').hide();
  	     $('#result').hide();
      }
    	  
    });
});
