﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Domain.Car16.Entities;
using Domain.Car16.Interfaces.DomainServices.Base;

namespace Domain.Car16.Interfaces.DomainServices
{
    public interface IPaisDomainService : IDomainServiceCar16<Pais>
    { 
        IEnumerable<Pais> BuscarPorNome(string nome);

    }
}
