$(document).ready(function () {
    createControl();
})
function createControl() {
    $('#semester').selectpicker();
    
    $('#student').DataTable({
        "paging": true,
        "ordering": false,
        "info": false,
        "searching": false,
        //data: data,
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
            "emptyTable": "Không có dữ liệu",
            "aria": {
                "sortAscending": ": activate to sort column ascending",
                "sortDescending": ": activate to sort column descending"
            }
        },
        columns: [
                 {
                     "data": "id",
                     render: function (data, type, row, meta) {
                         return meta.row + meta.settings._iDisplayStart + 1;
                     }
                 },
                { 'data': 'StudentID', "visible": false},
                { 'data': 'FullName', "width": "20%" },
                { 'data': 'SubjectName' },
                { 'data': 'Test15Minutes' },
                { 'data': 'Test45Minutes' },
                { 'data': 'TestSemester' },
                { 'data': 'Average' },                
        ]
    });

}
function ViewDetail() {

    
    var id = $('#txtIDName').val();

    $.ajax({
        type: "GET",
        dataType: "json",
        url: "/Classes/GetStudentPointDetail",
        data: { ID: id, semester: $('#semester').val() },
        success: function (data) {
            var dataTable = $('#student').DataTable();
            dataTable.clear().draw();
            dataTable.rows.add(data).draw();
        }
    });
}