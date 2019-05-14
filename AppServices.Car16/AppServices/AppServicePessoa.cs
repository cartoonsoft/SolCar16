using Domain.Car16.Entities.Car16;
using Domain.Car16.Entities.Car16New;
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
        private readonly IUnitOfWorkDataBaseCar16 _unitOfWorkCar16;
        private readonly IUnitOfWorkDataBaseCar16New _unitOfWorkCar16New;

        public AppServicePessoa(IUnitOfWorkDataBaseCar16 unitOfWorkCar16, IUnitOfWorkDataBaseCar16New unitOfWorkcar16New) 
        {
            //
            _unitOfWorkCar16 = unitOfWorkCar16;
            _unitOfWorkCar16New = unitOfWorkcar16New;
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
        public IEnumerable<DtoPessoaPesxPre> GetPessoasPrenotacao(long numeroPrenotacao)
        {
            IEnumerable<DtoPessoaPesxPre> Pessoas = new List<DtoPessoaPesxPre>();

            var listaPessoas =
                from pre in _unitOfWorkCar16.Repositories.GenericRepository<PESXPRE>().Get().Where(p => p.SEQPRE == numeroPrenotacao) 
                join pes in _unitOfWorkCar16.Repositories.GenericRepository<PESSOA>().Get() on pre.SEQPES equals pes.SEQPES
                orderby pes.NOM
                select new DtoPessoaPesxPre {
                    IdPessoa = pes.SEQPES,
                    TipoPessoa = pre.REL == "O" ? "Outorgante" : "Outorgado",
                    Bairro = pes.BAI,
                    Cidade = pes.CID,
                    CEP  = pes.CEP,
                    Endereco = pes.ENDER,
                    Nome = pes.NOM,
                    TipoDoc1 = pes.TIPODOC1,
                    Numero1 = pes.NRO1,
                    TipoDoc2 = pes.TIPODOC2,
                    Numero2 = pes.NRO1,
                    Telefone = pes.TEL,
                    UF = pes.UF
                };

            return Pessoas = listaPessoas;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="IdTipoAto"></param>
        /// <param name="IdPrenotacao"></param>
        /// <param name="IdMatricula"></param>
        /// <returns></returns>
        public DtoDadosImovel GetCamposModeloMatricula( long[] listIdsPessoas, long? IdTipoAto, long? IdPrenotacao, long? IdMatricula)
        {
            DtoDadosImovel dtoTmp = new DtoDadosImovel();

            //cQ
            var listaCamposImovel = 
                from campos in _unitOfWorkCar16New.Repositories.GenericRepository<CamposArquivoModeloDocx>().Get()
                .Where(c => c.IdTipoAto == IdTipoAto && c.Entidade == "IMOVEL")
                orderby campos.NomeCampo
                select new CamposArquivoModeloDocx
                {
                    Id = campos.Id,
                    IdAcessoSistema = campos.IdAcessoSistema,
                    IdTipoAto = campos.IdTipoAto,
                    NomeCampo = campos.NomeCampo,
                    PlaceHolder = campos.PlaceHolder,
                    Entidade = campos.Entidade
                };

            PREIMO Imovel = _unitOfWorkCar16.Repositories.GenericRepository<PREIMO>().
                GetWhere( i => i.SEQPRE == IdPrenotacao && i.MATRI == IdMatricula).FirstOrDefault();

            foreach (var item in listaCamposImovel)
            {
                var prop = Imovel.GetType().GetProperty(item.NomeCampo);

                if (prop != null)
                {
                    dtoTmp.CamposValorDadosImovel.Add(new DtoCamposValor
                    {
                        Campo = item.NomeCampo,
                        Valor = prop.GetValue(Imovel).ToString()
                    });
                }
            }

            var listaPessoas =
                from pes in _unitOfWorkCar16.Repositories.GenericRepository<PESSOA>().Get().Where(p => listIdsPessoas.Contains(p.SEQPES))
                join pre in _unitOfWorkCar16.Repositories.GenericRepository<PESXPRE>().Get().Where(p => p.SEQPRE == IdPrenotacao) on pes.SEQPES equals pre.SEQPES
                orderby pes.NOM
                select new DtoPessoaPesxPre
                {
                    IdPessoa = pes.SEQPES,
                    TipoPessoa = pre.REL == "O" ? "Outorgante" : "Outorgado",
                    Bairro = pes.BAI,
                    Cidade = pes.CID,
                    CEP = pes.CEP,
                    Endereco = pes.ENDER,
                    Nome = pes.NOM,
                    TipoDoc1 = pes.TIPODOC1,
                    Numero1 = pes.NRO1,
                    TipoDoc2 = pes.TIPODOC2.ToString(),
                    Numero2 = pes.NRO1,
                    Telefone = pes.TEL,
                    UF = pes.UF
                };

            var listaCamposPessoa =
                from campos in _unitOfWorkCar16New.Repositories.GenericRepository<CamposArquivoModeloDocx>().Get()
                .Where(c => c.IdTipoAto == IdTipoAto && c.Entidade == "PESSOA")
                orderby campos.NomeCampo
                select new CamposArquivoModeloDocx
                {
                    Id = campos.Id,
                    IdAcessoSistema = campos.IdAcessoSistema,
                    IdTipoAto = campos.IdTipoAto,
                    NomeCampo = campos.NomeCampo,
                    PlaceHolder = campos.PlaceHolder,
                    Entidade = campos.Entidade
                };

            foreach (var pessoa in listaPessoas)
            {
                foreach (var CampoPessoa in listaCamposPessoa)
                {
                    var prop = pessoa.GetType().GetProperty(CampoPessoa.NomeCampo);
                    
                    if (prop != null)
                    {
                        pessoa.listaCamposValor.Add(new DtoCamposValor
                        {
                            Campo = CampoPessoa.NomeCampo,
                            Valor = prop.GetValue(pessoa).ToString()
                        });
                    }
                }
            }

            return dtoTmp;
        }
    }
}
