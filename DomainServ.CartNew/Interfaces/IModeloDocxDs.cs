﻿using System;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Collections.Generic;
using DomainServ.CartNew.Interfaces.Base;
using Domain.CartNew.Entities;
using Dto.CartNew.Entities.Cart_11RI.Diversos;

namespace DomainServ.CartNew.Interfaces
{
    public interface IModeloDocxDs : IDomainServiceCartNew<ModeloDocx>
    {
        long? NovoModelo(ModeloDocx arquivoModeloDocx, LogModeloDocx logArquivoModeloDocx, string IdUsuario);

        long? EditarModelo(ModeloDocx arquivoModeloDocx, LogModeloDocx logArquivoModeloDocx, string IdUsuario);

        bool Desativar(long Id, string IdUsuario);

        IEnumerable<DtoModeloDocxList> GetListaModelosDocx(long? IdTipoAto = null);
    }
}
