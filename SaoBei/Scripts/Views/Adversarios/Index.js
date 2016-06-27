$(document).ready(function () {

    var ativoFiltro = $('#ativoFiltro').val();

    if (ativoFiltro != "")
        $('#selAtivoFiltro').val(ativoFiltro);

    $('#selAtivoFiltro').change(function () {
        var value = $('option:selected', $(this)).val();
        $('#ativoFiltro').val(value);
    });
})