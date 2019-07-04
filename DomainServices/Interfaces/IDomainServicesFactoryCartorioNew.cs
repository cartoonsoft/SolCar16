using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Domain.Core.Interfaces.DomainServices;

namespace DomainServices.Interfaces
{
    public interface IDomainServicesFactoryCartorioNew : IDomainServicesFactoryBase
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
