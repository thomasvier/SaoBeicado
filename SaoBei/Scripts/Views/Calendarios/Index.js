$(document).ready(function () {
    $(".details").click(function () {
        var id = $(this).attr("data-id");
        $("#detalhes").load("/Calendarios/Detalhes?id=" + id, function () {
            $("#detalhes").modal();
        })
    });
});