using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Domain.Core.Interfaces.DomainServices;

namespace DomainServ.CartNew.Interfaces.Factory
{
    public interface IDomainServicesFactoryCartNew : IDomainServicesFactoryBase
    {
        IPaisDs PaisDs
        {
            get;
        }
        IUfDs UfDs
        {
            get;
        }
        IMunicipioDs MunicipioDs
        {
            get;
        }
        
        IPessoaCartNewDs PessoaCartNewDs
        {
            get;
        }

        IModeloDocxDs ArquivoModeloDocxDs
        {
            get;
        }
        IAtoDs AtoDs
        {
            get;
        }

        IAcoesUsuariosDs AcoesUsuariosDs
        {
            get;
        }

    }
}
