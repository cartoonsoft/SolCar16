using AdmCartorio.Models;
using HtmlAgilityPack;
using System;
using System.Collections.Generic;
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
                MatriculasViewModel = new List<MatriculaViewModel>()
                {
                    new MatriculaViewModel()
                    {
                        EnderecoImovel = "Endereço 1",
                        MatriculaId = 1,
                        NomeImovel = "Imovel 1",
                        NomeProprietarioAtual = "Proprietario 1"

                    }
                },
                ModelosSimplificadoViewModel = new List<ArquivoModeloSimplificadoViewModel>()
                {
                    new ArquivoModeloSimplificadoViewModel()
                    {
                        Id = 1,
                        DescricaoTipoAto = "Ato Inicial",
                        NomeModelo = "Modelo 1"
                    },
                    new ArquivoModeloSimplificadoViewModel()
                    {
                        Id = 2,
                        DescricaoTipoAto = "Registro",
                        NomeModelo = "Modelo 2"
                    }
                }
            };
            return View(dados);
        }

        [HttpPost]
        [ValidateInput(false)]
        public ActionResult Index(MatriculaAtoViewModel modelo)
        {
            string filePath = Server.MapPath($"~/App_Data/Arquivos/TesteModelo.docx");

            try
            {
                //Ajusta a string de ato(HTML) -> ato(String)
                modelo.Ato = ConvertHtmlToString(modelo.Ato);


                if (ModelState.IsValid)
                {
                    using (FileStream fileStream = new FileStream(filePath, FileMode.Open, FileAccess.ReadWrite))
                    {

                        //Pulando linhas por segurança
                        DocX docX = DocX.Load(fileStream);
                        docX.InsertParagraph();
                        docX.InsertParagraph().Append(" meu texto").SpacingAfter(5);
                        fileStream.Close();
                        docX.SaveAs(filePath);

                        // Gravar no banco o array de bytes
                        var arrayBytesNovo = System.IO.File.ReadAllBytes(filePath);

                    }
                }
                return View(nameof(Index));
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
                throw;
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
                switch (linhaHtml.Name)
                {
                    case "p":
                    case "h1":
                    case "h2":
                    case "h3":
                    case "h4":
                    case "h5":
                    case "h6":
                        {
                            st.Append(linhaHtml.InnerHtml);
                            st.AppendLine();
                            break;
                        }
                }
            }

            return st.ToString();
        }

        /// <summary>
        /// Função que monta uma string HTML para mostrar na tela exatamente 
        /// oque esta escrito no documento
        /// </summary>
        /// <returns>string HTML</returns>
        public string UsaModeloParaAto()
        {
            StringBuilder textoFormatado = new StringBuilder();

            string filePath = Server.MapPath($"~/App_Data/Arquivos/TesteModelo.docx");
            try
            {
                using (FileStream fileStream = new FileStream(filePath, FileMode.Open, FileAccess.Read))
                {
                    //Carrega o Modelo
                    DocX docX = DocX.Load(fileStream);
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
                return textoFormatado.ToString();
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
                return "Ocorreu algum erro ao utilizar o modelo";
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

        // GET: Matricula/Details/5
        public ActionResult Details(int id)
        {
            return View();
        }

        // GET: Matricula/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: Matricula/Create
        [HttpPost]
        public ActionResult Create(FormCollection collection)
        {
            try
            {
                // TODO: Add insert logic here

                return RedirectToAction("Index");
            }
            catch
            {
                return View();
            }
        }

        // GET: Matricula/Edit/5
        public ActionResult Edit(int id)
        {
            return View();
        }

        // POST: Matricula/Edit/5
        [HttpPost]
        public ActionResult Edit(int id, FormCollection collection)
        {
            try
            {
                // TODO: Add update logic here

                return RedirectToAction("Index");
            }
            catch
            {
                return View();
            }
        }

        // GET: Matricula/Delete/5
        public ActionResult Delete(int id)
        {
            return View();
        }

        // POST: Matricula/Delete/5
        [HttpPost]
        public ActionResult Delete(int id, FormCollection collection)
        {
            try
            {
                // TODO: Add delete logic here

                return RedirectToAction("Index");
            }
            catch
            {
                return View();
            }
        }
    }
}
