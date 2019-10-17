using System;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Collections.Generic;
using DomainServ.CartNew.Interfaces.Base;
using Domain.CartNew.Entities;
using Dto.CartNew.Entities.Cart_11RI.Diversos;
using Dto.CartNew.Entities.Cart_11RI;
using Domain.CartNew.Entities.Diversos;

namespace DomainServ.CartNew.Interfaces
{
    public interface IAtoDs : IDomainServiceCartNew<Ato>
    {
        bool ExisteAtoCadastrado(long numMatricula);

        long? GetNumSequenciaAto(long numeroMatricula);

        IEnumerable<DtoPessoaPesxPre> GetPessoasPrenotacao(long numeroPrenotacao);

        IEnumerable<DtoAto> GetListAtosMatricula(string NumMatricula);

        IEnumerable<DtoAto> GetListAtosPeriodo(DateTime DataIni, DateTime DataFim);

        IEnumerable<DtoPessoaAto> GetListPessoasAto(long? IdAto);

        IEnumerable<DtoDocx> GetListDocxAto(long? IdAto);

        IEnumerable<DtoDadosImovel> GetDadosImoveisPrenotacao(long IdPrenotacao);
    }
}
