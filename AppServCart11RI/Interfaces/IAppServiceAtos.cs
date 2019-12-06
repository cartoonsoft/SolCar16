﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AppServ.Core.Interfaces;
using Domain.CartNew.Entities;
using Domain.CartNew.Enumerations;
using Dto.CartNew.Entities.Cart_11RI;
using Dto.CartNew.Entities.Cart_11RI.Diversos;

namespace AppServices.Cartorio.Interfaces
{
    public interface IAppServiceAtos : IAppServiceCartorio<DtoAto, Ato>
    {
        bool ExisteAtoCadastrado(string NumMatricula);

        long? GetNumSequenciaTipoAto(string NumMatricula, long IdTipoAto);

        void NovoAto(DtoAto Ato);


        void ImprimirMinutaAto(long IdAto);

        IEnumerable<DtoDocx> GerarFichas(long IdAto); //qd cria coisas no banco nome no infinitivo: ex Gerar

        void ImprimirFicha(long IdDocx);

        void ImprimirFichasAto(long IdAto);

        void UploadFicha(long IdDocx);

        bool ConfirmarAjusteImpressaoAto(long IdAto);

        bool ConfirmarFicha(long IdDocx);  //confirmar que ficha foi gerada corretamente

        bool ReabrirAto(long IdAto); //estudar esta caso, deve ser o ultimo ato, coidar quebra de pagina

        bool BloquearMatricula(string NumMatricula);

        bool BloquearAto(long IdAto);

        void DesativarAto(long IdAto);

        DateTime? DataRegPrenotacao(long IdPrenotacao);

        IEnumerable<DtoAto> GetListAtosMatricula(string NumMatricula);

        IEnumerable<DtoAto> GetListAtosPeriodo(DateTime DataIni, DateTime DataFim);

        IEnumerable<DtoPessoaPesxPre> GetListPessoasPrenotacao(long IdPrenotacao);

        IEnumerable<DtoPessoaPesxPre> GetListPessoas(long idTipoAto, long[] idsPessoas, long? idPrenotacao);

        DtoPessoaPesxPre GetPessoa(long idPessoa, long? idPrenotacao);

        IEnumerable<DtoPessoaAto> GetListPessoasAto(long? IdAto);

        IEnumerable<DtoDadosImovel> GetListImoveisPrenotacao(long IdPrenotacao);

        IEnumerable<DtoDocx> GetListDocxAto(long? IdAto);

        DtoDadosImovel GetDadosImovel(long IdPrenotacao, string NumMatricula);

        DtoReservaImovel ProcReservarMatImovel(TipoReservaMatImovel TipoReserva, long IdPrenotacao, string NumMatricula, string IdUsuario);

        StringBuilder GetTextoWordDocModelo(long IdModeloDoc, string ServerPath);

        StringBuilder GetTextoAto(DtoInfAto dtoInfAto);
    }
}
