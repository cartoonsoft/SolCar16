function ResetarCampos() {
    var limpaDados = "{\"SEQIMO\":0,\"SEQPRE\":0,\"SUBD\":0,\"TIPO\":\"\",\"TITULO\":\"\",\"ENDER\":\"\",\"NUM\":\"\",\"LOTE\":\"\",\"QUADRA\":\"\",\"APTO\":\"\",\"BLOCO\":\"\",\"EDIF\":\"\",\"VAGA\":\"\",\"OUTROS\":\"\",\"MATRI\":\"\",\"TRANS\":0,\"INSCR\":0,\"HIPO\":0,\"RD\":0,\"CONTRIB\":\"\"}";
    var resultado = JSON.parse(limpaDados);
    LimparTabelaPessoas();

    //COLOCA O RESULTADO NOS CAMPOS
    $('#numeroPre').text(resultado.SEQPRE);
    $('#PREIMO_MATRI').val(resultado.MATRI);
    $('#PREIMO_SEQPRE').val(resultado.SEQPRE);
    $('#PREIMO_ENDER').val(resultado.ENDER);
    $('#PREIMO_NUM').val(resultado.NUM);
    $('#PREIMO_APTO').val(resultado.APTO);
    $('#PREIMO_BLOCO').val(resultado.BLOCO);
    $('#PREIMO_QUADRA').val(resultado.QUADRA);
    $('#PREIMO_EDIF').val(resultado.EDIF);
    $('#PREIMO_OUTROS').val(resultado.OUTROS);
    $('#PREIMO_LOTE').val(resultado.LOTE);

    DesabilitarProximo();
}


function LimparDados() {
    var limpaDados = "{\"SEQIMO\":0,\"SEQPRE\":0,\"SUBD\":0,\"TIPO\":\"\",\"TITULO\":\"\",\"ENDER\":\"\",\"NUM\":\"\",\"LOTE\":\"\",\"QUADRA\":\"\",\"APTO\":\"\",\"BLOCO\":\"\",\"EDIF\":\"\",\"VAGA\":\"\",\"OUTROS\":\"\",\"MATRI\":\"\",\"TRANS\":0,\"INSCR\":0,\"HIPO\":0,\"RD\":0,\"CONTRIB\":\"\"}";
    var resultado = JSON.parse(limpaDados);
    enviarDadosMatricula(resultado);
    LimparTabelaPessoas();
    DesabilitarProximo();
}

function LimparTabelaPessoas() {
    var tabela = $('#tblPessoas > tbody > tr');
    for (var i = 0; i < tabela.length; i++) {
        tabela[i].remove();
    }
}