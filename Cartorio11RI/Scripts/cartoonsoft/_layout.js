/*
----------1---------2---------3---------4---------5---------6---------7--------8
01234567890123456789012345678901234567890123456789012345678901234567890123456789
--------------------------------------------------------------------------------
Cartoonsoft _layout
by Ronaldo Moreira - 2019
------------------------------------------------------------------------------*/

/** ----------------------------------------------------------------------------
 * Mostrar o progresbar
 * param {any} menssagem
----------------------------------------------------------------------------* */
function ShowProgreessBar(menssagem) {
    $('#frmProgreessBarModal').modal("show");
    $('#messageProgreessBarModal').html(menssagem);
}

/** ----------------------------------------------------------------------------
 * Esconder progressbar
----------------------------------------------------------------------------* */
function HideProgressBar() {
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
function AvisoSmallBox(titulo, msg, cor, timeout, icone) {
    //
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
function ShowMessageUser() {

    var tit = "Ação não permitida";
    var msg = "Usuário não tem permissão para esta funcionalidade <br/> Para maiores informações, contate o administrador.";
    var cor = "#B66246";
    var timeout = 8000;
    var icone = "fa fa-lock swing animated";

    Aviso(tit, msg, cor, timeout, icone)
}

/** ----------------------------------------------------------------------------
 * 
 * @param {any} obj
----------------------------------------------------------------------------* */
function VerifyActiveClass(obj) {
    var  objTmp = obj;

    if (!$(objTmp).closest("li").hasClass("active")) {
        $(objTmp).closest("li").addClass("active")
    }
}

/** ----------------------------------------------------------------------------
 * ShowDlgBoxCartorio
 * @param {any} options
----------------------------------------------------------------------------* */
function ShowDlgBoxCartorio(options) {
    var deferredObject = $.Deferred();

    var defaults = {
        type: "alert", //alert, prompt,confirm 
        modalSize: 'modal-sm', //modal-sm, modal-lg
        okButtonText: 'Ok',
        cancelButtonText: 'Cancela',
        yesButtonText: 'Sim',
        noButtonText: 'Não',
        headerText: 'Aviso',
        messageText: 'Mensagem',
        alertType: 'default', //default, primary, success, info, warning, danger
        inputFieldType: 'text', //could ask for number,email,etc
    }

    $.extend(defaults, options);

    var _show = function () {
        var headClass = "navbar-default";
        switch (defaults.alertType) {
            case "primary":
                headClass = "alert-primary";
                break;
            case "success":
                headClass = "alert-success";
                break;
            case "info":
                headClass = "alert-info";
                break;
            case "warning":
                headClass = "alert-warning";
                break;
            case "danger":
                headClass = "alert-danger";
                break;
        }
        $('BODY').append(
            '<div id="DlgBoxCartorio" class="modal fade">' +
            '<div class="modal-dialog" class="' + defaults.modalSize + '">' +
            '<div class="modal-content">' +
            '<div id="DlgBoxCartorio-header" class="modal-header ' + headClass + '">' +
            '<button id="close-button" type="button" class="close" data-dismiss="modal"><span aria-hidden="true">×</span><span class="sr-only">Close</span></button>' +
            '<h4 id="DlgBoxCartorio-title" class="modal-title">Modal title</h4>' +
            '</div>' +
            '<div id="DlgBoxCartorio-body" class="modal-body">' +
            '<div id="DlgBoxCartorio-message" ></div>' +
            '</div>' +
            '<div id="DlgBoxCartorio-footer" class="modal-footer">' +
            '</div>' +
            '</div>' +
            '</div>' +
            '</div>'
        );

        $('.modal-header').css({
            'padding': '15px 15px',
            '-webkit-border-top-left-radius': '5px',
            '-webkit-border-top-right-radius': '5px',
            '-moz-border-radius-topleft': '5px',
            '-moz-border-radius-topright': '5px',
            'border-top-left-radius': '5px',
            'border-top-right-radius': '5px'
        });

        $('#DlgBoxCartorio-title').text(defaults.headerText);
        $('#DlgBoxCartorio-message').html(defaults.messageText);

        var keyb = "false", backd = "static";
        var calbackParam = "";
        switch (defaults.type) {
            case 'alert':
                keyb = "true";
                backd = "true";
                $('#DlgBoxCartorio-footer').html('<button class="btn btn-' + defaults.alertType + '">' + defaults.okButtonText + '</button>').on('click', ".btn", function () {
                    calbackParam = true;
                    $('#DlgBoxCartorio').modal('hide');
                });
                break;
            case 'confirm':
                var btnhtml = '<button id="ezok-btn" class="btn btn-primary">' + defaults.yesButtonText + '</button>';
                if (defaults.noButtonText && defaults.noButtonText.length > 0) {
                    btnhtml += '<button id="ezclose-btn" class="btn btn-default">' + defaults.noButtonText + '</button>';
                }
                $('#DlgBoxCartorio-footer').html(btnhtml).on('click', 'button', function (e) {
                    if (e.target.id === 'ezok-btn') {
                        calbackParam = true;
                        $('#DlgBoxCartorio').modal('hide');
                    } else if (e.target.id === 'ezclose-btn') {
                        calbackParam = false;
                        $('#DlgBoxCartorio').modal('hide');
                    }
                });
                break;
            case 'prompt':
                $('#DlgBoxCartorio-message').html(defaults.messageText + '<br /><br /><div class="form-group"><input type="' + defaults.inputFieldType + '" class="form-control" id="prompt" /></div>');
                $('#DlgBoxCartorio-footer').html('<button class="btn btn-primary">' + defaults.okButtonText + '</button>').on('click', ".btn", function () {
                    calbackParam = $('#prompt').val();
                    $('#DlgBoxCartorio').modal('hide');
                });
                break;
        }

        $('#DlgBoxCartorio').modal({
            show: true,
            backdrop: backd,
            keyboard: keyb
        }).on('hidden.bs.modal', function (e) {
            $('#DlgBoxCartorio').remove();
            deferredObject.resolve(calbackParam);
        }).on('shown.bs.modal', function (e) {
            if ($('#prompt').length > 0) {
                $('#prompt').focus();
            }
        }).modal('show');
    }

    _show();
    return deferredObject.promise();
}






