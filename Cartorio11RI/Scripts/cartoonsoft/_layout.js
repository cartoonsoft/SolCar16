/*
----------1---------2---------3---------4---------5---------6---------7--------8
01234567890123456789012345678901234567890123456789012345678901234567890123456789
--------------------------------------------------------------------------------
Cartoonsoft _layout
by Ronaldo Moreira - 2019
------------------------------------------------------------------------------*/


/**
 * Mostrar o progresbar
 * param {any} menssagem
 */
function ShowProgreessBar(menssagem) {
    $('#frmProgreessBarModal').modal("show");
    $('#messageProgreessBarModal').html(menssagem);
}

/**
 * Esconder progressbar
 * */
function HideProgressBar() {
    $('#frmProgreessBarModal').modal("hide");
}


/**
 * Exibir Aviso
 * */
function Aviso(titulo, msg, cor, timeout, icone) {

    $.smallBox({
        title:   titulo,
        content: msg,
        color:   cor,
        timeout: timeout,
        icon:    icone
    });
}

/**
 * Mostrar Menssagem: Usuariu não tem permissão de acesso
 * */
function ShowMessageUser() {

    var tit = "Ação não permitida";
    var msg = "Usuário não tem permissão para esta funcionalidade <br/> Para maiores informações, contate o administrador.";
    var cor = "#B66246";
    var timeout = 8000;
    var icone = "fa fa-lock swing animated";

    Aviso(tit, msg, cor, timeout, icone)
}

/**
 * Mostra dialogo modal de aviso
 * @param {any} pAviso
 * @param {any} pTitulo
 * @param {any} pTexto
 */
function ShowModalDialog( pTitulo, pTexto) {
    $('#divDlg1 label[id*="lblDlgHeader"]').text(pTitulo);
    $('#divDlg1 > spanText').text(pTexto);
    $('#divDlg1').modal({
        transition: 'scale',
        centered: true,
        closable: false
    }).modal('show');
}

function VerifyActiveClass(obj) {
    var  objTmp = obj;

    if (!$(objTmp).closest("li").hasClass("active")) {
        $(objTmp).closest("li").addClass("active")
    }
}
/*-----------------------------------------------------------------------------*/


