using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Domain.CartNew.Entities;
using Domain.CartNew.Entities.Diversos;
using Domain.Core.Interfaces.Repositories;

namespace Domain.CartNew.Interfaces.Repositories
{
    public interface IRepositoryAto : IRepositoryBaseReadWrite<Ato>
    {
        bool ExisteAtoCadastrado(long numMatricula);

        long? GetNumSequenciaAto(long numeroMatricula);

        short GetUltimoNumFicha(long numeroMatricula);

        IEnumerable<Docx> GerarFichas(Ato ato); //qd cria coisas no banco nome no infinitivo: ex Gerar

        IEnumerable<Ato> GetListAtosMatricula(string NumMatricula);

        IEnumerable<Ato> GetListAtosPeriodo(DateTime DataIni, DateTime DataFim);

        IEnumerable<PessoaPesxPre> GetListPessoasPrenotacao(long numeroPrenotacao);
        
        IEnumerable<PessoaAto> GetListPessoasAto(long? IdAto);

        IEnumerable<DadosImovel> GetListImoveisPrenotacao(long IdPrenotacao);

        IEnumerable<Docx> GetListDocxAto(long? IdAto);

        IEnumerable<CamposValor> GetListCamposAto(long IdAto);

        IEnumerable<CamposValor> GetListCamposImovel(long numeroMatricula);  //num matricula vem da base onzeri
        
        IEnumerable<CamposValor> GetListCamposPessoa(long IdPessoa);
    }
}
