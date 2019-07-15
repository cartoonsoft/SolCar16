using AutoMapper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AppServices.Cartorio.AppServices.Base;
using AppServices.Cartorio.Interfaces;
using Domain.Cartorio.Entities.CartorioNew;
using Domain.Cartorio.Interfaces.UnitOfWork;
using Dto.Cartorio.Entities.Cadastros;
using Dto.Car16.Entities.Cadastros;
using Domain.Cartorio.Entities.Cartorio;

namespace AppServices.Cartorio.AppServices
{
    public class AppServiceAcesso : AppServiceCartorioNew<DtoAcesso, ACESSO>, IAppServiceAcesso
    {
        public AppServiceAcesso(IUnitOfWorkDataBaseCartorio UfwCart, IUnitOfWorkDataBaseCartorioNew UfwCartNew) : base(UfwCart, UfwCartNew)
        {
            //
        }

    }
}
