using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using AdmCartorio.Controllers.Base;
using AdmCartorio.ViewModels;
using Domain.Car16.Entities.Car16New;
using Domain.Car16.Interfaces.UnitOfWork;
using Domain.Car16.enums;
using Infra.Data.Car16.UnitsOfWork;
using AppServices.Car16.AppServices;
using Dto.Car16.Entities.Cadastros;
using Domain.Car16.Entities.Diversas;
using AutoMapper;
using Dto.Car16.Entities.Diversos;
using System.Threading;

namespace AdmCartorio.Controllers
{
    [Authorize]
    public class ArquivosController : AdmCartorioBaseController
    {
        public ArquivosController() : base(null, null)
        {
            //

        }

        public ArquivosController(IUnitOfWorkDataBaseCar16 unitOfWorkDataBaseCar16, IUnitOfWorkDataBaseCar16New unitOfWorkDataBseCar16New) : base(unitOfWorkDataBaseCar16, unitOfWorkDataBseCar16New)
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

        // GET: Arquivos
        public ActionResult Index()
        {
            IEnumerable<ArquivoModeloDocxListViewModel> listaArquivoModeloDocxListViewModel = new List<ArquivoModeloDocxListViewModel>();

            using (AppServiceArquivoModeloDocx appService = new AppServiceArquivoModeloDocx(this.UnitOfWorkDataBseCar16New))
            {
                IEnumerable<DtoArquivoModeloDocxList> listaDtoArquivoModelosDocx = appService.ListarArquivoModeloDocx();
                listaArquivoModeloDocxListViewModel = Mapper.Map<IEnumerable<DtoArquivoModeloDocxList>, IEnumerable<ArquivoModeloDocxListViewModel>>(listaDtoArquivoModelosDocx);
            }

            return View(listaArquivoModeloDocxListViewModel);
        }

        #region | CADASTRAR |
        // GET: Arquivos/Cadastrar
        public ActionResult Cadastrar()
        {
            try
            { 
                List<TipoAto> listaTipoAto = this.UnitOfWorkDataBseCar16New.Repositories.GenericRepository<TipoAto>().GetAll().ToList();
                ViewBag.listaTipoAto = new SelectList(listaTipoAto, "Id", "Descricao");

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
        public ActionResult Cadastrar([Bind(Include = "Id,NomeModelo,IdTipoAto,DescricaoTipoAto,Files,LogArquivoModeloDocxViewModel,Arquivo,IpLocal")]ArquivoModeloDocxViewModel arquivoModel)
        {
            bool success = true;
            string msg = "";

            try
            {
                List<TipoAto> listaTipoAto = this.UnitOfWorkDataBseCar16New.Repositories.GenericRepository<TipoAto>().GetAll().ToList();
                ViewBag.listaTipoAto = new SelectList(listaTipoAto, "Id", "Descricao");

                if (ModelState.IsValid)
                {
                    LogArquivoModeloDocxViewModel log = new LogArquivoModeloDocxViewModel();

                    for (int i = 0; i < arquivoModel.Files.Count; i++)
                    {
                        //Pega os dados do arquivo
                        HttpPostedFileBase arquivo = arquivoModel.Files[i];
                        //arquivo.FileName = "Mod_"+arquivoModel.DescricaoTipoAto+DateTime.Now.ToString("yyyyMMddTHHmmss")

                        var stream = arquivo.InputStream;
                        var memoryStream = new MemoryStream();
                        stream.CopyTo(memoryStream);
                        arquivoModel.ArquivoByte = memoryStream.ToArray();

                        log.IdUsuario = UsuarioAtual.Id;
                        log.UsuarioSistOp = HttpContext.User.Identity.Name;
                        log.IP = arquivoModel.IpLocal;
                        arquivoModel.LogArquivoModeloDocxViewModel = log;
                    }

                    using (UnitOfWorkDataBaseCar16 unitOfWork = new UnitOfWorkDataBaseCar16(BaseDados.DesenvDezesseisNew))
                    {
                        using (AppServiceArquivoModeloDocx appService = new AppServiceArquivoModeloDocx(unitOfWork))
                        {
                            appService.SalvarModelo(new DtoArquivoModeloDocxModel()
                            {
                                ArquivoByte = arquivoModel.ArquivoByte,
                                IdContaAcessoSistema = 1,
                                Ativo = true,
                                IdTipoAto = arquivoModel.IdTipoAto,
                                Arquivo = arquivoModel.Arquivo,
                                Files = arquivoModel.Files,
                                NomeModelo = arquivoModel.NomeModelo
                            }, UsuarioAtual.Id);
                        }
                        unitOfWork.Commit();
                    }

                    msg = "Arquivo salvo com sucesso!";
                }
            }
            catch (Exception ex)
            {
                success = false;
                msg = "Falha ao Cadastrar! [ArquivosController: " + ex.Message + " -> " + ex.InnerException.Message + "]";
                System.Diagnostics.Debug.WriteLine("ArquivosController Exception: " + ex.InnerException + " -> " + ex.InnerException.Message);
                //return new HttpStatusCodeResult(HttpStatusCode.InternalServerError);
            }

            var resultado = new
            {
                success = success,
                mensagem = msg
            };

            return Json(resultado, JsonRequestBehavior.AllowGet);
        }
        #endregion

        #region | EDITAR |
        // GET: Arquivos/Editar/{ID}
        public ActionResult Editar(int? ID)
        {
            if (ID.HasValue && ID > 0)
            {
                try
                {
                    #region | Busca dos dados do Arquivo |
                    ArquivoModeloDocxViewModel arquivoViewModel;

                    arquivoViewModel = new ArquivoModeloDocxViewModel()
                    {
                        Id = 1,
                        NomeModelo = "Modelo 1",
                        Arquivo = "/App_Data/Arquivos/testeWord.docx",
                        IdTipoAto = 1,

                    };

                    if (arquivoViewModel == null)
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
            else
            {
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
                IdArquivoModeloDocx = Id,
                IdUsuario = UsuarioAtual.Id,
                DataHora = DateTime.Now,
                IP = IP,
                TipoLogArquivoModeloDocx = TipoLogArquivoModeloDocx.Download
            };

            //todo: Ronaldo criar rotina de cadastro 

            return;
        }

        [ValidateAntiForgeryToken]
        public void DesativarArquivoModeloDocx([Bind(Include = "Id,Ip")]DadosPostArquivoUsuario dadosPost)
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
                CadastrarLogDownload(dadosPost.Ip, dadosPost.Id);

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
