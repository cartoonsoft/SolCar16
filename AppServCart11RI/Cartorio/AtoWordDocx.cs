using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text.RegularExpressions;
using System.Web;
using AppServices.Cartorio.Interfaces;
using Dto.CartNew.Entities.Cart_11RI;
using GemBox.Document;
using GemboxLib.Base;

namespace AppServCart11RI.Cartorio
{
    public class AtoWordDocx : WordGembox
    {
        private readonly IAppServiceAtos _appServiceAtos;

        /// <summary>
        /// Construtor
        /// </summary>
        /// <param name="layoutPagina">pode ser A4, B4</param>
        public AtoWordDocx(IAppServiceAtos appServiceAtos, long IdCtaAcessoSist) : base(IdCtaAcessoSist)
        {
            this._appServiceAtos = appServiceAtos;
        }

        /// <summary>
        /// Construtor
        /// </summary>
        /// <param name="filePathName">Nome do arquivo</param>
        public AtoWordDocx(IAppServiceAtos appServiceAtos, long IdCtaAcessoSist, string filePathName) : base(IdCtaAcessoSist, filePathName)
        {
            this._appServiceAtos = appServiceAtos;
        }

        #region IDisposable Support
        private bool disposedValue = false; // To detect redundant calls

        protected override void Dispose(bool disposing)
        {
            if (!disposedValue)
            {
                if (disposing)
                {
                    // dispose managed state (managed objects).
                }

                // free unmanaged resources (unmanaged objects) and override a finalizer below.
                // set large fields to null.
                disposedValue = true;
            }

            base.Dispose(disposing);
        }

        // override a finalizer only if Dispose(bool disposing) above has code to free unmanaged resources.
        // ~AppServiceBase() {
        //   // Do not change this code. Put cleanup code in Dispose(bool disposing) above.
        //   Dispose(false);
        // }

        // This code added to correctly implement the disposable pattern.
        public override void Dispose()
        {
            // Do not change this code. Put cleanup code in Dispose(bool disposing) above.
            Dispose(true);
            // uncomment the following line if the finalizer is overridden above.
            // GC.SuppressFinalize(this);
        }
        #endregion

        public List<DtoDocx> GerarDocx(DtoAto Ato, string filePathName)
        {
            List<DtoDocx> listaDocx = new List<DtoDocx>();
            DtoDocx docxTmp = new DtoDocx();
            bool flagText = true;
            string strTmp = string.Empty;
            int j = 0;
            int paginaInicial;
            int paginaAtual;

            /*
            List<DtoDocxList> lista = _appServiceAtos.GetListDtoDocxAto(Ato.NumMatricula).ToList();
            bool ExisteDocxAto = lista.Count > 0;

            if (ExisteDocxAto)
            {
                long maxIdDocx = lista.Max(a => a.IdDocx);

                DtoDocxList Ultimodoc = lista.Where(a => a.IdDocx == maxIdDocx).FirstOrDefault();

                if (this.VerificarSeArquivoExiste(Ultimodoc.NomeArquivo))
                {
                    this.LerDocumento(Ultimodoc.NomeArquivo);
                }
                else
                {
                    throw new IOException(string.Format("Arquivo: {0} não existe no servidor!", filePathName));
                }
            }
            else
            {
                this.LerDocumento(filePathName);
            }

            paginaInicial = this.WordDocument.GetPaginator().Pages.Count;
            paginaAtual = paginaInicial;
                       
            Section sec = ObterSection();
            Paragraph paragraph = this.NovoParagrafo(Ato.Texto, LoadOpitionsDocGemBox.Html);
            sec.Blocks.Add(paragraph);

            //AtoWordHelper.IndexParagrafo = sec.Blocks.IndexOf(paragraph);
            paginaAtual = paragraph.Document.GetPaginator().Pages.Count;

            if (paginaAtual > paginaInicial)
            {
                if (paginaAtual > 2)
                {
                    //AtoDocx.
                }
                else
                {
                    string texto = paragraph.Content.ToString();
                    string[] strWords = GetArryWords(texto);
                    paragraph.Content.Delete();

                    while (flagText)
                    {
                        strTmp = string.Empty;

                        for (int i = j; i < strWords.Length; i++)
                        {
                            strTmp += Regex.Replace(strWords[i], @"\t|\n|\r", "") + " ";
                            j = i + 1;

                            if ((j % 16) == 0)
                            {
                                break;
                            }
                        }

                        Run run = new Run(WordDocument, strTmp);
                        //run.CharacterFormat = new CharacterFormat()
                        //{

                        //};

                        paragraph.Inlines.Add(run);
                        paginaAtual = paragraph.Document.GetPaginator().Pages.Count;

                        if (paginaAtual > paginaInicial)
                        {
                            paragraph.Inlines.Remove(run);
                            paragraph.Inlines.Add(new Run(WordDocument, "<<Quebrou>>"));
                            paragraph.Inlines.Add(new SpecialCharacter(WordDocument, SpecialCharacterType.PageBreak));
                            paragraph.Inlines.Add(run);
                            paginaInicial = paginaAtual;
                        }

                        flagText = (j < strWords.Length);
                    }
                }
            }
            */

            return listaDocx;
        }

        protected override Paragraph NovoParagrafo(string texto, LoadOpitionsDocGemBox loadOpitionsDocGemBox)
        {
            Paragraph paragraph = new Paragraph(WordDocument);
            this.FormatParagraph(paragraph);
            paragraph.Content.LoadText(texto, LoadOptions.HtmlDefault);

            //Run runAnterior = null;
            return paragraph;
        }

        private string[] GetArryWords(string texto)
        {
            DocumentModel doc = new DocumentModel();
            Section sec = new Section(doc);
            doc.Sections.Add(sec);

            Paragraph par = new Paragraph(doc);
            par.Content.LoadText(texto, LoadOptions.HtmlDefault);
            sec.Blocks.Add(par);

            string strTmp = par.Content.ToString();
            string[] strWords = strTmp.Split(' ');

            doc = null;

            return strWords;
        }

        public void Teste()
        {


            /*
            section.HeadersFooters.Add(
                       new HeaderFooter(document, HeaderFooterType.HeaderFirst,
                           new Paragraph(document,
                               new Run(document, "First Header"))));

            // Add page number.
            section.HeadersFooters.Add(
                new HeaderFooter(document, HeaderFooterType.FooterFirst,
                    new Paragraph(document,
                        new Run(document, "First Footer")),
                    new Paragraph(document,
                        new Field(document, FieldType.Page))
                    {
                        ParagraphFormat = new ParagraphFormat() { Alignment = HorizontalAlignment.Right }
                    }));

            section.HeadersFooters.Add(
                new HeaderFooter(document, HeaderFooterType.HeaderDefault,
                    new Paragraph(document,
                        new Run(document, "Default Header"))));
            */

        }

    }

}


