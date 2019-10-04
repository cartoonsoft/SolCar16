function GetModelos() {
    $.get('/Ato/GetModelos', function (data, status) {
        resultado = JSON.parse(data);
        var tabela = $('#tabelaModelo tbody');
        tabela.html("");
        resultado.forEach(item => {
            tabela.append(`
                    <tr role"row" id=${item.Id}>
                        <td class="idModelo">${item.Id}</td>
                        <td class="nomeModelo">${item.NomeModelo}</td>
                        <td class="descricaoTipoAto">${item.DescricaoTipoAto}</td>
                        <td>
                            <button class="btn btn-success enviar" onclick="enviarDados(this)" type="button">
                                Usar
                            </button>
                        </td>
                    </tr>`);
        });
        AjustarTabelaModelo();
    });
}

function enviarDados(objeto) {
    var linha = objeto.closest("tr");
    var modeloNome = linha.querySelector(".nomeModelo");
    var tipoAto = linha.querySelector(".descricaoTipoAto");
    var idModelo = linha.querySelector(".idModelo");

    $('#Modelo_Id').val(idModelo.innerText);
    $('#Modelo_NomeModelo').val(modeloNome.innerText);
    $('#Modelo_DescricaoTipoAto').val(tipoAto.innerText);
    $('#Modelo_DescricaoTipoAto').valid();
    $('#Modelo_NomeModelo').valid();
    HabilitarProximo();
    $('#modeloInvalido').val("False");
    IrParaProximo();

}

function AtualizarModelo(idTipoAto) {
    var Id = parseInt($('#Modelo_Id').val());
    var NumMatricula = parseInt($('#PREIMO_MATRI').val());
    var IdPrenotacao = parseInt($('#PREIMO_SEQPRE').val());
    var IdTipoAto = parseInt(idTipoAto);
    var listIdsPessoas = [];
    participantes.forEach(item => {
        listIdsPessoas.push(parseInt($(item).attr('id')));
    });
    var DadosPostModelo = {
        Id,
        NumMatricula,
        IdPrenotacao,
        IdTipoAto,
        listIdsPessoas
    }

    $('#mensagemLoading').text('Buscando modelo...');
    $('#divRespImo').attr('hidden');
    $.ajax({
        url: '/Ato/UsaModeloParaAto',
        type: 'POST',
        data: {
            DadosPostModelo
        },
        success: function (result) {
            editor.setData(result);
            $('#upload').removeAttr('disabled');
            $('#ckEditorGrid').removeAttr('hidden');
            $('#ModalLoading').modal('hide');

        },
        error: function (error) {
            console.log(error);
            voltarPasso();
            $('#divRespImo').removeAttr('hidden');
            $('#labelRespImo').text(error.statusText);
            $('#ModalLoading').modal('hide');

        },
        beforeSend: function (xhr, settings) {
            $('#mensagemLoading').text('Escrevendo modelo...');
        }
    });
}

function AjustarTabelaModelo() {
    $('#tabelaModelo').DataTable({
        "searching": true,
        "language": {
            "lengthMenu": "Mostrando _MENU_ registros por página",
            "search": "Filtre os modelos:",
            "zeroRecords": "Nenhum Registro Encontrado",
            "info": "Mostrando Página _PAGE_ de _PAGES_",
            "infoEmpty": "Nenhum registro Disponível",
            "infoFiltered": "(filtrado dos registros)"
        },
        "pageLength": 5,
        "bLengthChange": false,
        "columnDefs": [
            {
                "targets": [3],
                orderable: false
            },
        ]
    });
};