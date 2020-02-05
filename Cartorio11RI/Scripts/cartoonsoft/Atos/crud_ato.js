/*
---------1---------2---------3---------4---------5---------6---------7---------8
01234567890123456789012345678901234567890123456789012345678901234567890123456789
--------------------------------------------------------------------------------
Cartoonsoft: Atos.*
by Ronaldo Moreira - 2019
------------------------------------------------------------------------------*/

/* urls chamadas ajax --------------------------------------------------------*/
var urlGetDadosImovel = '/Atos/GetListImoveisPrenotacao';
var urlGetListPessoasPrenotacao = '/Atos/GetListPessoasPrenotacao';
var urlGetLisModelosDocx = '/Atos/GetLisModelosDocx';
var urlProcReservarMatImovel = '/Atos/ProcReservarMatImovel';
var urlGetTextoAto = '/Atos/GetTextoAto';
var urlGetTextoWordDocModelo = '/Atos/GetTextoWordDocModelo';
var urlInsertOrUpdateAtoAjax = '/Atos/InsertOrUpdateAtoAjax';
var urlGetDadosPorPrenotacao = '/Atos/GetDadosPorPrenotacao';
var urlConfirmarUserLoginSenha = '/Account/ConfirmarUserLoginSenha';
var urlSetStatusAto = '/Atos/SetStatusAto';
var urlSetTextoConferido = '/Atos/SetTextoConferido';

var form_para_validar = null;

$(document).ready(function () {

	form_para_validar = $("#frm-cadastro-ato");

	/*-- validate ------------------------------------------------------------*/
	$("#frm-cadastro-ato").validate({
		// Rules for form validation
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
		// Messages for form validation
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
		height: '200px',
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
		removeButtons: 'Source,Save,Templates,Cut,Undo,Find,SelectAll,Scayt,Form,NewPage,Copy,Paste,PasteText,PasteFromWord,Redo,Replace,Radio,TextField,Select,ImageButton,HiddenField,Subscript,Superscript,RemoveFormat,NumberedList,BulletedList,Outdent,Indent,Blockquote,CreateDiv,JustifyLeft,JustifyCenter,JustifyRight,JustifyBlock,BidiLtr,Link,Anchor,Language,Image,Flash,Unlink,Table,HorizontalRule,Smiley,SpecialChar,PageBreak,Iframe,TextColor,BGColor,About,Button,Checkbox,Textarea,ShowBlocks'
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
		removeButtons: 'Source,Save,Templates,Cut,Find,SelectAll,Scayt,Form,NewPage,Copy,Replace,Radio,PasteText,TextField,Select,ImageButton,HiddenField,Subscript,Superscript,RemoveFormat,NumberedList,BulletedList,Outdent,Indent,Blockquote,CreateDiv,BidiLtr,Link,Anchor,Language,Flash,Unlink,Image,Smiley,SpecialChar,PageBreak,Iframe,BGColor,About,Button,Checkbox,Textarea,ShowBlocks'
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

		if (isNaN(selItem) || (selItem == "")) {
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

			$.ajax(urlGetTextoWordDocModelo, {
				method: 'POST',
				dataType: 'json',
				data: dados,
				beforeSend: function () {
					ShowProgreessBar("Processando requisição...");
				}
			}).done(function (dataReturn) {
				if (dataReturn.resposta) {
					var dadosValidos = !(typeof dataReturn.TextoHtml === 'undefined' || dataReturn.TextoHtml == null);
					if (dadosValidos) {
						CKEDITOR.instances.ckEditorPreviewModelo.setData(dataReturn.TextoHtml);
					}
					PodeAvancar3 = true;
					//$("#editor2").val(dataReturn.TextoHtml);
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

		var listIdsPessoas = [];

		arrayPessoas.forEach(item => {
			if (item.Selecionado) {
				listIdsPessoas.push(item.IdPessoa);
			}
		});

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
			ListIdsPessoas: listIdsPessoas
		};

		GetTextoAto(dados, urlGetTextoAto)
	});

	/*-- salvar o ato ------------------------------------------------------- */
	$("#btn-ato-salvar").click(function (e) {
		e.preventDefault();

		var frm_valid = form_para_validar.valid();

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
				IdsPessoasSelecionadas: $("#IdsPessoasSelecionadas").val(),
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
				StatusAto: $("#StatusAto").val()
			}
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

});

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

/** ----------------------------------------------------------------------------
 * 
 * @@param {any} tipoPesso
----------------------------------------------------------------------------- */
function GetDescTipoPessoaPrenotacao(tipoPessoa)
{
	return (tipoPessoa == 1) ? "Outorgante" : (tipoPessoa == 2) ? "Outorgado" : "Indefinido";
}

/**-----------------------------------------------------------------------------
 *
 * @@param btnObj
 * @@param idTipoAto
----------------------------------------------------------------------------- */
function SelecionarTipoAto(btnObj, idTipoAto, SiglaSeqAto)
{
	var btn = btnObj;
	var idTmp = idTipoAto;

	if (typeof btn != 'undefined' || btn != null) {
		var txtTipoAto = btn.innerText.trim();
		$("#IdTipoAto").val(idTipoAto);
		$("#DescricaoTipoAto").val(txtTipoAto);
		$("#SiglaSeqAto").val(SiglaSeqAto);
		$(".btn-tree-tipo-ato").removeClass("btn-danger");
		$(btn).addClass("btn-danger");
		var sel = $("#IdModeloDoc");
		CKEDITOR.instances.ckEditorPreviewModelo.setData("");
		BuscarListaModelos(idTmp, sel, '/Atos/GetListModelosDocx');
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

			var dadosValidos = !(typeof dataReturn.execute == 'undefined' || dataReturn.execute == null);

			if (dadosValidos) {

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
 * PovoarSelModelos
 * @@param {any} selObj
 * @@param {any} listaModelos
----------------------------------------------------------------------------- */
function PovoarSelModelos(selObj, listaModelos)
{
	var sel = selObj;
	$(sel).empty();

	$.each(listaModelos, function (index, item) {
		$(sel).append('<option value="' + item.Id + '" >' + item.DescricaoModelo + '</option>');
	});
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
