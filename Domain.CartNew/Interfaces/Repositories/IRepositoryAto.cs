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

        IEnumerable<PessoaPesxPre> GetPessoasPrenotacao(long numeroPrenotacao);

        IEnumerable<Ato> GetListAtosMatricula(string NumMatricula);

        IEnumerable<Ato> GetListAtosPeriodo(DateTime DataIni, DateTime DataFim);

        IEnumerable<PessoaAto> GetListPessoasAto(long? IdAto);

        IEnumerable<Docx> GetListDocxAto(long? IdAto);

        IEnumerable<DadosImovel> GetDadosImoveisPrenotacao(long IdPrenotacao);

        IEnumerable<CamposValor> GetCamposAto(long IdAto);
        IEnumerable<CamposValor> GetCamposImovel(long numeroMatricula);  //num matricula vem da base onzeri
        IEnumerable<CamposValor> GetCamposPessoa(long IdPessoa);

    }
}
