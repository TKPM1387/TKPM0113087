$(document).ready(function () {
    init();
    loadtable();
});
function init() {
    $('#liststudent').DataTable({
        "paging": true,
        "ordering": false,
        "info": false,
        "searching": false,
        //data: data,
        //"scrollX": true,
        //"sScrollX": '110%',
        "sScrollX": '100%',
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
                {
                    'data': 'StudentID',
                    "render": function (data, type, row) {
                        return '<a href= "/Students/ViewDetail?ID=' + row.StudentID + '">' + row.StudentID + '</a>';
                    }
                },
                { 'data': 'FullName' },
                { 'data': 'Class', "visible": false },

                { 'data': 'ClassName' },
                {
                    'data': 'BirthDay', 'render': function (date) {
                        var date = new Date(parseInt(date.toString().substr(6)));
                        var month = date.getMonth() + 1;
                        return date.getDate() + "/" + month + "/" + date.getFullYear();
                    }
                },
                {
                    'data': 'Gender', 'render': function (g) {
                        if (g == 1)
                            return 'Nam';
                        else
                            return 'Nữ';

                    }
                },
                { 'data': 'Email' },
                { 'data': 'PhoneNumber' },
                { 'data': 'Address' },
                 {
                     "render": function (data, type, row) {
                         return '<button type="button" data-toggle="modal" data-target="#exampleModal" onclick="edit(' + "'" + row.StudentID + "'" + ',' + "'" + row.FullName + "'" + ',' + row.Gender + ',' + row.BirthDay + ',' + "'" + row.Address + "','" + row.Email + "','" + row.PhoneNumber + "'," + row.Class + ')" class="btn btn-warning waves-effect waves-light"><i class="fa fa-pencil-square-o" ></i></button>' +
                             '<button onclick="DeleteStudent(' + "'" + row.StudentID + "'" + ')" style="margin-left: 5px;" type="button" class="btn btn-danger waves-effect waves-light"><i class="fa fa-times" aria-hidden="true"></i></button>';
                     }
                 }
        ]
    });
}

function loadtable() {
    $.ajax({
        type: "GET",
        dataType: "json",
        url: "/Students/GetListStudent",
        success: function (data) {
            var dataTable = $('#liststudent').DataTable();
            dataTable.clear().draw();
            dataTable.rows.add(data).draw();
        }
    });
}


function edit(fid, fname, fgender, fbirthday, faddress, femail, fphonenumber, fclass) {

    $('#sid').val(fid);
    $('#fullName').val(fname);
    $('#address').val(faddress);

    var date = new Date(parseInt(fbirthday.toString().substr(6)));
    var month = date.getMonth() + 1;
    $('#birthDay').val(date.getDate() + "/" + month + "/" + date.getFullYear());
    

    $('#gender').val(fgender);
    $('#gender').selectpicker('refresh')

    $('#grade2').val(fclass);
    $('#grade2').selectpicker('refresh')

    $('#email').val(femail);
    $('#phonenumber').val(fphonenumber);
}

function updateinfo() {
    var a = $('#birthDay').val().split('/');
    var b = a[1] + '/' + a[0] + '/' + a[2];

    var s = {
        StudentID: $('#sid').val(),
        FullName: $('#fullName').val(),
        BirthDay: b,
        Gender: $('#gender').val(),
        Email: $('#gender').val(),
        PhoneNumber: $('#gender').val(),
        Address: $('#address').val(),
        Class: $('#grade2').val()
    };


    $.ajax({
        type: "POST",
        url: '/Students/UpdateInfoStudent',
        contentType: "application/json; charset=utf-8",
        //data: stu,
        data: JSON.stringify(s),
        dataType: "json",
        success: function (result) {
            if (result[0].value == 1) {
                swal("Cập nhật thành công", "success", "success");
                loadtable();
            }
        }
    });

}

function DeleteStudent(id) {
    swal({
        title: "Xác nhận xóa học sinh này?",
        //text: "You will not be able to recover this imaginary file!",
        type: "warning",
        showCancelButton: true,
        confirmButtonColor: "#DD6B55",
        confirmButtonText: "Đồng ý",
        cancelButtonText: "Hủy",
        closeOnConfirm: false
    }, function () {
        //var form = new FormData();
        //form.append("id", id);
        $.ajax({
            type: "GET",
            url: '/Students/DeleteStudent',
            contentType: "application/json; charset=utf-8",
            //data: stu,
            data: { id: id },
            dataType: "json",
            success: function (result) {
                if (result[0].value == 1) {
                    swal("Thành công!", "", "success");
                    loadtable();
                }
            }
        });
    });
}