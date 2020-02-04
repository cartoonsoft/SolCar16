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
using Dto.CartNew.Base;
using Infra.Cross.Identity.Models;

namespace DomainServ.CartNew.Interfaces
{
    public interface IAtoDs : IDomainServiceCartNew<Ato>
    {
        bool ExisteAtoCadastrado(string NumMatricula);

        long? GetNumSequenciaAto(string NumMatricula);

        short GetUltimoNumFicha(string NumMatricula);

        string[] StatusEdtTexto();

        string[] StatusEdtDadosImp();

        string[] StatusAtoFinalizado();

        DtoExecProc SetTextoConferido(long? idAto, ApplicationUser usuario, bool conferido);

        IEnumerable<DtoDocx> GerarFichas(DtoAto ato); //qd cria coisas no banco nome no infinitivo: ex Gerar

        IEnumerable<DtoAto> GetListAtosMatricula(string NumMatricula);

        IEnumerable<DtoAto> GetListAtosPeriodo(DateTime DataIni, DateTime DataFim);
        
        IEnumerable<DtoPessoaAto> GetListPessoasAto(long? IdAto);

        IEnumerable<DtoPessoaPesxPre> GetListPessoasPrenotacao(long IdPrenotacao);

        IEnumerable<DtoPessoaPesxPre> GetListPessoas(long[] idsPessoas, long? idPrenotacao);

        IEnumerable<DtoDadosImovel> GetListImoveisPrenotacao(long IdPrenotacao);

        IEnumerable<DtoDocx> GetListDocxAto(long? IdAto);

        IEnumerable<DtoAtoEvento> GetListHistoricoAto(long? IdAto);

        DtoPessoaPesxPre GetPessoa(long idPessoa, long? idPrenotacao);

        DtoDadosImovel GetDadosImovel(long IdPrenotacao, string NumMatricula);

        DtoExecProc InsertOrUpdateAto(DtoAto ato, ApplicationUser usuario);

        bool AtoJaCadastrado(long idPrenotacao, string numMatricula);
    }
}
