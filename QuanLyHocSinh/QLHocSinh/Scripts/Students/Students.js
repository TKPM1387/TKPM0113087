$(document).ready(function () {
    load_table();
    hideError()
});

function load_table() {
    $(document).ready(function () {
        //$('#example').DataTable({
        //    data: dataSet,
        //    columns: [
        //        { title: "Name" },
        //        { title: "Position" },
        //        { title: "Office" },
        //        { title: "Extn." },
        //        { title: "Start date" },
        //        { title: "Salary" }
        //    ]
        //});

        $.ajax({
            type: "POST",
            dataType: "json",
            url: "/Students/GetStudents",
            //url: "/GetStudents",
            success: function (data) {
                var datatableVariable = $('#studentTable').DataTable({
                    "paging": true,
                    "ordering": false,
                    "info": false,
                    "searching": true,
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
                        { 'data': 'iD' },
                        { 'data': 'firstName' },
                        { 'data': 'lastName' },
                        {
                            'data': 'feesPaid', 'render': function (feesPaid) {
                                return '$ ' + feesPaid;
                            }
                        },
                        { 'data': 'gender' },
                        { 'data': 'emailId' },
                        { 'data': 'telephoneNumber' },
                        {
                            'data': 'dateOfBirth', 'render': function (date) {
                                var date = new Date(parseInt(date.substr(6)));
                                var month = date.getMonth() + 1;
                                return date.getDate() + "/" + month + "/" + date.getFullYear();
                            }
                        },
                         {
                             "render": function (data, type, full, meta) {
                                 return '<a href=add.html?id=">Edit</a>';
                             }
                         }
                    ]
                });
                $('#studentTable tfoot th').each(function () {
                    var placeHolderTitle = $('#studentTable thead th').eq($(this).index()).text();
                    $(this).html('<input type="text" class="form-control input input-sm" placeholder = "Tìm ' + placeHolderTitle + '" />');
                });
                datatableVariable.columns().every(function () {
                    var column = this;
                    $(this.footer()).find('input').on('keyup change', function () {
                        column.search(this.value).draw();
                    });
                });
                $('.showHide').on('click', function () {
                    var tableColumn = datatableVariable.column($(this).attr('data-columnindex'));
                    tableColumn.visible(!tableColumn.visible());
                });
            }
        });
    })
};

function isEmail(email) {
    var regex = /^([a-zA-Z0-9_.+-])+\@(([a-zA-Z0-9-])+\.)+([a-zA-Z0-9]{2,4})+$/;
    return regex.test(email);
}
function hideError() {
    $('#spfullname').hide()
    $('#spbirthday').hide()
    $('#spaddress').hide()
    $('#spemail').hide()
    $('#spblock').hide()
    $('#spgrade').hide()

    $("div.form-group").removeClass('has-error');
}

function checkAddStudent() {
    var sfullname = $("#fullName").val();
    var sbirthday = $("#birthDay").val();
    var saddress = $("#address").val();
    var sgender = $("#gender").val();
    var semail = $("#email").val();
    var snote = $("#note").val();

    var sblock = $("#block").val();
    var sgrade = $("#grade").val();
    var i = 0;
    if (sfullname == "") {
        $('#fmfullname').addClass('has-error');
        $('#spfullname').show()

        i++;
    }
    if (sbirthday == "") {
        $('#fmbirthday').addClass('has-error');
        $('#spbirthday').show()
        i++;
    }
    if (saddress == "") {
        $('#fmaddress').addClass('has-error');
        $('#spaddress').show()
        i++;
    }
    if (semail == "") {
        $('#fmemail').addClass('has-error');
        $('#spemail').show()
        i++;
    }
    if (sblock == "") {
        $('#fmblock').addClass('has-error');
        $('#spblock').show()
        i++;
    }
    if (sgrade == "") {
        $('#fmgrade').addClass('has-error');
        $('#spgrade').show()
        i++;
    }
    //if (i != 0) {
    //    alert(i);
    //    return;
    //}

    var stu = studentdetai = {
        iD: 123,
        firstName: sfullname,
        lastName: sfullname,
        feesPaid: 2,
        gender: "NN",
        emailId: snote,
        telephoneNumber: snote,
        dateOfBirth: sbirthday
    };


    $.ajax({
        type: "POST",
        url: '/Students/testadd',
        contentType: "application/json; charset=utf-8",
        //data: stu,
        data: JSON.stringify(stu),
        dataType: "json",
        success: function () { alert('Success'); },
        error: function (xhr, status, error) { alert('Error:'); }
    });
}