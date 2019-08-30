/*
---------1---------2---------3---------4---------5---------6---------7---------8
01234567890123456789012345678901234567890123456789012345678901234567890123456789
--------------------------------------------------------------------------------
Cartoonsoft: Atos.Novo 
by Ronaldo Moreira - 2019
------------------------------------------------------------------------------*/


/*------------------------------------------------------------------------------
 * Funçoes: * javascript da view: Atos.Novo
 *
 * ---------------------------------------------------------------------------*/

/**
 * Ajax busca dadso do imóvel por num prenotacao/matricula
 * @@param dataPreMat
 */
function GetDadosImovel(dataPreMat, url) {

    $.ajax(url, {
        method: 'POST',
        dataType: 'json',
        data: dataPreMat,
        beforeSend: function () {
            ShowProgreessBar("Processando requisição...");
        }
    }).done(function (dataReturn) {
        //
        if (dataReturn.Preimo.resposta) {

            PovoarDadosImovel(dataReturn.Preimo);
            HabilitarProximo();

            $.smallBox({
                title: "Requisição processada com sucesso!",
                content: dataReturn.Preimo.msg,
                color: "#296191",
                icon: "fa fa-thumbs-up bounce animated",
                timeout: 4000
            });
        } else {
            $.smallBox({
                title: "Não foi possivel processar sua requisição!",
                content: dataReturn.Preimo.msg,
                color: "#992111",
                icon: "fa fa-thumbs-down bounce animated",
                timeout: 8000
            });
        }
    }).fail(function (jq, textStatus, error) {
        //

    }).always(function () {
        HideProgressBar();
    });
}

/**
 * Busca lista pessoa por prenotação
 * @@param preimo
 */
function GetPessoasPrenotacao(dadosPrenotacao, url) {

    $.ajax(url, {
        method: 'POST',
        dataType: 'json',
        data: dadosPrenotacao,
        beforeSend: function () {
            ShowProgreessBar("Processando requisição...");
        }
    }).done(function (dataReturn) {
        //
        if (dataReturn.resposta) {

            ShowTblPessoasPrenotacao(dataReturn.listaPessoas);

        } else {
            $.smallBox({
                title: "Não foi possivel processar sua requisição!",
                content: dataReturn.msg,
                color: "#992111",
                icon: "fa fa-thumbs-down bounce animated",
                timeout: 8000
            });
        }
    }).fail(function (jq, textStatus, error) {
        //

    }).always(function () {
        HideProgressBar();
    });
}

/**
 * 
 * @@param listadados
 */
function ShowTblPessoasPrenotacao(listadados) {

    $("#tbl-selecao-pessoas").find("tr:gt(0)").remove();

    $.each(listadados, function (index, item) {

        var doc = item.Numero1.trim();

        if (doc == "") {
            doc = item.Numero2.trim();
        }
        $("#tbl-selecao-pessoas tbody").append(
            '<tr>' +
            '<td style="width:20px">' + '<input type="checkbox" id="chk-selecao-pessoas_' + item.IdPessoa + '">' + '</td>' +
            '<td style="width:50px">' + item.TipoPessoa + '</td>' +
            '<td style="width:80px">' + doc + '</td>' +
            '<td style="width:200px">' + item.Nome + '</td>' +
            '</tr>'
        );

    });

    $('#div-dlg-pessoas label[id*="lbl-dlg-pessoa-header"]').text("Selecionar outorgante(s)/outorgado(s), Prenotação: " + $("#PREIMO_SEQPRE").val());
    $('#div-dlg-pessoas').modal('show');
}

/**
 * 
 * */
function PovoarSelecionados() {

    $("#tbl-selecao-pessoas").find("tr:gt(0)").remove();

    $.each(listadados, function (index, item) {

        var doc = item.Numero1.trim();

        if (doc == "") {
            doc = item.Numero2.trim();
        }

    });

    $('#tbl-selecao-pessoas > tbody  > tr').each(function (elem) {

        $("#tblPessoasSelecionadas tbody").append(
            '<tr>' +
            '<td style="width:20px">' + '<input type="checkbox" id="chk-selecao-pessoas_' + item.IdPessoa + '">' + '</td>' +
            '<td style="width:50px">' + item.TipoPessoa + '</td>' +
            '<td style="width:80px">' + doc + '</td>' +
            '<td style="width:200px">' + item.Nome + '</td>' +
            '</tr>'
        );


    });

    $('#div-dlg-pessoas').modal('hide');
}

/**
 * povoar dados imovel
 * @@param preimo
 */
function PovoarDadosImovel(preimo) {

    $('#PREIMO_SEQPRE').val(preimo.SEQPRE);
    $('#PREIMO_MATRI').val(preimo.MATRI);

    $('#PREIMO_ENDER').val(preimo.ENDER);
    $('#PREIMO_NUM').val(preimo.NUM);
    $('#PREIMO_LOTE').val(preimo.LOTE);
    $('#PREIMO_QUADRA').val(preimo.QUADRA);
    $('#PREIMO_APTO').val(preimo.APTO);
    $('#PREIMO_BLOCO').val(preimo.BLOCO);
    $('#PREIMO_EDIF').val(preimo.EDIF);
    $('#PREIMO_VAGA').val(preimo.VAGA);
    $('#PREIMO_OUTROS').val(preimo.OUTROS);
    $('#PREIMO_TRANS').val(preimo.TRANS);
    $('#PREIMO_INSCR').val(preimo.INSCR);
    $('#PREIMO_HIPO').val(preimo.HIPO);
    $('#PREIMO_RD').val(preimo.RD);
    $('#PREIMO_CONTRIB').val(preimo.CONTRIB);
}

function DesabilitarProximo() {
    $('#btn-proximo').attr('disabled', 'disabled');
    $('#btn-proximo').removeClass('active');
}

function HabilitarProximo() {
    $('#btn-proximo').removeAttr('disabled');
    $('#btn-proximo').addClass('active');
}
