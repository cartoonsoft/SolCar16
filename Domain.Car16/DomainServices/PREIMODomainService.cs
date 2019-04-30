using Domain.Car16.DomainServices.Base;
using Domain.Car16.Entities.Car16;
using Domain.Car16.Interfaces.DomainServices;
using Domain.Car16.Interfaces.Repositories;
using Domain.Car16.Interfaces.UnitOfWork;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Car16.DomainServices
{
    public class PREIMODomainService : DomainServiceCar16<PREIMO>, IPREIMODomainService
    {
        private readonly IRepositoryPREIMO _repositoryPREIMO;

        public PREIMODomainService(IUnitOfWorkCar16 unitOfWorkCar16) : base(unitOfWorkCar16)
        {
        }
        /// <summary>
        /// Pega o registro mais recente dado um numero de prenotação e/ou matricula
        /// Se ambos null, pega o registro mais recente
        /// </summary>
        /// <param name="numeroPrenotacao">N°. Prenotação</param>
        /// <param name="numeroMatricula">N° Matricula</param>
        /// <returns></returns>
        public PREIMO BuscaDadosImovel(long? numeroPrenotacao = null, long? numeroMatricula = null)
        {
            return _repositoryPREIMO.BuscaDadosImovel(numeroPrenotacao, numeroMatricula);
        }
    }
}
