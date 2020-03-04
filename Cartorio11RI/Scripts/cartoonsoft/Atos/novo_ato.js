/*
---------1---------2---------3---------4---------5---------6---------7---------8
01234567890123456789012345678901234567890123456789012345678901234567890123456789
--------------------------------------------------------------------------------
Cartoonsoft: Atos.NovoAto 
by Ronaldo Moreira - 2019
------------------------------------------------------------------------------*/


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

        var idPrenotacao = $("#IdPrenotacao").val().trim();
        var sel = $("#ddListMatriculasPrenotacao");
        $("#IdPrenotacao_2").val(idPrenotacao);

        if (isNaN(idPrenotacao) || !idPrenotacao) {
            $.smallBox({
                title: "Valor inválido!",
                content: "Número de prenotação está inválido.",
                color: cor_smallBox_aviso,
                icon: "fa fa-exclamation bounce animated",
                timeout: 4000
            });
        } else {
            PesquisarPrenotacao(idPrenotacao, sel);
        }
    });

    /*-- btn-reserva-mat ---------------------------------------------------- */
    $("#btn-reserva-mat").click(function (e) {
        e.preventDefault();

        var obj = $("#ddListMatriculasPrenotacao");
        var idPrenotacao = $("#IdPrenotacao").val().trim();
        var numMat = $("option:selected", obj).val();
        $("#NumMatricula_2").val(numMat);

        ProcReservarMatImovel(1, idPrenotacao, numMat, urlProcReservarMatImovel);
    });

    /*-- btn-libera-mat ----------------------------------------------------- */
    $("#btn-libera-mat").click(function (e) {
        e.preventDefault();

        var obj = $("#ddListMatriculasPrenotacao");
        var idPrenotacao = $("#IdPrenotacao").val().trim();
        var numMat = $("option:selected", obj).val();

        ProcReservarMatImovel(2, idPrenotacao, numMat, urlProcReservarMatImovel);
    });

    var ato_wizard = $('#div-wizard-novo-ato').wizard();

    /*-- ato_wizard on change ----------------------------------------------- */
    $(ato_wizard).on('change', function (e, data) {

        var idPrenotacao = $("#IdPrenotacao").val();
        var idModeloDoc = $("#IdModeloDoc").val();
        var numMatricula = $("#NumMatricula").val();
        var erro = false;
        var errMsg = "";

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

                errMsg = "";

                if (isNaN(idPrenotacao) || !idPrenotacao) {
                    erro = true;
                    errMsg += "Número de Prenotação inválido <br/>";
                }

                if (isNaN(idModeloDoc) || !idModeloDoc) {
                    erro = true;
                    errMsg += "Modelo de documento inválido <br/>";
                }

                if (isNaN(numMatricula) || !numMatricula) {
                    erro = true;
                    errMsg += "Matrícula de imóvel inválida<br/>";
                }

                if (!listaPessoasSelecionadas || (listaPessoasSelecionadas.length < 1)) {
                    erro = true;
                    errMsg += "Selecione pelo menos um outorgante/outorgado <br/>";
                }

                if (erro) {
                    $.smallBox({
                        title: "Não foi possivel processar sua requisição!",
                        content: errMsg,
                        color: cor_smallBox_erro,
                        icon: "fa fa-thumbs-down bounce animated",
                        timeout: 8000
                    });
                    return false;
                }

                var listaPes = GetListaPessoasSelecionadas();
                var idAto = $("#Id").val();
                var idLivro = $("#ddListLivro option:selected").val();
                var idTipoAto = $("#IdTipoAto").val();
                var idModeloDoc = $("#IdModeloDoc").val();
                var idPrenotacao = $("#IdPrenotacao").val();
                var numMatricula = $("#NumMatricula").val();
                var dataRegPrenotacao = new Date($("#DataRegPrenotacao").val());
                var dataAto = new Date($("#DataAto").val());

                var dados = {
                    IdAto: idAto,
                    IdLivro: idLivro,
                    IdTipoAto: idTipoAto,
                    IdModeloDoc: idModeloDoc,
                    IdPrenotacao: idPrenotacao,
                    NumMatricula: numMatricula,
                    DataRegPrenotacao: dataRegPrenotacao,
                    DataAto: dataAto,
                    ListIdsPessoas: listaPes
                };

                GetTextoAto(dados, urlGetTextoAto);
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




