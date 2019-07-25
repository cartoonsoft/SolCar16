using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Domain.Car16.Entities.Car16New;
using Domain.Cartorio.Interfaces.UnitOfWork;
using DomainServices.Interfaces.Base;
using Dto.Cartorio.Entities.Diversos;

namespace DomainServices.Interfaces
{
    public interface IPessoaDs : IDomainServiceCartorioNew<PessoaCartorioNew>
    {
        IEnumerable<DtoPessoaPesxPre> GetPessoasPorPrenotacao(long IdPrenotacao);
        IEnumerable<DtoPessoaPesxPre> GetListaOutorgadosOutorgantes(long[] listIdsPessoas, long? IdTipoAto, long IdPrenotacao);
    }
}
