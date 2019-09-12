using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using AutoMapper;
using Domain.CartNew.Enumerations;
using Domain.CartNew.Entities;
using Domain.CartNew.Interfaces.UnitOfWork;
using Cartorio11RI.Controllers.Base;
using Cartorio11RI.ViewModels;
using AppServCart11RI.AppServices;
using Dto.CartNew.Entities.Cart_11RI.Diversos;
using Dto.CartNew.Entities.Cart_11RI;
using System.Net.Sockets;
using Domain.CartNew.Entities.Diversos;

namespace Cartorio11RI.Controllers
{
    /// <summary>
    /// Modelos de documento do word docx
    /// </summary>
    [Authorize]
    public class ModelosController : CartorioBaseController
    {
        public ModelosController(IUnitOfWorkDataBaseCartNew UfwCartNew = null) : base(UfwCartNew)
        {
            //
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
            IEnumerable<ModeloDocxListViewModel> listaArquivoModeloDocxListViewModel = new List<ModeloDocxListViewModel>();

            using (AppServiceModelosDocx appService = new AppServiceModelosDocx(this.UfwCartNew))
            {
                IEnumerable<DtoModeloDocxList> listaDtoModelosDocx = appService.ListarModelosDocx().Where(a => a.Ativo == true);
                listaArquivoModeloDocxListViewModel = Mapper.Map<IEnumerable<DtoModeloDocxList>, IEnumerable<ModeloDocxListViewModel>>(listaDtoModelosDocx);
            }

            return View(listaArquivoModeloDocxListViewModel);
        }

        // GET: Modelo/Novo
        public ActionResult Novo()
        {
            try
            {
                List<TipoAtoList> listaTipoAto = this.UfwCartNew.Repositories.RepositoryTipoAto.ListaTipoAtos(null).ToList();
                ViewBag.listaTipoAto = listaTipoAto; // new SelectList(listaTipoAto, "Id", "Descricao");

                return View();
            }
            catch (Exception)
            {
                return new HttpStatusCodeResult(HttpStatusCode.InternalServerError);
            }
        }

        // POST: Modelo/Novo
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Novo([Bind(Include = "Id,NomeModelo,IdTipoAto,DescricaoTipoAto,Files,LogArquivoModeloDocxViewModel,Arquivo,IpLocal")]ModeloDocxViewModel arquivoModel)
        {
            bool ControllerModelValid = ModelState.IsValid;
            bool success = false;
            string msg = "";
            long? NovoId;

            try
            {
                List<TipoAto> listaTipoAto = this.UfwCartNew.Repositories.GenericRepository<TipoAto>().GetAll().ToList();
                ViewBag.listaTipoAto = new SelectList(listaTipoAto, "Id", "Descricao");

                if (ControllerModelValid)
                {
                    LogModeloDocx logArquivo = new LogModeloDocx();
                    logArquivo.IdUsuario = UsuarioAtual.Id;
                    logArquivo.UsuarioSistOperacional = System.Security.Principal.WindowsIdentity.GetCurrent().Name;  // HttpContext.Current.User.Identity.Name; //  HttpContext.User.Identity.Name;
                    logArquivo.IP = arquivoModel.IpLocal;

                    string filePath = string.Empty;
                    arquivoModel.CaminhoEArquivo = Server.MapPath("~/App_Data/Arquivos/Modelos/");

                    using (AppServiceModelosDocx appService = new AppServiceModelosDocx(this.UfwCartNew))
                    {
                        NovoId = appService.NovoModelo(
                            new DtoModeloDocx()
                            {
                                IdCtaAcessoSist = 1,
                                Ativo = true,
                                IdTipoAto = arquivoModel.IdTipoAto,
                                CaminhoEArquivo = arquivoModel.CaminhoEArquivo, // Path.Combine(Server.MapPath("~/App_Data/Arquivos/Modelos/"), NovoId.ToString() + ".docx"),
                                Files = arquivoModel.Files,
                                NomeModelo = arquivoModel.NomeModelo,
                                LogArquivo = new DtoLogModeloDocx
                                {
                                    Id = logArquivo.Id,
                                    IdModeloDocx = logArquivo.IdModeloDocx,
                                    IdUsuario = logArquivo.IdUsuario,
                                    DataHora = logArquivo.DataHora,
                                    IP = logArquivo.IP,
                                    TipoLogModeloDocx = logArquivo.TipoLogModeloDocx,
                                    UsuarioSistOperacional = logArquivo.UsuarioSistOperacional
                                }
                            },
                            UsuarioAtual.Id
                        );
                    }

                    for (int i = 0; i < arquivoModel.Files.Count; i++)
                    {
                        //Pega os dados do arquivo
                        HttpPostedFileBase arquivo = arquivoModel.Files[i];
                        //arquivo.FileName = "Mod_"+arquivoModel.DescricaoTipoAto+DateTime.Now.ToString("yyyyMMddTHHmmss")
                        var nomeArquivo = Path.GetFileNameWithoutExtension(arquivo.FileName);

                        #region | Gravacao do arquivo fisicamente |
                        // Salva o arquivo fisicamente
                        filePath = Path.Combine(arquivoModel.CaminhoEArquivo, "modelo_" + NovoId.ToString() + ".docx");
                        arquivo.SaveAs(filePath);
                        #endregion
                    }

                    //ModelState.AddModelError(Guid.NewGuid().ToString(), "Erro generico");
                    msg = "Modelo adicionado com sucesso!";
                    success = true;
                }
            }
            catch (Exception ex)
            {
                msg = "Não foi possível salvar! [" + ex.Message + "]";
                ModelState.AddModelError(Guid.NewGuid().ToString(), msg);
                //System.Diagnostics.Debug.WriteLine("ArquivosController Exception: " + ex.Message);
                //return new HttpStatusCodeResult(HttpStatusCode.InternalServerError);
                //return RedirectToAction("InternalServerError", "Adm", new { excecao = ex });

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

            return View(arquivoModel);

        }

        // GET: Modelo/Editar/{ID}
        public ActionResult Editar(long? Id)
        {
            if (Id.HasValue && Id > 0)
            {
                try
                {
                    ModeloDocx arquivoModelo = this.UfwCartNew.Repositories.RepositoryModeloDocx.GetById(Id);
                    ModeloDocxViewModel arquivoViewModel = new ModeloDocxViewModel(this.IdCtaAcessoSist)
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
        public ActionResult Editar(ModeloDocxViewModel arquivoModeloDocxViewModel)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    // Fazendo Upload do arquivo
                    using (var appService = new AppServiceModelosDocx(this.UfwCartNew))
                    {
                        //Cadastro de log
                        LogModeloDocx logArquivo = new LogModeloDocx();
                        logArquivo.IdUsuario = UsuarioAtual.Id;
                        logArquivo.UsuarioSistOperacional = System.Security.Principal.WindowsIdentity.GetCurrent().Name;  // HttpContext.Current.User.Identity.Name; //  HttpContext.User.Identity.Name;
                        logArquivo.IP = arquivoModeloDocxViewModel.IpLocal;

                        appService.EditarModelo(new DtoModeloDocx()
                        {
                            Id = arquivoModeloDocxViewModel.Id,
                            IdCtaAcessoSist = 1,
                            Ativo = true,
                            IdTipoAto = arquivoModeloDocxViewModel.IdTipoAto,
                            CaminhoEArquivo = arquivoModeloDocxViewModel.CaminhoEArquivo, // Path.Combine(Server.MapPath("~/App_Data/Arquivos/Modelos/"), NovoId.ToString() + ".docx"),
                            Files = arquivoModeloDocxViewModel.Files,
                            NomeModelo = arquivoModeloDocxViewModel.NomeModelo,
                            LogArquivo = new DtoLogModeloDocx
                            {
                                Id = logArquivo.Id,
                                IdModeloDocx = logArquivo.IdModeloDocx,
                                IdUsuario = logArquivo.IdUsuario,
                                DataHora = logArquivo.DataHora,
                                IP = logArquivo.IP,
                                TipoLogModeloDocx = logArquivo.TipoLogModeloDocx,
                                UsuarioSistOperacional = logArquivo.UsuarioSistOperacional
                            }
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

        #region | METODOS COMPARTILHADOS |
        private void CadastrarLogDownload(string IP, long Id)
        {
            var arquivolog = new LogModeloDocx()
            {
                IdModeloDocx = Id,
                IdUsuario = UsuarioAtual.Id,
                DataHora = DateTime.Now,
                IP = IP,
                TipoLogModeloDocx = TipoLogModeloDocx.Download
            };

            //todo: Ronaldo criar rotina de cadastro 

            return;
        }

        //[ValidateAntiForgeryToken]
        public void Desativar([Bind(Include = "Id, Ip")]DadosPostModeloDocxDownload dadosPost)
        {
            bool respDesativar;

            using (AppServiceModelosDocx appService = new AppServiceModelosDocx(this.UfwCartNew))
            {
                respDesativar = appService.Desativar(Convert.ToInt64(dadosPost.Id), this.UsuarioAtual.Id);
            }

            if (!respDesativar)
            {
                throw new Exception("Não foi possível desativar o modelo");
            }
        }

        public FileResult DownloadFile([Bind(Include = "Id, Ip")]DadosPostModeloDocxDownload dadosPost)
        {
            string fileName = "modelo_"+dadosPost.Id.ToString()+".docx";
            string filePath = Server.MapPath($"~/App_Data/Arquivos/Modelos/modelo_{dadosPost.Id}.docx");

            try
            {
                string ipAddress = dadosPost.Ip;
                if (string.IsNullOrEmpty(ipAddress))
                {

                    var host = Dns.GetHostEntry(Dns.GetHostName());
                    foreach (var ip in host.AddressList)
                    {
                        if (ip.AddressFamily == AddressFamily.InterNetwork)
                        {
                            ipAddress = ip.ToString();
                        }
                    }
                }

                byte[] fileBytes = System.IO.File.ReadAllBytes(filePath);

                //Cadastro de LOG
                CadastrarLogDownload(ipAddress, dadosPost.Id);

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
