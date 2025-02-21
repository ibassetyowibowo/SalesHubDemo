// npm package: datatables.net-bs5
// github link: https://github.com/DataTables/Dist-DataTables-Bootstrap5

$(function () {
    'use strict';

    $(function () {


        $('#dataTableExample').DataTable({
            "aLengthMenu": [
                [10, 30, 50, -1],
                [10, 30, 50, "All"]
            ],
            "iDisplayLength": 10,
            "language": {
                search: ""
            }
        });
        $('#dataTableExample').each(function () {
            var datatable = $(this);
            // SEARCH - Add the placeholder for Search and Turn this into in-line form control
            var search_input = datatable.closest('.dataTables_wrapper').find('div[id$=_filter] input');
            search_input.attr('placeholder', 'Search');
            search_input.removeClass('form-control-sm');
            // LENGTH - Inline-Form control
            var length_sel = datatable.closest('.dataTables_wrapper').find('div[id$=_length] select');
            length_sel.removeClass('form-control-sm');
        });
    });



    // #region RUID Datatable

    //$('#dtRuid').DataTable({
    //    "aLengthMenu": [
    //        [5, 10, 30, 50, -1],
    //        [5, 10, 30, 50, "All"]
    //    ],
    //    "iDisplayLength": 5,
    //    "language": {
    //        search: ""
    //    }
    //});

	window.tableRuid = $('#dtRuid').DataTable({
		"aLengthMenu": [
			[10, 30, 50, 100, 150],
			[10, 30, 50, 100, 150]
		],
		"columnDefs": [
			{
				"targets": 0,
				"orderable": false
			}
		],
		"pageLength": 10,
		"processing": false,
		"serverSide": true,
		"ajax": {
			"url": UrlOrigin + '/RUID/GetFilteredTickets',
			"type": "POST",
			"data": function (d) {
				d.status = $('#statusFilter').val();
			},
			"beforeSend": function () {
				showLoading();
			},
			"complete": function () {
				stopLoading();
			},
			// Error handling for AJAX request
			"error": function (xhr, status, error) {
				// You can log the error to the console for debugging
				console.error("AJAX Error: " + status + ": " + error);

				// Handle different types of errors
				if (xhr.status === 0) {
					alert("Network error. Please check your internet connection.");
				} else if (xhr.status === 404) {
					alert("Requested page not found. [404]");
				} else if (xhr.status === 500) {
					alert("Internal Server Error. [500]");
				} else {
					alert("An unknown error occurred: " + error);
				}

				// Optionally, you can display a message in the DataTable to indicate the error
				$('#dtRuid').DataTable().clear().draw();
				$('#dtRuid').after('<div class="error-message">An error occurred while loading the data. Please try again later.</div>');

				//window.location = UrlOrigin + '/Home/Logout';
			}
		},
		"columns": [
			{ "data": "action" },
			{ "data": "ticketNo" },
			{ "data": "pemohonFullname" },
			{ "data": "pemohonDivisiUnit" },
			{ "data": "status" },
			{ "data": "creator" },
			{ "data": "updater" }
		],
		"drawCallback": function (settings) {
			try {
				feather.replace(); // Assuming feather icons are being used
			} catch (error) {
				console.error("Error during drawCallback:", error);
			//	window.location = UrlOrigin + '/Home/Logout';
			}
		},
		// Additional error handling when the table is drawn
		"error": function (settings, techNote, message) {
			console.error("DataTable Render Error: ", techNote, message);
			//window.location = UrlOrigin + '/Home/Logout';
		}
	});


	$('#statusFilter').on('change', function () { tableRuid.draw(); });


    $('#dtRuid').each(function () { 
        var datatable = $(this);
        // SEARCH - Add the placeholder for Search and Turn this into in-line form control
        var search_input = datatable.closest('.dataTables_wrapper').find('div[id$=_filter] input');
        search_input.attr('placeholder', 'Search');
        search_input.removeClass('form-control-sm');
        // LENGTH - Inline-Form control
        var length_sel = datatable.closest('.dataTables_wrapper').find('div[id$=_length] select');
        length_sel.removeClass('form-control-sm');
    });

    // #endregion RUID Datatable

});