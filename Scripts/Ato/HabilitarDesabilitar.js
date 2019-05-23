function DesabilitarProximo() {
    $('#btnProximo').attr('disabled', 'disabled');
    $('#btnProximo').removeClass('active');
}
function HabilitarProximo() {
    $('#btnProximo').removeAttr('disabled');
    $('#btnProximo').addClass('active');
}