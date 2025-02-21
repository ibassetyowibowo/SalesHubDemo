$(function () {
    'use strict';

    $(function () {

        $('#tbl_anggaranPettyCash').DataTable({
            "aLengthMenu": [
                [10, 30, 50, -1],
                [10, 30, 50, "All"]
            ],
            "iDisplayLength": 10,
            "language": {
                search: ""
            },
            drawCallback: function (settings) {
                feather.replace();
            },
        });

        $('#tbl_anggaranPettyCash').each(function () {
            var datatable = $(this);
            // SEARCH - Add the placeholder for Search and Turn this into in-line form control
            var search_input = datatable.closest('.dataTables_wrapper').find('div[id$=_filter] input');
            search_input.attr('placeholder', 'Search');
            search_input.removeClass('form-control-sm');
            // LENGTH - Inline-Form control
            var length_sel = datatable.closest('.dataTables_wrapper').find('div[id$=_length] select');
            length_sel.removeClass('form-control-sm');
        });

        $('#dataTableExample').DataTable({
            "aLengthMenu": [
                [10, 30, 50, -1],
                [10, 30, 50, "All"]
            ],
            "iDisplayLength": 10,
            "language": {
                search: ""
            },           
            drawCallback: function (settings) {
                feather.replace();
            },
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

        //#region dataTable2

        $('#dataTable2').DataTable({
            "aLengthMenu": [
                [10, 30, 50, -1],
                [10, 30, 50, "All"]
            ],
            "iDisplayLength": 10,
            "language": {
                search: ""
            },
            drawCallback: function (settings) {
                feather.replace();
            },
        });
        // end


        $('#Dt-Kso').each(function () {
            var datatable = $(this);
            // SEARCH - Add the placeholder for Search and Turn this into in-line form control
            var search_input = datatable.closest('.dataTables_wrapper').find('div[id$=_filter] input');
            search_input.attr('placeholder', 'Search');
            search_input.removeClass('form-control-sm');
            // LENGTH - Inline-Form control
            var length_sel = datatable.closest('.dataTables_wrapper').find('div[id$=_length] select');
            length_sel.removeClass('form-control-sm');
        });

        $('#Dt-Kso').DataTable({
            "aLengthMenu": [
                [10, 30, 50, -1],
                [10, 30, 50, "All"]
            ],
            "iDisplayLength": 10,
            "language": {
                search: ""
            },
            drawCallback: function (settings) {
                feather.replace();
            },
        });

        $('#dataTable2').each(function () {
            var datatable = $(this);
            // SEARCH - Add the placeholder for Search and Turn this into in-line form control
            var search_input = datatable.closest('.dataTables_wrapper').find('div[id$=_filter] input');
            search_input.attr('placeholder', 'Search');
            search_input.removeClass('form-control-sm');
            // LENGTH - Inline-Form control
            var length_sel = datatable.closest('.dataTables_wrapper').find('div[id$=_length] select');
            length_sel.removeClass('form-control-sm');
        });

        //#endregion

        //#region dataTable File
        $('#dataTableBakFile').DataTable({
            "aLengthMenu": [
                [10, 30, 50, -1],
                [10, 30, 50, "All"]
            ],
            "iDisplayLength": 10,
            "language": {
                search: ""
            },
            drawCallback: function (settings) {
                feather.replace();
            },
        });

        $('#dataTableBakFile').each(function () {
            var datatable = $(this);
            // SEARCH - Add the placeholder for Search and Turn this into in-line form control
            var search_input = datatable.closest('.dataTables_wrapper').find('div[id$=_filter] input');
            search_input.attr('placeholder', 'Search');
            search_input.removeClass('form-control-sm');
            // LENGTH - Inline-Form control
            var length_sel = datatable.closest('.dataTables_wrapper').find('div[id$=_length] select');
            length_sel.removeClass('form-control-sm');
        });

        $('#Tbl_Bpd_File').DataTable({
            "aLengthMenu": [
                [10, 30, 50, -1],
                [10, 30, 50, "All"]
            ],
            "iDisplayLength": 10,
            "language": {
                search: ""
            },
            drawCallback: function (settings) {
                feather.replace();
            },
        });

        $('#Tbl_Bpd_File').each(function () {
            var datatable = $(this);
            // SEARCH - Add the placeholder for Search and Turn this into in-line form control
            var search_input = datatable.closest('.dataTables_wrapper').find('div[id$=_filter] input');
            search_input.attr('placeholder', 'Search');
            search_input.removeClass('form-control-sm');
            // LENGTH - Inline-Form control
            var length_sel = datatable.closest('.dataTables_wrapper').find('div[id$=_length] select');
            length_sel.removeClass('form-control-sm');
        });

        $('#Tbl-FinancePettyCash-File').DataTable({
            "aLengthMenu": [
                [10, 30, 50, -1],
                [10, 30, 50, "All"]
            ],
            "iDisplayLength": 10,
            "language": {
                search: ""
            },
            drawCallback: function (settings) {
                feather.replace();
            },
        });

        $('#Tbl-FinancePettyCash-File').each(function () {
            var datatable = $(this);
            // SEARCH - Add the placeholder for Search and Turn this into in-line form control
            var search_input = datatable.closest('.dataTables_wrapper').find('div[id$=_filter] input');
            search_input.attr('placeholder', 'Search');
            search_input.removeClass('form-control-sm');
            // LENGTH - Inline-Form control
            var length_sel = datatable.closest('.dataTables_wrapper').find('div[id$=_length] select');
            length_sel.removeClass('form-control-sm');
        });

        $('#Tbl-FinancePickupService-File').DataTable({
            "aLengthMenu": [
                [10, 30, 50, -1],
                [10, 30, 50, "All"]
            ],
            "iDisplayLength": 10,
            "language": {
                search: ""
            },
            drawCallback: function (settings) {
                feather.replace();
            },
        });

        $('#Tbl-FinancePickupService-File').each(function () {
            var datatable = $(this);
            // SEARCH - Add the placeholder for Search and Turn this into in-line form control
            var search_input = datatable.closest('.dataTables_wrapper').find('div[id$=_filter] input');
            search_input.attr('placeholder', 'Search');
            search_input.removeClass('form-control-sm');
            // LENGTH - Inline-Form control
            var length_sel = datatable.closest('.dataTables_wrapper').find('div[id$=_length] select');
            length_sel.removeClass('form-control-sm');
        });

        $('#Tbl_Hc_File').DataTable({
            "aLengthMenu": [
                [10, 30, 50, -1],
                [10, 30, 50, "All"]
            ],
            "iDisplayLength": 10,
            "language": {
                search: ""
            },
            drawCallback: function (settings) {
                feather.replace();
            },
        });

        $('#Tbl_Hc_File').each(function () {
            var datatable = $(this);
            // SEARCH - Add the placeholder for Search and Turn this into in-line form control
            var search_input = datatable.closest('.dataTables_wrapper').find('div[id$=_filter] input');
            search_input.attr('placeholder', 'Search');
            search_input.removeClass('form-control-sm');
            // LENGTH - Inline-Form control
            var length_sel = datatable.closest('.dataTables_wrapper').find('div[id$=_length] select');
            length_sel.removeClass('form-control-sm');
        });

        $('#Tbl-Legal-File').DataTable({
            "aLengthMenu": [
                [10, 30, 50, -1],
                [10, 30, 50, "All"]
            ],
            "iDisplayLength": 10,
            "language": {
                search: ""
            },
            drawCallback: function (settings) {
                feather.replace();
            },
        });

        $('#Tbl-Legal-File').each(function () {
            var datatable = $(this);
            // SEARCH - Add the placeholder for Search and Turn this into in-line form control
            var search_input = datatable.closest('.dataTables_wrapper').find('div[id$=_filter] input');
            search_input.attr('placeholder', 'Search');
            search_input.removeClass('form-control-sm');
            // LENGTH - Inline-Form control
            var length_sel = datatable.closest('.dataTables_wrapper').find('div[id$=_length] select');
            length_sel.removeClass('form-control-sm');
        });

        $('#Tbl-Teknik-File').DataTable({
            "aLengthMenu": [
                [10, 30, 50, -1],
                [10, 30, 50, "All"]
            ],
            "iDisplayLength": 10,
            "language": {
                search: ""
            },
            drawCallback: function (settings) {
                feather.replace();
            },
        });

        $('#Tbl-Teknik-File').each(function () {
            var datatable = $(this);
            // SEARCH - Add the placeholder for Search and Turn this into in-line form control
            var search_input = datatable.closest('.dataTables_wrapper').find('div[id$=_filter] input');
            search_input.attr('placeholder', 'Search');
            search_input.removeClass('form-control-sm');
            // LENGTH - Inline-Form control
            var length_sel = datatable.closest('.dataTables_wrapper').find('div[id$=_length] select');
            length_sel.removeClass('form-control-sm');
        });

        $('#dataTableIctFile').DataTable({
            "aLengthMenu": [
                [10, 30, 50, -1],
                [10, 30, 50, "All"]
            ],
            "iDisplayLength": 10,
            "language": {
                search: ""
            },
            drawCallback: function (settings) {
                feather.replace();
            },
        });

        $('#dataTableIctFile').each(function () {
            var datatable = $(this);
            // SEARCH - Add the placeholder for Search and Turn this into in-line form control
            var search_input = datatable.closest('.dataTables_wrapper').find('div[id$=_filter] input');
            search_input.attr('placeholder', 'Search');
            search_input.removeClass('form-control-sm');
            // LENGTH - Inline-Form control
            var length_sel = datatable.closest('.dataTables_wrapper').find('div[id$=_length] select');
            length_sel.removeClass('form-control-sm');
        });

        //#endregion


        $('#dtCostCenterFico').DataTable({
            "aLengthMenu": [
                [10, 30, 50, -1],
                [10, 30, 50, "All"]
            ],
            "iDisplayLength": 5,
            "language": {
                search: ""
            },
            drawCallback: function (settings) {
                feather.replace();
            },
        });

        $('#dtCostCenterFico').each(function () {
            var datatable = $(this);
            // SEARCH - Add the placeholder for Search and Turn this into in-line form control
            var search_input = datatable.closest('.dataTables_wrapper').find('div[id$=_filter] input');
            search_input.attr('placeholder', 'Search');
            search_input.removeClass('form-control-sm');
            // LENGTH - Inline-Form control
            var length_sel = datatable.closest('.dataTables_wrapper').find('div[id$=_length] select');
            length_sel.removeClass('form-control-sm');
        });

        $('#dtCostCenterFico2').DataTable({
            "aLengthMenu": [
                [10, 30, 50, -1],
                [10, 30, 50, "All"]
            ],
            "iDisplayLength": 5,
            "language": {
                search: ""
            },
            drawCallback: function (settings) {
                feather.replace();
            },
        });

        $('#dtCostCenterFico2').each(function () {
            var datatable = $(this);
            // SEARCH - Add the placeholder for Search and Turn this into in-line form control
            var search_input = datatable.closest('.dataTables_wrapper').find('div[id$=_filter] input');
            search_input.attr('placeholder', 'Search');
            search_input.removeClass('form-control-sm');
            // LENGTH - Inline-Form control
            var length_sel = datatable.closest('.dataTables_wrapper').find('div[id$=_length] select');
            length_sel.removeClass('form-control-sm');
        });

        //$('#dtCostCenterStationRequest').DataTable({
        //    "aLengthMenu": [
        //        [10, 30, 50, -1],
        //        [10, 30, 50, "All"]
        //    ],
        //    "iDisplayLength": 5,
        //    "language": {
        //        search: ""
        //    },
        //    drawCallback: function (settings) {
        //        feather.replace();
        //    },
        //});

        //$('#dtCostCenterStationRequest').each(function () {
        //    var datatable = $(this);
        //    // SEARCH - Add the placeholder for Search and Turn this into in-line form control
        //    var search_input = datatable.closest('.dataTables_wrapper').find('div[id$=_filter] input');
        //    search_input.attr('placeholder', 'Search');
        //    search_input.removeClass('form-control-sm');
        //    // LENGTH - Inline-Form control
        //    var length_sel = datatable.closest('.dataTables_wrapper').find('div[id$=_length] select');
        //    length_sel.removeClass('form-control-sm');
        //});



        //#region Change Request Grid Datatables

        window.TableCR = $('#dataTableCR').DataTable({  
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
                "url": UrlOrigin + '/ChangeRequest/GetFilteredTickets',
                "type": "POST",
                "data": function (d) {
                    d.status = $('#statusFilter').val();
                    d.filterUsulanAplikasi = $('#FilterUsulanAplikasi').val();
                },
                "beforeSend": function () {
                    //$('#loadingBackdrop').show();
                    showLoader('Mohon menunggu, sedang menampilkan <strong>Data</strong>.');
                },
                "complete": function () {
                    //$('#loadingBackdrop').hide();
                    Swal.close();
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
                    $('#dataTableCR').DataTable().clear().draw();
                    $('#dataTableCR').after('<div class="error-message">An error occurred while loading the data. Please try again later.</div>');

                    //window.location = UrlOrigin + '/Home/Logout';
                }
            },
            "initComplete": function () {
                var $searchInput = $('div.dataTables_filter input');
                var typingTimer;                // Timer identifier
                var doneTypingInterval = 800;    // Time in ms (300ms delay)

                $searchInput.unbind();

                $searchInput.bind('keyup', function (e) {
                    clearTimeout(typingTimer); // Clear the previous timer

                    // Start a new timer when user stops typing
                    typingTimer = setTimeout(function () {
                        // Perform the search only if the user has typed at least 3 characters or cleared the input
                        if ($searchInput.val().length >= 3 || $searchInput.val().length == 0) {
                            TableCR.search($searchInput.val()).draw();
                        }
                    }, doneTypingInterval);
                });
            },
            "columns": [
                {
                    "data": null,
                    "render": function (data, type, row, meta) {
                        return meta.settings._iDisplayStart + meta.row + 1;
                    },
                    "orderable": false  
                },
                { "data": "usulan_aplikasi" },
                { "data": "created_at" },
                { "data": "start_date" },
                { "data": "end_date" },
                { "data": "pic" },
                {
                    "data": "status",    
                    "render": function (data, type, row, meta) {
                        var status = "danger";

                        if (data == "created") {
                            status = "primary";
                        } else if (data == "approved") {
                            status = "success";
                        } else if (data == "started") {
                            status = "warning";
                        } else if (data == "finished") {
                            status = "success";
                        } else if (data == "rejected") {
                            status = "danger";
                        } else if (data == "done") {
                            status = "success";
                        }

                        return `<span class="badge badge-${status}">&nbsp;${data.charAt(0).toUpperCase()}${data.slice(1)}&nbsp;</span>`;
                    }
                },
                { "data": "progress" },
                { "data": "action" }
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


        $('#FilterUsulanAplikasi').on('change', function () { TableCR.draw(); });


        $('#dataTableCR').each(function () {
            var datatable = $(this);
            // SEARCH - Add the placeholder for Search and Turn this into in-line form control
            var search_input = datatable.closest('.dataTables_wrapper').find('div[id$=_filter] input');
            search_input.attr('placeholder', 'Search');
            search_input.removeClass('form-control-sm');
            // LENGTH - Inline-Form control
            var length_sel = datatable.closest('.dataTables_wrapper').find('div[id$=_length] select');
            length_sel.removeClass('form-control-sm');
        });

        //#endregion


    });




});