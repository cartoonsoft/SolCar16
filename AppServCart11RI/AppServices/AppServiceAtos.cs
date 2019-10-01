using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AppServ.Core.AppServices;
using AppServices.Cartorio.Interfaces;
using AutoMapper;
using Domain.Cart11RI.Entities;
using Domain.CartNew.Entities;
using Domain.CartNew.Enumerations;
using Domain.CartNew.Interfaces.UnitOfWork;
using Dto.CartNew.Entities.Cart_11RI;
using Dto.CartNew.Entities.Cart_11RI.Diversos;

namespace AppServCart11RI.AppServices
{
    public class AppServiceAtos : AppServiceCartorio<DtoAto, Ato>, IAppServiceAtos
    {
        private List<DtoPessoaPesxPre> listaPessoasPrenotacao = null;  //PESXPRE

        public AppServiceAtos(IUnitOfWorkDataBaseCartNew UfwCartNew) : base(UfwCartNew)
        {
            //
            
        }

        #region Private Methods
        private List<DtoCamposValor> GetCamposPrenotacao(long? IdTipoAto, long? IdPrenotacao, long? IdMatricula)
        {
            DateTime dataTmp = DateTime.ParseExact("01/01/1800", "dd/MM/yyyy", CultureInfo.InvariantCulture);
            string valorTmp = string.Empty;

            List<DtoCamposValor> listaTmp = new List<DtoCamposValor>();
            List<CamposModeloDoc> listaCampos = this.UfwCartNew.Repositories.RepositoryModeloDocx.GetListaCamposIdTipoAto(IdTipoAto).Where(l => l.Entidade == "PRENOTACAO").ToList();

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
            List<CamposModeloDoc> listaCampos = this.UfwCartNew.Repositories.RepositoryModeloDocx.GetListaCamposIdTipoAto(IdTipoAto).Where(l => l.Entidade == "IMOVEL").ToList();

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
        #endregion

        public DtoAtoCadastro NovoAto(DtoAto Ato, string textoHtml)
        {
            throw new NotImplementedException();
        }

        public bool EditarAto(long IdAto, string textoHtml)
        {
            throw new NotImplementedException();
        }

        public void ConferirAto(long IdAto, TipoConferenciaAto tipoConferencia)
        {
            throw new NotImplementedException();
        }

        public void Desativar(long IdAto)
        {
            throw new NotImplementedException();
        }

        public bool FinalizarAto(long IdAto)
        {
            throw new NotImplementedException();
        }

        public IEnumerable<DtoAtoDocx> GerarFichas(long IdAto)
        {
            throw new NotImplementedException();
        }

        public void ImprimirAto(long IdAto)
        {
            throw new NotImplementedException();
        }

        public void ImprimirFicha(long IdDocx)
        {
            throw new NotImplementedException();
        }

        public void UploadFicha(long IdDocx)
        {
            throw new NotImplementedException();
        }

        public void Bloquear(long IdAto)
        {
            throw new NotImplementedException();
        }

        /// <summary>
        /// Listar atos de um período
        /// </summary>
        /// <param name="dataIni"></param>
        /// <param name="dataFim"></param>
        /// <param name="IdUsuario"></param>
        /// <returns></returns>
        public IEnumerable<DtoAtoList> GetListaAtos(DateTime dataIni, DateTime dataFim, string IdUsuario = null)
        {
            IEnumerable<DtoAtoList> lista = new List<DtoAtoList>();
            //todo: ronaldo fazer GetListAtos

            return lista;
        }

        /// <summary>
        /// Busca Dados do imovel por prenotação
        /// </summary>
        /// <param name="matriculaPrenotacao"></param>
        /// <returns></returns>
        public DtoPREIMO GetDadosImovelPrenotacao(long numPrenotacao)
        {
            DtoPREIMO dtoPreimo = new DtoPREIMO();
            try
            {
                PREIMO preimo = this.UfwCartNew.Repositories.GenericRepository<PREIMO>().GetWhere(a => a.SEQPRE == numPrenotacao).FirstOrDefault();
                dtoPreimo = Mapper.Map<PREIMO, DtoPREIMO>(preimo);
            }
            catch (Exception ex)
            {
                throw new Exception("Falha GetDadosImovelPrenotacao: " + ex.Message);
            }
            
            return dtoPreimo;
        }

        /// <summary>
        /// Lista de Pessoa por Prenotação
        /// </summary>
        /// <param name="numeroPrenotacao"></param>
        /// <returns></returns>
        public IEnumerable<DtoPessoaPesxPre> GetPessoasPrenotacao(long numeroPrenotacao)
        {
            if (listaPessoasPrenotacao == null)
            {
                try
                {
                    listaPessoasPrenotacao = this.DsFactoryCartNew.AtoDs.GetPessoasPrenotacao(numeroPrenotacao).ToList();
                }
                catch (Exception ex)
                {
                    throw new Exception("Falha GetPessoasPrenotacao: " + ex.Message);
                }
            }

            return listaPessoasPrenotacao;
        }

        public IEnumerable<DtoDocxList> GetListDtoDocxAto(string NumMatricula)
        {
            IEnumerable<DtoDocxList> lista = new List<DtoDocxList>();

            try
            {
                lista = this.DsFactoryCartNew.AtoDs.GetListDtoDocxAto(NumMatricula);
            }
            catch (Exception ex)
            {
                throw new Exception("Falha GetListDtoDocxAto: " + ex.Message);
            }

            return lista;
        }

    }
}
