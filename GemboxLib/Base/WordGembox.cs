using System;
using System.Collections.Generic;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using GemBox.Document;

namespace GemboxLib.Base
{
    public class WordGembox: WordGemboxCartoonSoft
    {
        private DocumentModel document = null;
        private readonly long _idCtaAcessoSist = 0;

        /// <summary>
        /// Constructor
        /// </summary>
        public WordGembox(long IdCtaAcessoSist)
        {
            _idCtaAcessoSist = IdCtaAcessoSist;
            this.GetLicense();
            this.InitDoc();
        }

        /// <summary>
        /// Constructor
        /// </summary>
        /// <param name="filePathName">Path e mome do arquivo</param>
        public WordGembox(long IdCtaAcessoSist, string filePathName) : base(filePathName)
        {
            _idCtaAcessoSist = IdCtaAcessoSist;
            this.GetLicense();
            this.InitDoc();
            this.LerDocumento(filePathName);
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

        #region PrivateMethods 
        private void GetLicense()
        {
            ComponentInfo.SetLicense("FREE-LIMITED-KEY");
        }

        private void InitDoc()
        {
            document = new DocumentModel();
            SetDefautFormatsDoc();

        }
        #endregion

        protected long IdCtaAcessoSist
        {
            get { return _idCtaAcessoSist; }
        }

        protected virtual void SetDefautFormatsDoc()
        {
            if (_idCtaAcessoSist == 1)
            {
                document.DefaultCharacterFormat.Size = 14;
                document.DefaultCharacterFormat.FontName = "Times New Roman";
                document.DefaultParagraphFormat.Alignment = HorizontalAlignment.Justify;
            }
        }

        /// <summary>
        /// SetUp da Pagina
        /// </summary>
        /// <param name="section"></param>
        protected virtual void SetUpPage(Section section)
        {
            PageSetup pageSetup = section.PageSetup;

            switch (this.LayoutDocument)
            {
                case LayoutPage.A4:
                    pageSetup.PaperType = PaperType.A4;
                    pageSetup.Orientation = Orientation.Landscape;

                    if (_idCtaAcessoSist == 1)
                    {
                        pageSetup.PageMargins.Top = 57;  //Default value for this property is 70.85 points (0.98 inch).
                        pageSetup.PageMargins.Bottom = 57; //Default value for this property is 70.85 points (0.98 inch).
                        pageSetup.PageMargins.Left = 170; // aprox. 60 mm
                        pageSetup.PageMargins.Right = 34; // aprox. 12 mm
                    }

                    break;
                case LayoutPage.B5:
                    pageSetup.PaperType = PaperType.B5;
                    pageSetup.Orientation = Orientation.Landscape;

                    if (_idCtaAcessoSist == 1)
                    {
                        pageSetup.PageMargins.Top = 57;  //Default value for this property is 70.85 points (0.98 inch).
                        pageSetup.PageMargins.Bottom = 57; //Default value for this property is 70.85 points (0.98 inch).
                        pageSetup.PageMargins.Left = 170; // aprox. 60 mm
                        pageSetup.PageMargins.Right = 34; // aprox. 12 mm
                    }
                    break;
                default:
                    break;
            }
        }

        protected virtual void LerDocumento(string filePathName)
        {
            document = DocumentModel.Load(filePathName);
            this.LayoutDocument = LayoutPage.DefinedByDoc;
        }

        protected virtual Section ObterSection()
        {
            VerificarLayoutPagina();
            Section section = null;  //new Section(document);

            if (this.LayoutDocument == LayoutPage.DefinedByDoc)
            {
                section = document.Sections[0];
            }
            else
            {
                section = new Section(document);
                document.Sections.Add(section);
                SetUpPage(section);
            }

            return section;
        }

        protected virtual void FormatParagraph(Paragraph paragraph)
        {
            paragraph.ParagraphFormat.KeepLinesTogether = true;
            paragraph.ParagraphFormat.Alignment = HorizontalAlignment.Justify;
        }

        protected virtual Paragraph NovoParagrafo(string texto, LoadOpitionsDocGemBox loadOpitionsDocGemBox)
        {
            Section sec = ObterSection();
            Paragraph paragraph = new Paragraph(document);
            this.FormatParagraph(paragraph);

            switch (loadOpitionsDocGemBox)
            {
                case LoadOpitionsDocGemBox.DocxDefaut:
                    break;
                case LoadOpitionsDocGemBox.Html:
                    //verificar primeira pag
                    paragraph.Content.LoadText(texto, LoadOptions.HtmlDefault);
                    sec.Blocks.Add(paragraph);
                    //this.document.Content.End.LoadText("\n");
                    //this.document.Content.End.LoadText(texto, LoadOptions.HtmlDefault);
                    break;
                case LoadOpitionsDocGemBox.Txt:
                    break;
                case LoadOpitionsDocGemBox.None:
                    break;
                default:
                    break;
            }

            //Paragraph paragraph = new Paragraph(doc, textoHtml);
            //this.doc.Content.End.InsertRange(new SpecialCharacter(this.doc, SpecialCharacterType.LineBreak).Content);
            //this.doc.Content.End.InsertRange(new Paragraph(doc, new SpecialCharacter(doc, SpecialCharacterType.LineBreak)).Content);
            //this.doc.Content.End.LoadText(paragraph.Content.ToString(), LoadOptions.HtmlDefault);

            return paragraph;
        }

        public DocumentModel WordDocument
        {
            get { return document; }
        }


        /// <summary>
        /// O método reescreve o ato que está pendente e manda para o original
        /// </summary>
        /// <param name="NumMatricula"></param>
        public virtual void CopiarDocumento(string novofilePathName)
        {
            //antigo: EscreverAtoPrincipal
            if (VerificarSeArquivoExiste(novofilePathName))
            {
                throw new IOException(string.Format("Arquivo de destino: {0} já existe no servidor, não é possível sobreescrever!", novofilePathName));
            }

            try
            {
                document.Save(novofilePathName);
            }
            catch (Exception)
            {
                throw;
            }
        }

    }
}
