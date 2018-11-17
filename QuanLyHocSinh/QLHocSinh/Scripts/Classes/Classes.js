$(document).ready(function () {

    var t = $('#classtable').DataTable({
        "paging": true,
        //"ordering": false,
        "info": false,
        "searching": false,
        //data: data,
        destroy: true,
        retrieve: true,
        buttons: [
            'copy', 'excel', 'pdf'
        ],
        "language": {
            //"url": "//cdn.datatables.net/plug-ins/1.10.19/i18n/Vietnamese.json",
            "decimal": "",
            "emptyTable": "Không có dữ liệu",
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
                //{ 'data': 'STT' },
                {"data": "id",
                    render: function (data, type, row, meta) {
                        return meta.row + meta.settings._iDisplayStart + 1;
                    }},
                { 'data': 'StudentID', "visible": false },
                {
                    'data': 'FullName',
                    "render": function (data, type, row) {
                        return '<a href= "/Students/ViewDetail?ID=' + row.StudentID + '">' + row.FullName + '</a>';
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
                {
                    'data': 'BirthDay', 'render': function (date) {
                        var date = new Date(parseInt(date.substr(6)));
                        var month = date.getMonth() + 1;
                        return date.getDate() + "/" + month + "/" + date.getFullYear();
                    }
                },
                { 'data': 'Email', "visible": false },
                { 'data': 'PhoneNumber', "visible": false },
                { 'data': 'Address' },
                 {
                     "render": function (data, type, row) {
                         return '<button type="button" data-toggle="modal" data-target="#exampleModal" onclick="edit(' + "'" + row.StudentID + "'" + ',' + "'" + row.FullName + "'" + ',' + row.Gender + ',' + row.BirthDay + ',' + "'" + row.Address + "'" + ')" class="btn btn-warning waves-effect waves-light"><i class="fa fa-pencil-square-o" ></i></button>'+
                             '<button onclick="DeleteStudent('+ "'" + row.StudentID + "'" +')" style="margin-left: 5px;" type="button" class="btn btn-danger waves-effect waves-light"><i class="fa fa-times" aria-hidden="true"></i></button>';
                     }
                 }
    ]
});

t.on('order.dt search.dt', function () {
    t.column(0, { search: 'applied', order: 'applied' }).nodes().each(function (cell, i) {
        cell.innerHTML = i + 1;
    });
}).draw();



createControl();

});

function DeleteStudent(id)
{
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
                    ViewListStudent();
                }
            }
        });
    });
}

function createControl() {
    $('#block').selectpicker();
    $('#grade').selectpicker();
    $.ajax({
        type: "GET",
        url: '/Classes/getClassLevel',
        contentType: "application/json; charset=utf-8",
        async: false,
        dataType: "json",
        success: function (data) {
            var jsonData = JSON.stringify(data);
            $.each(JSON.parse(jsonData), function (idx, obj) {
                $("#block").append('<option value="' + obj.value + '">' + obj.text + '</option>').selectpicker('refresh');
            });
        },
        error: function (xhr, status, error) {
            alert('err or seleccct 2222:');
        }
    });

    $('#block').on('change', function (e) {
        // console.log(this.value);
        $.ajax({
            type: "GET",
            url: '/Classes/getClassByLevel',
            async: false,
            contentType: "application/json; charset=utf-8",
            data: { idLevel: this.value },
            dataType: "json",
            success: function (data) {
                $('#grade').find('option').remove().end();
                var jsonData = JSON.stringify(data);
                $.each(JSON.parse(jsonData), function (idx, obj) {
                    $("#grade").append('<option value="' + obj.value + '">' + obj.text + '</option>').selectpicker('refresh');
                });
                loadtotal();
            },
            error: function (xhr, status, error) {
                alert('Error789:');
            }
        });
    });

    $('#grade').on('change', function (e) {
        // console.log(this.value);
        $.ajax({
            type: "GET",
            url: '/Classes/getTotalInClass',
            async: false,
            contentType: "application/json; charset=utf-8",
            data: { ID: this.value },
            dataType: "json",
            success: function (data) {
                $('#total').html(data.Total + '/' + data.MaxTotal)
            },
            error: function (xhr, status, error) {
                alert('Error789:');
            }
        });
    });
    initSP();

   
}

function edit(fid, fname, fgender, fbirthday, faddress) {
    $('#fullName').val(fname);
    $('#address').val(faddress);

    var date = new Date(parseInt(fbirthday.toString().substr(6)));
    var month = date.getMonth() + 1;
    $('#birthDay').val(date.getDate() + "/" + month + "/" + date.getFullYear());


    $('#gender').val(fgender);
    $('#gender').selectpicker('refresh')

    $('#grade2').val($('#grade').val());
    $('#grade2').selectpicker('refresh')

    $('#sid').val(fid);
    
}

function ViewListStudent() {
    var gr = $('#grade').val();

    $.ajax({
        type: "GET",
        dataType: "json",
        url: "/Students/GetStudentsByClass",
        data: { grade: gr },
        success: function (data) {
            var dataTable = $('#classtable').DataTable();
            dataTable.clear().draw();
            dataTable.rows.add(data).draw();
        }
    });
}

function initSP() {

    $.ajax({
        type: "GET",
        url: '/Classes/getClassByLevel',
        async: false,
        contentType: "application/json; charset=utf-8",
        data: { idLevel: $('#block').val() },
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
    loadtotal();
    
}
function loadtotal() {
    $.ajax({
        type: "GET",
        url: '/Classes/getTotalInClass',
        async: false,
        contentType: "application/json; charset=utf-8",
        data: { ID: $('#grade').val() },
        dataType: "json",
        success: function (data) {
            $('#total').html(data.Total + '/' + data.MaxTotal)
        },
        error: function (xhr, status, error) {
            alert('Error789:');
        }
    });
}

function updateinfo() {
    var a = $('#birthDay').val().split('/');
    var b = a[1] + '/' + a[0] + '/' + a[2];

    var s = studentdetail = {
        StudentID:  $('#sid').val(),
        FullName: $('#fullName').val(),
        BirthDay: b,
        Gender: $('#gender').val(),
        //Email: semail,
        //PhoneNumber: snote,
        Address: $('#address').val(),
        Class:$('#grade2').val()
    };


    $.ajax({
        type: "POST",
        url: '/Students/UpdateInfo',
        contentType: "application/json; charset=utf-8",
        //data: stu,
        data: JSON.stringify(s),
        dataType: "json",
        success: function (result) {
            if (result[0].value == 1) {
                swal("Cập nhật thành công", "success", "success");
                ViewListStudent();
            }
        }
    });
}
