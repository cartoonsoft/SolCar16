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
                IEnumerable<DtoArquivoModeloDocxList> listaDtoArquivoModelosDocx = appService.ListarArquivoModeloDocx().Where(a => a.Ativo == true);
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
        public ActionResult Cadastrar([Bind(Include = "NomeModelo,IdTipoAto,Files,LogArquivoModeloDocxViewModel")]ArquivoModeloDocxViewModel arquivoModel)
        {
            try
            {
                List<TipoAto> listaTipoAto = this.UnitOfWorkDataBseCar16New.Repositories.GenericRepository<TipoAto>().GetAll().ToList();
                ViewBag.listaTipoAto = new SelectList(listaTipoAto, "Id", "Descricao");

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
                        CadastraArquivoModeloDocx(arquivoModel);
                        #endregion
                    }
                    ViewBag.resultado = "Arquivo salvo com sucesso!";
                }

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

        private int? CadastraArquivoModeloDocx(ArquivoModeloDocxViewModel arquivoModel)
        {
            int? resultado;

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
                resultado = unitOfWork.Commit();
            }

            return resultado;
        }

        public void Desativar([Bind(Include = "Id,Ip")]DadosPostArquivoUsuario dadosPost)
        {
            //int respDesativar;
            //using (UnitOfWorkDataBaseCar16 unitOfWork = new UnitOfWorkDataBaseCar16(BaseDados.DesenvDezesseisNew))
            //{
            //    using (AppServiceArquivoModeloDocx appService = new AppServiceArquivoModeloDocx(unitOfWork))
            //    {
            //         respDesativar = appService.DesativarModelo(Convert.ToInt64(dadosPost.Id));
            //    }
            //    if (respDesativar == 1)
            //    {
            //        unitOfWork.Commit();
            //    }
            //    else
            //    {
            //        throw new Exception("Não foi possível desativar o modelo");
            //    }
            //}
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
