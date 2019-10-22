function ExisteAtoBanco(idTipoAto) {
    var NumMatricula = $('#PREIMO_MATRI').val();
    $.get('/Ato/ExisteAto',
        { NumMatricula },
        function (data, status) {
            if (status == "success") {
                $('#divRespImo').attr('hidden', 'hidden');
                var existeAto = data;
                debugger;
                if (existeAto == "False") {
                    $('#ModalLoading').modal('show');
                    $('#divRespImo').attr('hidden', 'hidden');
                    if (idTipoAto != 3) {
                        $('#ExisteNoSistema').val("False");
                        $('#infoAdicional').removeClass('hidden');
                        $('#btnConfirmar').attr('disabled');
                    } else {
                        $('#btnConfirmar').removeAttr('disabled');
                    }
                    AtualizarModelo(idTipoAto);
                } else {
                    $('#ExisteNoSistema').val("True");
                    $('#infoAdicional').addClass('hidden');
                    $('#btnConfirmar').removeAttr('disabled');
                    if (idTipoAto == 3) {
                        voltarPasso();
                        $('#labelRespImo').text('Já existe ato inicial para os dados!');
                        $('#divRespImo').removeAttr('hidden');
                        $('#modeloInvalido').val("True");
                        DesabilitarProximo();
                        return;
                    } else {
                        $('#ModalLoading').modal('show');
                        $('#divRespImo').attr('hidden', 'hidden');
                        AtualizarModelo(idTipoAto);
                    }
                }
                $('#modeloInvalido').val("False");
                HabilitarProximo();

            }
            else {
                $('#labelRespImo').text('Aconteceu algum erro ao buscar os dados.');
                $('#divRespImo').removeAttr('hidden');
                $('#modeloInvalido').val("True");
                DesabilitarProximo();
            }
        });
}

function AtualizarIdTipoAto(idModelo) {
    $.get('/Ato/GetIdTipoAtoPeloModelo',
        { idModelo },
        function (data, status) {
            $('#IdTipoAto').val(data);
            ExisteAtoBanco(data);
        });
}