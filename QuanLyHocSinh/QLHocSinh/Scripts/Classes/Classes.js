$(document).ready(function () {

    createControl();
    load_table();

});

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
            },
            error: function (xhr, status, error) {
                alert('Error789:');
            }
        });
    });
    initSP();
}
function load_table() {
    var gr = $('#grade').val();

    $.ajax({
        type: "GET",
        dataType: "json",
        url: "/Students/GetStudentsByClass",
        data: { grade: gr },
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
                        { 'data': 'Gender' },
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
                                 return '<button type="button" onclick="edit(' + row.StudentID + ')" class="btn btn-danger waves-effect waves-light">Sửa</button>';
                             }
                         }
                ]
            });
        }
    });
};

function edit(a) {
    alert(a);

}

function ViewListStudent() {
    var dataTable = $('#classtable').DataTable();
    dataTable.destroy();
    load_table();
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

}

