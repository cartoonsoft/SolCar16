/*
----------1---------2---------3---------4---------5---------6---------7---------8
012345678901234567890123456789012345678901234567890123456789012345678901234567890
---------------------------------------------------------------------------------
Cartoonsoft libs
by Ronaldo Moreira - 2019
-------------------------------------------------------------------------------*/


/**
 * Mostrar o progresbar
 * @param {any} menssagem
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

/*-----------------------------------------------------------------------------*/
