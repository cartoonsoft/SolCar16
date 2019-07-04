using System;
using System.Collections.Generic;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Dto.Cartorio.Entities.Cadastros;
using GemBox.Document;

namespace LibFunctions.Functions.Word
{
    public class WordHelperDocument: IDisposable
    {
        private readonly DocumentModel _doc = null;

        /// <summary>
        /// Constructor
        /// </summary>
        public WordHelperDocument()
        {
            _doc = new DocumentModel();
            SetUpPage();

        }

        /// <summary>
        /// Constructor
        /// </summary>
        /// <param name="filePath">Path e mome do arquivo</param>
        public WordHelperDocument(string filePath)
        {
            if (!VerificarSeArquivoExiste(filePath))
            {
                throw new IOException(string.Format("Documento : {0} não encontrado no servidor!", filePath));
            }

            _doc = DocumentModel.Load(filePath);
        }

        #region IDisposable Support
        private bool disposedValue = false; // To detect redundant calls

        protected virtual void Dispose(bool disposing)
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
        }

        // override a finalizer only if Dispose(bool disposing) above has code to free unmanaged resources.
        // ~AppServiceBase() {
        //   // Do not change this code. Put cleanup code in Dispose(bool disposing) above.
        //   Dispose(false);
        // }

        // This code added to correctly implement the disposable pattern.
        public void Dispose()
        {
            // Do not change this code. Put cleanup code in Dispose(bool disposing) above.
            Dispose(true);
            // uncomment the following line if the finalizer is overridden above.
            // GC.SuppressFinalize(this);
        }
        #endregion

        /// <summary>
        /// SetUp da Pagina
        /// </summary>
        /// <param name="section"></param>
        private void SetUpPage()
        {
            var section = new Section(_doc);
            PageSetup pageSetup = section.PageSetup;
            pageSetup.PaperType = PaperType.B5;
            pageSetup.Orientation = Orientation.Landscape;
            pageSetup.PageMargins.Top = 57;  //Default value for this property is 70.85 points (0.98 inch).
            pageSetup.PageMargins.Bottom = 57; //Default value for this property is 70.85 points (0.98 inch).
            pageSetup.PageMargins.Left = 170; // aprox. 60 mm
            pageSetup.PageMargins.Right = 34; // aprox. 12 mm

            _doc.Sections.Add(section);
        }

        private bool VerificarSeArquivoExiste(string filePath)
        {
            bool existe = File.Exists(filePath);
            return existe;
        }

        private void ValidarDoc()
        {
            if (_doc == null)
            {
                throw new ArgumentNullException("doc", "Documento não pode ser nulo!");
            }

        }

        public DocumentModel WordDocument
        {
            get { return _doc; }
        }

        /// <summary>
        /// O método reescreve o ato que está pendente e manda para o original
        /// </summary>
        /// <param name="NumMatricula"></param>
        public void CopiarDocumento( string novoFilePath)
        {
            //antigo: EscreverAtoPrincipal
            if (VerificarSeArquivoExiste(novoFilePath))
            {
                throw new IOException(string.Format("Arquivo de destino: {0} já existe no servidor, não é possível sobreescrever!", novoFilePath));
            }

            try
            {
                _doc.Save(novoFilePath);
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
        public string SelecionaTextoParaSalvar(DocumentModel doc)
        {
            if (doc == null)
            {
                throw new ArgumentNullException("doc", "Documento não pode ser nulo!");
            }
            //Vai para a ultima página, volta uma linha e seleciona até o final
            //WordSelectionHelper.Goto(doc, WdGoToItem.wdGoToPage, WdGoToDirection.wdGoToNext, WordPageHelper.GetNumeroPagina(doc, 1));
            //WordSelectionHelper.EndOf(doc, WdUnits.wdSection, WdMovementType.wdMove);
            //WordSelectionHelper.Goto(doc, WdGoToItem.wdGoToLine, WdGoToDirection.wdGoToPrevious, 1);
            //WordSelectionHelper.EndOf(doc, WdUnits.wdParagraph, WdMovementType.wdExtend);
            //var textoParaSalvar = WordSelectionHelper.GetSelectionText(doc);
            //WordSelectionHelper.DeleteSelectionText(doc);
            //WordParagraphHelper.InserirParagrafoEmBranco(doc);
            //WordSelectionHelper.EndOf(doc, WdUnits.wdParagraph, WdMovementType.wdExtend);
            //textoParaSalvar += WordSelectionHelper.GetSelectionText(doc);
            //WordSelectionHelper.DeleteSelectionText(doc);

            return "";
        }

        /// <summary>
        /// Função que escreve no documento quando ocorre mudança de pagina
        /// </summary>
        /// <param name="modelo">View Model</param>
        /// <param name="doc">Documento Ativo</param>
        /// <param name="numeroPagina">Numero da pagina (Atualizada por ref) </param>
        /// <param name="textoParaSalvar">Texto para escrever</param>
        /// <returns>A posição do cursor para continuar a escrita do documento</returns>
        public int EscreverNoDocumento(DtoCadastroDeAto modelo, string textoParaSalvar)
        {
            ValidarDoc();

            int numberOfSections = _doc.Sections.Count;
            int numberOfParagraphs = _doc.GetChildElements(true, ElementType.Paragraph).Count();
            int numberOfRunsAndFields = _doc.GetChildElements(true, ElementType.Run, ElementType.Field).Count();
            int numberOfInlines = _doc.GetChildElements(true).OfType<Inline>().Count();


            StringBuilder sb = new StringBuilder();

            foreach (Paragraph paragraph in _doc.GetChildElements(true, ElementType.Paragraph))
            {
                foreach (Run run in paragraph.GetChildElements(true, ElementType.Run))
                {
                    bool isBold = run.CharacterFormat.Bold;
                    string text = run.Text;

                    sb.AppendFormat("{0}{1}{2}", isBold ? "<b>" : "", text, isBold ? "</b>" : "");
                }
                sb.AppendLine();
            }

            Console.WriteLine(sb.ToString());


            //Section sec = (Section)doc.GetChildElements(true, ElementType.Section).First();


            Paragraph paragraph1 = new Paragraph(_doc, Enumerable.Repeat("_", 80).ToString());
            Paragraph paragraph2 = new Paragraph(_doc, textoParaSalvar );

            _doc.Sections[0].Blocks.Add(paragraph1);
            _doc.Sections[0].Blocks.Add(paragraph2);

             

            //Insere o texto de rodapé
            //WordParagraphHelper.InserirRodape(doc);

            //Escrever o texto depois do rodapé
            //EscreverCabecalhoETexto(modelo, doc, out numeroPagina, out int posicaoCursor);

            //Reescreve o texto que foi perdido pelo rodapé e retorna a posição do cursor atualizada
            //posicaoCursor = WordParagraphHelper.ReescreverTextoDeFinalDePagina(doc, posicaoCursor, textoParaSalvar);
            return 0;
        }

        /// <summary>
        /// Escreve o cabeçalho de acordo com a pagina,ficha e verso. Além do texto modelo
        /// </summary>
        /// <param name="modelo">View Model</param>
        /// <param name="doc">Documento ativo</param>
        /// <param name="numeroPagina">Numero da pagina</param>
        /// <param name="posicaoCursor">Posição do cursor</param>
        public void EscreverCabecalhoETexto(DtoCadastroDeAto modelo, int numeroPagina, int posicaoCursor)
        {
            //if (doc == null) throw new ArgumentNullException("doc", "Documento não pode ser nulo");

            //if (!WordPageHelper.IsVerso(WordPageHelper.GetNumeroPagina(doc)))
            //{
            //    //Posiciona o cursor na ultima pagina e ajusta o paragrafo
            //    WordSelectionHelper.Goto(doc, WdGoToItem.wdGoToPage, WdGoToDirection.wdGoToNext, WordPageHelper.GetNumeroPagina(doc, 1));
            //    WordParagraphHelper.SpaceAfterParagraphs(doc, 0);
            //}
            ////Insere o cabeçalho
            //WordLayoutPageHelper.InserirCabecalho(modelo, doc);

            ////Se for continuação de alguma ficha
            //WordLayoutPageHelper.InserirContinuacaoFicha(doc);
            //doc.Paragraphs.Last.Range.Bold = 0;

            //posicaoCursor = WordPageHelper.GetContentEnd(doc, 1);
            //numeroPagina = WordPageHelper.GetNumeroPagina(doc);
        }

        /// <summary>
        /// Função que escreve o ato no documento
        /// </summary>
        /// <param name="modelo">View model que contem o ato</param>
        /// <param name="doc">Documento ativo</param>
        /// <param name="numeroPagina">Numero da pagina inicial</param>
        /// <param name="posicaoCursor">Posição do cursor</param>
        public void EscreverAto(DtoCadastroDeAto modelo, int numeroPagina, int posicaoCursor, bool houveDesvio = false)
        {
            ValidarDoc();

            if (string.IsNullOrEmpty(modelo.Ato))
            {
                throw new ArgumentNullException("modelo", "O ato do modelo não pode ser nulo");
            };

            //for (int i = 0; i < modelo.Ato.Length; i++)
            //{
            //    if (WordPageHelper.GetNumeroPagina(doc) > numeroPagina)
            //    {
            //        //Selecionando texto para salvar
            //        string textoParaSalvar = string.Empty;
            //        textoParaSalvar = SelecionaTextoParaSalvar(doc);

            //        //Escreve no documento o texto para salvar
            //        posicaoCursor = EscreverNoDocumento(modelo, doc, ref numeroPagina, textoParaSalvar);

            //        //Quando ocorre quebra de página ele acaba pulando uma letra
            //        i--;
            //    }
            //    else
            //    {
            //        WordParagraphHelper.InserirTextoEmRange(doc, posicaoCursor++, modelo.Ato[i].ToString());
            //    }
            //}
        }
    }
}
