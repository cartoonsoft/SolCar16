using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web;
using AutoMapper;
using LibFunctions.Functions.DatesFunc;
using AppServices.Cartorio.AppServices.Base;
using AppServices.Cartorio.Interfaces;
using Domain.Cartorio.enums;
using Domain.Cartorio.Interfaces.UnitOfWork;
using Dto.Cartorio.Entities.Cadastros;
using Dto.Cartorio.Entities.Diversos;
using Domain.Cartorio.Entities.Diversas;
using System.Globalization;
using Domain.Car16.Entities.Car16New;
using Domain.Car16.Entities.Car16;

namespace AppServices.Cartorio.AppServices
{
    public class AppServiceArquivoModeloDocx : AppServiceCartorioNew<DtoArquivoModeloDocxModel, ArquivoModeloDocx>, IAppServiceArquivoModeloDocx
    {
        //private List<CamposArquivoModeloDocx> listaCamposArquivoModeloDocx = null;

        public AppServiceArquivoModeloDocx(IUnitOfWorkDataBaseCartorio UfwCart, IUnitOfWorkDataBaseCartorioNew UfwCartNew) : base(UfwCart, UfwCartNew)
        {
            //
        }

        private List<DtoCamposValor> GetCamposPrenotacao(long? IdTipoAto, long? IdPrenotacao, long? IdMatricula)
        {
            DateTime dataTmp = DateTime.ParseExact("01/01/1800", "dd/MM/yyyy", CultureInfo.InvariantCulture);
            string valorTmp = string.Empty;

            List<DtoCamposValor> listaTmp = new List<DtoCamposValor>();
            List<CamposArquivoModeloDocx> listaCampos = this.UfwCartNew.Repositories.RepositoryArquivoModeloDocx.GetListaCamposIdTipoAto(IdTipoAto).Where(l => l.Entidade == "PRENOTACAO").ToList();

            PREMAD premad = this.UfwCart.Repositories.GenericRepository<PREMAD>().
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
            List<CamposArquivoModeloDocx> listaCampos = this.UfwCartNew.Repositories.RepositoryArquivoModeloDocx.GetListaCamposIdTipoAto(IdTipoAto).Where(l => l.Entidade == "IMOVEL").ToList();

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

        public long? NovoModelo(DtoArquivoModeloDocxModel dtoArq, string IdUsuario)
        {
            long? NovoId = null;

            try
            {
                // Criando objeto do arquivo 
                ArquivoModeloDocx arquivoModelo = new ArquivoModeloDocx
                {
                    Id = dtoArq.Id,
                    IdContaAcessoSistema = dtoArq.IdContaAcessoSistema,
                    Ativo = dtoArq.Ativo,
                    IdTipoAto = dtoArq.IdTipoAto,
                    IdUsuarioCadastro = IdUsuario,
                    //ArquivoBytes = dtoArq.ArquivoByte,
                    CaminhoEArquivo = dtoArq.CaminhoEArquivo,
                    NomeModelo = dtoArq.NomeModelo
                };

                // Registro de Log                
                LogArquivoModeloDocx logArquivoModeloDocx = new LogArquivoModeloDocx()
                {
                    IdArquivoModeloDocx = arquivoModelo.Id ?? 0,
                    IdUsuario = IdUsuario,
                    DataHora = DateTime.Now,
                    UsuarioSistOperacional = dtoArq.LogArquivo.UsuarioSistOperacional,
                    IP = dtoArq.LogArquivo.IP,
                    TipoLogArquivoModeloDocx = TipoLogArquivoModeloDocx.Upload
                };

                NovoId = this.DsFactoryCartNew.ArquivoModeloDocxDs.NovoModelo(arquivoModelo, logArquivoModeloDocx, IdUsuario);
            }
            catch (Exception ex)
            {
                //Console.WriteLine(ex.Message);
                throw new Exception(ex.Message);
            }

            return NovoId;
        }

        public void EditarModelo(DtoArquivoModeloDocxModel dtoArq, string IdUsuario)
        {
            try
            {
                ArquivoModeloDocx arquivoModelo = new ArquivoModeloDocx
                {
                    Id = dtoArq.Id,
                    IdContaAcessoSistema = dtoArq.IdContaAcessoSistema,
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
                LogArquivoModeloDocx logArquivoModeloDocx = new LogArquivoModeloDocx()
                {
                    IdArquivoModeloDocx = Convert.ToInt64(dtoArq.Id),
                    IdUsuario = IdUsuario,
                    DataHora = DateTime.Now,
                    UsuarioSistOperacional = dtoArq.LogArquivo.UsuarioSistOperacional,
                    IP = dtoArq.LogArquivo.IP,
                    TipoLogArquivoModeloDocx = TipoLogArquivoModeloDocx.Upload
                };

                logArquivoModeloDocx.Id = this.DsFactoryCartNew.ArquivoModeloDocxDs.EditarModelo(arquivoModelo, logArquivoModeloDocx, IdUsuario);
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
                resposta = this.DsFactoryCartNew.ArquivoModeloDocxDs.Desativar(Id, IdUsuario);
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
                throw new Exception(ex.Message);
            }

            return resposta;
        }
        
        public IEnumerable<DtoArquivoModeloDocxList> ListarArquivoModeloDocx(long? IdTipoAto = null)
        {
            IEnumerable<ArquivoModeloDocxList> listaDomain = this.DsFactoryCartNew.ArquivoModeloDocxDs.ListarArquivoModeloDocx(IdTipoAto);
            IEnumerable <DtoArquivoModeloDocxList> listaDto = Mapper.Map<IEnumerable<ArquivoModeloDocxList>, IEnumerable<DtoArquivoModeloDocxList>>(listaDomain);

            return listaDto;
        }

        public IEnumerable<DtoArquivoModeloSimplificadoDocxList> ListarArquivoModeloSimplificado(long? IdTipoAto = null)
        {
            IEnumerable<ArquivoModeloSimplificadoDocxList> listaDomain = this.DsFactoryCartNew.ArquivoModeloDocxDs.ListarArquivoModeloSimplificadoDocx(IdTipoAto);
            IEnumerable<DtoArquivoModeloSimplificadoDocxList> listaDto = Mapper.Map<IEnumerable<ArquivoModeloSimplificadoDocxList>, IEnumerable<DtoArquivoModeloSimplificadoDocxList>>(listaDomain);

            return listaDto;
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
            
            //pessoas
            dtoTmp.Pessoas.AddRange(this.DsFactoryCartNew.PessoaDs.GetListaOutorgadosOutorgantes(listIdsPessoas, IdTipoAto, IdPrenotacao??0));

            //Imovel
            dtoTmp.listaCamposValor.AddRange(GetCamposImovel(IdTipoAto, IdPrenotacao, IdMatricula));

            return dtoTmp;
        }

    }
}
