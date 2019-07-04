using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AppServices.Cartorio.Interfaces.Base;
using Domain.Cartorio.Entities.CartorioNew;
using Domain.Cartorio.enums;
using Dto.Cartorio.Entities.Cadastros;

namespace AppServices.Cartorio.Interfaces
{
    public interface IAppServiceAto : IAppServiceCartorio<DtoAto,Ato>
    {
        DtoAtoCadastro NovoAto(DtoAto Ato, string textoHtml);
        bool EditarAto(long IdAto, string textoHtml);

        List<DtoAtoDocx> GerarFichas(long IdAto);
        void UploadFicha( long IdDocx);

        void ImprimirFicha(long IdDocx);
        void ImprimirAto(long IdAto);

        void ConferirAto(long IdAto, TipoConferenciaAto tipoConferencia);
        bool FinalizarAto(long IdAto);

        void Bloquear(long IdAto);
        void Desativar(long IdAto);

    }
}
