using Dto.Car16.Entities.Cadastros;
using Microsoft.Office.Interop.Word;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LibFunctions.Functions
{
    public static class WordHelper
    {
        /// <summary>
        /// O método reescreve o ato que está pendente e manda para o original
        /// </summary>
        /// <param name="NumMatricula"></param>
        public static void EscreverAtoPrincipal(string filePath, string novoFilePath)
        {
            try
            {
                Application app = new Application();
                Document doc = app.Documents.Open(filePath);
                doc.SaveAs(novoFilePath);
                doc.Close();
                doc = null;
                app = null;
                GC.Collect();
            }
            catch (Exception)
            {
                throw;
            }
        }
        /// <summary>
        /// Seleciona texto para salvar e deleta para inserir o rodapé
        /// </summary>
        /// <param name="doc">Documento ativo</param>
        /// <returns>texto para salvar (string) </returns>
        public static string SelecionaTextoParaSalvar(Document doc)
        {
            if (doc == null)
            {
                throw new ArgumentNullException("doc", "Documento não pode ser nulo!");
            }
            //Vai para a ultima página, volta uma linha e seleciona até o final
            WordSelectionHelper.Goto(doc, WdGoToItem.wdGoToPage, WdGoToDirection.wdGoToNext, WordPageHelper.GetNumeroPagina(doc, 1));
            WordSelectionHelper.EndOf(doc, WdUnits.wdSection, WdMovementType.wdMove);
            WordSelectionHelper.Goto(doc, WdGoToItem.wdGoToLine, WdGoToDirection.wdGoToPrevious, 1);
            WordSelectionHelper.EndOf(doc, WdUnits.wdParagraph, WdMovementType.wdExtend);
            var textoParaSalvar = WordSelectionHelper.GetSelectionText(doc);
            WordSelectionHelper.DeleteSelectionText(doc);
            WordParagraphHelper.InserirParagrafoEmBranco(doc);
            WordSelectionHelper.EndOf(doc, WdUnits.wdParagraph, WdMovementType.wdExtend);
            textoParaSalvar += WordSelectionHelper.GetSelectionText(doc);
            WordSelectionHelper.DeleteSelectionText(doc);

            return textoParaSalvar;
        }

        /// <summary>
        /// Função que escreve no documento quando ocorre mudança de pagina
        /// </summary>
        /// <param name="modelo">View Model</param>
        /// <param name="doc">Documento Ativo</param>
        /// <param name="numeroPagina">Numero da pagina (Atualizada por ref) </param>
        /// <param name="textoParaSalvar">Texto para escrever</param>
        /// <returns>A posição do cursor para continuar a escrita do documento</returns>
        public static int EscreverNoDocumento(DtoCadastroDeAto modelo, Document doc, ref int numeroPagina, string textoParaSalvar)
        {
            if (doc == null)
            {
                throw new ArgumentNullException("doc", "Documento não pode ser nulo!");
            }
            //Insere paragrafos em branco até chegar a proxima página
            while (WordPageHelper.GetNumeroPagina(doc) <= numeroPagina)
            {
                WordParagraphHelper.InserirParagrafoEmBranco(doc);
            }
            //Deleta um paragrafos para posicionar corretamente o cursor em relação a borda inferior da página
            doc.Application.ActiveDocument.Paragraphs.Last.Range.Delete();

            //Insere o texto de rodapé
            WordParagraphHelper.InserirRodape(doc);

            //Escrever o texto depois do rodapé
            EscreverCabecalhoETexto(modelo, doc, out numeroPagina, out int posicaoCursor);

            //Reescreve o texto que foi perdido pelo rodapé e retorna a posição do cursor atualizada
            posicaoCursor = WordParagraphHelper.ReescreverTextoDeFinalDePagina(doc, posicaoCursor, textoParaSalvar);
            return posicaoCursor;
        }
        /// <summary>
        /// Escreve o cabeçalho de acordo com a pagina,ficha e verso. Além do texto modelo
        /// </summary>
        /// <param name="modelo">View Model</param>
        /// <param name="doc">Documento ativo</param>
        /// <param name="numeroPagina">Numero da pagina</param>
        /// <param name="posicaoCursor">Posição do cursor</param>
        public static void EscreverCabecalhoETexto(DtoCadastroDeAto modelo, Document doc, out int numeroPagina, out int posicaoCursor)
        {
            if (doc == null) throw new ArgumentNullException("doc", "Documento não pode ser nulo");

            if (!WordPageHelper.IsVerso(WordPageHelper.GetNumeroPagina(doc)))
            {
                //Posiciona o cursor na ultima pagina e ajusta o paragrafo
                WordSelectionHelper.Goto(doc, WdGoToItem.wdGoToPage, WdGoToDirection.wdGoToNext, WordPageHelper.GetNumeroPagina(doc, 1));
                WordParagraphHelper.SpaceAfterParagraphs(doc, 0);
            }
            //Insere o cabeçalho
            WordLayoutPageHelper.InserirCabecalho(modelo, doc);

            //Se for continuação de alguma ficha
            WordLayoutPageHelper.InserirContinuacaoFicha(doc);
            doc.Paragraphs.Last.Range.Bold = 0;

            posicaoCursor = WordPageHelper.GetContentEnd(doc, 1);
            numeroPagina = WordPageHelper.GetNumeroPagina(doc);
        }

        /// <summary>
        /// Função que escreve o ato no documento
        /// </summary>
        /// <param name="modelo">View model que contem o ato</param>
        /// <param name="doc">Documento ativo</param>
        /// <param name="numeroPagina">Numero da pagina inicial</param>
        /// <param name="posicaoCursor">Posição do cursor</param>
        public static void EscreverAto(DtoCadastroDeAto modelo, Document doc, ref int numeroPagina, ref int posicaoCursor, bool houveDesvio = false)
        {
            if (doc == null) throw new ArgumentNullException("doc", "Documento não pode ser nulo");
            if (string.IsNullOrEmpty(modelo.Ato)) throw new ArgumentNullException("modelo", "O ato do modelo não pode ser nulo");

            for (int i = 0; i < modelo.Ato.Length; i++)
            {
                if (WordPageHelper.GetNumeroPagina(doc) > numeroPagina)
                {
                    //Selecionando texto para salvar
                    string textoParaSalvar = string.Empty;
                    if (!houveDesvio) textoParaSalvar = SelecionaTextoParaSalvar(doc);

                    //Escreve no documento o texto para salvar
                    posicaoCursor = EscreverNoDocumento(modelo, doc, ref numeroPagina, textoParaSalvar);

                    //Quando ocorre quebra de página ele acaba pulando uma letra
                    i--;
                }
                else
                {
                    WordParagraphHelper.InserirTextoEmRange(doc, posicaoCursor++, modelo.Ato[i].ToString());
                }
            }
        }

        

        /// <summary>
        /// A função desvia uma certa quantidade de centimetros após a margem, além de atualizar as variáveis (numero da pagina e posição do cursor)
        /// </summary>
        /// <param name="doc">Documento Ativo</param>
        /// <param name="modelo">View Model</param>
        /// <param name="quantidadeCentrimetros">Quantidade de centimetros</param>
        public static void DesviarCentimetros(Document doc, DtoCadastroDeAto modelo, string sigla,long numSequenciaAto, float quantidadeCentrimetros, ref int numeroPagina, ref int posicaoCursor, bool houveDesvioDeFicha = false)
        {
            if (quantidadeCentrimetros == 0)
            {
                //Insere o cabeçalho
                WordLayoutPageHelper.InserirCabecalho(modelo, doc, !houveDesvioDeFicha, houveDesvioDeFicha);
                WordParagraphHelper.InserirParagrafoEmBranco(doc);

                //Se for continuação de alguma ficha
                WordLayoutPageHelper.InserirContinuacaoFicha(doc);

                doc.Paragraphs.Last.Range.Bold = 0;
            }
            else
            {
                WordLayoutPageHelper.AlinharAoShape(doc, false);


                //centimetros apos a borda da margem
                WordPageHelper.DeslocarCentimetros(doc, quantidadeCentrimetros);
                WordParagraphHelper.InserirParagrafo(doc, $"{sigla}-{numSequenciaAto}/{modelo.PREIMO.MATRI} - ", true);
            }
            //atualiza as variaveis por ponteiro (REF)
            numeroPagina = WordPageHelper.GetNumeroPagina(doc);
            posicaoCursor = WordPageHelper.GetContentEnd(doc, 1);

        }

    }
}
