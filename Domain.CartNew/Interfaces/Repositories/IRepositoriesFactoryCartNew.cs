using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Domain.Core.Interfaces.Repositories;

namespace Domain.CartNew.Interfaces.Repositories
{
    public interface IRepositoriesFactoryCartNew: IRepositoriesFactoryBase
    {
        IRepositoryPais RepositoryPais {
            get;
        }

        IRepositoryUf RepositoryUf
        {
            get;
        }

        IRepositoryMunicipio RepositoryMunicipio
        {
            get;
        }

        IRepositoryPessoaCartNew RepositoryPessoaCartNew
        {
            get;
        }

        IRepositoryModeloDocx RepositoryModeloDocx
        {
            get;
        }

        IRepositoryAto RepositoryAto
        {
            get;
        }

        IRepositoryLogModeloDocx RepositoryLogModeloDocx
        {
            get;
        }

        IRepositoryTipoAto RepositoryTipoAto
        {
            get;
        }

    }
}
