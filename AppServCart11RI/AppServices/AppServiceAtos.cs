using System;
using System.Collections.Generic;
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

        public void Bloquear(long IdAto)
        {
            throw new NotImplementedException();
        }

        /// <summary>
        /// Busca Dados do imivel por matricula ou prenotação
        /// </summary>
        /// <param name="matriculaPrenotacao"></param>
        /// <returns></returns>
        public DtoPREIMO GetDadosImovel(long matriculaPrenotacao)
        {
            DtoPREIMO dtoPreimo = new DtoPREIMO();
            try
            {
                PREIMO preimo = this.UfwCartNew.Repositories.GenericRepository<PREIMO>().GetWhere(a => a.SEQPRE == matriculaPrenotacao).FirstOrDefault();

                if (preimo == null)
                {
                    preimo = this.UfwCartNew.Repositories.GenericRepository<PREIMO>().GetWhere(a => a.MATRI == matriculaPrenotacao).FirstOrDefault();

                }
                dtoPreimo = Mapper.Map<PREIMO, DtoPREIMO>(preimo);

                dtoPreimo.resposta = true;
                dtoPreimo.msg = "Dados do imóvel obtidos com sucesso";
            }
            catch (Exception ex)
            {
                dtoPreimo.resposta = false;
                dtoPreimo.msg = "Falha ao obter dados! [" + ex.Message + "]";
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
                catch 
                {
                    //
                }
            }

            return listaPessoasPrenotacao;
        }

        public void ConferirAto(long IdAto, TipoConferenciaAto tipoConferencia)
        {
            throw new NotImplementedException();
        }

        public void Desativar(long IdAto)
        {
            throw new NotImplementedException();
        }

        public bool EditarAto(long IdAto, string textoHtml)
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

        public IEnumerable<DtoAtoList> ListarAtos(DateTime dataIni, DateTime dataFim, string IdUsuario = null)
        {
            IEnumerable<DtoAtoList> lista = new List<DtoAtoList>();
            //todo: ronaldo fazer ListarAtos

            return lista;
        }

        public DtoAtoCadastro NovoAto(DtoAto Ato, string textoHtml)
        {
            throw new NotImplementedException();
        }

        public void UploadFicha(long IdDocx)
        {
            throw new NotImplementedException();
        }



    }
}
