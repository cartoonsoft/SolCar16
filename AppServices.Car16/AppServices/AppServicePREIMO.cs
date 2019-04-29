using AppServices.Car16.AppServices.Base;
using AppServices.Car16.Interfaces;
using AutoMapper;
using Domain.Car16.Entities.Car16;
using Domain.Car16.Interfaces.UnitOfWork;
using Dto.Car16.Entities.Cadastros;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AppServices.Car16.AppServices
{
    public class AppServicePREIMO : AppServiceCar16<DtoPREIMO, PREIMO>, IAppServicePREIMO
    {
        public AppServicePREIMO(IUnitOfWorkCar16 unitOfWorkCar16) : base(unitOfWorkCar16)
        {
        }
        /// <summary>
        /// Função que busca os dados do imóvel a partir da matricula ou prenotação
        /// </summary>
        /// <param name="numeroPrenotacao">Número da prenotação</param>
        /// <param name="numeroMatricula">Número da matrícula</param>
        /// <returns>Retorna uma linha de registro</returns>
        public DtoPREIMO BuscaDadosImovel(long? numeroPrenotacao = null, long? numeroMatricula = null)
        {
            IEnumerable<PREIMO> listaImoveis = new List<PREIMO>();
            IEnumerable<DtoPREIMO> listaDto = new List<DtoPREIMO>();


            if (numeroPrenotacao != null && numeroMatricula != null)
            {
                listaImoveis = this.DomainServices.GenericDomainService<PREIMO>()
                    .GetWhereOrderBy(mp => mp.SEQIMO == numeroMatricula && mp.SEQPRE == numeroPrenotacao, mp => mp.SEQPRE, false);
                    
            }
            else if (numeroMatricula != null)
            {
                listaImoveis = this.DomainServices.GenericDomainService<PREIMO>()
                    .GetWhereOrderBy(m => m.SEQIMO == numeroMatricula, mp => mp.SEQPRE);
            }
            else if (numeroPrenotacao != null)
            {
                listaImoveis = this.DomainServices.GenericDomainService<PREIMO>()
                    .GetWhereOrderBy(p => p.SEQPRE == numeroPrenotacao, mp => mp.SEQPRE);
            }
            else
            {
                listaImoveis = this.DomainServices.GenericDomainService<PREIMO>().GetAll().OrderBy(mp => mp.SEQPRE);
            }

            listaDto = Mapper.Map<IEnumerable<PREIMO>, IEnumerable<DtoPREIMO>>(listaImoveis);

            return listaDto.FirstOrDefault();
        }
    }
}
