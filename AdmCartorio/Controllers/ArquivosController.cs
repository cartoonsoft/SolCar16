using AdmCartorio.Models;
using AppServices.Car16.AppServices;
using Domain.Car16.Entities;
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
                    IdTipoAto = 1,
                    DescricaoTipoAto = "Ato Inicial"
                },
                new ArquivoModeloDocxViewModel()
                {
                    Id = 2,
                    NomeModelo = "Modelo 2",
                    DescricaoTipoAto = "Ato Inicial",
                    IdTipoAto = 1
                },
                new ArquivoModeloDocxViewModel()
                {
                    Id = 3,
                    NomeModelo = "Modelo 3",
                    IdTipoAto = 2,
                    DescricaoTipoAto = "Registro"
                    
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
                ViewData["IdTipoAto"] = 
                    new SelectList(
                        new List<TipoAto>() {
                            new TipoAto() { Id = 1,Descricao ="Ato Inicial" },
                            new TipoAto() { Id = 2,Descricao ="Registro"}
                    }, "Id", "Descricao");
                //ViewBag.NaturezaArquivoModeloDocx = new SelectList(Enum.GetValues(typeof(TipoArquivoModeloDocx)), TipoArquivoModeloDocx.Imoveis);

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
        public ActionResult Cadastrar([Bind(Include = "NomeModelo,IdTipoAto,Files,LogArquivoModeloDocxViewModel")]ArquivoModeloDocxViewModel arquivoModel)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    for (int i = 0; i < arquivoModel.Files.Count; i++)
                    {
                        //Pega os dados do arquivo
                        HttpPostedFileBase arquivo = arquivoModel.Files[i];
                        var nomeArquivo = Path.GetFileNameWithoutExtension(arquivo.FileName);
                        

                        #region | Gravacao do arquivo fisicamente |
                        // Salva o arquivo fisicamente
                        var filePath = Path.Combine(Server.MapPath("~/App_Data/Arquivos/"),
                            nomeArquivo + ".docx");
                        arquivo.SaveAs(filePath);
                        #endregion
                        
                        #region | Populando variavel do banco |

                        arquivoModel.ArquivoByte = System.IO.File.ReadAllBytes(filePath);
                        arquivoModel.Arquivo = filePath;
                        arquivoModel.NomeModelo = arquivoModel.NomeModelo;

                        #endregion
                        
                        #region |Cadastrando no banco|
                        //CadastraArquivoModeloDocx(arquivoModel);
                        #endregion
                    }
                    ViewData["IdTipoAto"] =
                                        new SelectList(
                                            new List<TipoAto>() {
                            new TipoAto() { Id = 1,Descricao ="Ato Inicial" },
                            new TipoAto() { Id = 2,Descricao ="Registro"}
                                        }, "Id", "Descricao");

                    ViewBag.resultado = "Arquivo salvo com sucesso!";
                    return View(nameof(Cadastrar));
                }
                ViewData["IdTipoAto"] =
                                    new SelectList(
                                        new List<TipoAto>() {
                            new TipoAto() { Id = 1,Descricao ="Ato Inicial" },
                            new TipoAto() { Id = 2,Descricao ="Registro"}
                                    }, "Id", "Descricao");

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
            if(ID.HasValue && ID > 0)
            {
                try {

                    #region | Busca dos dados do Arquivo |

                    ArquivoModeloDocxViewModel arquivoViewModel;

                    arquivoViewModel = new ArquivoModeloDocxViewModel()
                    {
                        Id = 1,
                        NomeModelo = "Modelo 1",
                        Arquivo = "/App_Data/Arquivos/testeWord.docx",
                        IdTipoAto = 1,
                        
                    };

                    if(arquivoViewModel == null)
                    {
                        return new HttpStatusCodeResult(HttpStatusCode.NotFound);
                    }


                    #endregion
                    return View(arquivoViewModel);
                }
                catch (Exception ex)
                {
                    Console.WriteLine(ex.Message);
                    return new HttpStatusCodeResult(HttpStatusCode.InternalServerError);
                }
            }
            else {
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
                    return View(nameof(Editar), arquivoModeloDocxViewModel);

                }
                return View(nameof(Editar), arquivoModeloDocxViewModel);
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
                return new HttpStatusCodeResult(HttpStatusCode.InternalServerError);
            }
        }
        #endregion
        
        #region | METODOS COMPARTILHADOS |
        private void CadastrarLogDownload(string IP, int Id)
        {
            var arquivolog = new LogArquivoModeloDocx()
            {
                ArquivoID = Id,
                DataHora = DateTime.Now,
                IP = IP,
                TipoLogArquivoModeloDocx = TipoLogArquivoModeloDocx.Download
            };
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
                        Arquivo = arquivoModel.Arquivo,
                        Files = arquivoModel.Files,
                        NomeModelo = arquivoModel.NomeModelo
                    });
                }
                resultado = unitOfWork.Commit();
            }

            return resultado;
        }

        [ValidateAntiForgeryToken]
        public void DesativarArquivoModeloDocx([Bind(Include ="Id,Ip")]DadosPostArquivoUsuario dadosPost)
        {
            
        }

        public FileResult DownloadFile([Bind(Include = "Id,Ip,Arquivo")]DadosPostArquivoUsuario dadosPost)
        {
            string fileName = Path.GetFileNameWithoutExtension(dadosPost.Arquivo);
            string filePath = Server.MapPath($"~/App_Data/Arquivos/{fileName}.docx");
            try
            {
                byte[] fileBytes = System.IO.File.ReadAllBytes(filePath);
                
                //Cadastro de LOG
                CadastrarLogDownload(dadosPost.Ip,dadosPost.Id);

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