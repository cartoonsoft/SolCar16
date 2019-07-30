using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Domain.CartNew.Entities;
using Domain.Core.Interfaces.Repositories;

namespace Domain.CartNew.Interfaces.Repositories
{
    public interface IRepositoryPessoaCartNew: IRepositoryBaseReadWrite<PessoaCartNew>
    {

        IEnumerable<PessoaCartNew> BuscarPorNome(string nome);
        PessoaCartNew BuscarPorCPFCNPJ(string CpfCnpj);

    }
}
