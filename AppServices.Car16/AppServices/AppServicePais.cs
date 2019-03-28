using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AppServices.Car16.AppServices.Base;
using AppServices.Car16.Interfaces;
using AutoMapper;
using Domain.Car16.DomainServices;
using Domain.Car16.Entities;
using Domain.Car16.Interfaces.DomainServices;
using Domain.Car16.Interfaces.UnitOfWork;
using Dto.Car16.Entities.Cadastros;

namespace AppServices.Car16.AppServices
{
    public class AppServicePais : AppServiceCar16<DtoPaisModel, Pais>, IAppServicePais
    {
        private readonly IPaisDomainService paisDomainService = null;

        public AppServicePais(IUnitOfWorkCar16 unitOfWorkCar16) : base(unitOfWorkCar16)
        {
            paisDomainService = new PaisDomainService(unitOfWorkCar16);

            Type listType = typeof(List<string>);
            List<string> instance = (List<string>)Activator.CreateInstance(listType);
        }

        public IEnumerable<DtoPaisModel> BuscarPorNome(string nome)
        {
            IEnumerable<Pais> listpaizes = paisDomainService.BuscarPorNome(nome);
            IEnumerable<DtoPaisModel> listPaizes = Mapper.Map<IEnumerable<Pais>, IEnumerable<DtoPaisModel>>(listpaizes);

            return listPaizes;
        }

    }
}
