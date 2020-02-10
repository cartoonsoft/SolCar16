using System;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Collections.Generic;
using DomainServ.CartNew.Interfaces.Base;
using Domain.CartNew.Entities;
using Dto.CartNew.Entities.Cart_11RI.Diversos;
using Dto.CartNew.Entities.Cart_11RI;

namespace DomainServ.CartNew.Interfaces
{
    public interface IModeloDocxDs : IDomainServiceCartNew<ModeloDoc>
    {
        long? NovoModelo(DtoModeloDoc dtoModeloDoc);

        void EditarModelo(DtoModeloDoc dtoModeloDoc);

        bool Desativar(long Id, string IdUsuario);

        IEnumerable<DtoModeloDocxList> GetListModelosDocx(long? IdTipoAto = null);
    }
}
