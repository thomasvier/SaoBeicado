$(document).ready(function () {
    $("#CalendarioID").change(function () {
        if ($('#CalendarioID').val() != '') {
           $("#formIndexMensalidades").submit();
        }
    });
});
