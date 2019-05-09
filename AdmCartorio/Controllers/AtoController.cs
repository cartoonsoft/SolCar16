using AutoMapper;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Net;
using System.Text;
using System.Web.Mvc;
using Xceed.Words.NET;
using AdmCartorio.Controllers.Base;
using AdmCartorio.ViewModels;
using AppServices.Car16.AppServices;
using Domain.Car16.Entities.Car16;
using Domain.Car16.Entities.Car16New;
using Domain.Car16.Interfaces.UnitOfWork;
using Dto.Car16.Entities.Cadastros;
using Dto.Car16.Entities.Diversos;

namespace AdmCartorio.Controllers
{
    [Authorize]
    public class AtoController : AdmCartorioBaseController
    {
        #region | Construtores |
        public AtoController() : base(null, null)
        {
            //

        }

        public AtoController(IUnitOfWorkDataBaseCar16 unitOfWorkDataBaseCar16, IUnitOfWorkDataBaseCar16New unitOfWorkDataBaseCar16New) : base(unitOfWorkDataBaseCar16, unitOfWorkDataBaseCar16New)
        {
            //Criar instancia dos seus App services aqui
        }
        #endregion

        // GET: Ato
        public ActionResult Index(MatriculaAtoViewModel modelo)
        {
            IEnumerable<AtoListViewModel> listaAtoListViewModel = new List<AtoListViewModel>();

            using (AppServiceAto appService = new AppServiceAto(this.UnitOfWorkDataBaseCar16New))
            {
                IEnumerable<DtoAtoList> listaDto = appService.ListarAtos(null, null);
                listaAtoListViewModel = Mapper.Map<IEnumerable<DtoAtoList>, IEnumerable<AtoListViewModel>>(listaDto);
            }
            
            return View(listaAtoListViewModel);
        }

        #region | CADASTRO |
        public ActionResult Cadastrar()
        {
            var dados = new CadastroDeAtoViewModel();

            return View(dados);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        [ValidateInput(false)]
        public ActionResult Cadastrar(CadastroDeAtoViewModel modelo)
        {
            string filePath = Server.MapPath($"~/App_Data/Arquivos/{modelo.PREIMO.MATRI}.docx");
            bool respEscreverWord = false;
            try
            {

                if (modelo.Ato == null)
                {
                    ViewBag.erro = "O Ato é obrigatório";
                    return View(nameof(Cadastrar), modelo);
                }

                //Ajusta a string de ato
                modelo.Ato = RemoveUltimaMarcacao(modelo.Ato);

                if (ModelState.IsValid)
                {

                    //Representa o documento e o numero de pagina
                    DtoCadastroDeAto modeloDto = Mapper.Map<CadastroDeAtoViewModel, DtoCadastroDeAto>(modelo);
                    long? numSequenciaAto = null;

                    if (modelo.NumSequencia == 0)
                    {
                        using (var appService = new AppServiceAto(this.UnitOfWorkDataBaseCar16New))
                        {
                            numSequenciaAto = appService.GetNumSequenciaAto(Convert.ToInt64(modelo.PREIMO.MATRI));
                            numSequenciaAto = numSequenciaAto + 1 ?? 1;
                        }
                    }
                    else
                    {
                        numSequenciaAto = modelo.NumSequencia;
                    }

                    using (var appService = new AppServiceCadastroDeAto(this.UnitOfWorkDataBaseCar16New))
                    {
                        
                        respEscreverWord = appService.EscreverAtoNoWord(modeloDto, filePath, Convert.ToInt64(numSequenciaAto));
                    }
                    if (respEscreverWord)
                    {
                        // Gravar no banco o array de bytes
                        var arrayBytesNovo = System.IO.File.ReadAllBytes(filePath);
                        // Pegar a ultima "versão" do ato e somar

                        // Gravar o ato e buscar o selo e gravar o selo
                        Ato ato = new Ato()
                        {
                            Ativo = true,
                            Bloqueado = false,
                            IdPrenotacao = modelo.PREIMO.SEQPRE,
                            IdTipoAto = modelo.IdTipoAto,
                            NomeArquivo = $"{ modelo.PREIMO.MATRI }.docx",
                            Observacao = "Cadastro de teste",
                            NumMatricula = modelo.PREIMO.MATRI.ToString(),
                            IdUsuarioAlteracao = this.UsuarioAtual.Id,
                            IdContaAcessoSistema = 1
                        };

                        this.UnitOfWorkDataBaseCar16New.Repositories.GenericRepository<Ato>().Add(ato);
                        //this.UnitOfWorkDataBaseCar16New.Commit();

                    }
                    else
                    {
                        //Teve algum erro ao escrever o documento no WORD
                        return new HttpStatusCodeResult(HttpStatusCode.InternalServerError);
                    }
                    ViewBag.sucesso = "Ato cadastrado com sucesso!";
                    return View(nameof(Cadastrar), modelo);
                }

                ViewBag.erro = "Erro ao cadastrar o ato!";

                return View(nameof(Cadastrar), modelo);
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
                return new HttpStatusCodeResult(HttpStatusCode.InternalServerError);
                throw;
            }
        }
        #endregion

        #region | EDITAR |

        #endregion

        #region | VIEWS PARCIAIS |
        public PartialViewResult PartialDadosAdicionais()
        {
            return PartialView();
        }
        
        #endregion

        #region | JsonResults e .GET |
        /// <summary>
        /// Função que retorna os arquivos de modelo (JSON)
        /// </summary>
        /// <returns>Lista de arquivos</returns>
        public JsonResult GetModelos()
        {
            using (var appService = new AppServiceArquivoModeloDocx(this.UnitOfWorkDataBaseCar16New))
            {
                var listaDtoArquivoModelosDocx = appService.ListarArquivoModeloSimplificado();
                var listaModelos = Mapper.Map<IEnumerable<DtoArquivoModeloSimplificadoDocxList>, IEnumerable<ArquivoModeloSimplificadoViewModel>>(listaDtoArquivoModelosDocx);
                var jsonResult = JsonConvert.SerializeObject(listaModelos);
                return Json(jsonResult, JsonRequestBehavior.AllowGet);
            }
        }

        public JsonResult GetDadosImovel(long? numeroMatricula = null, long? numeroPrenotacao = null)
        {
            string jsonResult;
            try
            {
                var PREIMO = this.UnitOfWorkDataBaseCar16.Repositories.RepositoryPREIMO.BuscaDadosImovel(numeroPrenotacao, numeroMatricula);
                jsonResult = JsonConvert.SerializeObject(PREIMO);
            }
            catch (Exception)
            {
                jsonResult = "";
               
                //Cadastrar log de erro

            }
            return Json(jsonResult, JsonRequestBehavior.AllowGet);
            
        }

        /// <summary>
        /// Essa função retorna se a pessoa é um Ortogante ou Ortogado
        /// </summary>
        /// <param name="numeroPrenotacao">Numero da prenotação</param>
        /// <returns>JSON</returns>
        public JsonResult GetTipoPessoa(long numeroPrenotacao)
        {
            string jsonResult;
            try
            {
                PESXPRE pessoaPre = this.UnitOfWorkDataBaseCar16.Repositories.GenericRepository<PESXPRE>()
                .GetWhere(n => n.SEQPRE == numeroPrenotacao).OrderByDescending(n => n.SEQPRE).FirstOrDefault();
                PESSOA pessoa = this.UnitOfWorkDataBaseCar16.Repositories.GenericRepository<PESSOA>()
                    .GetWhere(p => p.SEQPES == pessoaPre.SEQPES).OrderByDescending(p => p.SEQPES).FirstOrDefault();

                DadosPessoaViewModel dados = new DadosPessoaViewModel
                {
                    TipoPessoa = pessoaPre.REL == "O" ? "Outorgante" : "Outorgado",
                    BAI = pessoa.BAI,
                    SEQPES = pessoa.SEQPES,
                    CEP = pessoa.CEP,
                    CID = pessoa.CID,
                    ENDER = pessoa.ENDER,
                    NOM = pessoa.NOM,
                    NRO1 = pessoa.NRO1,
                    NRO2 = pessoa.NRO2,
                    TEL = pessoa.TEL,
                    TIPODOC1 = pessoa.TIPODOC1,
                    TIPODOC2 = pessoa.TIPODOC2,
                    UF = pessoa.UF
                };
                jsonResult = JsonConvert.SerializeObject(dados);
            }
            catch (Exception)
            {
                jsonResult = "";
            }
            return Json(jsonResult, JsonRequestBehavior.AllowGet);

        }
        public long GetIdTipoAtoPeloModelo(long idModelo)
        {
            return this.UnitOfWorkDataBaseCar16New.Repositories.RepositoryArquivoModeloDocx
                .GetWhere(i => i.Id == idModelo).FirstOrDefault().IdTipoAto;
            
        }
        public bool ExisteAtoNoBanco(long numeroMatricula)
        {
            try
            {
                using (var appService = new AppServiceAto(this.UnitOfWorkDataBaseCar16New))
                {
                    return appService.ExisteAtoCadastrado(numeroMatricula);
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        #endregion

        /// <summary>
        /// Retorna o numero de Ato do modelo
        /// </summary>
        /// <param name="modelo">Modelo</param>
        /// <returns>N° da Ato</returns>
        public static long GetNumeroAto(MatriculaAtoViewModel modelo)
        {
            return modelo.IdMatricula;
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
        /// Remove a ultima marcação de espaço da string (\n)
        /// </summary>
        /// <param name="ato">ATO como String</param>
        /// <returns>Ato como string</returns>
        private static string RemoveUltimaMarcacao(string ato)
        {
            var atoString = ato.Substring(0, ato.Length - 1);
            atoString = atoString.Replace('\n', ' ');
            return atoString;
        }

        /// <summary>
        /// Função que monta uma string HTML para mostrar na tela exatamente 
        /// oque esta escrito no documento
        /// </summary>
        /// <returns>string HTML</returns>
        public string UsaModeloParaAto([Bind(Include = "ModeloNome,Id")]string ModeloNome, long Id)
        {
            StringBuilder textoFormatado = new StringBuilder();

            //var arquivo = this.UnitOfWorkDataBaseCar16New.Repositories.GenericRepository<ArquivoModeloDocx>().GetById(Id);
            //System.IO.File.WriteAllBytes(ModeloNome + ".docx", arquivo.BytesArray);

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
    }
}
