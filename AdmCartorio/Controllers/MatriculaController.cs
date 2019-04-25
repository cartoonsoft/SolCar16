using AdmCartorio.Controllers.Base;
using AdmCartorio.Models;
using AdmCartorio.ViewModels;
using AppServices.Car16.AppServices;
using AutoMapper;
using Domain.Car16.Interfaces.UnitOfWork;
using Dto.Car16.Entities.Cadastros;
using Dto.Car16.Entities.Diversos;
using HtmlAgilityPack;
using Microsoft.Office.Core;
using Microsoft.Office.Interop.Word;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Net;
using System.Text;
using System.Web.Mvc;
using Xceed.Words.NET;
using Shapes = Microsoft.Office.Interop.Word.Shapes;

namespace AdmCartorio.Controllers
{
    [Authorize]
    public class MatriculaController : AdmCartorioBaseController
    {
        #region |Construtores e dispose|
        public MatriculaController() : base(null, null)
        {
            //
        }
        public MatriculaController(IUnitOfWorkDataBaseCar16 unitOfWorkDataBaseCar16, IUnitOfWorkDataBaseCar16New unitOfWorkDataBseCar16New) : base(unitOfWorkDataBaseCar16, unitOfWorkDataBseCar16New)
        {
            //Criar instancia dos seus App services aqui
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                //coloque aqui os app servics para dar dispose
            }
            base.Dispose(disposing);
        }

        #endregion

        // GET: Matricula
        public ActionResult Index()
        {
            var dados = new MatriculaAtoViewModel();

            using (var appService = new AppServiceArquivoModeloDocx(this.UnitOfWorkDataBseCar16New))
            {
                IEnumerable<DtoArquivoModeloSimplificadoDocxList> ListaModeloSimplificado = appService.ListarArquivoModeloSimplificado();
                dados.ModelosSimplificadoViewModel = Mapper.Map<IEnumerable<DtoArquivoModeloSimplificadoDocxList>, IEnumerable<ArquivoModeloSimplificadoViewModel>>(ListaModeloSimplificado);
                dados.MatriculasViewModel = getMatriculaViewModel();
            }

            //var dados = new MatriculaAtoViewModel()
            //{
            //    MatriculasViewModel = getMatriculaViewModel(),
            //    ModelosSimplificadoViewModel = getModeloSimplificadoViewModel()
            //};
            return View(dados);
        }

        private static List<ArquivoModeloSimplificadoViewModel> getModeloSimplificadoViewModel()
        {
            return new List<ArquivoModeloSimplificadoViewModel>()
                {
                    new ArquivoModeloSimplificadoViewModel()
                    {
                        Id = 1,
                        DescricaoTipoAto = "Ato Inicial",
                        NomeModelo = "TesteModelo"
                    },
                    new ArquivoModeloSimplificadoViewModel()
                    {
                        Id = 2,
                        DescricaoTipoAto = "Registro",
                        NomeModelo = "testeWord"
                    }
                };
        }
        
        private static List<MatriculaViewModel> getMatriculaViewModel()
        {
            return new List<MatriculaViewModel>()
                {
                    new MatriculaViewModel()
                    {
                        EnderecoImovel = "Endereço 1",
                        MatriculaId = 1,
                        NomeImovel = "Imovel 1",
                        NomeProprietarioAtual = "Proprietario 1"

                    },
                    new MatriculaViewModel()
                    {
                        EnderecoImovel = "Endereço 2",
                        MatriculaId = 2,
                        NomeImovel = "Imovel 2",
                        NomeProprietarioAtual = "Proprietario 2"

                    }
                };
        }

        [HttpPost]
        [ValidateInput(false)]
        public ActionResult Index(MatriculaAtoViewModel modelo)
        {
            string filePath = Server.MapPath($"~/App_Data/Arquivos/{modelo.MatriculaID}.docx");
            try
            {

                if (modelo.Ato == null)
                {
                    modelo.MatriculasViewModel = getMatriculaViewModel();
                    modelo.ModelosSimplificadoViewModel = getModeloSimplificadoViewModel();
                    ViewBag.erro = "O Ato é obrigatório";
                    return View(nameof(Index), modelo);
                }
                //Ajusta a string de ato(HTML) -> ato(String)
                modelo.Ato = ConvertHtmlToString(modelo.Ato);

                if (ModelState.IsValid)
                {

                    //Representa o documento e o numero de pagina
                    Application app = new Application();
                    Document doc = null;
                    int numeroPagina;
                    int posicaoCursor = 0;
                    int numeroFicha = 6;
                    float quantidadeCentrimetros = 5;
                    bool isVerso = false;
                    try
                    {
                        if (modelo.ModeloTipoAto == "Ato Inicial")
                        {
                            //Inicia a variavel que representa o documento
                            app.Visible = true;
                            doc = app.Documents.Add();

                            //Configuração do documento
                            WordParagraphHelper.ParapraphAlignment(doc, WdParagraphAlignment.wdAlignParagraphJustify);
                            WordPageHelper.InicialConfiguration(doc, WdPaperSize.wdPaperB5, 14, "Times New Roman", true);

                            //Pegando o numero da pagina para configurar o layout
                            numeroPagina = WordPageHelper.GetNumeroPagina(doc);
                            WordPageHelper.ConfigurePageLayout(doc, numeroPagina);

                            //Insere o cabeçalho
                            WordLayoutPageHelper.InserirCabecalho(modelo, doc,true);
                        }
                        else
                        {
                            //Abre o arquivo para escrever o ATO e faz as configurações iniciais
                            app.Visible = true;
                            doc = app.Documents.Open(filePath);
                            WordPageHelper.InicialConfiguration(doc, WdPaperSize.wdPaperB5, 14, "Times New Roman", true);

                            //Numero de paginas do documento e a posição do cursor
                            numeroPagina = WordPageHelper.GetNumeroPagina(doc);
                            posicaoCursor = WordPageHelper.GetContentEnd(doc, 1);

                            if (numeroFicha > 0)
                            {
                                WordPageHelper.DeslocarAte(doc, numeroFicha, isVerso);

                                if (quantidadeCentrimetros > 0)
                                {
                                    ///Desloca os centimetros e escreve o cabeçalho, se necessario. 
                                    ///Atualiza o numero da pagina e a posição do cursor
                                    WordHelper.DesviarCentimetros(doc, modelo, quantidadeCentrimetros, ref numeroPagina, ref posicaoCursor, true);
                                }
                                else
                                {
                                    WordLayoutPageHelper.InserirCabecalho(modelo, doc, true, false);
                                    numeroPagina = WordPageHelper.GetNumeroPagina(doc);
                                }

                            }
                            else
                            {
                                WordParagraphHelper.InserirParagrafoEmBranco(doc);
                                if (WordPageHelper.GetNumeroPagina(doc) > numeroPagina)
                                {
                                    doc.Paragraphs.Last.Range.Delete();
                                    WordLayoutPageHelper.InserirCabecalho(modelo, doc, false);
                                    doc.Paragraphs.Last.Range.Delete();
                                    WordLayoutPageHelper.InserirContinuacaoFicha(doc);

                                    posicaoCursor = WordPageHelper.GetContentEnd(doc, 1);
                                    WordTextStyleHelper.Bold(doc, posicaoCursor, false);

                                    //TO DO : Pegar o numero do até em sequencia.
                                    //Escreve o tipo de ato (R ou AV), além disso, escreve o numero da sequencia e a matricula
                                    WordParagraphHelper.InserirTextoEmRange(doc, posicaoCursor, $"R-12/{modelo.MatriculaID} - ");
                                    numeroPagina = WordPageHelper.GetNumeroPagina(doc);

                                }
                                else
                                {
                                    doc.Paragraphs.Last.Range.Delete();
                                    posicaoCursor = WordPageHelper.GetContentEnd(doc, 1);
                                    WordParagraphHelper.InserirTextoEmRange(doc, posicaoCursor, $"R-12/{modelo.MatriculaID} - ");

                                }
                            }

                        }
                        #region | Metodo para escrever o ATO |
                        //Pega a posição do cursor do final do documento
                        posicaoCursor = WordPageHelper.GetContentEnd(doc, 1);
                        //Não deixa o texto começar com negrito
                        WordTextStyleHelper.Bold(doc, posicaoCursor, false);
                        //Escreve o ato e ajusta o documento, caso necessário
                        WordHelper.EscreverAto(modelo, doc, ref numeroPagina, ref posicaoCursor, numeroFicha>0 );
                        WordLayoutPageHelper.AjustarFinalDocumento(doc, numeroPagina, posicaoCursor, modelo);

                        #endregion

                        //Salvando e finalizando documento
                        doc.SaveAs2(filePath);
                        doc.Close();
                    }
                    catch (Exception)
                    {
                        doc.Close(WdSaveOptions.wdDoNotSaveChanges);
                        throw;
                    }
                    finally
                    {
                        app.Quit();
                        app = null;
                        doc = null;
                        GC.Collect();
                    }

                    // Gravar no banco o array de bytes
                    var arrayBytesNovo = System.IO.File.ReadAllBytes(filePath);
                    // Pegar a ultima "versão" do ato e somar

                    // Gravar o ato e buscar o selo e gravar o selo

                }
                modelo.MatriculasViewModel = getMatriculaViewModel();
                modelo.ModelosSimplificadoViewModel = getModeloSimplificadoViewModel();
                ViewBag.sucesso = "Ato cadastrado com sucesso!";

                return View(nameof(Index), modelo);
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
                return new HttpStatusCodeResult(HttpStatusCode.InternalServerError);
                throw;
            }
        }


        /// <summary>
        /// Retorna o numero de matricula do modelo
        /// </summary>
        /// <param name="modelo">Modelo</param>
        /// <returns>N° da Matricula</returns>
        public static long GetNumeroMatricula(MatriculaAtoViewModel modelo)
        {
            return modelo.MatriculaID;
        }



        /// <summary>
        /// Deixa o texto transparente do arquivo
        /// </summary>
        /// <param name="docX">Representa o documento</param>
        private static void SetTextColorTransparent(DocX docX)
        {
            var texto = docX.Paragraphs;
            foreach (var item in texto)
            {
                item.Color(Color.Transparent);
                item.UnderlineStyle(UnderlineStyle.none);
            }
        }


        /// <summary>
        /// Função que retorna o ato de html para string
        /// </summary>
        /// <param name="ato">ATO como HTML</param>
        /// <returns>ato como string</returns>
        private static string ConvertHtmlToString(string ato)
        {
            var documentoHtml = new HtmlDocument();
            StringBuilder st = new StringBuilder();
            documentoHtml.LoadHtml(ato);
            int countNodes = documentoHtml.DocumentNode.ChildNodes.Count;

            if (countNodes > 0)
            {
                for (int i = 0; i < documentoHtml.DocumentNode.ChildNodes.Count; i++)
                {
                    HtmlNode linhaHtml = documentoHtml.DocumentNode.ChildNodes[i];
                    if (linhaHtml.InnerHtml == "&nbsp;")
                    {
                        st.Append('\n');
                    }
                    else
                    {
                        st.Append(linhaHtml.InnerHtml);
                    }
                    if (i < countNodes && linhaHtml.InnerHtml != "&nbsp;")
                        st.Append('\n');

                }
            }
            return st.ToString();
        }

        /// <summary>
        /// Função que monta uma string HTML para mostrar na tela exatamente 
        /// oque esta escrito no documento
        /// </summary>
        /// <returns>string HTML</returns>
        public string UsaModeloParaAto([Bind(Include = "ModeloNome")]string ModeloNome)
        {
            StringBuilder textoFormatado = new StringBuilder();

            string filePath = Server.MapPath($"~/App_Data/Arquivos/{ModeloNome}.docx");
            try
            {
                using (FileStream fileStream = new FileStream(filePath, FileMode.Open, FileAccess.Read))
                {
                    //Carrega o Modelo
                    using (DocX docX = DocX.Load(fileStream))
                    {
                        //Varre todos os paragrafos do Modelo
                        foreach (var paragrafo in docX.Paragraphs)
                        {
                            if (paragrafo.Text != "")
                            {
                                StringBuilder textoParagrafo = new StringBuilder();
                                for (int i = 0; i < paragrafo.Text.Length; i++)
                                {
                                    if (paragrafo.Text[i] == '[')
                                    {
                                        i++;
                                        string nomeCampo = string.Empty;
                                        string resultadoQuery = string.Empty;
                                        while (paragrafo.Text[i] != ']')
                                        {
                                            nomeCampo += paragrafo.Text[i].ToString().Trim();
                                            i++;
                                            if (i >= paragrafo.Text.Length || paragrafo.Text[i] == '[')
                                            {
                                                return "Arquivo com campos corrompidos, verifique o modelo";
                                            }
                                        }
                                        //Buscar dado da pessoa aqui
                                        resultadoQuery = "teste query";

                                        //atualiza o texto formatado
                                        textoParagrafo.Append(resultadoQuery);
                                    }
                                    else
                                    {
                                        //caso não seja um campo somente adiciona o caractere
                                        textoParagrafo.Append(paragrafo.Text[i].ToString());
                                    }

                                }
                                // Populando campo de retorno
                                textoFormatado.Append($"<p>{textoParagrafo}</p>");
                            }
                        }
                    }
                }
                return textoFormatado.ToString();
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
                throw new Exception("Ocorreu algum erro ao utilizar o modelo");
            }
        }




        public PartialViewResult BuscaMatricula()
        {
            return PartialView();
        }
        public PartialViewResult BuscaModelo(int? idMatricula)
        {
            if (idMatricula.HasValue) { return PartialView(); }
            return PartialView(nameof(BuscaMatricula));
        }

    }

    public static class DataHelper
    {
        /// <summary>
        /// Retorna a data de hoje por extenso
        /// </summary>
        /// <returns>DD de MM de YYYY</returns>
        public static string GetDataPorExtenso()
        {
            var date = DateTime.Now.ToLongDateString().Split(',');

            return date[1].Trim();
        }
    }

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
                    doc.Paragraphs.Add().Range.Text = text;
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
                    doc.Application.ActiveDocument.Range(posicaoInicial).Text = text;
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
    }
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
    }
    public static class WordTextStyleHelper
    {
        /// <summary>
        /// Função responsável por controlar o negrito
        /// </summary>
        /// <param name="doc">Documento Ativo</param>
        /// <param name="posicaoInicial">Posição Inicial</param>
        /// <param name="posicaoFinal">Posição Final</param>
        /// <param name="negrito"> TRUE - NEGRITO; FALSE - TIRAR NEGRITO </param>
        public static void Bold(Document doc, int posicaoInicial, int posicaoFinal, bool negrito = true)
        {
            if (doc == null)
            {
                throw new ArgumentNullException("doc", "Documento não pode ser nulo!");
            }
            //Coloca o texto em negrito se negrito = true, se não, tira o negrito
            doc.Application.ActiveDocument.Range(posicaoInicial, posicaoFinal).Bold = negrito ? 1 : 0;
        }
        /// <summary>
        /// Função responsável por controlar o negrito
        /// </summary>
        /// <param name="doc">Documento Ativo</param>
        /// <param name="posicaoInicial">Posição Inicial</param>
        /// <param name="negrito"> TRUE - NEGRITO; FALSE - TIRAR NEGRITO </param>
        public static void Bold(Document doc, int posicaoInicial, bool negrito = true)
        {
            if (doc == null)
            {
                throw new ArgumentNullException("doc", "Documento não pode ser nulo!");
            }
            //Coloca o texto em negrito se negrito = true, se não, tira o negrito
            doc.Application.ActiveDocument.Range(posicaoInicial).Bold = negrito ? 1 : 0;
        }

        /// <summary>
        /// Função que configura a cor de fundo das letras a partir da posição inicial até a posição final
        /// </summary>
        /// <param name="doc">Documento ativo</param>
        /// <param name="posicaoInicial">Posição Inicial</param>
        /// <param name="posicaoFinal">Posição Final</param>
        /// <param name="wdColorIndex">Configuração da cor de fundo</param>
        public static void SetHighlightColor(Document doc, int posicaoInicial, int posicaoFinal, WdColorIndex wdColorIndex)
        {
            if (doc == null)
            {
                throw new ArgumentNullException("doc", "Documento não pode ser nulo!");
            }
            doc.Application.ActiveDocument.Range(posicaoInicial, posicaoFinal).HighlightColorIndex = wdColorIndex;
        }
        /// <summary>
        /// Função que configura a cor de fundo das letras a partir da posição inicial até o final do documento
        /// </summary>
        /// <param name="doc">Documento ativo</param>
        /// <param name="posicaoInicial">Posição Inicial</param>
        /// <param name="wdColorIndex">Configuração da cor de fundo</param>
        public static void SetHighlightColor(Document doc, int posicaoInicial, WdColorIndex wdColorIndex)
        {
            if (doc == null)
            {
                throw new ArgumentNullException("doc", "Documento não pode ser nulo!");
            }
            doc.Application.ActiveDocument.Range(posicaoInicial).HighlightColorIndex = wdColorIndex;
        }
        /// <summary>
        /// Função que controla o sublinhado do texto e configura até o final do documento
        /// </summary>
        /// <param name="doc">Documento ativo</param>
        /// <param name="posicaoInicial">Posição inicial</param>
        /// <param name="wdUnderline">Tipo de configuração para o sublinhado</param>
        public static void Underline(Document doc, int posicaoInicial, WdUnderline wdUnderline)
        {
            if (doc == null)
            {
                throw new ArgumentNullException("doc", "Documento não pode ser nulo!");
            }
            doc.Application.ActiveDocument.Range(posicaoInicial).Font.Underline = wdUnderline;
        }
        /// <summary>
        /// Função que controla o sublinhado do texto de uma posição inicial até a final
        /// </summary>
        /// <param name="doc">Documento Ativo</param>
        /// <param name="posicaoInicial">Posição Inicial</param>
        /// <param name="posicaoFinal">Posição Final</param>
        /// <param name="wdUnderline">Configuração para o sublinhado</param>
        public static void Underline(Document doc, int posicaoInicial, int posicaoFinal, WdUnderline wdUnderline)
        {
            if (doc == null)
            {
                throw new ArgumentNullException("doc", "Documento não pode ser nulo!");
            }
            doc.Application.ActiveDocument.Range(posicaoInicial, posicaoFinal).Font.Underline = wdUnderline;
        }
    }
    public static class WordShapeHelper
    {
        /// <summary>
        /// Função que ajusta a cor de fundo do texto 'Matricula' e 'ficha' para dar ilusão de legend do shape
        /// Utilizar somente após de inserir o texto da ficha
        /// </summary>
        /// <param name="doc">Documento Ativo</param>
        /// <param name="rangeFinal">Posição Final do texto 'ficha'</param>
        [Obsolete("Não precisa ser mais usado, pois não há necessidade de colocar mais Shape")]
        public static void AjustarBackGroundShapeMatriculaFicha(Document doc, int rangeFinal)
        {
            //Fundo Branco
            WordTextStyleHelper.SetHighlightColor(doc, rangeFinal - 7, rangeFinal, WdColorIndex.wdWhite);
            WordTextStyleHelper.SetHighlightColor(doc, rangeFinal - 46, rangeFinal - 35, WdColorIndex.wdWhite);
            //Sem cor de fundo
            WordTextStyleHelper.SetHighlightColor(doc, rangeFinal - 35, rangeFinal - 7, WdColorIndex.wdNoHighlight);
            WordTextStyleHelper.SetHighlightColor(doc, rangeFinal - 54, rangeFinal - 46, WdColorIndex.wdNoHighlight);
        }
        /// <summary>
        /// Inserir o texto de matricula e ficha dos shapes
        /// </summary>
        /// <param name="doc">Documento Ativo</param>
        [Obsolete("Não precisa ser mais usado, pois não há necessidade de colocar mais Shape")]
        public static void InserirTextoMatriculaFicha(Document doc)
        {
            if (doc == null)
            {
                throw new ArgumentNullException("doc", "Documento não pode ser nulo!");
            }
            WordParagraphHelper.InserirParagrafoEmRange(doc, new string(' ', 5) + "matrícula" + new string(' ', 30) + "ficha");
        }
        /// <summary>
        /// Get Shapes Object
        /// </summary>
        /// <param name="doc">Documento ativo</param>
        /// <returns>Shapes Object</returns>
        public static Shapes GetShapes(Document doc)
        {
            return doc != null ?
                    doc.Paragraphs.Add().Application.ActiveDocument.Shapes
                :
                    throw new ArgumentNullException("doc", "Documento não pode ser nulo");
        }
        /// <summary>
        /// Simula uma margem
        /// </summary>
        /// <param name="shapes">Shapes Obj</param>
        /// <param name="left">Padding Left</param>
        /// <param name="top">Padding Top</param>
        [Obsolete("Não precisa ser mais usado, pois não há necessidade de colocar mais Shape")]
        public static void InserirShapeMargem(Shapes shapes, float left, float top)
        {

            shapes.AddShape((int)MsoAutoShapeType.msoShapeRectangle, 50, 50, 428, 705)
                                            .ZOrder(MsoZOrderCmd.msoSendBehindText);
            int index = shapes.Count;
            //Configuração de estilo de shape
            SetBackgroundColor(shapes, index, XlRgbColor.xlWhite);
            SetBorderColor(shapes, index, XlRgbColor.xlBlack);
            SetRelativePosition(shapes, index, true, true);
            shapes[index].Left = left;
            shapes[index].Top = top;
        }
        /// <summary>
        /// Função que adiciona um shape para matricula e outro para ficha
        /// </summary>
        /// <param name="shapes">Objecto de Shapes</param>
        [Obsolete("Não precisa ser mais usado, pois não há necessidade de colocar mais Shape")]
        public static void InserirShapeMatriculaFicha(Shapes shapes)
        {
            int index;

            #region | Shape N° Matricula |
            shapes.AddShape((int)MsoAutoShapeType.msoShapeRoundedRectangle, 50, 50, 80, 30)
                                                        .ZOrder(MsoZOrderCmd.msoSendBehindText);
            index = shapes.Count;

            SetBackgroundColor(shapes, index, XlRgbColor.xlWhite);
            SetBorderColor(shapes, index, XlRgbColor.xlBlack);
            SetRelativePosition(shapes, index, true, true);

            shapes[index].Left = 85;
            shapes[index].Top = 57;
            #endregion
            #region | Shape Ficha |
            shapes.AddShape((int)MsoAutoShapeType.msoShapeRoundedRectangle, 50, 50, 65, 30)
               .ZOrder(MsoZOrderCmd.msoSendBehindText);
            index = shapes.Count;

            SetBackgroundColor(shapes, index, XlRgbColor.xlWhite);
            SetBorderColor(shapes, index, XlRgbColor.xlBlack);
            SetRelativePosition(shapes, index, true, true);
            shapes[index].Left = 200;
            shapes[index].Top = 57;
            #endregion

        }
        /// <summary>
        /// Função que pinta a cor de fundo do shape
        /// </summary>
        /// <param name="shapes">Shapes Obj</param>
        /// <param name="index">Indice do shape no vetor</param>
        /// <param name="color">Cor desejada (XlRgbColor) </param>
        [Obsolete("Não precisa ser mais usado, pois não há necessidade de colocar mais Shape")]
        public static void SetBackgroundColor(Shapes shapes, int index, XlRgbColor color)
        {
            shapes[index].Fill.ForeColor.RGB = (int)color;
        }
        /// <summary>
        /// Preenche a cor da borda do shape
        /// </summary>
        /// <param name="shapes">Shapes Obj</param>
        /// <param name="index">Indice do shape no vetor</param>
        /// <param name="color">Cor desejada (XlRgbColor) </param>
        [Obsolete("Não precisa ser mais usado, pois não há necessidade de colocar mais Shape")]
        public static void SetBorderColor(Shapes shapes, int index, XlRgbColor color)
        {
            shapes[index].Line.ForeColor.RGB = (int)color;
        }
        /// <summary>
        /// Função que configura a posição relativa do shape
        /// </summary>
        /// <param name="shapes">Shapes</param>
        /// <param name="index">Indice do shape no vetor</param>
        /// <param name="RelativeVerticalPosition">Set RelativeVerticalPosition (true or false)</param>
        /// <param name="RelativeHorizontalPosition">Set RelativeHorizontalPosition (true or false)</param>
        [Obsolete("Não precisa ser mais usado, pois não há necessidade de colocar mais Shape")]
        public static void SetRelativePosition(Shapes shapes, int index, bool RelativeVerticalPosition = true, bool RelativeHorizontalPosition = true)
        {
            if (RelativeHorizontalPosition && RelativeVerticalPosition)
            {
                shapes[index].RelativeHorizontalPosition = WdRelativeHorizontalPosition.wdRelativeHorizontalPositionPage;
                shapes[index].RelativeVerticalPosition = WdRelativeVerticalPosition.wdRelativeVerticalPositionPage;
                return;
            }
            else if (RelativeVerticalPosition)
            {
                shapes[index].RelativeVerticalPosition = WdRelativeVerticalPosition.wdRelativeVerticalPositionPage;
                return;
            }
            else if (RelativeHorizontalPosition)
            {
                shapes[index].RelativeHorizontalPosition = WdRelativeHorizontalPosition.wdRelativeHorizontalPositionPage;
                return;
            }
        }
        /// <summary>
        /// Deleta o ultimo shape
        /// </summary>
        /// <param name="doc">Documento ativo</param>
        public static void DeleteLastShape(Document doc)
        {
            if (doc != null)
                doc.Application.ActiveDocument.InlineShapes[GetShapesCount(doc)].Delete();
            else
                throw new ArgumentNullException("doc", "Erro ao deletar, documento não pode ser nulo!");
        }
        /// <summary>
        /// Retorna a quantidade de shapes no documento
        /// </summary>
        /// <param name="doc">Documento ativo</param>
        /// <returns>Quantidade de shapes</returns>
        public static int GetShapesCount(Document doc)
        {
            return doc == null ?
                    throw new ArgumentNullException("doc", "Documento não pode ser nulo!")
                :
                    doc.Application.ActiveDocument.InlineShapes.Count;
        }
        /// <summary>
        /// Cria uma linha de separação de texto
        /// </summary>
        /// <param name="doc">Documento Ativo</param>
        public static void InserirLinhaHorizontal(Document doc)
        {
            if (doc != null)
                doc.Application.ActiveDocument.Paragraphs.Add().Range.InlineShapes.AddHorizontalLineStandard();
            else
                throw new ArgumentNullException("doc", "Erro ao adicionar, documento não pode ser nulo!");
        }
    }
    public static class WordSelectionHelper
    {
        /// <summary>
        /// Função que desloca para algum item, para certa direção e uma certa quantidade de deslocamento
        /// </summary>
        /// <param name="doc">Documento Ativo</param>
        /// <param name="wdGoToItem">Item que deseja (Page,line etc.)</param>
        /// <param name="wdGoToDirection">Direção que deslocamento</param>
        /// <param name="quantidadeDeslocamento">Quantidade de deslocamento</param>
        public static void Goto(Document doc, WdGoToItem wdGoToItem, WdGoToDirection wdGoToDirection, int quantidadeDeslocamento)
        {
            if (doc == null)
            {
                throw new ArgumentNullException("doc", "Documento não pode ser nulo!");
            }
            doc.Application.Selection.GoTo(wdGoToItem, wdGoToDirection, quantidadeDeslocamento);

        }
        /// <summary>
        /// Seleciona o texto até o final de uma unidade especifica ou apenas se move para o final de dela.
        /// </summary>
        /// <param name="doc">Documento Ativo</param>
        /// <param name="wdUnits">Unidade especifica (WdUnits) </param>
        /// <param name="wdMovementType">Tipo de movimento (Extend- Seleciona, Move - Move sem selecionar)</param>
        public static void EndOf(Document doc, WdUnits wdUnits, WdMovementType wdMovementType)
        {
            if (doc == null)
            {
                throw new ArgumentNullException("doc", "Documento não pode ser nulo!");
            }
            doc.Application.Selection.EndOf(wdUnits, wdMovementType);
        }

        /// <summary>
        /// Função que pega o texto selecionado
        /// </summary>
        /// <param name="doc">Documento ativo</param>
        /// <returns>Texto selecionado</returns>
        public static string GetSelectionText(Document doc)
        {
            if (doc == null)
            {
                throw new ArgumentNullException("doc", "Documento não pode ser nulo!");
            }
            return doc.Application.Selection.Text;
        }
        /// <summary>
        /// Delete o texto selecionado
        /// </summary>
        /// <param name="doc">Documento ativo</param>
        public static void DeleteSelectionText(Document doc)
        {
            if (doc == null)
            {
                throw new ArgumentNullException("doc", "Documento não pode ser nulo!");
            }
            doc.Application.Selection.Delete();
        }
    }
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
            if(!cabecalhoEscrito) WordParagraphHelper.InserirParagrafoEmBranco(doc);

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
        public static void AjustarFinalDocumento(Document doc, int numeroPagina, int posicaoCursor, MatriculaAtoViewModel modelo)
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
        public static void InserirCabecalho(MatriculaAtoViewModel modelo, Document doc, bool houveDesvio = false, bool isParagrafo = true)
        {
            if (doc == null)
            {
                throw new ArgumentNullException("doc", "Documento não pode ser nulo!");
            }
            if (WordPageHelper.IsVerso(WordPageHelper.GetNumeroPagina(doc)))
            {
                //Insere o paragrafo correspondente a matricula e ficha 
                WordParagraphHelper.InserirParagrafo(doc, new string(' ', 5) + modelo.MatriculaID + new string(' ', 30 + (15 - modelo.MatriculaID.ToString().Length)) +
                    WordPageHelper.GetNumeroFicha(doc, isParagrafo) + new string(' ', 5 - WordPageHelper.GetNumeroFicha(doc, isParagrafo).ToString().Length)
                    , !houveDesvio);

                //Posiciona o cursor na ultima pagina e configura a pagina
                WordPageHelper.ConfigureLastPage(doc);

            }
            else
            {
                //Insere o paragrafo correspondente a matricula, ficha e data por extenso
                WordParagraphHelper.InserirParagrafo(doc, new string(' ', 5) + modelo.MatriculaID + new string(' ', 17 + (15 - modelo.MatriculaID.ToString().Length)) +
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
                WordParagraphHelper.InserirParagrafo(doc, $"( CONTINUAÇÃO DA FICHA N°. { WordPageHelper.GetNumeroFicha(doc) - 1} )", true);

                doc.Paragraphs.Last.Range.Bold = 1;
                WordParagraphHelper.InserirParagrafoEmBranco(doc);
            }
        }
    }
    public static class WordHelper
    {
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
        public static int EscreverNoDocumento(MatriculaAtoViewModel modelo, Document doc, ref int numeroPagina, string textoParaSalvar)
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
        public static void EscreverCabecalhoETexto(MatriculaAtoViewModel modelo, Document doc, out int numeroPagina, out int posicaoCursor)
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
        public static void EscreverAto(MatriculaAtoViewModel modelo, Document doc, ref int numeroPagina, ref int posicaoCursor, bool houveDesvio = false)
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
        /// Verifica se já existe ato inicial
        /// </summary>
        /// <param name="filePath"></param>
        /// <returns></returns>
        public static bool ExisteAtoInicial(MatriculaAtoViewModel modelo)
        {
            //Busca no banco se existe algum ato para aquela Matricula
            int quantidadeAtos = 0;
            //Se ato > 1, então existe o ato inicial
            return quantidadeAtos > 0;
        }

        /// <summary>
        /// A função desvia uma certa quantidade de centimetros após a margem, além de atualizar as variáveis (numero da pagina e posição do cursor)
        /// </summary>
        /// <param name="doc">Documento Ativo</param>
        /// <param name="modelo">View Model</param>
        /// <param name="quantidadeCentrimetros">Quantidade de centimetros</param>
        public static void DesviarCentimetros(Document doc, MatriculaAtoViewModel modelo,float quantidadeCentrimetros, ref int numeroPagina, ref int posicaoCursor, bool houveDesvioDeFicha = false)
        {
            if (quantidadeCentrimetros == 0 )
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
                WordLayoutPageHelper.AlinharAoShape(doc,false);
                

                //centimetros apos a borda da margem
                WordPageHelper.DeslocarCentimetros(doc, quantidadeCentrimetros);
                WordParagraphHelper.InserirParagrafo(doc, $"R-12/{modelo.MatriculaID} - ", true);
            }
            //atualiza as variaveis por ponteiro (REF)
            numeroPagina = WordPageHelper.GetNumeroPagina(doc);
            posicaoCursor = WordPageHelper.GetContentEnd(doc, 1);

        }

    }
}
