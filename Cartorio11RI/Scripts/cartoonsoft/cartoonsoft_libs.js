/*
---------1---------2---------3---------4---------5---------6---------7---------8
01234567890123456789012345678901234567890123456789012345678901234567890123456789
--------------------------------------------------------------------------------
Cartoonsoft libs
by Ronaldo Moreira - 2019
------------------------------------------------------------------------------*/

/**
 * /
 * IP local
 * param {any} onNewIP
 */
function getUserIP(onNewIP) { //  onNewIp - your listener function for new IPs
    //compatibility for firefox and chrome
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

/**
 * Adiciona uma div para um obj html ex: articleMesssagensCrud
 * param {any} objHtml
 * param {any} titleMsg
 * param {any} message
 * param {any} link
 * param {any} success
 */
function ShowMessageCrud(objHtml, titleMsg, message, success = false) {
    var div = objHtml;
    var div_ok    = '<div class="alert alert-success fade in"><a class="close" data-dismiss="alert" href="#">x</a><h4 class="alert-heading">' + titleMsg + '</h4><label">' + message + '</label></div>';
    var div_error = '<div class="alert alert-danger  fade in"><a class="close" data-dismiss="alert" href="#">x</a><h4 class="alert-heading">' + titleMsg + '</h4><label">' + message + '</label></div>';

    if (success) {
        $(div).append(div_ok);

    } else {
        $(div).append(div_error);

    }
}

/*-----------------------------------------------------------------------------*/


