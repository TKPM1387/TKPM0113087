$(document).ready(function () {
    load_table();
});


function load_table() {
    $(document).ready(function () {
        $.ajax({
            type: "GET",
            dataType: "json",
            url: "/Subjects/getSubjects",
            success: function (data) {
                var datatableVariable = $('#subjecttable').DataTable({
                    "paging": false,
                    "ordering": false,
                    "info": false,
                    "searching": false,
                    data: data,
                    buttons: [
                        'copy', 'excel', 'pdf'
                    ],
                    "language": {
                        "decimal": "",
                        "emptyTable": "No data available in table",
                        "info": "Showing _START_ to _END_ of _TOTAL_ entries",
                        "infoEmpty": "Showing 0 to 0 of 0 entries",
                        "infoFiltered": "(filtered from _MAX_ total entries)",
                        "infoPostFix": "",
                        "thousands": ",",
                        "lengthMenu": "thể _MENU_ entries",
                        "loadingRecords": "Loading...",
                        "processing": "Processing...",
                        "search": "Tìm nè:",
                        "zeroRecords": "No matching records found",
                        "paginate": {
                            "first": "Đầu",
                            "last": "Cuối",
                            "next": "Sau",
                            "previous": "Trước"
                        },
                        "aria": {
                            "sortAscending": ": activate to sort column ascending",
                            "sortDescending": ": activate to sort column descending"
                        }
                    },
                    columns: [
                        { 'data': 'STT' },
                        { 'data': 'SubjectID' },
                        { 'data': 'SubjectName' },
                         {
                             "render": function (data, type, row) {
                                 return '<button type="button" onclick="edit(1)" class="btn btn-danger waves-effect waves-light">Sửa</button>';
                             }
                         }
                    ]
                });
            }
        });
    })
};

function edit(a) {
    aler(a);
}





