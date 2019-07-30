using AutoMapper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Domain.CartNew.Entities;
using Dto.Cartorio.Entities.Cadastros;
using AppServices.Cartorio.Interfaces;
using AppServices.Cartorio.AppServices.Base;
using Domain.Cart.Interfaces.UnitOfWork;
using Domain.CartNew.Interfaces.UnitOfWork;

namespace AppServices.Cartorio.AppServices
{
    public class AppServicePais : AppServiceCartorio<DtoPaisModel, Pais>, IAppServicePais
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
