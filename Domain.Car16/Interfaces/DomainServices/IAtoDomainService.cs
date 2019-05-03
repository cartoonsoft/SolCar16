using Domain.Car16.Entities.Car16New;
using Domain.Car16.Interfaces.DomainServices.Base;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Car16.Interfaces.DomainServices
{
    public interface IAtoDomainService : IDomainServiceCar16<Ato>
    {
        bool CadastrarAto(Ato ato);
    }
}
