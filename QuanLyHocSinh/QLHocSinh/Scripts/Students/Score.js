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
        data: { idLevel: '1' },
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
function ViewStudentScore() {


}
