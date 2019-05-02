using AppServices.Car16.AppServices.Base;
using AppServices.Car16.Interfaces;
using AutoMapper;
using Domain.Car16.Entities.Car16;
using Domain.Car16.Interfaces.UnitOfWork;
using Domain.Core.Interfaces.UnitOfWork;
using Dto.Car16.Entities.Cadastros;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AppServices.Car16.AppServices
{
    public class AppServicePESXPRE : AppServiceCar16<DtoPESXPRE, PESXPRE>, IAppServicePESXPRE
    {
        public AppServicePESXPRE(IUnitOfWorkCar16 unitOfWork) : base(unitOfWork)
        {
        }

        public DtoPESXPRE GetPESXPRE(long numeroPrenotacao)
        {
            PESXPRE pesXpre = this.DomainServices.GenericDomainService<PESXPRE>()
                .GetWhere(p => p.SEQPRE == numeroPrenotacao)?.FirstOrDefault();
            DtoPESXPRE dtoPESXPRE = Mapper.Map<PESXPRE, DtoPESXPRE>(pesXpre);
            return dtoPESXPRE;
        }
    }
}
