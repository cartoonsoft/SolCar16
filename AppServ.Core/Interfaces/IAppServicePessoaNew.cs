using System;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Collections.Generic;
using Domain.CartNew.Entities;
using AppServ.Core.Interfaces;
using Dto.CartNew.Entities.TodosCart;

namespace AppServices.Cartorio.Interfaces
{
    public interface IAppServicePessoaNew : IAppServiceCartorio<DtoPessoaCartNew, PessoaCartNew>
    {
        //nada ainda
    }
}
