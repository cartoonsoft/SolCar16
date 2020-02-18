/*
---------1---------2---------3---------4---------5---------6---------7---------8
01234567890123456789012345678901234567890123456789012345678901234567890123456789
--------------------------------------------------------------------------------
Cartoonsoft: Atos.NovoAto 
by Ronaldo Moreira - 2019
------------------------------------------------------------------------------*/

var PodeAvancar1 = false;
var PodeAvancar2 = false;
var PodeAvancar3 = false;

/** ----------------------------------------------------------------------------
 * Jquery procedures
------------------------------------------------------------------------------*/
$(document).ready(function () {

    /*-- mostrar tree view -------------------------------------------------- */
    $('.tree > ul').attr('role', 'tree').find('ul').attr('role', 'group');
    $('.tree').find('li:has(ul)').addClass('parent_li').attr('role', 'treeitem').find(' > span').on('click', function (e) {
        var children = $(this).parent('li.parent_li').find(' > ul > li');
        if (children.is(':visible')) {
            children.hide('slow');
            $(this).find(' > i').removeClass().addClass('fa fa-lg fa-plus-circle');
        } else {
            children.show('slow');
            $(this).find(' > i').removeClass().addClass('fa fa-lg fa-minus-circle');
        }
        e.stopPropagation();
    });

    var id = $("#Id").val();
    DesabilitarProximo();

    /*-- pesqusar por prenotacao Tecla enter -------------------------------- */
    $("#IdPrenotacao").keydown(function (e) {
        if (e.keyCode == 13) {
            $("#btn-pesq-prenotacao").click();
        }
    });

    /*-- btn-pesq-prenotacao ------------------------------------------------ */
    $("#btn-pesq-prenotacao").click(function (e) {
        e.preventDefault();

        var numPrenotacao = $("#IdPrenotacao").val().trim();
        var sel = $("#ddListMatriculasPrenotacao");
        $("#IdPrenotacao_2").val(numPrenotacao);

        if (isNaN(numPrenotacao) || !numPrenotacao) {
            $.smallBox({
                title: "Valor inválido!",
                content: "Número de prenotação está inválido.",
                color: cor_smallBox_aviso,
                icon: "fa fa-exclamation bounce animated",
                timeout: 4000
            });
        } else {
            PesquisarPrenotacao(numPrenotacao, sel);
        }

    });

    /*-- btn-reserva-mat ---------------------------------------------------- */
    $("#btn-reserva-mat").click(function (e) {
        e.preventDefault();

        var obj = $("#ddListMatriculasPrenotacao");
        var numPrenotacao = $("#IdPrenotacao").val().trim();
        var numMat = $("option:selected", obj).val();
        $("#NumMatricula_2").val(numMat);

        ProcReservarMatImovel(1, numPrenotacao, numMat, urlProcReservarMatImovel);
    });

    /*-- btn-libera-mat ----------------------------------------------------- */
    $("#btn-libera-mat").click(function (e) {
        e.preventDefault();

        var obj = $("#ddListMatriculasPrenotacao");
        var numPrenotacao = $("#IdPrenotacao").val().trim();
        var numMat = $("option:selected", obj).val();

        ProcReservarMatImovel(2, numPrenotacao, numMat, urlProcReservarMatImovel);
    });

    /*-- pesquisar pessoas -------------------------------------------------- */
    $("#btn-pesq-pessoas").click(function (e) {
        e.preventDefault();

        var numPrenotacao = $("#IdPrenotacao").val().trim();

        if (isNaN(numPrenotacao) || !numPrenotacao) {
            $.smallBox({
                title: "Valor inválido!",
                content: "Número de prenotação está inválido.",
                color: cor_smallBox_aviso,
                icon: "fa fa-exclamation bounce animated",
                timeout: 4000
            });
        } else {
            var dadosPrenotacao = {
                IdPrenotacao: numPrenotacao
            };

            GetListPessoasPrenotacao(dadosPrenotacao, urlGetListPessoasPrenotacao);
        }
    });

    /*-- btn-dlg-pessoas-ok   ----------------------------------------------- */
    $("#btn-dlg-pessoas-ok").click(function (e) {
        e.preventDefault();
        PodeAvancar2 = PovoarSelecionados($('#IdPrenotacao').val());
7    });

    var ato_wizard = $('#divWizardEdicaoAto').wizard();

    /*-- ato_wizard on change ----------------------------------------------- */
    $(ato_wizard).on('change', function (e, data) {

        var numPrenotacao = $("#IdPrenotacao").val().trim();

        /*-- etapa 1 -------------------------------------------------------- */
        if (data.step === 1 && data.direction === 'next') {
            if (!PodeAvancar1) {
                $.smallBox({
                    title: "Selecione uma matrícula!",
                    content: "Reserve uma matrícula para poder avançar.",
                    color: cor_smallBox_aviso,
                    icon: "fa fa-exclamation bounce animated",
                    timeout: 4000
                });
                return false;
            }
        }

        /*-- etapa 2 -------------------------------------------------------- */
        if (data.step === 2 && data.direction === 'next') {
            if (!PodeAvancar2) {
                $.smallBox({
                    title: "Selecione as pessoas!",
                    content: "Selecione pelo menos um outorgante/outorgado para continuar.",
                    color: cor_smallBox_aviso,
                    icon: "fa fa-exclamation bounce animated",
                    timeout: 4000
                });
                return false;
            }
        }

        /*-- etapa 3 -------------------------------------------------------- */
        if (data.step === 3 && data.direction === 'next') {
            if (!PodeAvancar3) {
                $.smallBox({
                    title: "Selecione um modelo!",
                    content: "Selecione um modelo de documento para continuar..",
                    color: cor_smallBox_aviso,
                    icon: "fa fa-exclamation bounce animated",
                    timeout: 4000
                });
                return false;
            } else {
                /*-- -------------------------------------------------------- */
                if (isNaN(numPrenotacao) || !numPrenotacao) {
                    $.smallBox({
                        title: "Número de prenotação inválido!",
                        content: "Preencha o número de prenotação...",
                        color: cor_smallBox_aviso,
                        icon: "fa fa-exclamation bounce animated",
                        timeout: 4000
                    });
                    return false;
                }
            }
        }

        return true;
    });

    /*-- ato_wizard on finished --------------------------------------------- */
    ato_wizard.on('finished', function (e, data) {
        //$("#fuelux-wizard").submit();
        if ($("#Salvo").prop("checked") == false) {
            ShowDlgBoxCartorio({
                type: "confirm",
                headerText: "Confirme ",
                messageText: "Salvar os dados do Ato?",
                alertType: "info"
            }).done(function (e) {
                //alert(e);
                //$("body").append('<div>Callback from confirm ' + e + '</div>');
                if (e) {
                    $("#btn-ato-salvar").click();
                }
            });
        }
    });

    /*-- frm-cadastro-ato on submit ----------------------------------------- */
    $('#frm-cadastro-ato').on('submit', function (event) {
        // Prevent the page from reloading
        event.preventDefault();
        //alert("deu submit");
        // Set the text-output span to the value of the first input
        //var $input = $(this).find('input');
        //var input = $input.val();
    });

});

/** ----------------------------------------------------------------------------
 * 
------------------------------------------------------------------------------*/
function DesabilitarProximo()
{
    $('#btn-proximo').attr('disabled', 'disabled');
    $('#btn-proximo').removeClass('active');
}

/** ----------------------------------------------------------------------------
 *
------------------------------------------------------------------------------*/
function HabilitarProximo()
{
    $('#btn-proximo').removeAttr('disabled');
    $('#btn-proximo').addClass('active');
}

/** ----------------------------------------------------------------------------
 * Pesquisa por prenotação e busca a lista de matriculas desta prenotação
 * @@param {any} numPrenotacao
 * @@param {any} selObj select que será povoado com  os núm. de matriculas de imoveis
 * @@param {any} url
----------------------------------------------------------------------------- */
function PesquisarPrenotacao(numPrenotacao, selObj)
{
    if (!isNaN(numPrenotacao) || !numPrenotacao) {

        var dadosPrenotacao = {
            IdPrenotacao: numPrenotacao
        };

        GetDadosPrenotacao(dadosPrenotacao, selObj);
    } else {
        $.smallBox({
            title: "Entrada inválida!",
            content: "Número de prenotação está inválido!",
            color: cor_smallBox_erro,
            icon: "fa fa-thumbs-down bounce animated",
            timeout: 4000
        });
    }
}

/** ----------------------------------------------------------------------------
 * 
 * @@param {any} dadosPrenotacao
 * @@param {any} selObj
 * @@param {any} url
 ---------------------------------------------------------------------------- */
function GetDadosPrenotacao(dadosPrenotacao, selObj)
{
    $.ajax(urlGetDadosPrenotacao, {
        method: 'POST',
        dataType: 'json',
        data: dadosPrenotacao,
        beforeSend: function () {
            ShowProgreessBar("Processando requisição...");
        }
    }).done(function (dataReturn) {
        if (dataReturn.resposta) {

            if (dataReturn.DataRegPrenotacao) {

                $("#DataRegPrenotacao").val(dataReturn.DataRegPrenotacao);
            }
            GetListPessoasPrenotacao(dadosPrenotacao);
            GetListMatriculasPrenotacao(dadosPrenotacao, selObj);

        } else {
            $.smallBox({
                title: "Não foi possivel processar sua requisição!",
                content: dataReturn.msg,
                color: cor_smallBox_erro,
                icon: "fa fa-thumbs-down bounce animated",
                timeout: 8000
            });
        }
    }).fail(function (jq, textStatus, error) {
        HideProgressBar();
        $.smallBox({
            title: "Falha na sua requisição!",
            content: textStatus + "[" + error + "]",
            color: cor_smallBox_erro,
            icon: "fa fa-thumbs-down bounce animated",
            timeout: 8000
        });
    }).always(function () {
        HideProgressBar();
    });
}

/** ----------------------------------------------------------------------------
 * 
 * @@param {any} dadosPrenotacao
----------------------------------------------------------------------------- */
function GetListPessoasPrenotacao(dadosPrenotacao)
{
    $.ajax(urlGetListPessoasPrenotacao, {
        method: 'POST',
        dataType: 'json',
        data: dadosPrenotacao,
        beforeSend: function () {
            ShowProgreessBar("Processando requisição...");
        }
    }).done(function (dataReturn) {
        if (dataReturn.resposta) {
            if (dataReturn.ListaPessoasPrenotacao) {
                PovoarTblPessoasPrenotacao(dataReturn.ListaPessoasPrenotacao);
            } else {
                $.smallBox({
                    title: "Pessoas da prenotação não encontradas!",
                    content: dataReturn.msg,
                    color: cor_smallBox_aviso,
                    icon: "fa fa-exclamation bounce animated",
                    timeout: 4000
                });
            }
        } else {
            $.smallBox({
                title: "Não foi possivel processar sua requisição!",
                content: dataReturn.msg,
                color: cor_smallBox_erro,
                icon: "fa fa-thumbs-down bounce animated",
                timeout: 8000
            });
        }
    }).fail(function (jq, textStatus, error) {
        HideProgressBar();
        $.smallBox({
            title: "Falha na sua requisição!",
            content: textStatus + "[" + error + "]",
            color: cor_smallBox_erro,
            icon: "fa fa-thumbs-down bounce animated",
            timeout: 8000
        });
    }).always(function () {
        HideProgressBar();
    });
}

/** ----------------------------------------------------------------------------
 * 
 * @@param {any} listaPessoasPrenotacao
----------------------------------------------------------------------------- */
function PovoarTblPessoasPrenotacao(listaPessoasPrenotacao)
{
    var doc = "";
    var chkTmp = "";
    $("#tbl-pessoas-prenotacao").find("tr:gt(0)").remove();

    $.each(listaPessoasPrenotacao, function (index, item) {

        doc = item.Numero1.trim();
        if (doc == "") {
            doc = item.Numero2.trim();
        }

        if (item.Valido) {
            chkTmp = '<input type="checkbox" id="chk_pes_pre_' + item.IdPessoa + '" class="chk_cartoon" />';
            doc = '<button type="button" class="btn btn-success btn-xs" title="' + item.RetornoValidacao + '"><i class="fa fa-thumbs-up"></i></button>&nbsp;' + doc;
        } else {
            chkTmp = '<input type="checkbox" id="chk_pes_pre' + item.Id + '" class="chk_cartoon" disabled />';
            doc = '<button type="button" class="btn btn-danger btn-xs" title="' + item.RetornoValidacao + '"><i class="fa fa-thumbs-down"></i></button>&nbsp;' + doc;
        }

        $("#tbl-pessoas-prenotacao tbody").append(
            '<tr>' +
                '<td>' + chkTmp + '</td>' +
                '<td>' + item.DescTipoPessoa + '</td>' +
                '<td>' + doc + '</td>' +
                '<td>' + item.Nome + '</td>' +
                '<td>' + item.Endereco + '</td>' +
                '<td>' + item.Bairro + '</td>' +
                '<td>' + item.Cidade + '</td>' +
                '<td>' + item.Uf + '</td>' +
                '<td>' + item.Cep + '</td>' +
                '<td>' + item.Telefone + '</td>' +
            '</tr>'
        );
    });
} 

/** ----------------------------------------------------------------------------
 * 
 * @@param {any} dadosPrenotacao
 * @@param {any} selObj
 ---------------------------------------------------------------------------- */
function GetListMatriculasPrenotacao(dadosPrenotacao, selObj)
{
    $('#btn-reserva-mat').prop('disabled', true);
    $('#btn-libera-mat').prop('disabled', true);

    $.ajax(urlGetListMatriculasPrenotacao, {
        method: 'POST',
        dataType: 'json',
        data: dadosPrenotacao,
        beforeSend: function () {
            ShowProgreessBar("Processando requisição...");
        }
    }).done(function (dataReturn) {
        if (dataReturn.resposta) {
            if (dataReturn.ListaMatriculasPrenotacao) {
                PovoarSelMatriculasPrenotacao(selObj, dataReturn.ListaMatriculasPrenotacao);
                $('#btn-reserva-mat').prop('disabled', false);
                $('#btn-libera-mat').prop('disabled', false);
            } else {
                $.smallBox({
                    title: "Dados não encontrados!",
                    content: dataReturn.msg,
                    color: cor_smallBox_aviso,
                    icon: "fa fa-exclamation bounce animated",
                    timeout: 4000
                });
            }
        } else {
            $.smallBox({
                title: "Não foi possivel processar sua requisição!",
                content: dataReturn.msg,
                color: cor_smallBox_erro,
                icon: "fa fa-thumbs-down bounce animated",
                timeout: 8000
            });
        }
    }).fail(function (jq, textStatus, error) {
        HideProgressBar();
        $.smallBox({
            title: "Falha na sua requisição!",
            content: textStatus + "[" + error + "]",
            color: cor_smallBox_erro,
            icon: "fa fa-thumbs-down bounce animated",
            timeout: 8000
        });
    }).always(function () {
        HideProgressBar();
    });
}

/** ----------------------------------------------------------------------------
 * processar reservar
 * @@param {any} tipoReserva
 * @@param {any} idPrenotacao
 * @@param {any} numMatricula
----------------------------------------------------------------------------- */
function ProcReservarMatImovel(tipoReserva, numPrenotacao, numMatricula, url)
{
    var msg_title = "";

    var dadosReserva = {
        TipoReserva:  tipoReserva,
        IdPrenotacao: numPrenotacao,
        NumMatricula: numMatricula
    };

    if (tipoReserva == 1) {
        msg_title = "Matrícula reservada!"
    } else if (tipoReserva == 2) {
        msg_title = "Matrícula liberada!"
    }

    $.ajax(url, {
        method: 'POST',
        dataType: 'json',
        data: dadosReserva,
        beforeSend: function () {
            ShowProgreessBar("Processando requisição...");
        }
    }).done(function (dataReturn) {
        if (dataReturn.resposta) {

            //reservar
            if (tipoReserva == 1) {
                var dadosValidos = !(typeof dataReturn.Reserva.Imovel == 'undefined' || dataReturn.Reserva.Imovel == null);
                if (dadosValidos) {
                    PodeAvancar1 = true;
                    HabilitarProximo();
                    PovoarDadosImovel(dataReturn.Reserva.Imovel);
                }
            } else {
                PodeAvancar1 = false;
                DesabilitarProximo();
                LimparDadosImovel();
            }

            if (dataReturn.tipoMsg == 1) {
                $.smallBox({
                    title: msg_title,
                    content: dataReturn.msg,
                    color: cor_smallBox_ok,
                    icon: "fa fa-thumbs-up bounce animated",
                    timeout: 4000
                });
            } else {
                $.smallBox({
                    title: msg_title,
                    content: dataReturn.msg,
                    color: cor_smallBox_aviso,
                    icon: "fa fa-exclamation bounce animated",
                    timeout: 4000
                });
            }
        } else {
            $.smallBox({
                title: "Não foi possivel processar sua requisição!",
                content: dataReturn.msg,
                color: cor_smallBox_erro,
                icon: "fa fa-thumbs-down bounce animated",
                timeout: 8000
            });
        }
    }).fail(function (jq, textStatus, error) {
        $.smallBox({
            title: "Falha na sua requisição!",
            content: textStatus + "[" + error + "]",
            color: cor_smallBox_erro,
            icon: "fa fa-thumbs-down bounce animated",
            timeout: 8000
        });

    }).always(function () {
        HideProgressBar();
    });
}

/** ----------------------------------------------------------------------------
 * PovoarSelMatriculasPrenotacao - povoar select : ddListMatriculasPrenotacao
 * @@param {any} selObj
 * @@param {any} listaModelos
----------------------------------------------------------------------------- */
function PovoarSelMatriculasPrenotacao(selObj, listaMatriculas)
{
    var sel = selObj;

    if (sel) {
        $(sel).empty();
        $.each(listaMatriculas, function (index, item) {
            $(sel).append('<option value="' + item+ '" >' + item + '</option>');
        });
    }
}

/** ----------------------------------------------------------------------------
 * GerarTextoAto
 * @@param {any} dadosAto
 * @@param {any} url
----------------------------------------------------------------------------- */
function GetTextoAto(dadosAto, url)
{
    $.ajax(url, {
        method: 'POST',
        dataType: 'json',
        data: dadosAto,
        beforeSend: function () {
            ShowProgreessBar("Processando requisição...");
        }
    }).done(function (dataReturn) {
        //
        if (dataReturn.resposta) {
            var dadosValidos = !(typeof dataReturn.TextoHtml == 'undefined' || dataReturn.TextoHtml == null);

            if (dadosValidos) {
                CKEDITOR.instances.ckEditorAto.setData(dataReturn.TextoHtml);
            }
        } else {
            $.smallBox({
                title: "Não foi possivel processar sua requisição!",
                content: dataReturn.msg,
                color: cor_smallBox_erro,
                icon: "fa fa-thumbs-down bounce animated",
                timeout: 8000
            });
        }
    }).fail(function (jq, textStatus, error) {
        //
        HideProgressBar();
    }).always(function () {
        HideProgressBar();
    });
}

/** ----------------------------------------------------------------------------
 * Povoar dados do imovel
 * @@param {any} Imovel
 ---------------------------------------------------------------------------- */
function PovoarDadosImovel(Imovel)
{
    $('#NumMatricula').val(Imovel.MATRI);
    $('#PREIMO_SEQPRE').val(Imovel.SEQPRE);
    
    $('#PREIMO_ENDER').val(Imovel.ENDER);
    $('#PREIMO_NUM').val(Imovel.NUM);
    $('#PREIMO_LOTE').val(Imovel.LOTE);
    $('#PREIMO_QUADRA').val(Imovel.QUADRA);
    $('#PREIMO_APTO').val(Imovel.APTO);
    $('#PREIMO_BLOCO').val(Imovel.BLOCO);
    $('#PREIMO_EDIF').val(Imovel.EDIF);
    $('#PREIMO_VAGA').val(Imovel.VAGA);
    $('#PREIMO_OUTROS').val(Imovel.OUTROS);
    $('#PREIMO_TRANS').val(Imovel.TRANS);
    $('#PREIMO_INSCR').val(Imovel.INSCR);
    $('#PREIMO_HIPO').val(Imovel.HIPO);
    $('#PREIMO_RD').val(Imovel.RD);
    $('#PREIMO_CONTRIB').val(Imovel.CONTRIB);
}

/** ----------------------------------------------------------------------------
 * limpar dados do imovel
 * @@param {any} Imovel
 ---------------------------------------------------------------------------- */
function LimparDadosImovel()
{
    $('#NumMatricula').val("");
    $('#PREIMO_SEQPRE').val("");

    $('#PREIMO_ENDER').val("");
    $('#PREIMO_NUM').val("");
    $('#PREIMO_LOTE').val("");
    $('#PREIMO_QUADRA').val("");
    $('#PREIMO_APTO').val("");
    $('#PREIMO_BLOCO').val("");
    $('#PREIMO_EDIF').val("");
    $('#PREIMO_VAGA').val("");
    $('#PREIMO_OUTROS').val("");
    $('#PREIMO_TRANS').val("");
    $('#PREIMO_INSCR').val("");
    $('#PREIMO_HIPO').val("");
    $('#PREIMO_RD').val("");
    $('#PREIMO_CONTRIB').val("");
}



