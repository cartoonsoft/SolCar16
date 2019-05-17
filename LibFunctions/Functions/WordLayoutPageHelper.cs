using Dto.Car16.Entities.Cadastros;
using Microsoft.Office.Interop.Word;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LibFunctions.Functions
{
    public static class WordLayoutPageHelper
    {
        /// <summary>
        /// Função que alinha o texto ao shape após inserir o cabeçalho
        /// </summary>
        /// <param name="doc">Documento Ativo</param>
        /// <param name="cabecalhoEscrito">Se o cabecalho não foi escrito, sera incluido um paragrafo em branco a mais</param>
        public static void AlinharAoShape(Document doc, bool cabecalhoEscrito = true)
        {
            if (doc == null)
            {
                throw new ArgumentNullException("doc", "Documento não pode ser nulo!");
            }
            //simular o cabeçalho
            if (!cabecalhoEscrito) WordParagraphHelper.InserirParagrafoEmBranco(doc);

            WordParagraphHelper.InserirParagrafoEmBranco(doc);
            WordParagraphHelper.SpaceAfterParagraphs(doc, 0);
            WordParagraphHelper.InserirParagrafoEmBranco(doc);
        }

        /// <summary>
        /// Ajusta o final do documento com a margem e ajusta páginas obsoletas sem texto
        /// </summary>
        /// <param name="doc">Documento Ativo</param>
        /// <param name="numeroPagina">Numero da página</param>
        /// <param name="modelo">View Model</param>
        /// <param name="posicaoCursor">Posição do cursor</param>
        public static void AjustarFinalDocumento(Document doc, int numeroPagina, int posicaoCursor, DtoCadastroDeAto modelo)
        {
            if (doc == null)
            {
                throw new ArgumentNullException("doc", "Documento não pode ser nulo!");
            }
            //Vai até o final da seção e adiciona linha de separação
            WordSelectionHelper.EndOf(doc, WdUnits.wdSection, WdMovementType.wdMove);
            WordShapeHelper.InserirLinhaHorizontal(doc);

            if (WordPageHelper.GetNumeroPagina(doc) > numeroPagina)
            {
                //Deleta a linha de separação e deleta até sumir a ultima página
                WordShapeHelper.DeleteLastShape(doc);
                while (WordPageHelper.GetNumeroPagina(doc) > numeroPagina)
                    doc.Application.ActiveDocument.Paragraphs.Last.Range.Delete();

                var textoParaSalvar = WordHelper.SelecionaTextoParaSalvar(doc);

                //Insere rodapé e cabeçalho
                WordParagraphHelper.InserirRodape(doc);
                WordHelper.EscreverCabecalhoETexto(modelo, doc, out numeroPagina, out posicaoCursor);

                //Reescreve o texto salvo e insere a linha horizontal
                posicaoCursor = WordParagraphHelper.ReescreverTextoDeFinalDePagina(doc, posicaoCursor, textoParaSalvar);
                WordShapeHelper.InserirLinhaHorizontal(doc);
            }
            else
            {
                WordParagraphHelper.InserirParagrafoEmBranco(doc);
                if (WordPageHelper.GetNumeroPagina(doc) > numeroPagina)
                {
                    //Deleta os ultimos paragráfos e a linha horizontal
                    doc.Application.ActiveDocument.Paragraphs.Last.Range.Delete();
                    WordShapeHelper.DeleteLastShape(doc);

                    //Insere o rodapé
                    WordParagraphHelper.InserirRodape(doc);
                }
                else
                {
                    //Deleta o ultimo paragrafo adicionado para teste
                    doc.Application.ActiveDocument.Paragraphs.Last.Range.Delete();
                }

            }
        }

        /// <summary>
        /// Insere o cabeçalho de acordo com o numero da pagina
        /// </summary>
        /// <param name="modelo"></param>
        /// <param name="doc"></param>
        public static void InserirCabecalho(DtoCadastroDeAto modelo, Document doc, bool houveDesvio = false, bool isParagrafo = true)
        {
            if (doc == null)
            {
                throw new ArgumentNullException("doc", "Documento não pode ser nulo!");
            }
            if (!WordPageHelper.IsVerso(WordPageHelper.GetNumeroPagina(doc)))
            {
                //Insere o paragrafo correspondente a Ato e ficha 
                WordParagraphHelper.InserirParagrafo(doc, new string(' ', 5) + modelo.PREIMO.MATRI + new string(' ', 30 + (15 - modelo.PREIMO.MATRI.ToString().Length)) +
                    WordPageHelper.GetNumeroFicha(doc, isParagrafo) + new string(' ', 5 - WordPageHelper.GetNumeroFicha(doc, isParagrafo).ToString().Length)
                    , !houveDesvio);

                //Posiciona o cursor na ultima pagina e configura a pagina
                WordPageHelper.ConfigureLastPage(doc);

            }
            else
            {
                //Insere o paragrafo correspondente a Ato, ficha e data por extenso
                WordParagraphHelper.InserirParagrafo(doc, new string(' ', 5) + modelo.PREIMO.MATRI + new string(' ', 17 + (15 - modelo.PREIMO.MATRI.ToString().Length)) +
                    WordPageHelper.GetNumeroFicha(doc, isParagrafo) + new string(' ', 18 + (5 - WordPageHelper.GetNumeroFicha(doc, isParagrafo).ToString().Length)) + new string(' ', 14) + DataHelper.GetDataPorExtenso() + "."
                    , !houveDesvio);
                //Posiciona o cursor na ultima pagina e configura a pagina
                WordPageHelper.ConfigureLastPage(doc);

                WordTextStyleHelper.Bold(doc, WordPageHelper.GetRangeEnd(doc, 24), WordPageHelper.GetRangeEnd(doc), true);
            }
            AlinharAoShape(doc);
        }
        /// <summary>
        /// Função que escreve o texto CONTINUAÇÃO DA FICHA N.° {X} , se necessário
        /// </summary>
        /// <param name="doc">Documento Ativo</param>
        public static void InserirContinuacaoFicha(Document doc)
        {
            if (!WordPageHelper.IsVerso(WordPageHelper.GetNumeroPagina(doc)) && WordPageHelper.GetNumeroFicha(doc) > 1)
            {
                doc.Paragraphs.Last.Range.Delete();
                WordParagraphHelper.InserirParagrafo(doc, $"( CONTINUAÇÃO DA FICHA N°. { WordPageHelper.GetNumeroFicha(doc) - 1} )", true);

                doc.Paragraphs.Last.Range.Bold = 1;
                WordParagraphHelper.InserirParagrafoEmBranco(doc);
            }
        }
    }
}
