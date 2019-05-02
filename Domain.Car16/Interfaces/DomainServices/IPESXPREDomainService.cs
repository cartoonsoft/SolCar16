using Domain.Car16.Entities.Car16;
using Domain.Car16.Interfaces.DomainServices.Base;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Car16.Interfaces.DomainServices
{
    public interface IPESXPREDomainService : IDomainServiceCar16<PESXPRE>
    {
        PESXPRE GetPESXPRE(long numeroPrenotacao);
    }
}
