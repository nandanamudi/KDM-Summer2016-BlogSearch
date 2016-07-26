//$(window).load(function(){	
//	 
//	
//	
//	 $.ajax({
//	      type: "GET",
//	      url: "LoadPostDetailsCode",
//	      dataType: "json",	      
//	      success: function(data) { 
//	    	   $("#PostData").empty();
//	    	   $.each(data, function(i,item1){
//               $("#PostTable tbody").append("<tr><td hidden='true'>"+item1.postid11+"</td><td>"+item1.name+"</td><td>"+item1.title+"</td><td>"+item1.date1+"</td><td><a class='btn btn-md btn-warning' href='UpdatePost.html?postid="+item1.postid11+"'>Edit</a></td><td><a class='btn btn-md btn-danger' href='DeletePost.html?postid="+item1.postid11+"'>Delete</a></td></tr>");	
//	          });
//	      }
//
//	 }); 
//	
//	 $("#PostTable").DataTable({
//	        responsive: true,
//	        dom: '<"html5buttons"B>lTfgitp',
//	        buttons: [{ extend: 'csv', text: 'Export CSV' }]
//	       
//	 });
//	
// });

   $(document).ready(function() {
	   $.ajax({
		    url: 'LoadPostDetailsCode',
		    type: 'GET',
		    dataType: 'json',
		    success: function (data) {
		        assignToEventsColumns(data);
		    }
		});
   });

		function assignToEventsColumns(data) {
		var table = $('#PostTable').dataTable({
			 "responsive": true,
			 "dom": '<"html5buttons"B>lTfgitp',		    
		     "bAutoWidth": false,
		    "buttons": [{ extend: 'csv', text: 'Export CSV' }],
		    "aaData": data,
		    "aaSorting": [],
		    "aoColumnDefs": [
		       {
                 "aTargets": [0],
                 "mData": "postid11",
                 "bVisible": false
               },
               {
                 "aTargets": [1], 
                 "mData": "name"
               },
               {
                 "aTargets": [2],
                 "mData": "title"
               },
               {
                 "aTargets": [3],
                  "mData": "date1"
               },
               {
            	   "aTargets": [4],
            	   "mData": "editurl",
                 
               },
               {
                  "aTargets": [5],
                  "mData": "deleteurl",
                 
               },
               
		      ]
		});
		}
  


