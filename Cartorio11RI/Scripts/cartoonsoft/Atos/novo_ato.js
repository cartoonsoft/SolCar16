/*
---------1---------2---------3---------4---------5---------6---------7---------8
01234567890123456789012345678901234567890123456789012345678901234567890123456789
--------------------------------------------------------------------------------
Cartoonsoft: Atos.Novo 
by Ronaldo Moreira - 2019
------------------------------------------------------------------------------*/

var PodeAvancar1 = false;
var PodeAvancar2 = false;
var PodeAvancar3 = false;

/*----------------------------------------------------------------------------*/
var arrayPessoas = [];

/*----------------------------------------------------------------------------*/
class PessoaPrenotacao {

    constructor() {
        var _IdPessoa = 0;
        var _IdPrenotacao = 0;
        var _TipoPessoa = 0;
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

    get TipoPessoa() {
        return this._TipoPessoa; /* 1- Outorgante 2-Outorgado*/
    }

    get Nome() {
        return this._Nome;
    }

    get Endereco() {
        return this._Endereco;
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

    set TipoPessoa(value) {
        this._TipoPessoa = value; /* 1- Outorgante 2-Outorgado*/
    }

    set Selecionado(value) {

        this._Selecionado = value;
    }

    set Nome(value) {
        this._Nome = value;
    }

    set Endereco(value) {
        this._Endereco = value;
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

/** ----------------------------------------------------------------------------
 * Pesquisa por prenotação e busca dados do imovel
 * @@param {any} numPrenotacao
 * @@param {any} selObj select que será povoado se retornar matriculas de imoveis
 * @@param {any} url
----------------------------------------------------------------------------- */
function PesquisarPrenotacao(numPrenotacao, selObj, url) {

    if (!isNaN(numPrenotacao)) {
        var dadosPrenotacao = {
            IdPrenotacao: numPrenotacao
        };
        GetDadosPorPrenotacao(dadosPrenotacao, selObj, url);
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
function GetDadosPorPrenotacao(dadosPrenotacao, selObj, url) {

    $('#btn-reserva-mat').prop('disabled', true);
    $('#btn-libera-mat').prop('disabled', true);

    $.ajax(url, {
        method: 'POST',
        dataType: 'json',
        data: dadosPrenotacao,
        beforeSend: function () {
            ShowProgreessBar("Processando requisição...");
        }
    }).done(function (dataReturn) {
        if (dataReturn.resposta) {

            var dadosValidos = !(typeof dataReturn.listaDtoDadosImovel == 'undefined' || dataReturn.listaDtoDadosImovel == null);
            $("#DataRegPrenotacao").val(dataReturn.DataRegPrenotacao);

            if (dadosValidos) {
                PovoarSelImoveis(selObj, dataReturn.listaDtoDadosImovel);
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
 * Ajax busca dados dos imoveis por prenotacao
 * @@param {any} dadosPrenotacao
 * @@param {any} selObj
 * @@param {any} url
 -----------------------------------------------------------------------------*/
function GetListImoveisPrenotacao(dadosPrenotacao, selObj, url) {

    $('#btn-reserva-mat').prop('disabled', true);
    $('#btn-libera-mat').prop('disabled', true);

    $.ajax(url, {
        method: 'POST',
        dataType: 'json',
        data: dadosPrenotacao,
        beforeSend: function () {
            ShowProgreessBar("Processando requisição...");
        }
    }).done(function (dataReturn) {
        if (dataReturn.resposta) {

            var dadosValidos = !(typeof dataReturn.listaDtoDadosImovel == 'undefined' || dataReturn.listaDtoDadosImovel == null);

            if (dadosValidos) {
                PovoarSelImoveis(selObj, dataReturn.listaDtoDadosImovel);
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
 * Busca lista pessoa por prenotação
 * @@param {any} dadosPrenotacao
 * @@param {any} url
 -----------------------------------------------------------------------------*/
function GetListPessoasPrenotacao(dadosPrenotacao, url) {

    $.ajax(url, {
        method: 'POST',
        dataType: 'json',
        data: dadosPrenotacao,
        beforeSend: function () {
            ShowProgreessBar("Processando requisição...");
        }
    }).done(function (dataReturn) {
        if (dataReturn.resposta) {

            var dadosValidos = !(typeof dataReturn.listaPessoas == 'undefined' || dataReturn.listaPessoas == null);

            if (dadosValidos) {
                ShowTblPessoasPrenotacao(dataReturn.listaPessoas);
            } else {
                $.smallBox({
                    title: "Dados não encontrados!",
                    content: "Lista inválida.",
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
 * ShowTblPessoasPrenotacao
 * @@param listadados
 -----------------------------------------------------------------------------*/
function ShowTblPessoasPrenotacao(listadados) {

    arrayPessoas = [];

    $("#tbl-selecao-pessoas").find("tr:gt(0)").remove();

    $.each(listadados, function (index, item) {

        var pessoa = new PessoaPrenotacao();

        pessoa.IdPessoa = item.IdPessoa;
        pessoa.IdPrenotacao = item.IdPrenotacao;
        pessoa.TipoPessoa = item.TipoPessoa;
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
        arrayPessoas.push(pessoa)

    });

    arrayPessoas.forEach(InserirLinhasSelecaoPessoas);

    $('#div-dlg-pessoas label[id*="lbl-dlg-pessoa-header"]').text("Selecionar outorgante(s)/outorgado(s), Prenotação: " + $("#IdPrenotacao").val());
    $('#div-dlg-pessoas').modal('show');
}

/** ----------------------------------------------------------------------------
 * 
 * @@param {any} tipoPesso
----------------------------------------------------------------------------- */
function GetDescTipoPessoaPrenotacao(tipoPessoa) {
    return (tipoPessoa == 1) ? "Outorgante" : (tipoPessoa  == 2) ? "Outorgado" : "Indefinido";
}

/** ----------------------------------------------------------------------------
 * 
 * @@param {any} pessoa
 * @@param {any} index
 * @@param {any} array
----------------------------------------------------------------------------- */
function InserirLinhasSelecaoPessoas(pessoa, index, array) {

    var doc = pessoa.Numero1.trim();

    if (doc == "") {
        doc = pessoa.Numero2.trim();
    }

    $("#tbl-selecao-pessoas tbody").append(
        '<tr>' +
        '<td>' + '<input type="checkbox" id="chk-selecao-pessoas_' + pessoa.IdPessoa + '" class="checkbox-selecao-pessoas" value="' + pessoa.IdPessoa + '" onclick="MarcarDesmarcarPessoa(this);">' + '</td>' +
        '<td>' + GetDescTipoPessoaPrenotacao(pessoa.TipoPessoa) + '</td>' +
        '<td>' + doc + '</td>' +
        '<td>' + pessoa.Nome + '</td>' +
        '<td>' + pessoa.Endereco + '</td>' +
        '</tr>'
    );
}

/** ----------------------------------------------------------------------------
 * 
 * @@param {any} chkObj
----------------------------------------------------------------------------- */
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

/** ----------------------------------------------------------------------------
 * 
 * @@param {any} id
----------------------------------------------------------------------------- */
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

/** ----------------------------------------------------------------------------
 * idPrenotacao (IdPrenotacao)
 ---------------------------------------------------------------------------- */
function PovoarSelecionados(numPrenotacao) {

    var povoou = false;
    var IdsSel = ""; 

    $("#tbl-pessoas-selecionadas").find("tr:gt(0)").remove();

    for (var i = 0, len = arrayPessoas.length; i < len; i++) {
        if (arrayPessoas[i].Selecionado) {

            var doc = arrayPessoas[i].Numero1.trim();

            if (doc == "") {
                doc = arrayPessoas[i].Numero2.trim();
            }

            IdsSel += arrayPessoas[i].IdPessoa.toString() +";";
            
            $("#tbl-pessoas-selecionadas tbody").append(
                '<tr>' +
                '<td>' + GetDescTipoPessoaPrenotacao(arrayPessoas[i].TipoPessoa) + '</td>' +
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

            povoou = true;
        }
    }

    $("#IdsPessoasSelecionadas").val(IdsSel.substring(0, IdsSel.length -1));
    $('#div-dlg-pessoas').modal('hide');

    return povoou;
}

function DesabilitarProximo() {
    $('#btn-proximo').attr('disabled', 'disabled');
    $('#btn-proximo').removeClass('active');
}

function HabilitarProximo() {
    $('#btn-proximo').removeAttr('disabled');
    $('#btn-proximo').addClass('active');
}

/** ----------------------------------------------------------------------------
 * Povoar select selModelosDocx
 * @param {any} IdTipoAto
 * @param {any} selObj
 * @param {any} url
----------------------------------------------------------------------------- */
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

            var dadosValidos = !(typeof dataReturn.ListaModelosDocx == 'undefined' || dataReturn.ListaModelosDocx == null);

            if (dadosValidos) {
                PovoarSelModelos(selObj, dataReturn.ListaModelosDocx);
            } else {
                $.smallBox({
                    title: "Dados não encontrados!",
                    content: "Lista inválida.",
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
        //

    }).always(function () {
        HideProgressBar();
    });
}

/** ----------------------------------------------------------------------------
 * 
 * @@param {any} selObj
 * @@param {any} listaModelos
----------------------------------------------------------------------------- */
function PovoarSelImoveis(selObj, listaImoveis) {

    var sel = selObj;
    $(sel).empty();

    $.each(listaImoveis, function (index, item) {

        $(sel).append('<option value="' + item.NumMatricula + '" >' + item.NumMatricula + '</option>');
    });
}

/** ----------------------------------------------------------------------------
 * 
 * @@param {any} selObj
 * @@param {any} listaModelos
----------------------------------------------------------------------------- */
function PovoarSelModelos(selObj, listaModelos) {

    var sel = selObj;
    $(sel).empty();

    $.each(listaModelos, function (index, item) {
        $(sel).append('<option value="' + item.Id + '" >' + item.DescricaoModelo + '</option>');
    });
}

/** ----------------------------------------------------------------------------
 * GerarTextoAto
 * @@param {any} arrayPessoas
 * @@param {any} url
----------------------------------------------------------------------------- */
function GetTextoAto(dadosAto, url) {

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

/**
 * Povoar dados do imovel
 * @@param {any} Imovel
 */
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

/**
 * Povoar dados do imovel
 * @@param {any} Imovel
 */
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

/**
 * *
 * @@param {any} dados
 * @@param {any} url
 */
function InsertOrUpdateAto(dados, url)
{
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

            var dadosValidos = !(typeof dataReturn.execute == 'undefined' || dataReturn.execute == null);

            if (dadosValidos) {
                $.smallBox({
                    title: msg_title,
                    content: dataReturn.msg,
                    color: cor_smallBox_ok,
                    icon: "fa fa-thumbs-up bounce animated",
                    timeout: 4000
                });
            } else {
                $.smallBox({
                    title: "Dados não foram salvos!",
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
        //

    }).always(function () {
        HideProgressBar();
    });

}



