using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Domain.Cartorio.Entities.CartorioNew;
using Domain.Core.Interfaces.Repositories;

namespace Domain.Cartorio.Interfaces.Repositories
{
    public interface IRepositoryPessoaCartorioNew: IRepositoryBaseReadWrite<PessoaCartorioNew>
    {

        IEnumerable<PessoaCartorioNew> BuscarPorNome(string nome);
        PessoaCartorioNew BuscarPorCPFCNPJ(string CpfCnpj);

    }
}
