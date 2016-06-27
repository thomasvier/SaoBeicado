$(document).ready(function () {

    var ativoFiltro = $('#ativoFiltro').val();

    if (ativoFiltro != "")
        $('#selAtivoFiltro').val(ativoFiltro);

    $('#selAtivoFiltro').change(function () {
        var value = $('option:selected', $(this)).val();
        $('#ativoFiltro').val(value);
    });

    $(".details").click(function () {
        var id = $(this).attr("data-id");
        $("#detalhes").load("/Integrantes/Detalhes?id=" + id, function () {
            $("#detalhes").modal();
        })
    });
})