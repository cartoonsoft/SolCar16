using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web;
using System.Globalization;
using Domain.CartNew.Entities;
using Domain.CartNew.Enumerations;
using Domain.CartNew.Interfaces.UnitOfWork;
using Domain.Cart11RI.Entities;
using Dto.CartNew.Entities.Cart_11RI;
using Dto.CartNew.Entities.Cart_11RI.Diversos;
using AppServ.Core.AppServices;
using AppServices.Cartorio.Interfaces;
using LibFunctions.Functions.DatesFunc;

namespace AppServCart11RI.AppServices
{
    public class AppServiceModeloDocx : AppServiceCartorio<DtoModeloDocx, ModeloDocx>, IAppServiceModeloDocx
    {
        //private List<CamposArquivoModeloDocx> listaCamposArquivoModeloDocx = null;

        public AppServiceModeloDocx(IUnitOfWorkDataBaseCartNew UfwCartNew) : base(UfwCartNew)
        {
            //
        }

        private List<DtoCamposValor> GetCamposPrenotacao(long? IdTipoAto, long? IdPrenotacao, long? IdMatricula)
        {
            DateTime dataTmp = DateTime.ParseExact("01/01/1800", "dd/MM/yyyy", CultureInfo.InvariantCulture);
            string valorTmp = string.Empty;

            List<DtoCamposValor> listaTmp = new List<DtoCamposValor>();
            List<CamposModeloDocx> listaCampos = this.UfwCartNew.Repositories.RepositoryModeloDocx.GetListaCamposIdTipoAto(IdTipoAto).Where(l => l.Entidade == "PRENOTACAO").ToList();

            PREMAD premad = this.UfwCartNew.Repositories.GenericRepository<PREMAD>().
                GetWhere(p => (p.SEQPRED == IdPrenotacao) && (p.TIPODATA.Trim() == "R")).OrderByDescending(o => o.DATA).FirstOrDefault();

            if (premad != null)
            {
                foreach (var item in listaCampos)
                {
                    var prop = premad.GetType().GetProperty(item.Campo);

                    if (prop != null)
                    {
                        valorTmp = (prop.GetValue(premad) == null) ? "" : prop.GetValue(premad).ToString();

                        if (item.Campo == "IdPrenotacao")
                        {
                            valorTmp = IdPrenotacao.ToString();
                        }

                        if (item.Campo == "DATA")
                        {
                            valorTmp = dataTmp.AddDays(premad.DATA).ToString("dd/MM/yyyy");
                        }

                        listaTmp.Add(new DtoCamposValor
                        {
                            Campo = item.NomeCampo,
                            Valor = valorTmp
                        });
                    }
                }
            }

            return listaTmp;
        }

        private List<DtoCamposValor> GetCamposImovel(long? IdTipoAto, long? IdPrenotacao, long? IdMatricula)
        {
            List<DtoCamposValor> listaTmp = new List<DtoCamposValor>();
            List<CamposModeloDocx> listaCampos = this.UfwCartNew.Repositories.RepositoryModeloDocx.GetListaCamposIdTipoAto(IdTipoAto).Where(l => l.Entidade == "IMOVEL").ToList();

            PREIMO Imovel = this.UfwCartNew.Repositories.GenericRepository<PREIMO>().GetWhere(i => i.SEQPRE == IdPrenotacao && i.MATRI == IdMatricula).FirstOrDefault();

            if (Imovel != null)
            {
                foreach (var item in listaCampos)
                {
                    var prop = Imovel.GetType().GetProperty(item.Campo);

                    if (prop != null)
                    {
                        listaTmp.Add(new DtoCamposValor
                        {
                            Campo = item.NomeCampo,
                            Valor = (prop.GetValue(Imovel) == null) ? "" : prop.GetValue(Imovel).ToString()
                        });
                    }
                }
            }

            return listaTmp;
        }

        public long? NovoModelo(DtoModeloDocx dtoArq, string IdUsuario)
        {
            long? NovoId = null;

            try
            {
                // Criando objeto do arquivo 
                ModeloDocx arquivoModelo = new ModeloDocx
                {
                    Id = dtoArq.Id,
                    IdCtaAcessoSist = dtoArq.IdCtaAcessoSist,
                    Ativo = dtoArq.Ativo,
                    IdTipoAto = dtoArq.IdTipoAto,
                    IdUsuarioCadastro = IdUsuario,
                    //ArquivoBytes = dtoArq.ArquivoByte,
                    CaminhoEArquivo = dtoArq.CaminhoEArquivo,
                    NomeModelo = dtoArq.NomeModelo
                };

                // Registro de Log                
                LogModeloDocx logArquivoModeloDocx = new LogModeloDocx()
                {
                    IdModeloDocx = arquivoModelo.Id ?? 0,
                    IdUsuario = IdUsuario,
                    DataHora = DateTime.Now,
                    UsuarioSistOperacional = dtoArq.LogArquivo.UsuarioSistOperacional,
                    IP = dtoArq.LogArquivo.IP,
                    TipoLogModeloDocx = TipoLogModeloDocx.Upload
                };

                //NovoId = this.DsFactoryBase .NovoModelo(arquivoModelo, logArquivoModeloDocx, IdUsuario);
            }
            catch (Exception ex)
            {
                //Console.WriteLine(ex.Message);
                throw new Exception(ex.Message);
            }

            return NovoId;
        }

        public void EditarModelo(DtoModeloDocx dtoArq, string IdUsuario)
        {
            try
            {
                ModeloDocx arquivoModelo = new ModeloDocx
                {
                    Id = dtoArq.Id,
                    IdCtaAcessoSist = dtoArq.IdCtaAcessoSist,
                    Ativo = dtoArq.Ativo,
                    IdTipoAto = dtoArq.IdTipoAto,
                    IdUsuarioAlteracao = IdUsuario,
                    //ArquivoBytes = dtoArq.ArquivoByte,
                    CaminhoEArquivo = dtoArq.CaminhoEArquivo,
                    NomeModelo = dtoArq.NomeModelo
                };

                HttpPostedFileBase arquivo = dtoArq.Files[0];
                #region | Gravacao do arquivo fisicamente |
                // Salva o arquivo fisicamente
                arquivo.SaveAs(dtoArq.CaminhoEArquivo);
                #endregion

                // Registro de Log                
                LogModeloDocx logModeloDocx = new LogModeloDocx()
                {
                    IdModeloDocx = Convert.ToInt64(dtoArq.Id),
                    IdUsuario = IdUsuario,
                    DataHora = DateTime.Now,
                    UsuarioSistOperacional = dtoArq.LogArquivo.UsuarioSistOperacional,
                    IP = dtoArq.LogArquivo.IP,
                    TipoLogModeloDocx = TipoLogModeloDocx.Upload
                };

                //logArquivoModeloDocx.Id = this.DsFactoryCartNew.ArquivoModeloDocxDs.EditarModelo(arquivoModelo, logModeloDocx, IdUsuario);
            }
            catch (Exception)
            {
                throw;
            }
        }

        public bool Desativar(long Id, string IdUsuario)
        {
            bool resposta = false;

            try
            {
                //resposta = this.DsFactoryCartNew.ArquivoModeloDocxDs.Desativar(Id, IdUsuario);
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
                throw new Exception(ex.Message);
            }

            return resposta;
        }
        
        public IEnumerable<DtoModeloDocxList> ListarModeloDocx(long? IdTipoAto = null)
        {
            IEnumerable<DtoModeloDocxList> listaDs = this.DsFactoryCartNew.ArquivoModeloDocxDs.ListarArquivoModeloDocx(IdTipoAto);
            return listaDs;
        }

        public IEnumerable<DtoModeloDocxSimplificadoList> ListarModeloSimplificado(long? IdTipoAto = null)
        {
            //IEnumerable<ArquivoModeloSimplificadoDocxList> listaDomain = this.DsFactoryCartNew.ArquivoModeloDocxDs.ListarArquivoModeloSimplificadoDocx(IdTipoAto);
            //IEnumerable<DtoArquivoModeloSimplificadoDocxList> listaDto = Mapper.Map<IEnumerable<ArquivoModeloSimplificadoDocxList>, IEnumerable<DtoArquivoModeloSimplificadoDocxList>>(listaDomain);

            return null; // listaDto;
        }

        public DtoDadosImovel GetDatosImovel(long[] listIdsPessoas, long? IdTipoAto, long? IdPrenotacao, long? IdMatricula)
        {
            DtoDadosImovel dtoTmp = new DtoDadosImovel();

            //Geral 
            dtoTmp.listaCamposValor.Add(new DtoCamposValor
            {
                Campo = "DataAtualExtenso",
                Valor = DataHelper.GetDataPorExtenso("São Paulo")
            });

            //Prenotacao
            dtoTmp.listaCamposValor.AddRange(GetCamposPrenotacao(IdTipoAto, IdPrenotacao, IdMatricula));

            //Matricula
            dtoTmp.listaCamposValor.Add(new DtoCamposValor
            {
                Campo = "Matricula",
                Valor = IdMatricula.ToString()
            });
            
            //todo: ronaldo arrumar
            //pessoas
            //dtoTmp.Pessoas.AddRange(this.DsFactoryCartNew.PessoaCartNewDs.GetListaOutorgadosOutorgantes(listIdsPessoas, IdTipoAto, IdPrenotacao??0));

            //Imovel
            dtoTmp.listaCamposValor.AddRange(GetCamposImovel(IdTipoAto, IdPrenotacao, IdMatricula));

            return dtoTmp;
        }

    }
}
