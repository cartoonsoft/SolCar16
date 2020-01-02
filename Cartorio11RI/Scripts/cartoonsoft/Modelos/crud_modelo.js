/*
---------1---------2---------3---------4---------5---------6---------7---------8
01234567890123456789012345678901234567890123456789012345678901234567890123456789
--------------------------------------------------------------------------------
Cartoonsoft: Modelos.*
by Ronaldo Moreira - 2019
------------------------------------------------------------------------------*/

$(document).ready(function () {

	/*-- tree view ---------------------------------------------------------- */
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

	/*-- validate ----------------------------------------------------------- */
	var frm_cadastro_modelo_docx = $("#frm-cadastro-modelo-docx").validate({
		// Rules for form validation
		rules: {
			DescricaoTipoAto: {
				required: true
			},
			DescricaoModelo: {
				required: true
			},
			Files: {
				RequiredHttpPostedFileBase: true,
				IsWordFile: true
			}
		},

		// Messages for form validation
		messages: {
			DescricaoTipoAto: {
				required: "Selecione tipo de ato"
			},
			DescricaoModelo: {
				required: "Digite um nome para o modelo"
			},

			Files: {
				RequiredHttpPostedFileBase: "è necessário anexar uma arquivo Docx",
				IsWordFile: "Arquivo tem que ser do tipo Docx"
			}
		},

		// Do not change code below
		errorPlacement: function (error, element) {
			error.insertAfter(element.parent());
		}
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
function SelecionarTipoAto(btnObj, idTipoAto) {
	var btn = btnObj;
	var idTmp = idTipoAto;

	if (typeof btn !== 'undefined' || btn !== null) {
		var txtTipoAto = btn.innerText.trim();
		$("#IdTipoAto").val(idTipoAto);
		$("#DescricaoTipoAto").val(txtTipoAto);

		$(".btn-tree-tipo-ato").removeClass("btn-danger");
		$(btn).addClass("btn-danger");
	}
}

