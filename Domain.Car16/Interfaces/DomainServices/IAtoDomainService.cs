using System;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Collections.Generic;
using Domain.Car16.Entities.Car16New;

namespace Domain.Car16.Interfaces.DomainServices
{
    public interface IAtoDomainService : IDomainServiceCar16New<Ato>
    {
        bool CadastrarAto(Ato ato);
    }
}
