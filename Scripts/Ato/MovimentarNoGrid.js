/***************************************************************************************
*****************************  FUNCOES DE IR E VOLTAR A TELA  *************************
**************************************************************************************/
function voltarPasso() {
    //DADOS INICIAIS
    var lista = $('#list');
    var quantidade = lista.find('.complete').length;
    var objeto = lista.find('.active');

    if (quantidade == 1) {
        $('#btnProximo').removeAttr('disabled');
        $('#btnProximo').addClass('active');
    }


    //TIRA O ATIVO DA TELA ATUAL E PASSA PARA A ANTERIOR
    var item = objeto[0];
    item.classList.remove('active');
    objeto = lista.find('.complete');
    item = objeto[quantidade - 1];

    item.classList.remove('complete');
    item.classList.add('active');


    $(`#step${quantidade + 1}`).removeClass('active');
    $(`#step${quantidade}`).addClass('active');
    if (quantidade == 1) {
        $('#btnVoltar').attr('disabled', 'disabled');
    }
}

//CLICA NO LINK INVISIVEL PARA IR AO PRÓXIMO
function IrParaProximo() {
    $('#goToNext').click();
}