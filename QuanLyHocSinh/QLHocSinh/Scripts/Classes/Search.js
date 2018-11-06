$(document).ready(function () {
    $('#detailtable').DataTable();


});


function load_table() {

    $.ajax({
        type: "GET",
        dataType: "json",
        url: "/Classes/GetStudentDetail",
        data: { idname: $('#txtIDName').val() },
        success: function (data) {
            var datatableVariable = $('#detailtable').DataTable({
                "paging": true,
                "ordering": false,
                "info": false,
                "searching": false,
                data: data,
                destroy: true,
                retrieve: true,
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
                    "lengthMenu": "Hiển thị  _MENU_  dòng",
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
                        { 'data': 'StudentID' },
                        { 'data': 'FullName' },
                        { 'data': 'Class' },
                        { 'data': 'TBHK1' },
                        { 'data': 'TBHK2' },
                ]
            });
        }
    });
};
function ViewDetailStudent() {
    var dataTable = $('#detailtable').DataTable();
    dataTable.destroy();
    load_table();
}