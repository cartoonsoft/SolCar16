using Microsoft.Office.Interop.Word;
using Spire.Doc.Documents;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xceed.Words.NET;

namespace LibFunctions.Functions.Word
{
    public static class WordPageHelper
    {
        /// <summary>
        /// Função para deslocar o cursor até o numero de ficha desejada, se esta no verso ou não
        /// </summary>
        /// <param name="doc">Documento ativo</param>
        /// <param name="numeroFicha">Numero da ficha que deseja que o cursor esteja</param>
        /// <param name="isVerso">No verso da ficha? (true or false)</param>
        public static void DeslocarAte(Document doc, int numeroFicha, bool isVerso)
        {
            while (GetNumeroFicha(doc) < numeroFicha || IsVerso(GetNumeroPagina(doc)) != isVerso)
            {
                WordParagraphHelper.InserirParagrafoEmBranco(doc);
                WordParagraphHelper.SpaceAfterParagraphs(doc, 0);
            }
        }

        /// <summary>
        /// Configura a ultima página e alinha o texto como justificado
        /// </summary>
        /// <param name="doc"></param>
        public static void ConfigureLastPage(Document doc)
        {
            if (doc == null)
            {
                throw new ArgumentNullException("doc", "Documento não pode ser nulo!");
            }
            WordSelectionHelper.Goto(doc, WdGoToItem.wdGoToPage, WdGoToDirection.wdGoToNext, WordPageHelper.GetNumeroPagina(doc, 1));
            ConfigurePageLayout(doc, GetNumeroPagina(doc));
            doc.Paragraphs.Last.Alignment = WdParagraphAlignment.wdAlignParagraphJustify;
        }

        /// <summary>
        /// Pega o numero da página do documento ativo
        /// </summary>
        /// <param name="doc">Documento ativo</param>
        /// <param name="backOFF">Quantidade de recuos</param>
        /// <returns>Numero da pagina (INT) </returns>
        public static int GetNumeroPagina(Document doc, int backOFF = 0)
        {
            if (doc == null)
            {
                throw new ArgumentNullException("doc", "Documento não pode ser nulo!");
            }
            return doc.Application.Selection.Information[WdInformation.wdNumberOfPagesInDocument] - backOFF;
        }

        public static int GetNumeroPaginaSpire(Spire.Doc.Document doc, int backOFF = 0)
        {
            if (doc == null)
            {
                throw new ArgumentNullException("doc", "Documento não pode ser nulo!");
            }
            return doc.PageCount - backOFF;
        }


        /// <summary>
        /// Pega o range final do documento ativo
        /// </summary>
        /// <param name="doc">Documento ativo</param>
        /// <param name="backOFF">Posições que recuam do range final do documento (recuar N posições)</param>
        /// <returns>Posição do range final do documento</returns>
        public static int GetRangeEnd(Document doc, int backOFF = 0)
        {
            if (doc == null)
            {
                throw new ArgumentNullException("doc", "Documento não pode ser nulo!");
            }
            return doc.Application.ActiveDocument.Range().End - backOFF;
        }
        /// <summary>
        /// Pega a posição final do content do Documento
        /// </summary>
        /// <param name="doc">Documento ativo</param>
        /// <returns>Posição final do content</returns>
        public static int GetContentEnd(Document doc, int backOFF = 0)
        {
            if (doc == null)
            {
                throw new ArgumentNullException("doc", "Documento não pode ser nulo!");
            }
            return doc.Application.ActiveDocument.Content.End - backOFF;
        }

        /// <summary>
        /// Verifica se é o verso da ficha de acordo com o numero da página
        /// </summary>
        /// <param name="numeroPagina">Numero da pagina</param>
        /// <returns>Se é o verso da pagina (TRUE) se a frente (FALSE)</returns>
        public static bool IsVerso(int numeroPagina)
        {

            return numeroPagina % 2 == 0;
        }
        /// <summary>
        /// Pega o numero da ficha de acordo com o numero da pagina
        /// </summary>
        /// <param name="doc">Documento ativo</param>
        /// <returns>Retorna o numero da ficha para a pagina corrente</returns>
        public static int GetNumeroFicha(Document doc, bool isParagrafo = false)
        {
            if (doc == null)
            {
                throw new ArgumentNullException("doc", "Documento não pode ser nulo!");
            }
            return isParagrafo ?
                    ((WordPageHelper.GetNumeroPagina(doc) < 2 ? WordPageHelper.GetNumeroPagina(doc) : WordPageHelper.GetNumeroPagina(doc) + 1) % 2 + (WordPageHelper.GetNumeroPagina(doc) < 2 ? WordPageHelper.GetNumeroPagina(doc) : WordPageHelper.GetNumeroPagina(doc) + 1) / 2)
                :
                    WordPageHelper.GetNumeroPagina(doc) % 2 + WordPageHelper.GetNumeroPagina(doc) / 2;

        }

        public static int GetNumeroFichaSpire(Spire.Doc.Document doc, bool isParagrafo = false)
        {
            if (doc == null)
            {
                throw new ArgumentNullException("doc", "Documento não pode ser nulo!");
            }
            return isParagrafo ?
                    ((WordPageHelper.GetNumeroPaginaSpire(doc) < 2 ? WordPageHelper.GetNumeroPaginaSpire(doc) : WordPageHelper.GetNumeroPaginaSpire(doc) + 1) % 2 + (WordPageHelper.GetNumeroPaginaSpire(doc) < 2 ? WordPageHelper.GetNumeroPaginaSpire(doc) : WordPageHelper.GetNumeroPaginaSpire(doc) + 1) / 2)
                :
                    WordPageHelper.GetNumeroPaginaSpire(doc) % 2 + WordPageHelper.GetNumeroPaginaSpire(doc) / 2;

        }

        /// <summary>
        /// Desloca uma quantidade de centimetros em relação ao SHAPE
        /// </summary>
        /// <param name="centimetros">Centimetros</param>
        /// <param name="doc">Documento Ativo</param>
        public static void DeslocarCentimetros(Document doc, float centimetros)
        {
            if (doc == null)
            {
                throw new ArgumentNullException("doc", "Documento não pode ser nulo!");
            }
            //Cada paragrafo desloca aproximadamente 0.85 centrimetros
            int quantidadeDeEspacos = (int)Math.Ceiling(centimetros / 0.85);
            while (quantidadeDeEspacos > 0)
            {
                WordParagraphHelper.InserirParagrafoEmBranco(doc);
                quantidadeDeEspacos--;
            }
            WordParagraphHelper.SpaceAfterParagraphs(doc, 0);
        }
        /// <summary>
        /// Função responsável por configurar as margens de acordo com a página
        /// </summary>
        /// <param name="doc">Documento Ativo</param>
        /// <param name="numeroPagina">Numero da página</param>
        public static void ConfigurePageLayout(Document doc, int numeroPagina)
        {
            if (doc == null)
            {
                throw new ArgumentNullException("doc", "Documento não pode ser nulo!");
            }
            if (numeroPagina > 0)
            {
                if (numeroPagina > 1)
                    InsertBreakOfSection(doc);
                doc.Application.ActiveDocument.Sections.Last.PageSetup.BottomMargin = 35;

                if (WordPageHelper.IsVerso(numeroPagina))
                {
                    doc.Application.ActiveDocument.Sections.Last.PageSetup.LeftMargin = 36.35f;
                    doc.Application.ActiveDocument.Sections.Last.PageSetup.RightMargin = 62.3f;

                    return;
                }
                doc.Application.ActiveDocument.Sections.Last.PageSetup.LeftMargin = 62.3f;
                doc.Application.ActiveDocument.Sections.Last.PageSetup.RightMargin = 36.5f;
            }
        }

        public static void ConfigurePageLayoutSpire(int numeroPagina, Spire.Doc.Section section)
        {
            if (section == null)
            {
                throw new ArgumentNullException("doc", "Sessão não pode ser nula!");
            }
            if (numeroPagina > 0)
            {
                if (numeroPagina > 1)
                    InsertBreakOfSectionSpire(section);
                section.PageSetup.Margins.Bottom = 40;
                section.PageSetup.Margins.Top = 25;
                if (WordPageHelper.IsVerso(numeroPagina))
                {
                    section.PageSetup.Margins.Left = 36.35f;
                    section.PageSetup.Margins.Right = 62.3f;

                    return;
                }
                section.PageSetup.Margins.Left = 62.3f;
                section.PageSetup.Margins.Right = 36.0f;
            }
        }


        /// <summary>
        /// Quebra a seção
        /// </summary>
        /// <param name="doc">Documento Ativo</param>
        public static void InsertBreakOfSection(Document doc)
        {
            if (doc == null)
            {
                throw new ArgumentNullException("doc", "Documento não pode ser nulo!");
            }
            doc.Application.Selection.InsertBreak(WdBreakType.wdSectionBreakNextPage);
        }

        public static void InsertBreakOfSectionSpire(Spire.Doc.Section section)
        {
            if (section == null)
            {
                throw new ArgumentNullException("doc", "Sessão não pode ser nula!");
            }
            section.Paragraphs[section.Paragraphs.Count].AppendBreak(BreakType.PageBreak);
        }
        /// <summary>
        /// Configura a pagina do word da seção ativa
        /// </summary>
        /// <param name="doc">Documento Ativo</param>
        /// <param name="wdPaperSize">Tamanho do papel</param>
        /// <param name="fontSize">Tamanho da letra</param>
        /// <param name="fontName">Nome da fonte</param>
        /// <param name="autoHyphenation">Deseja auto hifenização? (True of False)</param>
        public static void InicialConfiguration(Document doc, WdPaperSize wdPaperSize, float fontSize, string fontName, bool autoHyphenation)
        {
            if (doc == null)
            {
                throw new ArgumentNullException("doc", "Documento não pode ser nulo!");
            }
            doc.PageSetup.PaperSize = wdPaperSize;
            doc.Application.ActiveDocument.AutoHyphenation = autoHyphenation;
            doc.Application.Selection.Font.Size = fontSize;
            doc.Application.Selection.Font.Name = fontName;
        }
        public static void InicialConfigurationSpire(Spire.Doc.Document doc,Spire.Doc.Section section, string fontName, bool autoHyphenation)
        {
            if (doc == null)
            {
                throw new ArgumentNullException("doc", "Documento não pode ser nulo!");
            }
            section.PageSetup.PageSize = new System.Drawing.SizeF(510,720); 
            //doc.Application.ActiveDocument.AutoHyphenation = autoHyphenation;
            foreach (Spire.Doc.Documents.Paragraph paragrafo in section.Paragraphs)
            {
                AplicarEstilo(doc, fontName, paragrafo);
            }
        }

        private static void AplicarEstilo(Spire.Doc.Document doc, string fontName, Spire.Doc.Documents.Paragraph paragrafo)
        {
            ParagraphStyle style = new ParagraphStyle(doc);
            style.Name = fontName;
            style.CharacterFormat.FontSize = 14;
            style.CharacterFormat.FontName = "Times New Roman";
            doc.Styles.Add(style);
            paragrafo.ApplyStyle(style.Name);
        }

        public static void InicialConfigurationDocX(DocX doc, WdPaperSize wdPaperSize, float fontSize, string fontName, bool autoHyphenation)
        {
            if (doc == null)
            {
                throw new ArgumentNullException("doc", "Documento não pode ser nulo!");
            }

        }
    }
}
