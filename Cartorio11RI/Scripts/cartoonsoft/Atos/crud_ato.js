/*
---------1---------2---------3---------4---------5---------6---------7---------8
01234567890123456789012345678901234567890123456789012345678901234567890123456789
--------------------------------------------------------------------------------
Cartoonsoft: Atos.*
by Ronaldo Moreira - 2019
------------------------------------------------------------------------------*/

$(document).ready(function () {

	/* urls chamadas ajax ------------------------------------------------*/
	var urlGetDadosImovel = '@Url.Action("GetListImoveisPrenotacao", "Atos")';
	var urlGetListPessoasPrenotacao = '@Url.Action("GetListPessoasPrenotacao", "Atos")';
	var urlGetLisModelosDocx = '@Url.Action("GetLisModelosDocx", "Atos")';
	var urlProcReservarMatImovel = '@Url.Action("ProcReservarMatImovel", "Atos")';
	var urlGetTextoAto = '@Url.Action("GetTextoAto", "Atos")';
	var urlGetTextoWordDocModelo = '@Url.Action("GetTextoWordDocModelo", "Atos")';
	var urlInsertOrUpdateAtoAjax = '@Url.Action("InsertOrUpdateAtoAjax", "Atos")';
	var urlGetDadosPorPrenotacao = '@Url.Action("GetDadosPorPrenotacao", "Atos")';

	var form_para_validar = $("#frm-cadastro-ato");

	/*-- validate --------------------------------------------------------*/
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

	/*-- ckeditor ckEditorPreviewModelo --------------------------------- */
	/*
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
	*/

	/*-- ckeditor ckEditorAto ------------------------------------------- */
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
		removeButtons: 'Source,Save,Templates,Cut,Find,SelectAll,Scayt,Form,NewPage,Copy,Replace,Radio,PasteText,PasteFromWord,TextField,Select,ImageButton,HiddenField,Subscript,Superscript,RemoveFormat,NumberedList,BulletedList,Outdent,Indent,Blockquote,CreateDiv,BidiLtr,Link,Anchor,Language,Flash,Unlink,Image,Smiley,SpecialChar,PageBreak,Iframe,TextColor,BGColor,About,Button,Checkbox,Textarea,ShowBlocks'
	});

	$("#btn-ato-show-hide-frm-data-ato").click(function (e) {
		e.preventDefault();

		var ele = $("#div-ato-frm-dados-ato");
		var img = document.getElementById("img-ato-show-hide-frm-data-ato");

		if (img != null) {
			$(img).toggleClass("fa-chevron-down fa-chevron-up", 1000);
		}

		if ($(ele).is(':visible')) {
			$(ele).slideUp("slow");
		} else {
			$(ele).slideDown("slow");
		}
	});


	/*-- IdModeloDoc ---------------------------------------------------- */
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

	/*-- gerar o texto do ato --------------------------------------------*/
	$("#btn-ato-gerar-texto").click(function (e) {
		e.preventDefault();

		var listIdsPessoas = [];

		arrayPessoas.forEach(item => {
			if (item.Selecionado) {
				listIdsPessoas.push(item.IdPessoa);
			}
		});

		var idAto = $("#Id").val();
		var idTipoAto = $("#IdTipoAto").val();
		var idLivro = $("#ddListLivro option:selected").val();
		var idModeloDoc = $("#IdModeloDoc").val();
		var idPrenotacao = $("#IdPrenotacao").val();
		var numMatricula = $("#NumMatricula").val();
		var dataRegPrenotacao = new Date($("#DataRegPrenotacao").val());
		var dataAto = new Date($("#DataAto").val());

		var dados = {
			IdAto: idAto,
			IdTipoAto: idTipoAto,
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

	/*-- salvar o ato ----------------------------------------------------*/
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
				DataAlteracao: $("#DataAlteracao").val(),
				IdsPessoasSelecionadas: $("#IdsPessoasSelecionadas").val(),
				NumMatricula: $("#NumMatricula").val(),
				SiglaSeqAto: $("#SiglaSeqAto").val(),
				NumSequenciaAto: $("#NumSequenciaAto").val(),
				DataAto: $("#DataAto").val(),
				DescricaoAto: $("#DescricaoAto").val(),
				DescricaoTipoAto: $("#DescricaoTipoAto").val(),
				Texto: CKEDITOR.instances['ckEditorAto'].getData(), //$("#Texto").val(),
				Observacao: $("#Observacao").val(),
				StatusAto: $("#StatusAto").val(),
				Ativo: $("#Ativo").val(),
				IpLocal: $("#IpLocal").val(),
				NumFicha: $("#NumFicha").val(),
				TextoDistanciaTopo: $("#DistanciaTopo").val(),
				GeradoFicha: $("#GeradoFicha").val(),
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


});