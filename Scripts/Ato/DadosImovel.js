function PesquisarImovel() {
    setInterval(
        setTimeout(function () {
            var numeroPrenotacao = parseInt($('#txtPrenotacao').val());
            var numeroMatricula = parseInt($('#txtMatricula').val());
            debugger;
            if (isNaN(numeroPrenotacao) && numeroPrenotacao == "" && isNaN(numeroMatricula) && numeroMatricula == "") {
                $('#labelResp').text('Preencha o numero da prenotação e/ou a matricula.');
                $('#divResp').removeAttr('hidden');

                //LIMPANDO CAMPOS
                LimparDados();
                LimparTabelaPessoas();
            } else {
                $('#divResp').attr('hidden');
                GetDadosImovel(numeroPrenotacao, numeroMatricula);
                $('#btnProximo').removeAttr('disabled');
                $('#btnProximo').addClass('active');
            }
        }, 200),
        400);
}

function GetDadosImovel(numPre, numMat) {
    $('#ModalLoading').modal('show');
    $('#mensagemLoading').text('Buscando dados do imóvel...');
    $.get('/Ato/GetDadosImovel',
        { numeroPrenotacao: numPre, numeroMatricula: numMat },
        function (data, status) {
            debugger;
            if (status == "success") {
                debugger;
                if (data != "" && data != "null") {
                    $('#divResp').attr('hidden', 'hidden');
                    resultado = JSON.parse(data);
                    enviarDadosMatricula(resultado);
                    GetDadosPessoa(resultado.SEQPRE);
                }
                else {
                    $('#labelResp').text('Não existem dados com as informações fornecidas.');
                    $('#divResp').removeAttr('hidden');
                    LimparDados();
                    LimparTabelaPessoas();
                    $('#ModalLoading').modal('hide');
                }
            }
            else {
                $('#labelResp').text('Aconteceu algum erro ao buscar os dados do imóvel.');
                $('#divResp').removeAttr('hidden');
                LimparDados();
                LimparTabelaPessoas();
                $('#ModalLoading').modal('hide');
            }
        });
}

function enviarDadosMatricula(resultado) {

    //COLOCA O RESULTADO NOS CAMPOS
    $('#numeroPre').text(resultado.SEQPRE);
    $('#PREIMO_MATRI').val(resultado.MATRI);
    $('#PREIMO_SEQPRE').val(resultado.SEQPRE);
    $('#PREIMO_ENDER').val(resultado.ENDER);
    $('#PREIMO_NUM').val(resultado.NUM);
    $('#PREIMO_APTO').val(resultado.APTO);
    $('#PREIMO_BLOCO').val(resultado.BLOCO);
    $('#PREIMO_QUADRA').val(resultado.QUADRA);
    $('#PREIMO_EDIF').val(resultado.EDIF);
    $('#PREIMO_OUTROS').val(resultado.OUTROS);
    $('#PREIMO_LOTE').val(resultado.LOTE);

    //VALIDA SE OS CAMPOS FORAM PREENCHIDOS
    $('#PREIMO_MATRI').valid();
    $('#PREIMO_ENDER').valid();
    $('#PREIMO_NUM').valid();
    $('#PREIMO_APTO').valid();
    $('#PREIMO_BLOCO').valid();
    $('#PREIMO_QUADRA').valid();
    $('#PREIMO_EDIF').valid();
    $('#PREIMO_OUTROS').valid();
    $('#PREIMO_LOTE').valid();
}