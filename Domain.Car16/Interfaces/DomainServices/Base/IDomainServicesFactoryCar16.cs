using Domain.Core.Interfaces.DomainServices;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Car16.Interfaces.DomainServices.Base
{

    public interface IDomainServicesFactoryCar16 : IDomainServicesFactoryBase
    {
        IPaisDomainService PaisDomainService
        {
            get;
        }
        IUfDomainService UfDomainService
        {
            get;
        }
        IMunicipioDomainService MunicipioDomainService
        {
            get;
        }

        IPessoaDomainService PessoaDomainService
        {
            get;
        }

        IArquivoModeloDocxDomainService ArquivoModeloDocxDomainService
        {
            get;
        }



    }
}
