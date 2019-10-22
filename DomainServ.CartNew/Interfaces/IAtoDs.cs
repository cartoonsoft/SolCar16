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
        bool ExisteAtoCadastrado(string NumMatricula);

        long? GetNumSequenciaAto(string NumMatricula);

        IEnumerable<DtoDocx> GerarFichas(DtoAto ato); //qd cria coisas no banco nome no infinitivo: ex Gerar

        IEnumerable<DtoAto> GetListAtosMatricula(string NumMatricula);

        IEnumerable<DtoAto> GetListAtosPeriodo(DateTime DataIni, DateTime DataFim);

        IEnumerable<DtoPessoaPesxPre> GetListPessoasPrenotacao(long IdPrenotacao);

        IEnumerable<DtoPessoaAto> GetListPessoasAto(long? IdAto);

        IEnumerable<DtoDadosImovel> GetListImoveisPrenotacao(long IdPrenotacao);

        IEnumerable<DtoDocx> GetListDocxAto(long? IdAto);

        IEnumerable<DtoCamposValor> GetListCamposAto(long IdAto);

        IEnumerable<DtoCamposValor> GetListCamposImovel(string NumMatricula);  //num matricula vem da base onzeri

        IEnumerable<DtoCamposValor> GetListCamposPessoa(long IdPessoa);
    }
}
