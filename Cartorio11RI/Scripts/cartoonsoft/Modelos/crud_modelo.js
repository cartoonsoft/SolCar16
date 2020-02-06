/*
---------1---------2---------3---------4---------5---------6---------7---------8
01234567890123456789012345678901234567890123456789012345678901234567890123456789
--------------------------------------------------------------------------------
Cartoonsoft: Modelos.*
by Ronaldo Moreira - 2019
------------------------------------------------------------------------------*/

var form_para_validar = null;

$(document).ready(function () {

	$("#sel-tipo-ato").selectpicker({
		liveSearch: true,
		language: 'pt-br'
	});

	/*-- coloque aqui seu código javascript --------------------------------- */
	CKEDITOR.replace('ckeditor-modelo-ato', {
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

	/*-- validate ----------------------------------------------------------- */
	var form_para_validar = $("#frm-cadastro-modelo-docx").validate({
		// Rules for form validation
		rules: {
			DescricaoTipoAto: {
				required: true
			},
			DescricaoModelo: {
				required: true
			}
		},

		// Messages for form validation
		messages: {
			DescricaoTipoAto: {
				required: "Selecione tipo de ato"
			},
			DescricaoModelo: {
				required: "Digite um nome para o modelo"
			}
		},

		// Do not change code below
		errorPlacement: function (error, element) {
			error.insertAfter(element.parent());
		}
	});

	$("#frm-cadastro-modelo-docx").submit(function (e) {
		e.preventDefault();
		var frm_valid = form_para_validar.valid();

		return frm_valid;
	});

	var ControllerModelValid = ("@ViewBag.ControllerModelValid" == "true");
	var success = ("@ViewBag.success" == "true");
	var msg = "@ViewBag.msg";
	var divMsg = $("#articleMesssagensCrud");

	if (ControllerModelValid) {
		if (success) {
			ShowMessageCrud(divMsg, "Modelo salvo com sucesso", msg, true);
		} else {
			ShowMessageCrud(divMsg, "Não foi possível salvar", msg, false);
		}
	}

});

/**
 * /
 * @@param btnObj
 * @@param idTipoAto
 */
function SelecionarTipoAto(btnObj, idTipoAto)
{
	var btn = btnObj;
	var idTmp = idTipoAto;

	if (typeof btn !== 'undefined' || btn !== null) {
		var txtTipoAto = btn.innerText.trim();
		$("#IdTipoAto").val(idTipoAto);
	}
}

