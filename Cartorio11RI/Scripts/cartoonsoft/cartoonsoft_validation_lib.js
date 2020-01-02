/*
---------1---------2---------3---------4---------5---------6---------7---------8
01234567890123456789012345678901234567890123456789012345678901234567890123456789
--------------------------------------------------------------------------------
cartoonsoft_validation_lib
by Ronaldo Moreira - 2019
------------------------------------------------------------------------------*/

/**
 * Validar CPF
 * @@param {any} strCPF
 */
function ValidarCPF(strCPF)
{
    var soma;
    var resto;

    soma = 0;
    strCPF = Infra.Filter.somenteNumeros(strCPF);

    if (strCPF == "00000000000")
        return false;

    for (i = 1; i <= 9; i++)
        soma = soma + parseInt(strCPF.substring(i - 1, i)) * (11 - i);
    resto = (soma * 10) % 11;

    if ((resto == 10) || (resto == 11))
        resto = 0;

    if (resto != parseInt(strCPF.substring(9, 10)))
        return false;

    soma = 0;
    for (i = 1; i <= 10; i++)
        soma = soma + parseInt(strCPF.substring(i - 1, i)) * (12 - i);

    resto = (soma * 10) % 11;

    if ((resto == 10) || (resto == 11))
        resto = 0;

    if (resto != parseInt(strCPF.substring(10, 11)))
        return false;

    return true;
}

/**
 * VAlidar CNPJ
 * @param {any} strCNPJ
 */
function ValidarCNPJ(strCNPJ)
{
    var cnpj = Infra.Filter.somenteNumeros(strCNPJ);
    var valida = new Array(6, 5, 4, 3, 2, 9, 8, 7, 6, 5, 4, 3, 2);
    var dig1 = new Number;
    var dig2 = new Number;

    exp = /\.|\-|\//g
    cnpj = cnpj.toString().replace(exp, "");

    var digito = new Number(eval(cnpj.charAt(12) + cnpj.charAt(13)));

    for (i = 0; i < valida.length; i++) {
        dig1 += (i > 0 ? (cnpj.charAt(i - 1) * valida[i]) : 0);
        dig2 += cnpj.charAt(i) * valida[i];
    }

    dig1 = (((dig1 % 11) < 2) ? 0 : (11 - (dig1 % 11)));
    dig2 = (((dig2 % 11) < 2) ? 0 : (11 - (dig2 % 11)));

    if (((dig1 * 10) + dig2) != digito)
        return false;

    return true;
}

/**
 * Validar Email
 * @@param {any} email
 */
function ValidarEmail(email)
{
    var re = /^(([^<>()[\]\\.,;:\s@\"]+(\.[^<>()[\]\\.,;:\s@\"]+)*)|(\".+\"))@((\[[0-9]{1,3}\.[0-9]{1,3}\.[0-9]{1,3}\.[0-9]{1,3}\])|(([a-zA-Z\-0-9]+\.)+[a-zA-Z]{2,}))$/;
    return re.test(email);
}

/**
 * Validar data
 * @@param {any} date
 */
function ValidarData(date)
{
    if (!(/^\d{2}\/\d{2}\/\d{4}$/.test(date)))
        return false;

    // Parse the date parts to integers
    var parts = date.split("/");
    var day = parseInt(parts[0], 10);
    var month = parseInt(parts[1], 10);
    var year = parseInt(parts[2], 10);

    // Check the ranges of month and year
    if (year < 1000 || year > 3000 || month == 0 || month > 12)
        return false;

    var monthLength = [31, 28, 31, 30, 31, 30, 31, 31, 30, 31, 30, 31];

    // Adjust for leap years
    if (year % 400 == 0 || (year % 100 != 0 && year % 4 == 0))
        monthLength[1] = 29;

    // Check the range of the day
    return day > 0 && day <= monthLength[month - 1];
}

/**
 * Validar Telefone
 * @@param {any} telefone
 */
function ValidarTel(telefone)
{
    return (/^\(\d{2}\) \d{4,5}\d?-\d{4}$/.test(telefone));
}

/**
 * Validar CEP
 * @@param {any} cep
 */
function ValidarCEP(cep)
{
    return (/^\d{5}-\d{3}$/.test(cep));
}
