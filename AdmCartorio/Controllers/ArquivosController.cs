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
using System.Web;
using System.Web.Mvc;

namespace AdmCartorio.Controllers
{
    public class ArquivosController : Controller
    {
        // GET: Arquivos
        public ActionResult Index()
        {
            return View();
        }
        #region | CADASTRAR |
        [HttpGet]
        // GET: Arquivos/Cadastrar
        public ActionResult Cadastrar()
        {

            ViewBag.NaturezaArquivoModeloDocx = new SelectList(Enum.GetValues(typeof(NaturezaArquivoModeloDocx)), NaturezaArquivoModeloDocx.Imoveis);

            return View();
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
                        var filePath = Path.Combine(Server.MapPath("~/App_Data/Modelos/"),
                            nomeArquivo + extension);
                        arquivo.SaveAs(filePath);
                        #endregion
                        #region | Populando variavel do banco |

                        arquivoModel.ArquivoByte = System.IO.File.ReadAllBytes(filePath);
                        arquivoModel.CaminhoArquivo = filePath;
                        arquivoModel.NomeArquivo = nomeArquivo;
                        arquivoModel.ExtensaoArquivo = extension;

                        #endregion
                        //#region |Cadastrando no banco|
                        //ContextMainCar16 context = new ContextMainCar16("connOraDbNew");
                        //using (UnitOfWorkCar16 unitOfWork = new UnitOfWorkCar16(context))
                        //{
                        //    using (AppServiceArquivoModeloDocx appService = new AppServiceArquivoModeloDocx(unitOfWork))
                        //    {

                        //        appService.SalvarModelo(new DtoArquivoModeloDocxModel()
                        //        {
                        //            ArquivoByte = arquivoModel.ArquivoByte,
                        //            CaminhoArquivo = arquivoModel.CaminhoArquivo,
                        //            ExtensaoArquivo = arquivoModel.ExtensaoArquivo,
                        //            Files = arquivoModel.Files,
                        //            NomeArquivo = arquivoModel.NomeArquivo,
                        //            NomeModelo = arquivoModel.NomeModelo
                        //        });
                        //    }
                        //    unitOfWork.Commit();
                        //}
                        //#endregion
                    }

                    ViewBag.resultado = "Arquivo salvo com sucesso!";
                    return View(nameof(Cadastrar));
                }
                ViewBag.NaturezaArquivoModeloDocx = new SelectList(Enum.GetValues(typeof(NaturezaArquivoModeloDocx)), NaturezaArquivoModeloDocx.Imoveis);
                return View(nameof(Cadastrar));
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
                return View();
            }
        }

        #endregion
        #region | EDITAR |
        // GET: Arquivos/Editar/{ID}
        [HttpGet]
        public ActionResult Editar(int ID)
        {
            #region | Busca dos dados do Arquivo |

            //ArquivoViewModel arquivoViewModel = new ArquivoViewModel();

            #endregion

            return View();
        }
        #endregion

        #region | METODOS COMPARTILHADOS |
        [ValidateAntiForgeryToken]
        public void CadastrarLogUpload(string IP)
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
        #endregion
    }
}