$(document).ready(function () {
    createControl();
});

function createControl() {
    $('#grade').selectpicker();
    $('#subject').selectpicker();
    $('#semester').selectpicker();
    $.ajax({
        type: "GET",
        url: '/Classes/getClassByLevel',
        contentType: "application/json; charset=utf-8",
        data: { idLevel: '0' },
        dataType: "json",
        success: function (data) {
            $('#grade').find('option').remove().end();
            var jsonData = JSON.stringify(data);
            $.each(JSON.parse(jsonData), function (idx, obj) {
                $("#grade").append('<option value="' + obj.value + '">' + obj.text + '</option>').selectpicker('refresh');
            });
        },
        error: function (xhr, status, error) {
            alert('Error789:');
        }
    });


    $.ajax({
        type: "GET",
        url: '/Subjects/getListSubject',
        contentType: "application/json; charset=utf-8",
        data: { idLevel: '0' },
        dataType: "json",
        success: function (data) {
            $('#subject').find('option').remove().end();
            var jsonData = JSON.stringify(data);
            $.each(JSON.parse(jsonData), function (idx, obj) {
                $("#subject").append('<option value="' + obj.SubjectID + '">' + obj.SubjectName + '</option>').selectpicker('refresh');
            });
        },
        error: function (xhr, status, error) {
            alert('Error789:');
        }
    });
}
function ViewStudentScore() {
    var dataTable = $('#classtable').DataTable();
    dataTable.destroy();

    var gr = $('#grade').val();
    var sj = $('#subject').val();
    var sr = $('#semester').val();

        $.ajax({
            type: "GET",
            dataType: "json",
            url: "/Classes/GetStudentPoint",
            data: { grade: gr, subject: sj },
            success: function (data) {
                var datatableVariable = $('#classtable').DataTable({
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
                            { 'data': 'StudentID', "visible": false },
                            { 'data': 'FullName' },
                            { 'data': 'Test15Minutes' },
                            { 'data': 'Test45Minutes' },
                            { 'data': 'TestSemester' },
                             {
                                 "render": function (data, type, row) {
                                     return '<button type="button" onclick="edit(' + row.StudentID + ',' + "'" + row.FullName + "'" + ',' + row.Test15Minutes + ',' + row.Test45Minutes + ',' + row.TestSemester + ')" class="btn btn-success waves-effect waves-light"  data-toggle="modal" data-target="#exampleModal">Cập nhật</button>';
                                 }
                             }
                    ]
                });
            }
        });
    

}
function edit(id, name, p15, p45, pl) {
    $('#fullName').val(name);
    $('#fid').val(id);
    $('#fp15').val(p15);
    $('#fp45').val(p45);
    $('#fpl').val(pl);
}

function updatepoint() {


    var stu = studentpoint = {
        StudentID: $('#fid').val(),
        FullName: $('#fullName').val(),
        Test15Minutes: $('#fp15').val(),
        Test45Minutes: $('#fp45').val(),
        TestSemester: $('#fpl').val(),
    };

    $.ajax({
        type: "POST",
        url: '/Students/UpdateStudentPoint',
        contentType: "application/json; charset=utf-8",
        //data: stu,
        data: JSON.stringify(stu),
        dataType: "json",
        success: function (result) {
            if (result[0].value == 1)
                swal("Cập nhật thành công", "nhé", "success");
            ViewStudentScore();
        },
        error: function (xhr, status, error) { alert('Error:'); }
    });
}

