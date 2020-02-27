/*
---------1---------2---------3---------4---------5---------6---------7---------8
01234567890123456789012345678901234567890123456789012345678901234567890123456789
--------------------------------------------------------------------------------
Cartoonsoft: Atos.*
by Ronaldo Moreira - 2019
------------------------------------------------------------------------------*/

/* flags para o wizard -------------------------------------------------------*/
var PodeAvancar1 = false;
var PodeAvancar2 = false;
var PodeAvancar3 = false;

/* urls chamadas ajax --------------------------------------------------------*/
var urlGetDadosImovel = '/Atos/GetListImoveisPrenotacao';
var urlGetListPessoasPrenotacao = '/Atos/GetListPessoasPrenotacao';
var urlGetLisModelosDocx = '/Atos/GetLisModelosDocx';
var urlProcReservarMatImovel = '/Atos/ProcReservarMatImovel';
var urlGetTextoAto = '/Atos/GetTextoAto';
var urlGetTextoModeloDoc = '/Atos/GetTextoModeloDoc';
var urlInsertOrUpdateAtoAjax = '/Atos/InsertOrUpdateAtoAjax';
var urlGetDadosPrenotacao = '/Atos/GetDadosPrenotacao';
var urlGetListMatriculasPrenotacao = '/Atos/GetListMatriculasPrenotacao';
var urlConfirmarUserLoginSenha = '/Account/ConfirmarUserLoginSenha';
var urlSetStatusAto = '/Atos/SetStatusAto';
var urlSetTextoConferido = '/Atos/SetTextoConferido';
var urlGetListModelosDocx = '/Atos/GetListModelosDocx';

var form_para_validar = null;

/*-----------------------------------------------------------------------------*/
var listaPessoasPrenotacao   = null;
var listaPessoasSelecionadas = null; 

/*----------------------------------------------------------------------------*/
class PessoaPrenotacao {
	constructor() {
		var _IdPessoa = 0;
		var _IdPrenotacao = 0;
		var _TipoPessoa = 0;
		var _DescTipoPessoa = "";
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
		return this._TipoPessoa;
	}

	get DescTipoPessoa() {
		return this._DescTipoPessoa;
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
		this._TipoPessoa = value;
	}

	set Selecionado(value) {

		this._Selecionado = value;
	}

	set DescTipoPessoa(value) {
		this._DescTipoPessoa = value;
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

$(document).ready(function () {

	form_para_validar = $("#frm-cadastro-ato");

	/*-- validate ------------------------------------------------------------*/
	$("#frm-cadastro-ato").validate({
		// Rules
		rules: {
			IdPrenotacao: {
				required: true
			},
			IdCtaAcessoSist: {
				required: true
			},
			IdTipoAto: {
				required: true
			},
			NumMatricula: {
				required: true
			},
			IdModeloDoc: {
				required: true
			},
			IdLivro: {
				required: true
			},
			NumFicha: {
				required: true,
				min: 1,
				number: true
			},
			NumSequenciaAto: {
				required: true,
				min: 1,
				number: true
			},
			FolhaFicha: {
				required: true,
				valueNotEquals: "0"
			},
			DistanciaTopo: {
				required: true,
				min: 1,
				number: true
			},
			DescricaoAto: {
				required: true
			}
		},
		// Messages
		messages: {
			IdPrenotacao: {
				required: "Entre com um número de prenotação"
			},
			IdTipoAto: {
				required: "Selecione um Tipo"
			},
			NumMatricula: {
				required: "Núm. de matrícula inválida"
			},
			IdModeloDoc: {
				required: "Selecione um modelo de documento"
			},
			IdLivro: {
				required: "Selecione um livro"
			},
			NumFicha: {
				required: "Preencha o número da ficha",
				min: "Número não pode ser zero",
				number: "Deve ser valor numérico"
			},
			NumSequenciaAto: {
				required: "Preencha o número de seqência",
				min: "Número não pode ser zero",
				number: "Deve ser valor numérico"
			},
			FolhaFicha: {
				required: "Preencha se é frente ou verso",
				valueNotEquals: "Selecione frente ou verso"
			},
			DistanciaTopo: {
				required: "Preencha o ajuste superior",
				min: "Ajuste não pode ser zero",
				number: "Deve ser um valor numérico"
			},
			DescricaoAto: {
				required: "Digite um descrição para o ato"
			}
		},

		// Do not change code below
		highlight: function (element) {
			$(element).closest('.form-group').removeClass('has-success').addClass('has-error');
		},
		unhighlight: function (element) {
			$(element).closest('.form-group').removeClass('has-error').addClass('has-success');
		},
		errorElement: 'span',
		errorClass: 'help-block',
		errorPlacement: function (error, element) {
			if (element.parent('.input-group').length) {
				error.insertAfter(element.parent());
			} else {
				error.insertAfter(element);
			}
		}
	});

	/*-- ckeditor ckEditorPreviewModelo ------------------------------------- */
	CKEDITOR.replace('ckEditorPreviewModelo', {
		width: '100%',
		height: '290px',
		language: 'pt-br',
		uiColor: '#1686E4',
		contentsCss: 'body { font-family: "Times New Roman, Times, serif";, font-size: 14;}',
		font_defaultLabel: 'Times New Roman',
		fontSize_defaultLabel: '14',
		//startupFocus: true,
		toolbarGroups: [
			{ name: 'document', groups: ['mode', 'document', 'doctools'] },
			{ name: 'clipboard', groups: ['clipboard', 'undo'] },
			{ name: 'editing', groups: ['find', 'selection', 'spellchecker', 'editing'] },
			{ name: 'forms', groups: ['forms'] },
			{ name: 'basicstyles', groups: ['basicstyles', 'cleanup'] },
			{ name: 'paragraph', groups: ['list', 'indent', 'blocks', 'align', 'bidi', 'paragraph'] },
			{ name: 'links', groups: ['links'] },
			{ name: 'insert', groups: ['insert'] },
			{ name: 'styles', groups: ['styles'] },
			{ name: 'colors', groups: ['colors'] },
			{ name: 'tools', groups: ['tools'] },
			{ name: 'others', groups: ['others'] },
			{ name: 'about', groups: ['about'] }
		],
		removeButtons: 'Source,Save,Templates,Cut,Copy,Find,Form,HiddenField,ImageButton,NewPage,Paste,PasteText,PasteFromWord,Replace,Radio,TextField,Scayt,Select,SelectAll,Subscript,Superscript,RemoveFormat,NumberedList,BulletedList,Outdent,Indent,Blockquote,CreateDiv,BidiLtr,Link,Anchor,Language,Flash,Unlink,Image,Smiley,SpecialChar,PageBreak,Iframe,BGColor,About,Button,Checkbox,Textarea,ShowBlocks'
	});

	/*-- ckeditor ckEditorAto ----------------------------------------------- */
	CKEDITOR.replace('ckEditorAto', {
		width: '100%',
		height: '360px',
		language: 'pt-br',
		uiColor: '#1686E4',
		contentsCss: 'body { font-family: "Times New Roman, Times, serif";, font-size: 14;}',
		font_defaultLabel: 'Times New Roman',
		fontSize_defaultLabel: '14',
		disableNativeSpellChecker: false,
		removePlugins: 'liststyle,tabletools,scayt,menubutton,contextmenu',
		startupFocus: true,
		toolbarGroups: [
			{ name: 'document', groups: ['mode', 'document', 'doctools'] },
			{ name: 'clipboard', groups: ['clipboard', 'undo'] },
			{ name: 'editing', groups: ['find', 'selection', 'spellchecker', 'editing'] },
			{ name: 'forms', groups: ['forms'] },
			{ name: 'basicstyles', groups: ['basicstyles', 'cleanup'] },
			{ name: 'paragraph', groups: ['list', 'indent', 'blocks', 'align', 'bidi', 'paragraph'] },
			{ name: 'links', groups: ['links'] },
			{ name: 'insert', groups: ['insert'] },
			{ name: 'styles', groups: ['styles'] },
			{ name: 'colors', groups: ['colors'] },
			{ name: 'tools', groups: ['tools'] },
			{ name: 'others', groups: ['others'] },
			{ name: 'about', groups: ['about'] }
		],
		removeButtons: 'Source,Save,Templates,Cut,Copy,Find,Form,HiddenField,ImageButton,NewPage,Paste,PasteText,PasteFromWord,Replace,Radio,TextField,Scayt,Select,SelectAll,Subscript,Superscript,RemoveFormat,NumberedList,BulletedList,Outdent,Indent,Blockquote,CreateDiv,BidiLtr,Link,Anchor,Language,Flash,Unlink,Image,Smiley,SpecialChar,PageBreak,Iframe,BGColor,About,Button,Checkbox,Textarea,ShowBlocks'
	});

	/*-- btn-ato-show-hide-frm-data-ato ------------------------------------- */
	$("#btn-ato-show-hide-frm-data-ato").click(function (e) {
		e.preventDefault();

		var ele1 = $("#div-ato-form-hist");
		var img = document.getElementById("img-ato-show-hide-frm-data-ato");

		if (img != null) {
			$(img).toggleClass("fa-chevron-down fa-chevron-up", 1000);
		}

		if ($(ele1).is(':visible')) {
			$(ele1).slideUp("slow");
		} else {
			$(ele1).slideDown("slow");
		}
	});

	/*-- btn-ato-show-hide-frm-data-ato ------------------------------------- */
	$("#btn-ato-show-hide-historico").click(function (e) {
		e.preventDefault();

		var btn = $(this);
		var ele1 = $("#div-ato-frm-dados-ato");
		var ele2 = $("#div-historico-ato");

		if ($(ele1).is(':visible')) {

			$(ele1).hide("slow");
			$(ele2).show("slow");
			btn.text("Dados");

		} else {
			$(ele1).show("slow");
			$(ele2).hide("slow");
			btn.text("Histórico");
		}
	});

	/*-- IdModeloDoc -------------------------------------------------------- */
	$("#IdModeloDoc").click(function (e) {
		e.preventDefault();

		var selItem = $("option:selected", this).val();

		if (isNaN(selItem) || !selItem) {
			$.smallBox({
				title: "Valor inválido!",
				content: "Selecine um modelo da lista.",
				color: cor_smallBox_aviso,
				icon: "fa fa-exclamation bounce animated",
				timeout: 4000
			});
		} else {
			var dados = {
				IdModeloDoc: selItem
			}

			$.ajax(urlGetTextoModeloDoc, {
				method: 'POST',
				dataType: 'json',
				data: dados,
				beforeSend: function () {
					ShowProgreessBar("Processando requisição...");
				}
			}).done(function (dataReturn) {
				if (dataReturn.resposta) {
					if (dataReturn.Texto) {
						CKEDITOR.instances.ckEditorPreviewModelo.setData(dataReturn.Texto);
					}
					PodeAvancar3 = true;
				}
			}).fail(function (jq, textStatus, error) {
				alert("Erro: " + textStatus);
			}).always(function () {
				HideProgressBar();
			});
		}
	});

	/*-- gerar o texto do ato ------------------------------------------------*/
	$("#btn-ato-gerar-texto").click(function (e) {
		e.preventDefault();

		//verificar se vc selecionou pesoas
		if (!listaPessoasSelecionadas || (listaPessoasSelecionadas.length < 1)) {
			$.smallBox({
				title: "Não foi possivel processar sua requisição!",
				content: dataReturn.msg,
				color: cor_smallBox_erro,
				icon: "fa fa-thumbs-down bounce animated",
				timeout: 8000
			});
		} else {

			listaPessoasSelecionadas.forEach(item => {
				listaPes.push(item.IdPessoa);
			});

			var listaPes = GetListaPessoasSelecionadas();
			var idAto = $("#Id").val();
			var idLivro = $("#ddListLivro option:selected").val();
			var idModeloDoc = $("#IdModeloDoc").val();
			var idPrenotacao = $("#IdPrenotacao").val();
			var numMatricula = $("#NumMatricula").val();
			var dataRegPrenotacao = new Date($("#DataRegPrenotacao").val());
			var dataAto = new Date($("#DataAto").val());

			var dados = {
				IdAto: idAto,
				IdLivro: idLivro,
				IdModeloDoc: idModeloDoc,
				IdPrenotacao: idPrenotacao,
				NumMatricula: numMatricula,
				DataRegPrenotacao: dataRegPrenotacao,
				DataAto: dataAto,
				ListIdsPessoas: listaPes
			};

			GetTextoAto(dados, urlGetTextoAto);
		}
	});

	/*-- salvar o ato ------------------------------------------------------- */
	$("#btn-ato-salvar").click(function (e) {
		e.preventDefault();

		var frm_valid = form_para_validar.valid();

		listaPessoasSelecionadas.forEach(item => {
			listaPes.push(item.IdPessoa);
		});

		if (frm_valid) {
			var form = $('#frm-cadastro-ato');
			var token = $('input[name="__RequestVerificationToken"]', form).val();
			var dadosAto = {
				__RequestVerificationToken: token,
				Id: $("#Id").val(),
				IdCtaAcessoSist: $("#IdCtaAcessoSist").val(),
				IdLivro: $("#ddListLivro option:selected").val(),
				IdTipoAto: $("#IdTipoAto").val(),
				IdModeloDoc: $("#IdModeloDoc").val(),
				IdPrenotacao: $("#IdPrenotacao").val(),
				IdUsuarioCadastro: $("#IdUsuarioCadastro").val(),
				DataCadastro: $("DataCadastro").val(),
				IdUsuarioAlteracao: $("#IdUsuarioAlteracao").val(),
				NumMatricula: $("#NumMatricula").val(),
				DataAlteracao: $("#DataAlteracao").val(),
				IdsPessoasSelecionadas: GetListaPessoasSelecionadas(),
				NumFicha: $("#NumFicha").val(),
				SiglaSeqAto: $("#SiglaSeqAto").val(),
				NumSequenciaAto: $("#NumSequenciaAto").val(),
				FolhaFicha: $("#FolhaFicha option:selected").val(),
				DistanciaTopo: $("#DistanciaTopo").val(),
				DataAto: $("#DataAto").val(),
				DescricaoAto: $("#DescricaoAto").val(),
				TextoAnterior: $("#TextoAnterior").val(),
				Texto: CKEDITOR.instances['ckEditorAto'].getData(), //$("#Texto").val(),
				Observacao: $("#Observacao").val(),
				StatusAto: $("#StatusAto").val(),
				Salvo: $("#Salvo").val(),
				ConfTexto: $("#ConfTexto").val(),
				ConfDocx: $("#ConfDocx").val(),
				GeradoFicha: $("#GeradoFicha").val(),
				Finalizado: $("#Finalizado").val(),
				Ativo: $("#Ativo").val(),
				IpLocal: $("#IpLocal").val(),
			};

			InsertOrUpdateAtoAjax(dadosAto, urlInsertOrUpdateAtoAjax);
		} else {
			$.smallBox({
				title: "Revise o preenchimento de dados!",
				content: "Preencha todos os campos que sejam obrigatórios.",
				color: cor_smallBox_aviso,
				icon: "fa fa-exclamation bounce animated",
				timeout: 4000
			});
		}
	});

	/*-- btn-ato-conf-texto --------------------------------------------------*/
	$("#btn-ato-conf-texto").click(function (e) {
		e.preventDefault();

		$.SmartMessageBox({
			title: "Confirmar com identificação de usuário",
			content: "Nome usuário",
			buttons: "[Cancelar][Avançar]",
			input: "text",
			inputValue: "",
			placeholder: "Usuário"
		}, function (ButtonPress, Value) {
			if (ButtonPress == "Cancelar") {
				//alert("Why did you cancel that? :(");
				return 0;
			}

			var username = Value;
			var userNameLower = username.toLowerCase(); // toUpperCase();

			$.SmartMessageBox({
				title: "<strong>" + userNameLower + ",</strong>",
				content: "Digite sua senha:",
				buttons: "[Cancelar][Login]",
				input: "password",
				inputValue: "",
				placeholder: "Senha"
			}, function (ButtonPress, Value) {
				if (ButtonPress == "Login") {
					//alert("Usuário: " + username + " pass : " + Value);
					ConfirmarUserLoginSenha(username, Value, 1, SetTextoConferido);
				}
			});
		});
	});

	/*-- btn-ato-colocar-edicao --------------------------------------------- */
	$("#btn-ato-colocar-edicao").click(function (e) {
		e.preventDefault();
		//CKEDITOR.instances['ckEditorAto'].setReadOnly(true);
	});

	/*-- btn-ato-gerar-docx ------------------------------------------------- */
	$("#btn-ato-gerar-docx").click(function (e) {
		e.preventDefault();
		//CKEDITOR.instances['ckEditorAto'].setReadOnly(false);
	});


	/*-- pesquisar pessoas -------------------------------------------------- */
	$("#btn-pesq-pessoas-prenotacao").click(function (e) {
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

	/*-- btn-dlg-pes-pre-ok   ----------------------------------------------- */
	$("#btn-dlg-pes-pre-ok").click(function (e) {
		e.preventDefault();

		PovoartblPessoasSelecionadas();

		if (listaPessoasSelecionadas) {
			PodeAvancar2 = (listaPessoasSelecionadas.length > 0);
		}
			
		if (PodeAvancar2) {
			$('#div-dlg-pessoas-prenotacao').modal('hide');
		}
	});

});

/** ----------------------------------------------------------------------------
 * processar reservar
 * @@param {any} tipoReserva
 * @@param {any} idPrenotacao
 * @@param {any} numMatricula
----------------------------------------------------------------------------- */
function ProcReservarMatImovel(tipoReserva, numPrenotacao, numMatricula, url) {
	var msg_title = "";

	var dadosReserva = {
		TipoReserva: tipoReserva,
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
				if (dataReturn.Reserva.Imovel) {
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
function PovoarSelMatriculasPrenotacao(selObj, listaMatriculas) {
	var sel = selObj;

	if (sel) {
		$(sel).empty();
		$.each(listaMatriculas, function (index, item) {
			$(sel).append('<option value="' + item + '" >' + item + '</option>');
		});
	}
}

/** ----------------------------------------------------------------------------
 * Pesquisa por prenotação e busca a lista de matriculas desta prenotação
 * @@param {any} numPrenotacao
 * @@param {any} selObj select que será povoado com  os núm. de matriculas de imoveis
 * @@param {any} url
----------------------------------------------------------------------------- */
function PesquisarPrenotacao(numPrenotacao, selObj) {
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
function GetDadosPrenotacao(dadosPrenotacao, selObj) {
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
function GetListPessoasPrenotacao(dadosPrenotacao) {
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
				listaPessoasPrenotacao = dataReturn.ListaPessoasPrenotacao;
				ShowDlgPessoasPrenotacao(listaPessoasPrenotacao);
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
 * Confirmar login e senha do usuário
 * @param {any} pUser
 * @param {any} pPass
----------------------------------------------------------------------------- */
function ConfirmarUserLoginSenha(pUsuario, pPass, pAcao, callBack) {
	if (Boolean(pUsuario) && Boolean(pPass)) {

		var dados = {
			usuario: pUsuario,
			pass: pPass
		}

		$.ajax(urlConfirmarUserLoginSenha, {
			method: 'POST',
			dataType: 'json',
			data: dados,
			beforeSend: function () {
				ShowProgreessBar("Processando requisição...");
			}
		}).done(function (dataReturn) {
			if (dataReturn.resposta) {
				if (dataReturn.idUsuario) {
					var idAto = $("#Id").val();

					if (pAcao == 1) {
						callBack(idAto, dataReturn.idUsuario, true);
					}
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
			alert("Retornou erro: " + textStatus);
		}).always(function () {
			HideProgressBar();
		});
	}
}

/** ----------------------------------------------------------------------------
 * SetTextoConferido - altera status do Ato para CT
 * @@param {any} pIdAto
 * @@param {any} pIdUsuario
 * @@param {any} pConferido
 ---------------------------------------------------------------------------- */
function SetTextoConferido(pIdAto, pIdUsuario, pConferido)
{
	//alert("IdAto => " +pIdAto + "    Usuario => " + pIdUsuario);

	if (pIdAto && pIdUsuario) {
		//mudar status ato
		var dados = {
			idAto: pIdAto,
			idUsuario: pIdUsuario,
			conferido: pConferido
		}

		$.ajax(urlSetTextoConferido, {
			method: 'POST',
			dataType: 'json',
			data: dados,
			beforeSend: function () {
				//ShowProgreessBar("Processando requisição...");
			}
		}).done(function (dataReturn) {
			if (dataReturn.resposta) {

				CKEDITOR.instances['ckEditorAto'].setReadOnly(true);

				$.smallBox({
					title: "Texto conferido",
					content: "<i class='fa fa-check-circle'></i><i> " + dataReturn.msg,
					color: cor_smallBox_confima,
					icon: "fa fa-thumbs-up bounce animated",
					timeout: 8000
				});

			} else {
				if (dataReturn.tipoMsg == 5) // error
				{
					$.smallBox({
						title: "Não foi possivel processar sua requisição!",
						content: dataReturn.msg + "<br />" + dataReturn.msgDetalhe,
						color: cor_smallBox_erro,
						icon: "fa fa-thumbs-down bounce animated",
						timeout: 8000
					});
				} else if (dataReturn.tipoMsg == 4) { // warning
					$.smallBox({
						title: "Aviso!",
						content: dataReturn.msg + "<br />" + dataReturn.msgDetalhe,
						color: cor_smallBox_aviso,
						icon: "fa fa-exclamation bounce animated",
						timeout: 8000
					});
				}
			}
		}).fail(function (jq, textStatus, error) {
			alert("Retornou erro: " + textStatus);
		}).always(function () {
			//HideProgressBar();
		});
	}
} 

/**-----------------------------------------------------------------------------
 *
 * @@param btnObj
 * @@param idTipoAto
----------------------------------------------------------------------------- */
function SelecionarTipoAto(btnObj, idTipoAto, SiglaSeqAto)
{
	var btn = btnObj;
	var idTipoAtoTmp = idTipoAto;

	if (typeof btn != 'undefined' || btn != null) {
		var txtTipoAto = btn.innerText.trim();
		$("#IdTipoAto").val(idTipoAto);
		$("#DescricaoTipoAto").val(txtTipoAto);
		$("#SiglaSeqAto").val(SiglaSeqAto);
		$(".btn-tree-tipo-ato").removeClass("btn-danger");
		$(btn).addClass("btn-danger");
		var sel = $("#IdModeloDoc");
		CKEDITOR.instances.ckEditorPreviewModelo.setData("");
		BuscarListaModelos(idTipoAtoTmp, sel, urlGetListModelosDocx);
	}
}

/** ----------------------------------------------------------------------------
 * 
 * @@param {any} dados
 * @@param {any} url
 ---------------------------------------------------------------------------- */
function InsertOrUpdateAtoAjax(dados, url)
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

			if (dataReturn.execute) {

				if (!((typeof dataReturn.execute.Entidade == 'undefined') || (dataReturn.execute.Entidade == null))) {
					//alert("Id ===> " + dataReturn.execute.Entidade.Id);
					$("#Id").val(dataReturn.execute.Entidade.Id);

					$("#IdUsuarioCadastro").val(dataReturn.execute.Entidade.IdUsuarioCadastro);
					$("#DataCadastro").val(dataReturn.execute.Entidade.DataCadastro != null ? ToJavaScriptDate(dataReturn.execute.Entidade.DataCadastro) : "");

					$("#IdUsuarioAlteracao").val(dataReturn.execute.Entidade.IdUsuarioAlteracao);
					$("#DataAlteracao").val(dataReturn.execute.Entidade.DataAlteracao != null ? ToJavaScriptDate(dataReturn.execute.Entidade.DataAlteracao) : "");
					$("#StatusAto").val(dataReturn.execute.Entidade.StatusAto);
					$("#Salvo").prop('checked', true);

					$.smallBox({
						title: "Ato salvo com sucesso!",
						content: dataReturn.msg,
						color: cor_smallBox_ok,
						icon: "fa fa-thumbs-up bounce animated",
						timeout: 4000
					});
				}
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

/** ----------------------------------------------------------------------------
 * BuscarListaModelos
 * @param {any} IdTipoAto
 * @param {any} selObj
 * @param {any} url
----------------------------------------------------------------------------- */
function BuscarListaModelos(IdTipoAto, selObj, url)
{
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
			if (dataReturn.ListaModelosDocx) {
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
 * PovoarSelModelos
 * @@param {any} selObj
 * @@param {any} listaModelos
----------------------------------------------------------------------------- */
function PovoarSelModelos(selObj, listaModelos)
{
	var sel = selObj;
	$(sel).empty();

	$.each(listaModelos, function (index, item) {
		$(sel).append('<option value="' + item.Id + '" >' + item.Descricao + '</option>');
	});
}

/** ----------------------------------------------------------------------------
 * 
 * @@param {any} listaPessoasPrenotacao
----------------------------------------------------------------------------- */
function PovoarTblPessoasPrenotacao(listaPessoas) {
	var doc = "";
	var chkTmp = "";
	var resp = false;
	var bgColorLinha = "";

	$("#tbl-pessoas-prenotacao").find("tr:gt(0)").remove();

	if (listaPessoas) {

		$.each(listaPessoas, function (index, item) {

			doc = item.Numero1.trim();
			if (doc == "") {
				doc = item.Numero2.trim();
			}

			if (item.Valido) {
				chkTmp = '<input type="checkbox" id="chk_pes_pre_' + item.IdPessoa + '" value="' + item.IdPessoa + '" class="chk_cartoon" />';
				doc = '<button type="button" class="btn btn-success btn-xs" title="' + item.RetornoValidacao + '"><i class="fa fa-thumbs-up"></i></button>&nbsp;' + doc;
			} else {
				chkTmp = '<input type="checkbox" id="chk_pes_pre_' + item.IdPessoa + '" value="' + item.IdPessoa + '" class="chk_cartoon" disabled />';
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

		resp = listaPessoas.length > 0;
	}

	return resp;
} 

/** ----------------------------------------------------------------------------
 * 
 * @@param {any} chkObj
----------------------------------------------------------------------------- */
function SelTodosPessoasPrenotacao(chkObj)
{
	if (chkObj) {
		$("#tbl-pessoas-prenotacao tbody").find('input[type=checkbox]').each(function () {
			var chk = chkObj.checked;
			var obj = this;

			if (!obj.disabled) {
				this.checked = chk;
			}
		});
	}
}

/** ----------------------------------------------------------------------------
 * 
 * @@param {any} dadosPrenotacao
 * @@param {any} selObj
 ---------------------------------------------------------------------------- */
function GetListMatriculasPrenotacao(dadosPrenotacao, selObj) {
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
 * GerarTextoAto
 * @@param {any} dadosAto
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
			if (dataReturn.Texto) {
				CKEDITOR.instances.ckEditorAto.setData(dataReturn.Texto);
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
 * 
 * @@param {any} listaPessoasPrenotacao
----------------------------------------------------------------------------- */
function ShowDlgPessoasPrenotacao(listaPessoasPrenotacao)
{
	$("#chk-seltodos-pes-prenotacao").prop('checked', false);

	if (listaPessoasPrenotacao) {
		$('#div-dlg-pessoas-prenotacao label[id*="lbl-dlg-pessoa-header"]').text("Selecionar outorgante(s)/outorgado(s), Prenotação: " + $("#IdPrenotacao").val());

		if (PovoarTblPessoasPrenotacao(listaPessoasPrenotacao)) {
			$('#div-dlg-pessoas-prenotacao').modal({
				keyboard: false,
				focus: true
			});
		}
	}
}

/** ----------------------------------------------------------------------------
 * 
 * @@param {any} idPrenotacao
----------------------------------------------------------------------------- */
function PovoartblPessoasSelecionadas()
{
	var doc = "";
	var resp = false;
	var pessoaTmp = null;
	var htmlAcoes = "";

	listaPessoasSelecionadas = [];
	$("#tbl-pessoas-selecionadas").find("tr:gt(0)").remove();

	$("#tbl-pessoas-prenotacao tbody").find('input[type=checkbox]').each(function () {
		var chk = this;
		pessoaTmp = null;

		if (!chk.disabled && chk.checked)
		{
			var idTmp = chk.value;

			if (listaPessoasPrenotacao) {

				for (var i = 0, len = listaPessoasPrenotacao.length; i < len; i++) {

					if (listaPessoasPrenotacao[i].IdPessoa == idTmp) {
						pessoaTmp = listaPessoasPrenotacao[i];
						break;
					}
				}

				if (pessoaTmp) {

					doc = pessoaTmp.Numero1.trim();
					if (doc == "") {
						doc = pessoaTmp.Numero2.trim();
					}

					if (pessoaTmp.Valido) {
						doc = '<button type="button" class="btn btn-success btn-xs" title="' + pessoaTmp.RetornoValidacao + '"><i class="fa fa-thumbs-up"></i></button>&nbsp;' + doc;
					} else {
						doc = '<button type="button" class="btn btn-danger btn-xs"  title="' + pessoaTmp.RetornoValidacao + '"><i class="fa fa-thumbs-down"></i></button>&nbsp;' + doc;
					}

					htmlAcoes = '<button type="button" class="btn btn-danger btn-xs" title="Retirar esta pessoa da seleção" onclick="RemoverPessoaSel(this, ' + pessoaTmp.IdPessoa + ');"><i class="glyphicon glyphicon-trash"></i></button>';

					$("#tbl-pessoas-selecionadas tbody").append(
						'<tr>' +
						'<td>' + pessoaTmp.IdPessoa + '</td>' +
						'<td>' + pessoaTmp.DescTipoPessoa + '</td>' +
						'<td>' + doc + '</td>' +
						'<td>' + pessoaTmp.Nome + '</td>' +
						'<td>' + pessoaTmp.Endereco + '</td>' +
						'<td>' + pessoaTmp.Bairro + '</td>' +
						'<td>' + pessoaTmp.Cidade + '</td>' +
						'<td>' + pessoaTmp.Uf + '</td>' +
						'<td>' + pessoaTmp.Cep + '</td>' +
						'<td>' + pessoaTmp.Telefone + '</td>' +
						'<td>' + htmlAcoes + '</td>' + 
						'</tr>'
					);

					listaPessoasSelecionadas.push(pessoaTmp);
					resp = true;
				}
			}
		}

	});

	return resp;
}

/** ----------------------------------------------------------------------------
 * Povoar dados do imovel
 * @@param {any} Imovel
 ---------------------------------------------------------------------------- */
function PovoarDadosImovel(Imovel) {
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
function LimparDadosImovel() {
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

/**-----------------------------------------------------------------------------
 * 
----------------------------------------------------------------------------- */
function GetListaPessoasSelecionadas()
{
	var listaPes = [];

	if (listaPessoasSelecionadas) {
		listaPessoasSelecionadas.forEach(item => {
			listaPes.push(item.IdPessoa);
		});
	}

	return listaPes;
}

/** ----------------------------------------------------------------------------
 * 
 * @@param {any} idPessoa
----------------------------------------------------------------------------- */
function RemoverPessoaSel(objBtn, idPessoa)
{
	if (listaPessoasSelecionadas) {
		for (var i = 0, len = listaPessoasSelecionadas.length; i < len; i++) {

			if (listaPessoasSelecionadas[i].IdPessoa == idPessoa) {
				//var pessoaTmp = listaPessoasPrenotacao[i];
				listaPessoasSelecionadas.splice(i, 1);
				$(objBtn).closest("tr").remove();
				break;
			}
		}
		PodeAvancar2 = (listaPessoasSelecionadas.length > 0);
	}
}