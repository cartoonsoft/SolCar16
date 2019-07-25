using AutoMapper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AppServices.Cartorio.AppServices.Base;
using AppServices.Cartorio.Interfaces;
using Domain.Car16.Entities.Car16New;
using Domain.Cartorio.Interfaces.UnitOfWork;
using Dto.Cartorio.Entities.Cadastros;

namespace AppServices.Cartorio.AppServices
{
    public class AppServicePais : AppServiceCartorioNew<DtoPaisModel, Pais>, IAppServicePais
    {
        public AppServicePais(IUnitOfWorkDataBaseCartorio UfwCart, IUnitOfWorkDataBaseCartorioNew UfwCartNew) : base(UfwCart, UfwCartNew)
        {
            //
        }

        public IEnumerable<DtoPaisModel> BuscarPorNome(string nome)
        {
            IEnumerable<Pais> listpaizes = this.DsFactoryCartNew.PaisDs.BuscarPorNome(nome);
            IEnumerable<DtoPaisModel> listPaizes = Mapper.Map<IEnumerable<Pais>, IEnumerable<DtoPaisModel>>(listpaizes);

            return listPaizes;
        }

    }
}
