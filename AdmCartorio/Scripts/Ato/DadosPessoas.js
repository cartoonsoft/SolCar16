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