using AdmCartorio.Models;
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
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;
using Xceed.Words.NET;

namespace AdmCartorio.Controllers
{
    public class MatriculaController : Controller
    {
        // GET: Matricula
        public ActionResult Index()
        {
            var dados = new MatriculaAtoViewModel()
            {
                MatriculasViewModel = getMatriculaViewModel(),
                ModelosSimplificadoViewModel = getModeloSimplificadoViewModel()
            };
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
            string filePath = Server.MapPath($"~/App_Data/Arquivos/{modelo.MatriculaID}_.docx");
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


                    using (FileStream fileStream = new FileStream(filePath, FileMode.OpenOrCreate, FileAccess.ReadWrite))
                    {
                        if (modelo.ModeloTipoAto == "Ato Inicial")
                        {
                            var app = new Application();
                            app.Visible = true;
                            var doc = app.Documents.Add();
                            //var numeroPagina = doc.PageSetup.LineNumbering;
                            doc.Paragraphs.Add().Range.Text = "LIVRO N.° 2 - REGISTRO" + new string(' ', 52) + "16.° CARTÓRIO DE REGISTRO DE IMÓVEIS";
                            doc.Paragraphs.Add().Range.InsertAfter(new string(' ', 30) + "GERAL" + new string(' ', 79) + "de São Paulo");
                            doc.Paragraphs.SpaceAfter = 0;

                            var shapes = doc.Paragraphs.Add().Application.ActiveDocument.Shapes;

                            #region | Configurando Shape | 
                            shapes.AddShape((int)MsoAutoShapeType.msoShapeRoundedRectangle, 50, 50, 80, 30)
                                .ZOrder(MsoZOrderCmd.msoSendBehindText);
                            shapes[shapes.Count].Fill.ForeColor.RGB = (int)XlRgbColor.xlWhite;
                            shapes[shapes.Count].Line.ForeColor.RGB = (int)XlRgbColor.xlBlack;
                            //shapes[shapes.Count].Left = 200;
                            //shapes[shapes.Count].Top = 50;
                            #endregion



                            doc.Save();




#if false
                            using (DocX docX = DocX.Create(fileStream, DocumentTypes.Document))
                            {
                                docX.InsertParagraph().Append(modelo.Ato).SpacingAfter(5);

                                //docX.InsertParagraph().InsertText(modelo.Ato);

                                //Espaço de segurança
                                docX.InsertParagraph();
                                docX.InsertParagraph().InsertHorizontalLine();
                                
                                //docX.InsertParagraph().InsertPageNumber(PageNumberFormat.normal);

                                //docX.Sections.First().InsertParagraph().AppendPageNumber(PageNumberFormat.normal);

                                //docX.InsertParagraph().AppendPageNumber(PageNumberFormat.normal);

                                fileStream.Close();
                                docX.SaveAs(filePath);
                            }
#endif
                        }
                        else
                        {

                            //using (DocX docx = DocX.Load(Server.MapPath($"~/App_Data/Arquivos/ModeloAtoMacro.docm")))
                            //{
                            //    SetTextColorTransparent(docx);

                            //    docx.InsertParagraph().Append(modelo.Ato).SpacingAfter(5);

                            //    //docx.Footers.First.InsertParagraph().Append("TESTE DE RODAPE");
                            //    fileStream.Close();
                            //    docx.SaveAs(filePath);
                            //}

                            using (DocX docX = DocX.Load(fileStream))
                            {
                                //deixa texto transparente
                                SetTextColorTransparent(docX);

                                //Cadastro do texto e registro do arquivo
                                docX.InsertParagraph().Append(modelo.Ato).SpacingAfter(5);

                                //espaço de segurança
                                docX.InsertParagraph();
                                docX.InsertParagraph().InsertHorizontalLine();

                                fileStream.Close();
                                docX.Save();
                                docX.SaveAs(filePath);
                            }
                        }

                        // Gravar no banco o array de bytes
                        var arrayBytesNovo = System.IO.File.ReadAllBytes(filePath);
                        // Pegar a ultima "versão" do ato e somar

                        // Gravar o ato e buscar o selo e gravar o selo


                    }
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

            foreach (var linhaHtml in documentoHtml.DocumentNode.ChildNodes)
            {

                st.Append(linhaHtml.InnerHtml);
                st.AppendLine();

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
}
