
function enviarInfoAdicionais() {
    $('#IrParaFicha').val($("#numFicha").val());
    $('#IrParaVerso').val($('#ckVerso[type=checkbox]').prop('checked'));
    $('#QuantidadeCentimetrosDaBorda').val($("#qtdeCentimetros").val());
    $('#NumSequencia').val($("#numeSequencia").val());
    $('#btnConfirmar').removeAttr('disabled');
}
function limparInfoAdicionais() {
    $('#IrParaFicha').val("");
    $('#IrParaVerso').val("");
    $('#QuantidadeCentimetrosDaBorda').val("");
    $('#NumSequencia').val("");
    $('#btnConfirmar').attr('disabled', 'disabled');
    $('#spanInfoAdicionais').text('Por favor, salve as informações adicionais preenchidas  ');
    $('#spanInfoAdicionais').append('<a class="linkModal" data-toggle="modal" data-target="#PartialDadosAdicionais">Clicando Aqui</a>');

}