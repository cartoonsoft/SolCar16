using System;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Collections.Generic;
using AppServices.Cartorio.Interfaces.Base;
using Dto.Cartorio.Entities.Cadastros;
using Domain.CartNew.Entities;

namespace AppServices.Cartorio.Interfaces
{
    public interface IAppServicePais : IAppServiceCartorio<DtoPaisModel, Pais>
    {
        IEnumerable<DtoPaisModel> BuscarPorNome(string nome);
    }
}
