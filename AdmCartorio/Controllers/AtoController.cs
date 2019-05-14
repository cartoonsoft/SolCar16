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
            string filePath = Server.MapPath($"~/App_Data/Arquivos/Atos/{modelo.PREIMO.MATRI}.docx");
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

                //if (ModelState.IsValid)
                //{

                //Representa o documento e o numero de pagina
                DtoCadastroDeAto modeloDto = Mapper.Map<CadastroDeAtoViewModel, DtoCadastroDeAto>(modelo);
                long? numSequenciaAto = null;

                if (modelo.NumSequencia == 0 && modelo.IdTipoAto != (int)Domain.Car16.enums.TipoAtoEnum.AtoInicial)
                {
                    numSequenciaAto = this.UnitOfWorkDataBaseCar16New.Repositories.RepositoryAto.GetNumSequenciaAto(Convert.ToInt64(modelo.PREIMO.MATRI));
                    numSequenciaAto = numSequenciaAto != null ? numSequenciaAto + 1 : 1;
                }
                else
                {
                    numSequenciaAto = modelo.NumSequencia;
                }

                using (var appService = new AppServiceCadastroDeAto(this.UnitOfWorkDataBaseCar16New))
                {

                        // Gravar o ato e buscar o selo e gravar o selo
                        Ato ato = new Ato()
                        {
                            Ativo = true,
                            Bloqueado = false,
                            IdPrenotacao = 511898,//modelo.PREIMO.SEQPRE,
                            IdTipoAto = modelo.IdTipoAto,
                            NomeArquivo = $"{ modelo.PREIMO.MATRI }.docx",
                            Observacao = "Cadastro de teste",
                            NumMatricula = modelo.PREIMO.MATRI.ToString(),
                            IdUsuarioCadastro = this.UsuarioAtual.Id,
                            IdContaAcessoSistema = 1,
                            NumSequencia = Convert.ToInt64(numSequenciaAto)
                        };

                        this.UnitOfWorkDataBaseCar16New.Repositories.GenericRepository<Ato>().Add(ato);
                        this.UnitOfWorkDataBaseCar16New.SaveChanges();


                    respEscreverWord = appService.EscreverAtoNoWord(modeloDto, filePath, Convert.ToInt64(numSequenciaAto));
                }
                if (respEscreverWord)
                {
                    // Gravar no banco o array de bytes
                    var arrayBytesNovo = System.IO.File.ReadAllBytes(filePath);
                    
                    // Gravar o ato e buscar o selo e gravar o selo
                    Ato ato = new Ato()
                    {
                        Ativo = true,
                        Bloqueado = false,
                        IdPrenotacao = 511898,//modelo.PREIMO.SEQPRE,
                        IdTipoAto = modelo.IdTipoAto,
                        NomeArquivo = $"{ modelo.PREIMO.MATRI }.docx",
                        Observacao = "Cadastro de teste",
                        NumMatricula = modelo.PREIMO.MATRI.ToString(),
                        IdUsuarioCadastro = this.UsuarioAtual.Id,
                        IdContaAcessoSistema = 1,
                        NumSequencia = Convert.ToInt64(numSequenciaAto)
                    };

                    this.UnitOfWorkDataBaseCar16New.Repositories.GenericRepository<Ato>().Add(ato);
                    this.UnitOfWorkDataBaseCar16New.SaveChanges();

                }
                else
                {
                    //Teve algum erro ao escrever o documento no WORD
                    return new HttpStatusCodeResult(HttpStatusCode.InternalServerError);
                }
                ViewBag.sucesso = "Ato cadastrado com sucesso!";
                return View(nameof(Cadastrar), modelo);
                //}

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
        public PartialViewResult PartialDadosPessoas(string listaPessoas)
        {

            var dados = JsonConvert.DeserializeObject<List<DadosPessoaViewModel>>(listaPessoas);
            return PartialView(dados);
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
        /// Essa função retorna uma lista de pessoa por um id de prenotação
        /// </summary>
        /// <param name="numeroPrenotacao">Numero da prenotação</param>
        /// <returns>JSON</returns>
        public JsonResult GetPessoasPremo(long numeroPrenotacao)
        {
            var jsonResult = "";
            try
            {
                using(AppServicePessoa appServicePessoa = new AppServicePessoa(this.UnitOfWorkDataBaseCar16, this.UnitOfWorkDataBaseCar16New))
                {
                    jsonResult = JsonConvert.SerializeObject(appServicePessoa.GetPessoasPrenotacao(numeroPrenotacao));
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex);
                //
            }
            return Json(jsonResult, JsonRequestBehavior.AllowGet);

        }
        public long GetIdTipoAtoPeloModelo(long idModelo)
        {

            return this.UnitOfWorkDataBaseCar16New.Repositories.RepositoryArquivoModeloDocx
                .GetById(idModelo).IdTipoAto;

        }
        public bool ExisteAto(long numeroMatricula)
        {
            try
            {
                string filePath = Server.MapPath($"~/App_Data/Arquivos/Atos/{numeroMatricula}.docx");
                using (FileStream fileStream = new FileStream(filePath, FileMode.Open, FileAccess.Read))
                {

                }
                return true;
            }
            catch (Exception)
            {
                return false;
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
        public string UsaModeloParaAto([Bind(Include = "ModeloNome,IdMatricula,IdPrenotacao,listIdsPessoas,IdTipoAto")]DadosPostModelo DadosPostModelo)
        {
            //using (var appService = new AppServicePessoa(this.UnitOfWorkDataBaseCar16,this.UnitOfWorkDataBaseCar16New))
            //{
            //    DtoDadosImovel dadosImovel = appService.GetCamposModeloMatricula(DadosPostModelo.listIdsPessoas, DadosPostModelo.IdTipoAto, DadosPostModelo.IdPrenotacao, DadosPostModelo.IdMatricula);
            //}

            #region | MOCAR DADOS |
            DtoDadosImovel dadosImovel = new DtoDadosImovel()
            {
                CamposValorDadosImovel = new List<DtoCamposValor>()
                {
                    new DtoCamposValor()
                    {
                       Campo = "Nome",
                       Valor = "Edificio Pedro HP"
                    },
                    new DtoCamposValor()
                    {
                        Campo = "Endereco",
                        Valor = "Rua primeiro"
                    },new DtoCamposValor()
                    {
                        Campo = "Apto",
                        Valor = "Apartamento 1"
                    }

                },
                IdMatricula = Convert.ToInt64(DadosPostModelo.IdMatricula),
                IdPrenotacao = Convert.ToInt64(DadosPostModelo.IdPrenotacao),
                Imovel = new Domain.Car16.Entities.Car16.PREIMO()
                {
                    APTO = "APARTAMENTO 1",
                    BLOCO = "BLOCO A",
                    CONTRIB = "98782398755",
                    EDIF = "EDIFICIO PEDRO HP",
                    ENDER = "RUA DOS PASSAROS",
                    HIPO = 0,
                    INSCR = 123321,
                    LOTE = "LOTE 1",
                    MATRI = Convert.ToInt32(DadosPostModelo.IdMatricula),
                    NUM = "96",
                    OUTROS = "PRIMEIRO EDIFICIO DE TESTE",
                    QUADRA = "",
                    RD = 1,
                    SEQIMO = 1,
                    SEQPRE = Convert.ToInt64(DadosPostModelo.IdPrenotacao),
                    SUBD = 1,
                    TIPO = "T",
                    TITULO = "TITULO",
                    TRANS = 0,
                    VAGA = ""
                },
                Pessoas = new List<DtoPessoaPesxPre>()
                {
                    new DtoPessoaPesxPre()
                    {
                        Bairro = "Caucaia do alto",
                        CEP = 12345623,
                        Cidade = "Cotia",
                        Endereco = "Rua primeiro",
                        IdPessoa = 1,
                        Nome = "Pedro Pires",
                        Numero1 = "555345235",
                        TipoDoc1 = 1,
                        Numero2 = "12312312345",
                        TipoDoc2 = "CPF",
                        Telefone = "99887766",
                        TipoPessoa = "Outorgante",
                        UF = "SP",
                        listaCamposValor = new List<DtoCamposValor>()
                        {
                            new DtoCamposValor()
                            {
                                Campo = "Bairro",
                                Valor = "Caucaia do alto"
                            },
                            new DtoCamposValor()
                            {
                                Campo = "Cidade",
                                Valor = "Cotia"
                            },
                            new DtoCamposValor()
                            {
                                Campo = "Nome",
                                Valor = "Pedro Pires"
                            }
                        }
                    }
                }
            };

            #endregion


            StringBuilder textoFormatado = new StringBuilder();

            string filePath = Server.MapPath($"~/App_Data/Arquivos/Modelos/{DadosPostModelo.ModeloNome}.docx");
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
