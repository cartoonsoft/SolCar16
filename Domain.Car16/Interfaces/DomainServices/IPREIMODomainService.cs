using Domain.Car16.Entities.Car16;
using Domain.Car16.Interfaces.DomainServices.Base;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Car16.Interfaces.DomainServices
{
    public interface IPREIMODomainService : IDomainServiceCar16<PREIMO>
    {
        PREIMO BuscaDadosImovel(long? numeroPrenotacao = null, long? numeroMatricula = null);

    }
}
