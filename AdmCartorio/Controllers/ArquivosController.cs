using AdmCartorio.Models;
using AppServices.Car16.AppServices;
using Domain.Car16.Enumeradores;
using Dto.Car16.Entities.Cadastros;
using Infra.Data.Car16.Context;
using Infra.Data.Car16.UnitOfWorkCar16;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;

namespace AdmCartorio.Controllers
{
    public class ArquivosController : Controller
    {
        // GET: Arquivos
        public ActionResult Index()
        {

            var dados = new List<ArquivoModeloDocxViewModel>()
            {
                new ArquivoModeloDocxViewModel()
                {
                    Id = 50,
                    NomeModelo = "Modelo 50",
                    NaturezaArquivoModeloDocx = Models.Enumeradores.NaturezaArquivoModeloDocx.Civil
                },
                new ArquivoModeloDocxViewModel()
                {
                    Id = 2,
                    NomeModelo = "Modelo 2",
                    NaturezaArquivoModeloDocx = Models.Enumeradores.NaturezaArquivoModeloDocx.Civil
                },
                new ArquivoModeloDocxViewModel()
                {
                    Id = 3,
                    NomeModelo = "Modelo 3",
                    NaturezaArquivoModeloDocx = Models.Enumeradores.NaturezaArquivoModeloDocx.Civil
                }
            };
            return View(dados);
        }
        #region | CADASTRAR |
        // GET: Arquivos/Cadastrar
        public ActionResult Cadastrar()
        {
            try
            { 
                ViewBag.NaturezaArquivoModeloDocx = new SelectList(Enum.GetValues(typeof(NaturezaArquivoModeloDocx)), NaturezaArquivoModeloDocx.Imoveis);

                return View();
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
                return new HttpStatusCodeResult(HttpStatusCode.InternalServerError);
            }
        }

        // POST: Arquivos/Cadastrar
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Cadastrar(ArquivoModeloDocxViewModel arquivoModel/*DtoArquivoModeloDocxModel arq*/)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    for (int i = 0; i < arquivoModel.Files.Count; i++)
                    {
                        //Pega os dados do arquivo
                        HttpPostedFileBase arquivo = arquivoModel.Files[i];
                        var extension = Path.GetExtension(arquivo.FileName);
                        var nomeArquivo = Path.GetFileNameWithoutExtension(arquivo.FileName);

                        #region | Gravacao do arquivo fisicamente |
                        // Salva o arquivo fisicamente
                        var filePath = Path.Combine(Server.MapPath("~/App_Data/Arquivos/"),
                            nomeArquivo + extension);
                        arquivo.SaveAs(filePath);
                        #endregion
                        
                        #region | Populando variavel do banco |

                        arquivoModel.ArquivoByte = System.IO.File.ReadAllBytes(filePath);
                        arquivoModel.CaminhoArquivo = filePath;
                        arquivoModel.NomeArquivo = nomeArquivo;
                        arquivoModel.ExtensaoArquivo = extension;

                        #endregion
                        
                        #region |Cadastrando no banco|
                        //CadastraArquivoModeloDocx(arquivoModel);
                        #endregion
                    }
                    ViewBag.NaturezaArquivoModeloDocx = new SelectList(Enum.GetValues(typeof(NaturezaArquivoModeloDocx)), NaturezaArquivoModeloDocx.Imoveis);
                    ViewBag.resultado = "Arquivo salvo com sucesso!";
                    return View(nameof(Cadastrar));
                }
                ViewBag.NaturezaArquivoModeloDocx = new SelectList(Enum.GetValues(typeof(NaturezaArquivoModeloDocx)), NaturezaArquivoModeloDocx.Imoveis);
                return View(nameof(Cadastrar));
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
                return new HttpStatusCodeResult(HttpStatusCode.InternalServerError);
            }
        }

        #endregion
        
        #region | EDITAR |
        // GET: Arquivos/Editar/{ID}
        public ActionResult Editar(int? ID)
        {
            if(ID.HasValue)
            {
                try { 
                
                    #region | Busca dos dados do Arquivo |

                    var arquivoViewModel = new ArquivoModeloDocxViewModel()
                    {
                        Id = 1,
                        NomeModelo = "Modelo 1",
                        NomeArquivo = "testeWord",
                        ExtensaoArquivo =".docx"
                    };


                    #endregion
                    return View(arquivoViewModel);
                }
                catch (Exception ex)
                {
                    Console.WriteLine(ex.Message);
                    return new HttpStatusCodeResult(HttpStatusCode.InternalServerError);
                }
            }
            else{
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }

        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Editar(ArquivoModeloDocxViewModel arquivoModeloDocxViewModel)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    // Fazendo Upload do arquivo
                    //UploadArquivo(arquivoModeloDocxViewModel);

                    ViewBag.resultado = "Arquivo salvo com sucesso!";
                    return View(nameof(Editar));

                }
                return View(nameof(Editar));
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
                return new HttpStatusCodeResult(HttpStatusCode.InternalServerError);
            }
        }
        #endregion
        
        #region | METODOS COMPARTILHADOS |
        private void CadastrarLogDownload(string IP)
        {
            //var arquivolog = new logarquivomodelodocxviewmodel()
            //{
            //    arquivoid = id,
            //    datahora = datetime.now,
            //    ip = ip,
            //    tipologarquivomodelodocx = admcartorio.models.enumeradores.tipologarquivomodelodocx.download
            //};
            return;
        }

        public int? CadastraArquivoModeloDocx(ArquivoModeloDocxViewModel arquivoModel)
        {
            ContextMainCar16 context = new ContextMainCar16("connOraDbNew");
            int? resultado;

            using (UnitOfWorkCar16 unitOfWork = new UnitOfWorkCar16(context))
            {
                using (AppServiceArquivoModeloDocx appService = new AppServiceArquivoModeloDocx(unitOfWork))
                {

                    appService.SalvarModelo(new DtoArquivoModeloDocxModel()
                    {
                        ArquivoByte = arquivoModel.ArquivoByte,
                        CaminhoArquivo = arquivoModel.CaminhoArquivo,
                        ExtensaoArquivo = arquivoModel.ExtensaoArquivo,
                        Files = arquivoModel.Files,
                        NomeArquivo = arquivoModel.NomeArquivo,
                        NomeModelo = arquivoModel.NomeModelo
                    });
                }
                resultado = unitOfWork.Commit();
            }

            return resultado;
        }

        //[ValidateAntiForgeryToken]
        public void DesativarArquivoModeloDocx(string ip, int id)
        {
            
        }

        [ValidateAntiForgeryToken]
        public FileResult DownloadFile()
        {
            string filePath = Server.MapPath("~/App_Data/Arquivos/testeWord.docx");
            try
            {
                byte[] fileBytes = System.IO.File.ReadAllBytes(filePath);
                string fileName = "testeWord.docx";

                //Cadastro de LOG
                CadastrarLogDownload("X");

                return File(fileBytes, System.Net.Mime.MediaTypeNames.Application.Octet, fileName);
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
                return null;
            }
        }

        #endregion
    }
}