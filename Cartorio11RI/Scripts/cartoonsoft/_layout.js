/*
----------1---------2---------3---------4---------5---------6---------7--------8
01234567890123456789012345678901234567890123456789012345678901234567890123456789
--------------------------------------------------------------------------------
Cartoonsoft _layout
by Ronaldo Moreira - 2019
------------------------------------------------------------------------------*/

var cor_smallBox_aviso = "#D99651";
var cor_smallBox_erro = "#D16E60";
var cor_smallBox_ok = "#1F7706";
var cor_smallBox_confima = "#1A6588";

/** ----------------------------------------------------------------------------
 * Mostrar o progresbar
 * param {any} menssagem
----------------------------------------------------------------------------* */
function ShowProgreessBar(menssagem)
{
    $('#frmProgreessBarModal').modal("show");
    $('#messageProgreessBarModal').html(menssagem);
}

/** ----------------------------------------------------------------------------
 * Esconder progressbar
----------------------------------------------------------------------------* */
function HideProgressBar()
{
    $('#frmProgreessBarModal').modal("hide");
}

/** ----------------------------------------------------------------------------
 * 
 * @param {any} titulo
 * @param {any} msg
 * @param {any} cor
 * @param {any} timeout
 * @param {any} icone
----------------------------------------------------------------------------- */
function AvisoSmallBox(titulo, msg, cor, timeout, icone)
{
    $.smallBox({
        title:   titulo,
        content: msg,
        color:   cor,
        timeout: timeout,
        icon:    icone
    });
}

/** ----------------------------------------------------------------------------
 * Mostrar Menssagem: Usuariu não tem permissão de acesso
 ---------------------------------------------------------------------------* */
function ShowMessageUser()
{
    var tit = "Ação não permitida";
    var msg = "Usuário não tem permissão para esta funcionalidade <br/> Para maiores informações, contate o administrador.";
    var cor = "#B66246";
    var timeout = 8000;
    var icone = "fa fa-lock swing animated";

    AvisoSmallBox(tit, msg, cor, timeout, icone)
}

/** ----------------------------------------------------------------------------
 * 
 * @param {any} obj
----------------------------------------------------------------------------* */
function VerifyActiveClass(obj)
{
    var  objTmp = obj;

    if (!$(objTmp).closest("li").hasClass("active")) {
        $(objTmp).closest("li").addClass("active")
    }
}






