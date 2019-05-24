using Microsoft.Office.Interop.Word;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xceed.Words.NET;

namespace LibFunctions.Functions.Word
{
    public static class WordParagraphHelper
    {
        /// <summary>
        /// Imprime uma linha em branco
        /// </summary>
        /// <param name="doc">Documento ativo</param>
        public static void InserirParagrafoEmBranco(Document doc)
        {
            if (doc == null)
                throw new ArgumentNullException("doc", "Documento não pode ser nulo!");
            else
                doc.Paragraphs.Add();
        }

        public static void InserirParagrafoEmBrancoSpire(Spire.Doc.Section section)
        {
            if (section == null)
                throw new ArgumentNullException("doc", "Documento não pode ser nulo!");
            else
                section.AddParagraph();
        }
        /// <summary>
        /// Insere um novo paragrafo com texto em range
        /// </summary>
        /// <param name="doc">Documento Ativo</param>
        /// <param name="text">Texto para inserção</param>
        public static void InserirParagrafoEmRange(Document doc, string text)
        {
            if (doc == null)
            {
                throw new ArgumentNullException("doc", "Documento não pode ser nulo!");
            }
            else
            {
                if (!String.IsNullOrEmpty(text))
                {
                    doc.Paragraphs.Add().Range.Text = text;
                }
            }
        }
        /// <summary>
        /// Adiciona o texto a partir do range
        /// </summary>
        /// <param name="doc">Documento Ativo</param>
        /// <param name="posicaoInicial">Posição Inicial</param>
        /// <param name="text">Texto</param>
        public static void InserirTextoEmRange(Document doc, int posicaoInicial, string text)
        {
            if (doc == null)
            {
                throw new ArgumentNullException("doc", "Documento não pode ser nulo!");
            }
            else
            {
                if (!String.IsNullOrEmpty(text))
                {
                    doc.Application.ActiveDocument.Range(posicaoInicial).Text = text;
                }
            }
        }
        /// <summary>
        /// Adiciona o texto a partir do range
        /// </summary>
        /// <param name="doc">Documento Ativo</param>
        /// <param name="posicaoInicial">Posição Inicial</param>
        /// <param name="posicaoFinal">Posição Final</param>
        /// <param name="text">Texto</param>
        public static void InserirTextoEmRange(Document doc, int posicaoInicial, int posicaoFinal, string text)
        {
            if (doc == null)
            {
                throw new ArgumentNullException("doc", "Documento não pode ser nulo!");
            }
            else
            {
                if (!String.IsNullOrEmpty(text) && posicaoFinal >= posicaoInicial)
                    doc.Application.ActiveDocument.Range(posicaoInicial, posicaoFinal).Text = text;
                else
                    throw new ArgumentOutOfRangeException("Os campos não estão preenchidos corretamente. (text, posicaoInicial ou posicaoFinal)");
            }
        }
        /// <summary>
        /// Insere um paragrafo, na linha atual ou na próxima
        /// </summary>
        /// <param name="doc">Documento ativo</param>
        /// <param name="text">Texto do parágrafo</param>
        /// <param name="insertAfter">Flag que identifica se o paragrafo é uma continuação</param>
        public static void InserirParagrafo(Document doc, string text, bool insertAfter)
        {
            if (doc == null)
            {
                throw new ArgumentNullException("doc", "Documento não pode ser nulo!");
            }
            else
            {
                if (!String.IsNullOrEmpty(text.Trim()))
                {
                    if (!insertAfter)
                    {
                        doc.Paragraphs.Add().Range.Text = text;
                        return;
                    }
                    doc.Paragraphs.Add().Range.InsertAfter(text);
                }
                else
                {
                    throw new ArgumentOutOfRangeException("O texto não pode ser nulo");
                }
            }
        }

        public static void InserirParagrafoSpire(Spire.Doc.Section section, string text, bool insertAfter)
        {
            if (section == null)
            {
                throw new ArgumentNullException("doc", "Documento não pode ser nulo!");
            }
            else
            {
                if (!String.IsNullOrEmpty(text.Trim()))
                {
                    if (!insertAfter)
                    {
                        section.AddParagraph().Text = text;
                        return;
                    }
                    section.AddParagraph().AppendText(text);
                }
                else
                {
                    throw new ArgumentOutOfRangeException("O texto não pode ser nulo");
                }
            }
        }
        /// <summary>
        /// Método que imprime o rodapé de acordo com o numero da pagina
        /// </summary>
        /// <param name="doc">Documento Ativo</param>
        public static void InserirRodape(Document doc)
        {
            if (doc == null)
            {
                throw new ArgumentNullException("doc", "Documento não pode ser nulo!");
            }
            else
            {
                if (WordPageHelper.IsVerso(WordPageHelper.GetNumeroPagina(doc)))
                    WordParagraphHelper.InserirParagrafo(doc, $"(CONTINUA NA FICHA N°. { WordPageHelper.GetNumeroFicha(doc) + 1})", false);
                else
                    WordParagraphHelper.InserirParagrafo(doc, "(CONTINUA NO VERSO)", false);

                doc.Application.ActiveDocument.Paragraphs.Last.Alignment = WdParagraphAlignment.wdAlignParagraphRight;
            }
        }
        /// <summary>
        /// Reescreve o texto final de página que foi perdido pelo rodapé
        /// </summary>
        /// <param name="doc">Documento Ativo</param>
        /// <param name="posicaoCursor">Posição do cursor</param>
        /// <param name="textoParaSalvar">Texto para salvar</param>
        /// <returns>Posição do cursor após a inserção</returns>
        public static int ReescreverTextoDeFinalDePagina(Document doc, int posicaoCursor, string textoParaSalvar)
        {
            if (doc == null)
            {
                throw new ArgumentNullException("doc", "Documento não pode ser nulo!");
            }

            ////Texto quebrou a linha
            //if (textoParaSalvar.Contains("\r\r"))
            //{
            //    textoParaSalvar += "\r";
            //}

            InserirParagrafo(doc, textoParaSalvar, false);            

            /*Reposiciona o cursor no final do arquivo, pois foram reescrita as ultimas linhas
            devido a inserção de rodapé dinâmica.*/
            posicaoCursor = doc.Application.ActiveDocument.Content.End - 3;
            return posicaoCursor;
        }

        /// <summary>
        /// Configura o alinhamento dos paragrafos
        /// </summary>
        /// <param name="doc">Documento Ativo</param>
        /// <param name="wdParagraphAlignment">Alinhamento do paragrafo</param>
        public static void ParapraphAlignment(Document doc, WdParagraphAlignment wdParagraphAlignment)
        {
            if (doc == null)
            {
                throw new ArgumentNullException("doc", "Documento não pode ser nulo!");
            }
            doc.Paragraphs.Format.Alignment = wdParagraphAlignment;
        }

        public static void ParapraphAlignmentSpire(Spire.Doc.Document doc, Spire.Doc.Documents.HorizontalAlignment alignment)
        {
            if (doc == null)
            {
                throw new ArgumentNullException("doc", "Documento não pode ser nulo!");
            }
            foreach (Spire.Doc.Section section in doc.Sections)
            {
                foreach (Spire.Doc.Documents.Paragraph paragrafo in section.Paragraphs)
                {
                    paragrafo.Format.HorizontalAlignment = alignment;
                }
            }
        }

        /// <summary>
        /// Função que configura o espaçamento entre os paragrafos
        /// </summary>
        /// <param name="doc"></param>
        /// <param name="spaceLenght"></param>
        public static void SpaceAfterParagraphs(Document doc, float spaceLenght)
        {
            if (doc == null)
            {
                throw new ArgumentNullException("doc", "Documento não pode ser nulo!");
            }
            doc.Paragraphs.SpaceAfter = spaceLenght;
        }

        public static void SpaceAfterParagraphsSpire(Spire.Doc.Section section, float spaceLenght)
        {
            if (section == null)
            {
                throw new ArgumentNullException("doc", "Documento não pode ser nulo!");
            }
            foreach (Spire.Doc.Documents.Paragraph paragraph in section.Paragraphs)
            {
                paragraph.Format.AfterSpacing = spaceLenght;
            }
        }
    }
}
