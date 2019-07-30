using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Domain.Core.Interfaces.Repositories;

namespace Domain.CartNew.Interfaces.Repositories
{
    public interface IRepositoriesFactoryCartorioNew: IRepositoriesFactoryBase
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
        IRepositoryPessoaCartNew RepositoryPessoa
        {
            get;
        }
        IRepositoryArquivoModeloDocx RepositoryArquivoModeloDocx
        {
            get;
        }
        IRepositoryAto RepositoryAto
        {
            get;
        }

        IRepositoryLogArquivoModeloDocx RepositoryLogArquivoModeloDocx
        {
            get;
        }
    }
}
