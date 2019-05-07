using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Domain.Core.Interfaces.DomainServices;

namespace Domain.Car16.Interfaces.DomainServices
{
    public interface IDomainServicesFactoryCar16New : IDomainServicesFactoryBase
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
        IAtoDomainService AtoDomainService
        {
            get;
        }

    }
}
