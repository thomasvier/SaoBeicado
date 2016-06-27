$(document).ready(function () {

    var ativoFiltro = $('#hfsituacao').val();

    if (ativoFiltro != "")
        $('#situacao').val(ativoFiltro);

    $('#situacao').change(function () {
        var value = $('option:selected', $(this)).val();
        $('#hfsituacao').val(value);
    });

    $(".details").click(function () {
        var id = $(this).attr("data-id");
        $("#detalhes").load("/Jogos/Detalhes?id=" + id, function () {
            $("#detalhes").modal();
        })
    });

    //$('#teste').click(function () {
    //    bootbox.confirm("Teste", function (result) {
    //        console.log(result);
    //    });
    //});
})