using System;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Collections.Generic;
using DomainServ.CartNew.Interfaces.Base;
using Domain.CartNew.Entities;
using Dto.CartNew.Entities.Cart_11RI.Diversos;

namespace DomainServ.CartNew.Interfaces
{
    public interface IModeloDocxDs : IDomainServiceCartNew<ModeloDoc>
    {
        long? NovoModelo(ModeloDoc arquivoModeloDocx, LogModeloDoc logArquivoModeloDocx, string IdUsuario);

        long? EditarModelo(ModeloDoc arquivoModeloDocx, LogModeloDoc logArquivoModeloDocx, string IdUsuario);

        bool Desativar(long Id, string IdUsuario);

        IEnumerable<DtoModeloDocxList> GetListaModelosDocx(long? IdTipoAto = null);
    }
}
