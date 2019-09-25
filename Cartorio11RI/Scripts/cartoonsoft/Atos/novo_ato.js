﻿/*
---------1---------2---------3---------4---------5---------6---------7---------8
01234567890123456789012345678901234567890123456789012345678901234567890123456789
--------------------------------------------------------------------------------
Cartoonsoft: Atos.Novo 
by Ronaldo Moreira - 2019
------------------------------------------------------------------------------*/

/*----------------------------------------------------------------------------*/
var arrayPessoas = [];

/*----------------------------------------------------------------------------*/
class PessoaPrenotacao {

    constructor() {
        var _IdPessoa = 0;
        var _IdPrenotacao = 0;
        var _Selecionado = false;
        var _Nome = "";
        var _Endereco = "";
        var _TipoDoc = ""
        var _NumDoc = "";
        var _Bairro = "";
        var _Cidade = "";
        var _UF = "";
        var _CEP = "";
        var _Telefone = "";
    }

    /*- Gets -----------------------------------------------------------------*/
    get IdPessoa() {
        return this._IdPessoa;
    }

    get IdPrenotacao() {
        return this._IdPrenotacao;
    }

    get Selecionado() {
        return this._Selecionado;
    }

    get Nome() {
        return this._Nome;
    }

    get TipoDoc() {
        return this._TipoDoc;
    }

    get NumDoc() {
        return this._NumDoc;
    }

    get Bairro() {
        return this._Bairro;
    }
    get Cidade() {
        return this._Cidade;
    }

    get UF() {
        return this._UF;
    }
    get CEP() {
        return this._CEP;
    }

    get Telefone() {
        return this._Telefone;
    }

    /*- Sets -----------------------------------------------------------------*/
    set IdPessoa(value) {
        this._IdPessoa = value;
    }

    set IdPrenotacao(value) {
        this._IdPrenotacao = value;
    }

    set Selecionado(value) {

        this._Selecionado = value;
    }

    set Nome(value) {
        this._Nome = value;
    }

    set TipoDoc(value) {
        this._TipoDoc = value;
    }

    set NumDoc(value) {
        this._NumDoc = value;
    }

    set Bairro(value) {
        this._Bairro = value;
    }

    set Cidade(value) {
        this._Cidade = value;
    }

    set UF(value) {
        this._UF = value;
    }

    set CEP(value) {
        this._CEP = value;
    }

    set Telefone(value) {
        this._Telefone = value;
    }
}

/*------------------------------------------------------------------------------
 * Funçoes: * javascript da view: Atos.Novo
 *
 * ---------------------------------------------------------------------------*/

/**
 * Pesquisa por prenotação e busca dados do imovel
 * @@param {any} numPrenotacao: numero prenotacao
 */
function PesquisarPrenotacao(numPrenotacao, url) {

    if (!isNaN(numPrenotacao)) {
        var dadosPrenotacao = {
            numPrenotacao: numPrenotacao
        };
        GetDadosImovelPrenotacao(dadosPrenotacao, url);
    } else {
        $.smallBox({
            title: "Entrada inválida!",
            content: "Número de prenotação está inválido!",
            color: "#992111",
            icon: "fa fa-thumbs-down bounce animated",
            timeout: 4000
        });
    }     
}

/**
 * Ajax busca dadso do imóvel por prenotacao
 * @@param dataPreMat
 */
function GetDadosImovelPrenotacao(dadosPrenotacao, url) {

    $.ajax(url, {
        method: 'POST',
        dataType: 'json',
        data: dadosPrenotacao,
        beforeSend: function () {
            ShowProgreessBar("Processando requisição...");
        }
    }).done(function (dataReturn) {
        if (dataReturn.resposta) {

            var dadosInvalidos = (typeof dataReturn.Preimo == 'undefined' || dataReturn.Preimo == null);

            if (!dadosInvalidos) {
                PovoarDadosImovel(dataReturn.Preimo);
                HabilitarProximo();
                $.smallBox({
                    title: "Requisição processada com sucesso!",
                    content: dataReturn.msg,
                    color: "#296191",
                    icon: "fa fa-thumbs-up bounce animated",
                    timeout: 4000
                });
            } else {
                $.smallBox({
                    title: "Dados não encontrados!",
                    content: dataReturn.msg,
                    color: "#EF7F0A",
                    icon: "fa fa-warning bounce animated",
                    timeout: 4000
                });
            }
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
        $.smallBox({
            title: "Falha na sua requisição!",
            content: textStatus + "[" + error + "]",
            color: "#992111",
            icon: "fa fa-thumbs-down bounce animated",
            timeout: 8000
        });
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
        if (dataReturn.resposta) {

            var dadosInvalidos = (typeof dataReturn.listaPessoas == 'undefined' || dataReturn.listaPessoas == null);

            if (!dadosInvalidos) {
                ShowTblPessoasPrenotacao(dataReturn.listaPessoas);
            } else {
                $.smallBox({
                    title: "Dados não encontrados!",
                    content: "Lista inválida.",
                    color: "#EF7F0A",
                    icon: "fa fa-warning bounce animated",
                    timeout: 4000
                });
            }

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
        $.smallBox({
            title: "Falha na sua requisição!",
            content: textStatus + "[" + error + "]",
            color: "#992111",
            icon: "fa fa-thumbs-down bounce animated",
            timeout: 8000
        });

    }).always(function () {
        HideProgressBar();
    });
}

/**
 * 
 * @@param listadados
 */
function ShowTblPessoasPrenotacao(listadados) {

    arrayPessoas = [];

    $("#tbl-selecao-pessoas").find("tr:gt(0)").remove();

    $.each(listadados, function (index, item) {

        var pessoa = new PessoaPrenotacao();

        pessoa.IdPessoa = item.IdPessoa;
        pessoa.IdPrenotacao = item.IdPrenotacao;
        pessoa.Nome = item.Nome;             
        pessoa.Relacao = item.Relacao;     
        pessoa.Endereco = item.Endereco;    
        pessoa.Bairro = item.Bairro;      
        pessoa.Cidade = item.Cidade;      
        pessoa.UF = item.Uf;          
        pessoa.CEP = item.Cep;         
        pessoa.Telefone = item.Telefone;
        pessoa.TipoDoc1 = item.TipoDoc1;
        pessoa.Numero1 = item.Numero1;
        pessoa.TipoDoc2 = item.TipoDoc2;
        pessoa.Numero2 = item.Numero2;
        pessoa.TipoPessoa = item.TipoPessoa;
        arrayPessoas.push(pessoa)

    });

    arrayPessoas.forEach(InserirLinhasSelecaoPessoas);

    $('#div-dlg-pessoas label[id*="lbl-dlg-pessoa-header"]').text("Selecionar outorgante(s)/outorgado(s), Prenotação: " + $("#PREIMO_SEQPRE").val());
    $('#div-dlg-pessoas').modal('show');
}

function InserirLinhasSelecaoPessoas(pessoa, index, array) {

    var doc = pessoa.Numero1.trim();

    if (doc == "") {
        doc = pessoa.Numero2.trim();
    }

    $("#tbl-selecao-pessoas tbody").append(
        '<tr>' +
        '<td>' + '<input type="checkbox" id="chk-selecao-pessoas_' + pessoa.IdPessoa + '" class="checkbox-selecao-pessoas" value="' + pessoa.IdPessoa + '" onclick="MarcarDesmarcarPessoa(this);">' + '</td>' +
        '<td>' + pessoa.TipoPessoa + '</td>' +
        '<td>' + doc + '</td>' +
        '<td>' + pessoa.Nome + '</td>' +
        '</tr>'
    );
}

/**
 * 
 * @@param {any} chkObj
 */
function MarcarDesmarcarPessoa(chkObj) {
    var chkTmp = chkObj;
    var idTmp = chkObj.value;
    var idxTmp = ArrayPessoasIndexOfById(idTmp);

    if (idxTmp >= 0) {
        if (chkObj) {
            arrayPessoas[idxTmp].Selecionado = true;
        } else {
            arrayPessoas[idxTmp].Selecionado = false;
        }
    }
}

function ArrayPessoasIndexOfById(id) {
    var idx = -1;

    for (var i = 0, len = arrayPessoas.length; i < len; i++) {
        if (arrayPessoas[i].IdPessoa == id) {
            idx = i;
            break;
        }
    }

    return idx;
}

/**
 * idPrenotacao (preimo.SEQPRE)
 * */
function PovoarSelecionados(idPrenotacao) {

    $("#tbl-pessoas-selecionadas").find("tr:gt(0)").remove();

    for (var i = 0, len = arrayPessoas.length; i < len; i++) {
        if (arrayPessoas[i].Selecionado) {

            var doc = arrayPessoas[i].Numero1.trim();

            if (doc == "") {
                doc = arrayPessoas[i].Numero2.trim();
            }

            $("#tbl-pessoas-selecionadas tbody").append(
                '<tr>' +
                '<td>' + arrayPessoas[i].TipoPessoa + '</td>' +
                '<td>' + doc + '</td>' +
                '<td>' + arrayPessoas[i].Nome + '</td>' +
                '<td>' + arrayPessoas[i].Bairro + '</td>' +
                '<td>' + arrayPessoas[i].Endereco + '</td>' +
                '<td>' + arrayPessoas[i].Cidade + '</td>' +
                '<td>' + arrayPessoas[i].UF + '</td>' +
                '<td>' + arrayPessoas[i].CEP + '</td>' +
                '<td>' + arrayPessoas[i].Telefone + '</td>' +
                '</tr>'
            );
        }
    }

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

/**
 * Povoar select selModelosDocx
 * @@param {any} selObj
 */
function BuscarListaModelos(IdTipoAto, selObj, url) {

    var dados = {
        IdTipoAto: IdTipoAto
    };

    $.ajax(url, {
        method: 'POST',
        dataType: 'json',
        data: dados,
        beforeSend: function () {
            ShowProgreessBar("Processando requisição...");
        }
    }).done(function (dataReturn) {
        //
        if (dataReturn.resposta) {

            var dadosInvalidos = (typeof dataReturn.ListaModelosDocx == 'undefined' || dataReturn.ListaModelosDocx == null);

            if (!dadosInvalidos) {
                PovoarSelModelos(selObj, dataReturn.ListaModelosDocx);
            } else {
                $.smallBox({
                    title: "Dados não encontrados!",
                    content: "Lista inválida.",
                    color: "#EF7F0A",
                    icon: "fa fa-warning bounce animated",
                    timeout: 4000
                });
            }
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
 * @@param {any} listaModelos
 */
function PovoarSelModelos(selObj, listaModelos) {

    var sel = selObj;
    $(sel).empty();

    $.each(listaModelos, function (index, item) {
        $(sel).append('<option value="' + item.Id + '" >' + item.DescricaoModelo + '</option>');
    });
}
