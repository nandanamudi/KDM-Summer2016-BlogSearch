$().ready(function () {
    $("#response_report").DataTable({
        responsive: true,
        dom: '<"html5buttons"B>lTfgitp',
        buttons: [{ extend: 'csv', text: 'Export CSV' }],
        "pageLength": 50

    });
});