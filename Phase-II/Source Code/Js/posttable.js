$(window).load(function(){	
	 
	 $("#PostTable").DataTable({
	        responsive: true,
	        dom: '<"html5buttons"B>lTfgitp',
	        buttons: [{ extend: 'csv', text: 'Export CSV' }]
	       
	 });
	
	 $.ajax({
	      type: "GET",
	      url: "LoadPostDetailsCode",
	      dataType: "json",	      
	      success: function(data) { 
	    	   $("#PostData").empty();
	    	   $.each(data, function(i,item1){
               $("#PostTable tbody").append("<tr><td hidden='true'>"+item1.postid11+"</td><td>"+item1.name+"</td><td>"+item1.title+"</td><td>"+item1.date1+"</td><td><a class='btn btn-md btn-warning' href='UpdatePost.html?postid="+item1.postid11+"'>Edit</a></td><td><a class='btn btn-md btn-danger' href='DeletePost.html?postid="+item1.postid11+"'>Delete</a></td></tr>");	
	          });
	      }

	 }); 
	
	
	
 });


