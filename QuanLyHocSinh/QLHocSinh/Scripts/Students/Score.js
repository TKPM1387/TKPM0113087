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


    $('#classtable').DataTable({
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
                { 'data': 'StudentID', "visible": false },
                {
                    'data': 'FullName',
                    "render": function (data, type, row) {
                        return '<a href= "/Students/ViewDetail?ID=' + row.StudentID + '">' + row.FullName + '</a>';
                    }
                },
                { 'data': 'Test15Minutes' },
                { 'data': 'Test45Minutes' },
                { 'data': 'TestSemester' },
                { 'data': 'Average' },
                 {
                     "render": function (data, type, row) {
                         return '<button type="button" onclick="edit(' + "'" + row.StudentID + "'" + ',' + "'" + row.FullName + "'" + ',' + row.Test15Minutes + ',' + row.Test45Minutes + ',' + row.TestSemester + ')" class="btn btn-warning waves-effect waves-light"  data-toggle="modal" data-target="#exampleModal"><i class="fa fa-pencil" aria-hidden="true"></i></button> ' +
                             '<button onclick="DeletePoint(' + "'" + row.StudentID + "'" + ')" type="button" class="btn btn-danger waves-effect waves-light"><i  class="fa fa-trash-o" aria-hidden="true"></i></button>';
                     }
                 }
        ]
    });

}
function ViewStudentScore() {
    var gr = $('#grade').val();
    var sj = $('#subject').val();
    var sr = $('#semester').val();

    $.ajax({
        type: "GET",
        dataType: "json",
        url: "/Classes/GetStudentPoint",
        data: { grade: gr, subject: sj },
        success: function (data) {
            var dataTable = $('#classtable').DataTable();
            dataTable.clear().draw();
            dataTable.rows.add(data).draw();
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

    //public int ID { get; set; }
    //public string StudenID { get; set; }
    //public Nullable<int> SubjectID { get; set; }
    //public Nullable<int> Semester { get; set; }
    //public Nullable<double> Test15Minutes { get; set; }
    //public Nullable<double> Test45Minutes { get; set; }
    //public Nullable<double> TestSemester { get; set; }
    //public Nullable<int> Average { get; set; }

    var p = {
        StudenID: $('#fid').val(),
        SubjectID: $('#subject').val(),
        FullName: $('#fullName').val(),
        Test15Minutes: $('#fp15').val(),
        Test45Minutes: $('#fp45').val(),
        TestSemester: $('#fpl').val(),
        Semester: $('#semester').val(),
    };

    $.ajax({
        type: "POST",
        url: '/Students/UpdateStudentPoint',
        contentType: "application/json; charset=utf-8",
        //data: stu,
        data: JSON.stringify(p),
        dataType: "json",
        success: function (result) {
            if (result[0].value == 1)
                swal("Cập nhật thành công", "success", "success");
            ViewStudentScore();
        },
        error: function (xhr, status, error) { alert('Error:'); }
    });
}

function DeletePoint(id) {
    swal({
        title: "Xác nhận xóa điểm học sinh?",
        //text: "You will not be able to recover this imaginary file!",
        type: "warning",
        showCancelButton: true,
        confirmButtonColor: "#DD6B55",
        confirmButtonText: "Đồng ý",
        cancelButtonText: "Hủy",
        closeOnConfirm: false
    }, function () {
        $.ajax({
            type: "GET",
            url: '/Students/DeletePoint',
            contentType: "application/json; charset=utf-8",
            //data: stu,
            data: { studentid: id, subjectid: $("#subject").val() },
            dataType: "json",
            success: function (result) {
                if (result[0].value == 1) {
                    swal("Thành công!", "", "success");
                    ViewStudentScore();
                }
            }
        });
    });
}
