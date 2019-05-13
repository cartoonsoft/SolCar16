using Domain.Car16.Entities.Car16;
using Domain.Car16.Interfaces.UnitOfWork;
using Dto.Car16.Entities.Diversos;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AppServices.Car16.AppServices
{
    public class AppServicePessoa: IDisposable
    {
        private readonly IUnitOfWorkDataBaseCar16 _unitOfWork;

        public AppServicePessoa(IUnitOfWorkDataBaseCar16 unitOfWork) 
        {
            //
            _unitOfWork = unitOfWork;
        }

        #region IDisposable Support
        private bool disposedValue = false; // To detect redundant calls

        protected virtual void Dispose(bool disposing)
        {
            if (!disposedValue)
            {
                if (disposing)
                {
                    // dispose managed state (managed objects).
                }

                // free unmanaged resources (unmanaged objects) and override a finalizer below.
                // set large fields to null.
                disposedValue = true;
            }
        }

        // override a finalizer only if Dispose(bool disposing) above has code to free unmanaged resources.
        // ~AppServiceBase() {
        //   // Do not change this code. Put cleanup code in Dispose(bool disposing) above.
        //   Dispose(false);
        // }

        // This code added to correctly implement the disposable pattern.
        public void Dispose()
        {
            // Do not change this code. Put cleanup code in Dispose(bool disposing) above.
            Dispose(true);
            // uncomment the following line if the finalizer is overridden above.
            // GC.SuppressFinalize(this);
        }
        #endregion


        /// <summary>
        /// Lista de Pessoa por Prenotação
        /// </summary>
        /// <param name="numeroPrenotacao"></param>
        /// <returns></returns>
        public IEnumerable<DtoPessoaPesxPre> GetPessoasPremo(long numeroPrenotacao)
        {
            IEnumerable<DtoPessoaPesxPre> Pessoas = new List<DtoPessoaPesxPre>();

            var listaPessoas =
                from pes in _unitOfWork.Repositories.GenericRepository<PESSOA>().GetAll()
                join pre in _unitOfWork.Repositories.GenericRepository<PESXPRE>().GetAll() on pes.SEQPES equals pre.SEQPES
                where  ( pre.SEQPRE == numeroPrenotacao)
                orderby pes.NOM
                select new DtoPessoaPesxPre {
                    IdPessoa = pes.SEQPES,
                    TipoPessoa = pre.REL == "O" ? "Outorgante" : "Outorgado",
                    Bairro = pes.BAI,
                    Cidade = pes.CID,
                    CEP  = pes.CEP.ToString(),
                    Endereco = pes.ENDER,
                    Nome = pes.NOM,
                    TipoDoc1 = pes.TIPODOC1.ToString(),
                    NunDoc1 = pes.NRO1,
                    TipoDoc2 = pes.TIPODOC2.ToString(),
                    NunDoc2 = pes.NRO1,
                    Telefone = pes.TEL,
                    Uf = pes.UF
                };

            return Pessoas = listaPessoas;
        }
    }
}
