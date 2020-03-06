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

        string[] StatusPodeEditar();

        string[] StatusPodeGerarFicha();

        string[] StatusPodeConfigImp();

        string[] StatusAtoFinalizado();

        long? GetNumSequenciaAto(string NumMatricula);

        short GetUltimoNumFicha(string NumMatricula);

        bool SetStatusAto(long? idAto, string statusAto);

        DadosImovel GetDadosImovel(long IdPrenotacao, string NumMatricula);

        DateTime? DataRegPrenotacao(long IdPrenotacao);

        PessoaPesxPre GetPessoa(long idPessoa, long? idPrenotacao);

        IEnumerable<Docx> GerarFichas(Ato ato); //qd cria coisas no banco nome no infinitivo: ex Gerar

        IEnumerable<Ato> GetListAtosMatricula(string NumMatricula);

        IEnumerable<Ato> GetListAtosPeriodo(DateTime DataIni, DateTime DataFim);

        IEnumerable<PessoaAto> GetListPessoasAto(long? IdAto);

        IEnumerable<PessoaPesxPre> GetListPessoasPrenotacao(long IdPrenotacao);

        /// <summary>
        /// os métodos GetListPessoas, GetPessoa estão aqui pq se referem a pessoas da base onzeri, só são lidos para gerar o ato
        /// </summary>
        /// <param name="IdPrenotacao"></param>
        /// <returns></returns>
        IEnumerable<PessoaPesxPre> GetListPessoas(List<AtoPessoa> listaAtoPessoa, long? idPrenotacao);

        IEnumerable<DadosImovel> GetListImoveisPrenotacao(long IdPrenotacao);

        IEnumerable<Docx> GetListDocxAto(long? IdAto);

        IEnumerable<CampoTipoAto> GetListCamposAto(long IdTipoAto, long IdCtaAcessoSist);

        IEnumerable<CampoTipoAto> GetListCamposPrenotacao(long IdTipoAto, long IdCtaAcessoSist);

        IEnumerable<CampoTipoAto> GetListCamposImovel(long IdTipoAto, long IdCtaAcessoSist);  //só a lista de campos, ainda noa povoou
        
        IEnumerable<CampoTipoAto> GetListCamposPessoa(long IdTipoAto, long IdCtaAcessoSist);

        IEnumerable<AtoEvento> GetListHistoricoAto(long? IdAto);

        IEnumerable<string> GetListMatriculasPrenotacao(long IdPrenotacao);
    }
}
