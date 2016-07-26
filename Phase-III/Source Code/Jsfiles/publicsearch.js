 $(document).ready(function() {
	   $.ajax({
		    url: 'LoadPublicSearchCode',
		    type: 'GET',
		    dataType: 'json',
		    success: function (data) {
		        assignToEventsColumns(data);
		       
		    }
		});
   });

		function assignToEventsColumns(data) {
		var table = $('#PostTable1').DataTable({
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
                   "mData": "post1",
                   "bVisible": false
               },
               {
                 "aTargets": [2], 
                 "mData": "name"
               },
               {
                 "aTargets": [3],
                 "mData": "title"
               },
               {
                 "aTargets": [4],
                  "mData": "date1"
               },
               {
            	   "aTargets": [5],
            	   "mData": "viewurl",                  
               },
              
               
		      ]
		});
		}
  