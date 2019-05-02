using AppServices.Car16.AppServices.Base;
using AppServices.Car16.Interfaces;
using AutoMapper;
using Domain.Car16.Entities.Car16;
using Domain.Core.Interfaces.UnitOfWork;
using Dto.Car16.Entities.Cadastros;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AppServices.Car16.AppServices
{
    public class AppServicePESSOA : AppServiceBase<DtoPESSOA, PESSOA>, IAppServicePESSOA
    {
        public AppServicePESSOA(IUnitOfWork unitOfWork) : base(unitOfWork)
        {
        }
        public DtoPESSOA GetPESSOA(long ID)
        {
            PESSOA pessoa = this.DomainServices.GenericDomainService<PESSOA>().GetWhere(p => p.SEQPES == ID).FirstOrDefault();
            DtoPESSOA pessoaDTO = Mapper.Map<PESSOA, DtoPESSOA>(pessoa);
            return pessoaDTO;
        }
    }
}
