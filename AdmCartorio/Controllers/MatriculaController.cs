using AdmCartorio.Models;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
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

        public string UsaModeloParaAto()
        {
            string textoFormatado = null;

            string filePath = Server.MapPath($"~/App_Data/Arquivos/TesteModelo.docx");
            try
            {
                using (FileStream fileStream = new FileStream(filePath, FileMode.Open, FileAccess.Read))
                {
                    DocX docX = DocX.Load(fileStream);
                    var texto = docX.Text.ToArray();

                    for (int i = 0; i < texto.Length; i++)
                    {
                        if (texto[i] == '[')
                        {
                            i++;
                            string nomeCampo = null;
                            string resultadoQuery = null;
                            while (texto[i] != ']')
                            {
                                nomeCampo += texto[i].ToString().Trim();
                                i++;
                                if (i >= texto.Length || texto[i] == '[')
                                {
                                    return "Arquivo com campos corrompidos, verifique o modelo";
                                }
                            }
                            //Buscar dado da pessoa aqui
                            resultadoQuery = "teste query";

                            //atualiza o texto formatado
                            textoFormatado += resultadoQuery;
                        }
                        else
                        {
                            textoFormatado += texto[i].ToString();
                        }

                    }
                }
                return textoFormatado;

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
