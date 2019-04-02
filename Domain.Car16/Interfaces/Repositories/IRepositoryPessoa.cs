using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Domain.Car16.Entities.Car16New;
using Domain.Core.Interfaces.Repositories;

namespace Domain.Car16.Interfaces.Repositories
{
    public interface IRepositoryPessoa: IRepositoryBaseReadWrite<Pessoa>
    {

        IEnumerable<Pessoa> BuscarPorNome(string nome);
        Pessoa BuscarPorCPFCNPJ(string CpfCnpj);

    }
}
