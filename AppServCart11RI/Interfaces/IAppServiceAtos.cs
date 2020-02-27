using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AppServ.Core.Interfaces;
using Domain.CartNew.Entities;
using Domain.CartNew.Enumerations;
using Dto.CartNew.Base;
using Dto.CartNew.Entities.Cart_11RI;
using Dto.CartNew.Entities.Cart_11RI.Diversos;
using Infra.Cross.Identity.Models;

namespace AppServices.Cartorio.Interfaces
{
    public interface IAppServiceAtos : IAppServiceCartorio<DtoAto, Ato>
    {

        string[] StatusPodeEditar();

        string[] StatusPodeGerarFicha();

        string[] StatusPodeConfigImp();

        string[] StatusAtoFinalizado();

        DtoExecProc SetTextoConferido(long? idAto, string idUsuario, bool conferido);

        bool ExisteAtoCadastrado(string NumMatricula);

        long? GetNumSequenciaTipoAto(string NumMatricula, long IdTipoAto);

        void ImprimirMinutaAto(long IdAto);

        IEnumerable<DtoDocx> GerarFichas(long IdAto); //qd cria coisas no banco nome no infinitivo: ex Gerar

        void ImprimirFicha(long IdDocx);

        void ImprimirFichasAto(long IdAto);

        void UploadFicha(long IdDocx);

        bool ConfirmarAjusteImpressaoAto(long IdAto);

        bool ConfirmarFicha(long IdDocx);  //confirmar que ficha foi gerada corretamente

        void DesativarAto(long IdAto);

        DateTime? DataRegPrenotacao(long IdPrenotacao);

        DtoPessoaPesxPre GetPessoa(long idPessoa, long? idPrenotacao);

        DtoDadosImovel GetDadosImovel(long IdPrenotacao, string NumMatricula);

        DtoReservaImovel ProcReservarMatImovel(TipoReservaMatImovel TipoReserva, long IdPrenotacao, string NumMatricula, string IdUsuario);

        StringBuilder GetTextoModeloDoc(long IdModeloDoc);

        StringBuilder GetTextoAto(DtoInfAto dtoInfAto);

        DtoExecProc InsertOrUpdateAto(DtoAto ato, ApplicationUser usuario);

        IEnumerable<DtoAto> GetListAtosMatricula(string NumMatricula);

        IEnumerable<DtoAto> GetListAtosPeriodo(DateTime DataIni, DateTime DataFim);

        IEnumerable<DtoPessoaPesxPre> GetListPessoasPrenotacao(long IdPrenotacao);

        IEnumerable<DtoPessoaPesxPre> GetListPessoas(long idTipoAto, long[] idsPessoas, long? idPrenotacao);

        IEnumerable<string> GetListMatriculasPrenotacao(long IdPrenotacao);

        IEnumerable<DtoPessoaAto> GetListPessoasAto(long? IdAto);

        IEnumerable<DtoDadosImovel> GetListImoveisPrenotacao(long IdPrenotacao);

        IEnumerable<DtoDocx> GetListDocxAto(long? IdAto);
    }
}
