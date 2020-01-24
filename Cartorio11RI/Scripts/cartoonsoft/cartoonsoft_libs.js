/*
---------1---------2---------3---------4---------5---------6---------7---------8
01234567890123456789012345678901234567890123456789012345678901234567890123456789
--------------------------------------------------------------------------------
Cartoonsoft libs
by Ronaldo Moreira - 2019
------------------------------------------------------------------------------*/

/** ----------------------------------------------------------------------------
 * onNewIp - your listener function for new IPs
 * compatibility for firefox and chrome
 * 
 * param {any} onNewIP
 -----------------------------------------------------------------------------*/
function getUserIP(onNewIP)
{
    var myPeerConnection = window.RTCPeerConnection || window.mozRTCPeerConnection || window.webkitRTCPeerConnection;
    var pc = new myPeerConnection({
        iceServers: []
    }),
        noop = function () { },
        localIPs = {},
        ipRegex = /([0-9]{1,3}(\.[0-9]{1,3}){3}|[a-f0-9]{1,4}(:[a-f0-9]{1,4}){7})/g,
        key;

    function iterateIP(ip) {
        if (!localIPs[ip]) onNewIP(ip);
        localIPs[ip] = true;
    }

    //create a bogus data channel
    pc.createDataChannel("");

    // create offer and set local description
    pc.createOffer().then(function (sdp) {
        sdp.sdp.split('\n').forEach(function (line) {
            if (line.indexOf('candidate') < 0) return;
            line.match(ipRegex).forEach(iterateIP);
        });

        pc.setLocalDescription(sdp, noop, noop);
    }).catch(function (reason) {
        // An error occurred, so handle the failure to connect
    });

    //listen for candidate events
    pc.onicecandidate = function (ice) {
        if (!ice || !ice.candidate || !ice.candidate.candidate || !ice.candidate.candidate.match(ipRegex)) return;
        ice.candidate.candidate.match(ipRegex).forEach(iterateIP);
    };
}

/** ----------------------------------------------------------------------------
 * Adiciona uma div para um obj html ex: articleMesssagensCrud
 * param {any} objHtml
 * param {any} titleMsg
 * param {any} message
 * param {any} link
 * param {any} success
 -----------------------------------------------------------------------------*/
function ShowMessageCrud(objHtml, titleMsg, message, success = false)
{
    var div = objHtml;
    var div_ok    = '<div class="alert alert-success fade in"><a class="close" data-dismiss="alert" href="#">x</a><h4 class="alert-heading">' + titleMsg + '</h4><label">' + message + '</label></div>';
    var div_error = '<div class="alert alert-danger  fade in"><a class="close" data-dismiss="alert" href="#">x</a><h4 class="alert-heading">' + titleMsg + '</h4><label">' + message + '</label></div>';

    if (success) {
        $(div).append(div_ok);

    } else {
        $(div).append(div_error);

    }
}

/**
 * 
 * @param {any} value
 */
function ToJavaScriptDate(value)
{
    var pattern = /Date\(([^)]+)\)/;
    var results = pattern.exec(value);
    var dt = new Date(parseFloat(results[1]));

    return (dt.getMonth() + 1) + "/" + dt.getDate() + "/" + dt.getFullYear() + " " + dt.getHours() + ":" + dt.getMinutes();
}


/** ----------------------------------------------------------------------------
 * ShowDlgBoxCartorio
 * @param {any} options
----------------------------------------------------------------------------* */
function ShowDlgBoxCartorio(options) {
    var deferredObject = $.Deferred();

    var defaults = {
        type: "alert", //alert, prompt, confirm 
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
