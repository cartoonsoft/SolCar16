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
        bool ExisteAtoCadastrado(string numMatricula);

        long? GetNumSequenciaAto(string NumMatricula);

        short GetUltimoNumFicha(string NumMatricula);

        IEnumerable<Docx> GerarFichas(Ato ato); //qd cria coisas no banco nome no infinitivo: ex Gerar

        IEnumerable<Ato> GetListAtosMatricula(string NumMatricula);

        IEnumerable<Ato> GetListAtosPeriodo(DateTime DataIni, DateTime DataFim);

        IEnumerable<PessoaAto> GetListPessoasAto(long? IdAto);

        IEnumerable<PessoaPesxPre> GetListPessoasPrenotacao(long IdPrenotacao);

        IEnumerable<PessoaPesxPre> GetListPessoas(long[] idsPessoas, long? idPrenotacao);

        PessoaPesxPre GetPessoa(long idPessoa, long? idPrenotacao);

        IEnumerable<DadosImovel> GetListImoveisPrenotacao(long IdPrenotacao);

        IEnumerable<Docx> GetListDocxAto(long? IdAto);
        DateTime? DataRegPrenotacao(long IdPrenotacao);

        IEnumerable<CampoTipoAto> GetListCamposAto(long IdTipoAto, long IdCtaAcessoSist);

        IEnumerable<CampoTipoAto> GetListCamposPrenotacao(long IdTipoAto, long IdCtaAcessoSist);

        IEnumerable<CampoTipoAto> GetListCamposImovel(long IdTipoAto, long IdCtaAcessoSist);  //só a lista de campos, ainda noa povoou
        
        IEnumerable<CampoTipoAto> GetListCamposPessoa(long IdTipoAto, long IdCtaAcessoSist);

        IEnumerable<AtoEvento> GetListHistoricoAto(long? IdAto);

        DadosImovel GetDadosImovel(long IdPrenotacao, string NumMatricula);
    }
}
