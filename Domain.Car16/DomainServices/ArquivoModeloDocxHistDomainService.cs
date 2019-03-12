using Domain.Car16.Entities;
using Domain.Car16.Interfaces.DomainServices;
using Domain.Car16.Interfaces.UnitOfWork;
using Domain.Core.DomainServices.Base;
using Domain.Core.Interfaces.UnitOfWork;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Car16.DomainServices
{
    public class ArquivoModeloDocxHistDomainService : DomainServiceBase<ArquivoModeloDocxHist>, IArquivoModeloDocxHistDomainService
    {
        private readonly IUnitOfWorkCar16 _unitOfWorkCar16;

        public ArquivoModeloDocxHistDomainService(IUnitOfWorkCar16 unitOfWorkCar16) : base(unitOfWorkCar16)
        {
            _unitOfWorkCar16 = unitOfWorkCar16;

        }
    }
}
