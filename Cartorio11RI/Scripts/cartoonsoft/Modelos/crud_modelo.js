/*
---------1---------2---------3---------4---------5---------6---------7---------8
01234567890123456789012345678901234567890123456789012345678901234567890123456789
--------------------------------------------------------------------------------
Cartoonsoft: Modelos.*
by Ronaldo Moreira - 2019
------------------------------------------------------------------------------*/

var urlGetListCamposTipoAto = '/Modelos/GetListCamposTipoAto';
var form_para_validar = null;

$(document).ready(function () {

	//$("#ddlTipoAtoModelo").selectpicker({
	//	liveSearch: true,
	//	language: 'pt-br'
	//});

	/*-- validate ----------------------------------------------------------- */
	$("#frm-cadastro-modelo-docx").validate({
		// Rules for form validation
		rules: {
			ddlTipoAtoModelo: {
				required: true
			},
			Descricao: {
				required: true
			}
		},

		// Messages for form validation
		messages: {
			ddlTipoAtoModelo: {
				required: "Selecione um Tipo de ato"
			},
			Descricao: {
				required: "Preencha o campo: Descrição do modelo"
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

	form_para_validar = $("#frm-cadastro-modelo-docx");

	/*-- coloque aqui seu código javascript --------------------------------- */
	CKEDITOR.replace('ckedtModeloAto', {
		width: '100%',
		height: '320px',
		language: 'pt-br',
		uiColor: '#1686E4',
		contentsCss: 'body { font-family: "Times New Roman, Times, serif"; font-size: large;} p { margin: 2px 0px 0px 0px; }',
		font_defaultLabel: 'Times New Roman',
		fontSize_defaultLabel: '14',
		disableNativeSpellChecker: false,
		removePlugins: 'liststyle,tabletools,scayt,menubutton,contextmenu',
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

	$("#frm-cadastro-modelo-docx").submit(function (e) {
		var frm_valid = form_para_validar.valid();
		var txt = CKEDITOR.instances.ckedtModeloAto.getData();

		if (!txt) {
			frm_valid = false;
			ShowDlgBoxCartorio({
				headerText: "Alerta",
				messageText: "Não é possivel salvar um texto de modelo vazio!",
				alertType: "danger"
			});
		}

		if (!frm_valid) {
			e.preventDefault();
		}	

		return frm_valid;
	});

	$("#ddlTipoAtoModelo").change(function (e) {
		//alert(e.target.value);
		var idTipoAto = e.target.value;
		$("#IdTipoAto").val(idTipoAto);
		GetListCamposTipoAto(urlGetListCamposTipoAto, idTipoAto);
	});

	$("#btn-insert-campo-modelo").click(function (e) {
		e.preventDefault();
		var campo = $("#sel-campo-tipo-ato-modelo option:selected").text();
		CKEDITOR.instances.ckedtModeloAto.insertText("[" + campo + "]");
	});

	$("#btn-insert-grupo-modelo").click(function (e) {
		e.preventDefault();
		var campo = $("#sel-grupo-campo-modelo option:selected").val();

		if (campo == 1) {
			CKEDITOR.instances.ckedtModeloAto.insertText("[GrupoOutorgante] coloque seu texto aqui [FimGrupo]");
		} else if (campo == 2) {
			CKEDITOR.instances.ckedtModeloAto.insertText("[GrupoOutorgado] coloque seu texto aqui [FimGrupo]");
		}
	});

});

/** ----------------------------------------------------------------------------
 * 
 * 
 ---------------------------------------------------------------------------- */
function GetListCamposTipoAto(url, IdTipoAto)
{
	var dados = {
		IdTipoAto: IdTipoAto
	};

	$("#sel-campo-tipo-ato-modelo").empty();

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
			if (dataReturn.ListaCamposTipoAto) {
				$.each(dataReturn.ListaCamposTipoAto, function (index, item) {
					$("#sel-campo-tipo-ato-modelo").append('<option value="' + item.Id + '" >' + item.NomeCampo + '</option>');
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
