/*
---------1---------2---------3---------4---------5---------6---------7---------8
01234567890123456789012345678901234567890123456789012345678901234567890123456789
--------------------------------------------------------------------------------
Cartoonsoft jquery libs
by Ronaldo Moreira - 2019
------------------------------------------------------------------------------*/

$(document).ready(function () {

	/* obter ip local ----------------------------------------------------*/
	var ipLocal = getUserIP(function (ip) {
		var t = navigator.product;
		$("#IpLocal").val(ip); //vc deve criar um input hide com id = IpLocal
	});


	$.validator.addMethod("valueNotEquals", function (value, element, arg) {
		return arg !== value;
	}, "Value must not equal arg.");

	/*-- somente-numero-sem-decimal ------------------------------------- */
	$(".somente-numero-sem-decimal").on("keypress keyup blur", function (event) {
		$(this).val($(this).val().replace(/[^\d].+/, ""));
		if ((event.which < 48 || event.which > 57)) {
			event.preventDefault();
		}
	});


});