using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using AutoMapper;
using AdmCartorio.Controllers.Base;
using AdmCartorio.ViewModels;
using Domain.Cartorio.Entities.CartorioNew;
using Domain.Cartorio.Interfaces.UnitOfWork;
using Domain.Cartorio.enums;
using Infra.Data.Cartorio.UnitsOfWork.DbCartorioNew;
using AppServices.Cartorio.AppServices;
using Dto.Cartorio.Entities.Diversos;
using Dto.Cartorio.Entities.Cadastros;
using Infra.Data.Cartorio.UnitsOfWork.DbCartorio;

namespace AdmCartorio.Controllers
{
    [Authorize]
    public class ArquivosController : AdmCartorioBaseController
    {
        public ArquivosController() : base(null, null)
        {
            //
        }

        public ArquivosController(IUnitOfWorkDataBaseCartorio UnitOfWorkDataBaseCartorio, IUnitOfWorkDataBaseCartorioNew UnitOfWorkDataBaseCartorioNew) : base(UnitOfWorkDataBaseCartorio, UnitOfWorkDataBaseCartorioNew)
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

            using (AppServiceArquivoModeloDocx appService = new AppServiceArquivoModeloDocx(this.UnitOfWorkDataBaseCartorio, this.UnitOfWorkDataBaseCartorioNew))
            {
                IEnumerable<DtoArquivoModeloDocxList> listaDtoArquivoModelosDocx = appService.ListarArquivoModeloDocx().Where(a => a.Ativo == true);
                listaArquivoModeloDocxListViewModel = Mapper.Map<IEnumerable<DtoArquivoModeloDocxList>, IEnumerable<ArquivoModeloDocxListViewModel>>(listaDtoArquivoModelosDocx);
            }

            return View(listaArquivoModeloDocxListViewModel);
        }

        #region | CADASTRAR |
        // GET: Arquivos/Cadastrar
        public ActionResult Novo()
        {
            try
            {
                List<TipoAto> listaTipoAto = this.UnitOfWorkDataBaseCartorioNew.Repositories.GenericRepository<TipoAto>().GetAll().ToList();
                ViewBag.listaTipoAto = new SelectList(listaTipoAto, "Id", "Descricao");

                return View();
            }
            catch (Exception)
            {
                return new HttpStatusCodeResult(HttpStatusCode.InternalServerError);
            }
        }

        // POST: Arquivos/Cadastrar
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Novo([Bind(Include = "Id,NomeModelo,IdTipoAto,DescricaoTipoAto,Files,LogArquivoModeloDocxViewModel,Arquivo,IpLocal")]ArquivoModeloDocxViewModel arquivoModel)
        {
            bool success = false;
            bool ControllerModelValid = ModelState.IsValid;
            string msg = "";
            long? NovoId = null;

            try
            {
                List<TipoAto> listaTipoAto = this.UnitOfWorkDataBaseCartorioNew.Repositories.GenericRepository<TipoAto>().GetAll().ToList();
                ViewBag.listaTipoAto = new SelectList(listaTipoAto, "Id", "Descricao");

                if (ControllerModelValid)
                {
                    LogArquivoModeloDocx logArquivo = new LogArquivoModeloDocx();
                    logArquivo.IdUsuario = UsuarioAtual.Id;
                    logArquivo.UsuarioSistOperacional = System.Security.Principal.WindowsIdentity.GetCurrent().Name;  // HttpContext.Current.User.Identity.Name; //  HttpContext.User.Identity.Name;
                    logArquivo.IP = arquivoModel.IpLocal;


                    string filePath = string.Empty;
                    arquivoModel.CaminhoEArquivo = Server.MapPath("~/App_Data/Arquivos/Modelos/");

                    using (UnitOfWorkDataBaseCartorio unitOfWork1 = new UnitOfWorkDataBaseCartorio(BaseDados.DesenvDezesseis))
                    {
                        using (UnitOfWorkDataBaseCartorioNew unitOfWork2 = new UnitOfWorkDataBaseCartorioNew(BaseDados.DesenvDezesseisNew))
                        {
                            using (AppServiceArquivoModeloDocx appService = new AppServiceArquivoModeloDocx(unitOfWork1, unitOfWork2))
                            {
                                NovoId = appService.NovoModelo(
                                    new DtoArquivoModeloDocxModel()
                                    {
                                        IdContaAcessoSistema = 1,
                                        Ativo = true,
                                        IdTipoAto = arquivoModel.IdTipoAto,
                                        CaminhoEArquivo = arquivoModel.CaminhoEArquivo, // Path.Combine(Server.MapPath("~/App_Data/Arquivos/Modelos/"), NovoId.ToString() + ".docx"),
                                        Files = arquivoModel.Files,
                                        NomeModelo = arquivoModel.NomeModelo,
                                        LogArquivo = logArquivo
                                    },
                                    UsuarioAtual.Id
                                );
                            }
                        }
                    }

                    for (int i = 0; i < arquivoModel.Files.Count; i++)
                    {
                        //Pega os dados do arquivo
                        HttpPostedFileBase arquivo = arquivoModel.Files[i];
                        //arquivo.FileName = "Mod_"+arquivoModel.DescricaoTipoAto+DateTime.Now.ToString("yyyyMMddTHHmmss")
                        var nomeArquivo = Path.GetFileNameWithoutExtension(arquivo.FileName);

                        #region | Gravacao do arquivo fisicamente |
                        // Salva o arquivo fisicamente
                        filePath = Path.Combine(arquivoModel.CaminhoEArquivo, NovoId.ToString() + ".docx");
                        arquivo.SaveAs(filePath);
                        #endregion
                    }

                    success = true;
                    msg = "Modelo de documento salvo com sucesso!";
                }
            }
            catch (Exception ex)
            {
                msg = "Falha ao Cadastrar! [ArquivosController: " + ex.Message + "]";
                //System.Diagnostics.Debug.WriteLine("ArquivosController Exception: " + ex.Message);
                //return new HttpStatusCodeResult(HttpStatusCode.InternalServerError);
            }

            //var resultado = new
            //{
            //    success = success,
            //    mensagem = msg
            //};

            //return Json(resultado, JsonRequestBehavior.AllowGet);
            ViewBag.success = success ? "true" : "false";
            ViewBag.ControllerModelValid = ControllerModelValid ? "true" : "false";
            ViewBag.msg = msg;

            return View(nameof(Novo));

        }
        #endregion

        #region | EDITAR |
        // GET: Arquivos/Editar/{ID}
        public ActionResult Editar(long? Id)
        {
            if (Id.HasValue && Id > 0)
            {
                try
                {
                    ArquivoModeloDocx arquivoModelo = this.UnitOfWorkDataBaseCartorioNew.Repositories.RepositoryArquivoModeloDocx.GetById(Id);
                    ArquivoModeloDocxViewModel arquivoViewModel = new ArquivoModeloDocxViewModel
                    {
                        Id = arquivoModelo.Id,
                        DescricaoTipoAto = "",
                        IdTipoAto = arquivoModelo.IdTipoAto,
                        //logArquivoModeloDocxViewModel = 
                        NomeModelo = arquivoModelo.NomeModelo,
                        CaminhoEArquivo = arquivoModelo.CaminhoEArquivo
                    };

                    if (arquivoViewModel == null)
                    {
                        return new HttpStatusCodeResult(HttpStatusCode.NotFound);
                    }


                    return View(arquivoViewModel);
                }
                catch (Exception)
                {
                    //System.Diagnostics.Debug.WriteLine(ex.Message);
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
                    using (var appService = new AppServiceArquivoModeloDocx(this.UnitOfWorkDataBaseCartorio, this.UnitOfWorkDataBaseCartorioNew))
                    {
                        //Cadastro de log
                        LogArquivoModeloDocx logArquivo = new LogArquivoModeloDocx();
                        logArquivo.IdUsuario = UsuarioAtual.Id;
                        logArquivo.UsuarioSistOperacional = System.Security.Principal.WindowsIdentity.GetCurrent().Name;  // HttpContext.Current.User.Identity.Name; //  HttpContext.User.Identity.Name;
                        logArquivo.IP = arquivoModeloDocxViewModel.IpLocal;

                        appService.EditarModelo(new DtoArquivoModeloDocxModel()
                        {
                            Id = arquivoModeloDocxViewModel.Id,
                            IdContaAcessoSistema = 1,
                            Ativo = true,
                            IdTipoAto = arquivoModeloDocxViewModel.IdTipoAto,
                            CaminhoEArquivo = arquivoModeloDocxViewModel.CaminhoEArquivo, // Path.Combine(Server.MapPath("~/App_Data/Arquivos/Modelos/"), NovoId.ToString() + ".docx"),
                            Files = arquivoModeloDocxViewModel.Files,
                            NomeModelo = arquivoModeloDocxViewModel.NomeModelo,
                            LogArquivo = logArquivo,
                            
                        }, UsuarioAtual.Id);

                    }

                    //UploadArquivo(arquivoModeloDocxViewModel);

                    //ViewBag.resultado = "Arquivo salvo com sucesso!";
                    return RedirectToActionPermanent(nameof(Index));

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
        private void CadastrarLogDownload(string IP, long Id)
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

        //[ValidateAntiForgeryToken]
        public void Desativar([Bind(Include = "Id,Ip")]DadosPostArquivoUsuario dadosPost)
        {
            bool respDesativar;

            using (AppServiceArquivoModeloDocx appService = new AppServiceArquivoModeloDocx(this.UnitOfWorkDataBaseCartorio, this.UnitOfWorkDataBaseCartorioNew))
            {
                respDesativar = appService.Desativar(Convert.ToInt64(dadosPost.Id), this.UsuarioAtual.Id);
            }

            if (!respDesativar)
            {
                throw new Exception("Não foi possível desativar o modelo");
            }

        }


        public FileResult DownloadFile([Bind(Include = "Id,Ip")]DadosPostArquivoUsuario dadosPost)
        {
            string fileName = dadosPost.Id.ToString();
            string filePath = Server.MapPath($"~/App_Data/Arquivos/Modelos/{dadosPost.Id}.docx");
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
