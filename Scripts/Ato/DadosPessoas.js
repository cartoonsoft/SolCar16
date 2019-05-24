function AlterarParticipantes() {
    var seqPre = $('#PREIMO_SEQPRE').val();
    debugger;
    if (seqPre != null && seqPre != undefined && seqPre != "") {
        $('#ModalLoading').modal('show');
        GetDadosPessoa(seqPre);
    }
}

function GetDadosPessoa(numPre) {
    $('#mensagemLoading').text('Buscando dados dos participantes...');
    $.get('/Ato/GetPessoasPremo',
        { numeroPrenotacao: numPre },
        function (data, status) {
            $('#divResp').attr('hidden');
            if (status == "success") {
                if (data != "" && data != "null") {
                    resultado = JSON.parse(data);
                    debugger;
                    resultado = ajustaTipoDocumento(resultado);
                    debugger;
                    $.ajax({
                        method: 'POST',
                        url: '/Ato/PartialDadosPessoas',
                        data: { listaPessoas: JSON.stringify(resultado) },
                        success: function (data) {
                            $('#ModalLoading').modal('hide');
                            $('#PartialDadosPessoas').html(data);
                            $('#PartialDadosPessoas').modal('show');
                        }
                    });

                    $('#divRespPes').attr('hidden');
                } else {
                    $('#labelRespPes').text('Aconteceu algum erro ao buscar os dados da pessoa.');
                    $('#divRespPes').removeAttr('hidden');
                    LimparDados();
                    $('#ModalLoading').modal('hide');
                }
            }
            else {
                $('#labelRespPes').text('Aconteceu algum erro ao buscar os dados da pessoa.');
                $('#divRespPes').removeAttr('hidden');

                LimparDados();
                $('#ModalLoading').modal('hide');
            }
        });
}

function TestaCNPJ(cnpj) {

    cnpj = cnpj.replace(/[^\d]+/g, '');

    if (cnpj == '') return false;

    if (cnpj.length != 14)
        return false;

    // Elimina CNPJs invalidos conhecidos
    if (cnpj == "00000000000000" ||
        cnpj == "11111111111111" ||
        cnpj == "22222222222222" ||
        cnpj == "33333333333333" ||
        cnpj == "44444444444444" ||
        cnpj == "55555555555555" ||
        cnpj == "66666666666666" ||
        cnpj == "77777777777777" ||
        cnpj == "88888888888888" ||
        cnpj == "99999999999999")
        return false;

    // Valida DVs
    tamanho = cnpj.length - 2
    numeros = cnpj.substring(0, tamanho);
    digitos = cnpj.substring(tamanho);
    soma = 0;
    pos = tamanho - 7;
    for (i = tamanho; i >= 1; i--) {
        soma += numeros.charAt(tamanho - i) * pos--;
        if (pos < 2)
            pos = 9;
    }
    resultado = soma % 11 < 2 ? 0 : 11 - soma % 11;
    if (resultado != digitos.charAt(0))
        return false;

    tamanho = tamanho + 1;
    numeros = cnpj.substring(0, tamanho);
    soma = 0;
    pos = tamanho - 7;
    for (i = tamanho; i >= 1; i--) {
        soma += numeros.charAt(tamanho - i) * pos--;
        if (pos < 2)
            pos = 9;
    }
    resultado = soma % 11 < 2 ? 0 : 11 - soma % 11;
    if (resultado != digitos.charAt(1))
        return false;

    return true;

}
function TestaCPF(strCPF) {
    var Soma;
    var Resto;
    Soma = 0;
    if (strCPF == "00000000000") return false;

    for (i = 1; i <= 9; i++) Soma = Soma + parseInt(strCPF.substring(i - 1, i)) * (11 - i);
    Resto = (Soma * 10) % 11;

    if ((Resto == 10) || (Resto == 11)) Resto = 0;
    if (Resto != parseInt(strCPF.substring(9, 10))) return false;

    Soma = 0;
    for (i = 1; i <= 10; i++) Soma = Soma + parseInt(strCPF.substring(i - 1, i)) * (12 - i);
    Resto = (Soma * 10) % 11;

    if ((Resto == 10) || (Resto == 11)) Resto = 0;
    if (Resto != parseInt(strCPF.substring(10, 11))) return false;
    return true;
}

function ajustaTipoDocumento(resultado) {
    for (var i = 0; i < resultado.length; i++) {
        let documento = resultado[i].Numero1.trim().replace(/[^\w\s]/gi, "");
        let tipoDocumento = " - ";
        if (TestaCPF(documento)) {
            tipoDocumento = "CPF";
        } else if (TestaCNPJ(documento)) {
            tipoDocumento = "CNPJ";
        } else if (documento != null && documento != "") {
            tipoDocumento = "RG";
        }
        resultado[i].TipoDoc1 = tipoDocumento;
    }
    return resultado;
}