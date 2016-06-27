$(document).ready(function () {
    $("#ValorJogo").maskMoney({ allowNegative: true, thousands: '.', decimal: ',', affixesStay: false, precision: 2 });
});