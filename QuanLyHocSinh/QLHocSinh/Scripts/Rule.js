function GetMaxTotalInClass(classid) {
    $.ajax({
        type: "POST",
        url: '/Rule/AddNewStudent',
        contentType: "application/json; charset=utf-8",
        //data: stu,
        data: JSON.stringify(stu),
        dataType: "json",
        success: function (result) {
            if (result[0].value == 1) {
                
            }
            else {

            }
        },
        //error: function (xhr, status, error)
        //{
        //    alert('Có lỗi xảy ra!!');
        //}
    });
}