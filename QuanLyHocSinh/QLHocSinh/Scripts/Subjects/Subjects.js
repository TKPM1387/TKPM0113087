$(document).ready(function () {
    init();
    load_table();
});

function init() {
    $('#subjecttable').DataTable({
        "paging": false,
        "ordering": false,
        "info": false,
        "searching": false,
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
            {
                "data": "id",
                render: function (data, type, row, meta) {
                    return meta.row + meta.settings._iDisplayStart + 1;
                }
            },
            { 'data': 'SubjectID' },
            { 'data': 'SubjectName' },
            { 'data': 'Period' },
            { 'data': 'Type' },
             {
                 "render": function (data, type, row) {
                     return '<button type="button" onclick="edit(' + "'" + row.SubjectID + "','" + row.SubjectName + "'," + row.Period + ',' + row.Type + ')" data-toggle="modal" data-target="#exampleModal" class="btn btn-warning waves-effect waves-light"><i class="fa fa-pencil" aria-hidden="true"></i></button>' +
                             '<button onclick="DeleteSubject(' + "'" + row.SubjectID + "'" + ')" style="margin-left: 5px;" type="button" class="btn btn-danger waves-effect waves-light"><i class="fa fa-times" aria-hidden="true"></i></button>';
                 }
             }
        ]
    });
}

function load_table() {
    $(document).ready(function () {
        $.ajax({
            type: "GET",
            dataType: "json",
            url: "/Subjects/getSubjects",
            success: function (data) {
                var dataTable = $('#subjecttable').DataTable();
                dataTable.clear().draw();
                dataTable.rows.add(data).draw();
            }
        });
    })
};

function DeleteSubject(id) {
    swal({
        title: "Xác nhận xóa môn học này?",
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
            url: '/Subjects/DeleteSubject',
            contentType: "application/json; charset=utf-8",
            //data: stu,
            data: { id: id },
            dataType: "json",
            success: function (result) {
                if (result[0].value == 1) {
                    swal("Thành công!", "", "success");
                    load_table();
                }
            }
        });
    });
}

function edit(id, name, period, type) {
    $('#subjectid').val(id)
    $('#subjectname').val(name)
    $('#subjectperiod').val(period)
    $('#subjecttype').val(type)
}
function updateinfosubject() {

    var subject = {
        SubjectID: $('#subjectid').val(),
        SubjectName: $('#subjectname').val(),
        Period: $('#subjectperiod').val(),
        Type: $('#subjecttype').val()
    }

    $.ajax({
        type: "POST",
        url: '/Subjects/UpdateSubject',
        contentType: "application/json; charset=utf-8",
        //data: stu,
        data: JSON.stringify(subject),
        dataType: "json",
        success: function (result) {
            if (result[0].value == 1) {
                swal("Cập nhật thành công", "success", "success");
                load_table();
            }
        },
        error: function (xhr, status, error) { alert('Có lỗi xảy ra!!'); }
    });

}
function clearmodal() {
     $('#subjectnameadd').val('')
     $('#subjectperiodadd').val('')
     $('#subjecttypeadd').val('')
}
function addsubject() {
       
    var subject = {
        SubjectName: $('#subjectnameadd').val(),
        Period: $('#subjectperiodadd').val(),
        Type: $('#subjecttypeadd').val(),
    }

    $.ajax({
        type: "POST",
        url: '/Subjects/AddSubject',
        contentType: "application/json; charset=utf-8",
        //data: stu,
        data: JSON.stringify(subject),
        dataType: "json",
        success: function (result) {
            if (result[0].value == 1) {
                swal("Thêm thành công", "success", "success");
                load_table();
                clearmodal()
            }
        },
        error: function (xhr, status, error) { alert('Có lỗi xảy ra!!'); }
    });
}




