using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AppServices.Cartorio.AppServices.Base;
using AppServices.Cartorio.Interfaces;
using Domain.Cartorio.Entities.CartorioNew;
using Domain.Cartorio.enums;
using Domain.Cartorio.Interfaces.UnitOfWork;
using Dto.Cartorio.Entities.Cadastros;
using Dto.Cartorio.Entities.Diversos;

namespace AppServices.Cartorio.AppServices
{
    public class AppServiceAto : AppServiceCartorioNew<DtoAto, Ato>, IAppServiceAto
    {
        private readonly IUnitOfWorkDataBaseCartorio _ufwCart;

        public AppServiceAto(IUnitOfWorkDataBaseCartorio UfwCart, IUnitOfWorkDataBaseCartorioNew UfwCartNew) : base(UfwCart, UfwCartNew)
        {
            //
            _ufwCart = UfwCart;
        }

        public void Bloquear(long IdAto)
        {
            throw new NotImplementedException();
        }

        public void ConferirAto(long IdAto, TipoConferenciaAto tipoConferencia)
        {
            throw new NotImplementedException();
        }

        public void Desativar(long IdAto)
        {
            throw new NotImplementedException();
        }

        public bool EditarAto(long IdAto, string textoHtml)
        {
            throw new NotImplementedException();
        }

        public bool FinalizarAto(long IdAto)
        {
            throw new NotImplementedException();
        }

        public IEnumerable<DtoAtoDocx> GerarFichas(long IdAto)
        {
            throw new NotImplementedException();
        }

        public void ImprimirAto(long IdAto)
        {
            throw new NotImplementedException();
        }

        public void ImprimirFicha(long IdDocx)
        {
            throw new NotImplementedException();
        }

        public IEnumerable<DtoAtoList> ListarAtos(DateTime dataIni, DateTime dataFim, string IdUsuario = null)
        {
            throw new NotImplementedException();
        }

        public DtoAtoCadastro NovoAto(DtoAto Ato, string textoHtml)
        {
            throw new NotImplementedException();
        }

        public void UploadFicha(long IdDocx)
        {
            throw new NotImplementedException();
        }

    }
}
