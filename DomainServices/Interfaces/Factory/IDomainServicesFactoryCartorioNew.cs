using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Domain.Core.Interfaces.DomainServices;

namespace DomainServices.Interfaces.Factory
{
    public interface IDomainServicesFactoryCartorioNew : IDomainServicesFactoryBase
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

        IPessoaDs PessoaDs
        {
            get;
        }

        IArquivoModeloDocxDs ArquivoModeloDocxDs
        {
            get;
        }
        IAtoDs AtoDs
        {
            get;
        }

    }
}
