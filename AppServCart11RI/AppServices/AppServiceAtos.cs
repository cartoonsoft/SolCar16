using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AppServ.Core.AppServices;
using AppServices.Cartorio.Interfaces;
using Domain.CartNew.Entities;
using Domain.CartNew.Enumerations;
using Domain.CartNew.Interfaces.UnitOfWork;
using Dto.CartNew.Entities.Cart_11RI;
using Dto.CartNew.Entities.Cart_11RI.Diversos;

namespace AppServCart11RI.AppServices
{
    public class AppServiceAtos : AppServiceCartorio<DtoAto, Ato>, IAppServiceAtos
    {
        public AppServiceAtos(IUnitOfWorkDataBaseCartNew UfwCartNew) : base(UfwCartNew)
        {
            //

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
            IEnumerable<DtoAtoList> lista = new List<DtoAtoList>();
            //todo: ronaldo fazer ListarAtos

            return lista;
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
