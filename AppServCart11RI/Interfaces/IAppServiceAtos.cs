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
        DtoAto NovoAto(DtoAto Ato, string textoHtml);

        bool EditarAto(long IdAto, string textoHtml);

        IEnumerable<DtoAtoDocx> GerarFichas(long IdAto);

        void UploadFicha(long IdDocx);

        void ImprimirFicha(long IdDocx);

        void ImprimirAto(long IdAto);

        void ConferirAto(long IdAto, TipoConferenciaAto tipoConferencia);

        bool FinalizarAto(long IdAto);

        void Bloquear(long IdAto);

        void Desativar(long IdAto);

        IEnumerable<DtoAtoList> GetListaAtos(DateTime dataIni, DateTime dataFim, string IdUsuario = null);

        DtoPREIMO GetDadosImovelPrenotacao(long numPrenotacao);

        IEnumerable<DtoPessoaPesxPre> GetPessoasPrenotacao(long numeroPrenotacao);

        IEnumerable<DtoDocxList> GetListDtoDocxAto(string NumMatricula);
    }
}
