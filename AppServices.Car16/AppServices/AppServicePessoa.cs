using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using LibFunctions.Functions.DatesFunc;
using Domain.Car16.Entities.Car16;
using Domain.Car16.Entities.Car16New;
using Domain.Car16.Interfaces.UnitOfWork;
using Dto.Car16.Entities.Diversos;

namespace AppServices.Car16.AppServices
{
    public class AppServicePessoa : IDisposable
    {
        private readonly IUnitOfWorkDataBaseCar16 _unitOfWorkCar16;
        private readonly IUnitOfWorkDataBaseCar16New _unitOfWorkCar16New;
        private readonly string[] Relacoes = { "O", "E" };

        private List<CamposArquivoModeloDocx> listaCamposArquivoModeloDocx = null;
        private List<DtoPessoaPesxPre> listaDtoPessoaPesxPre = null;

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
            IEnumerable<DtoPessoaPesxPre> Pessoas = GetPessoasPorPrenotacao(numeroPrenotacao);

            return Pessoas;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="IdTipoAto"></param>
        /// <param name="IdPrenotacao"></param>
        /// <param name="IdMatricula"></param>
        /// <returns></returns>
        public DtoDadosImovel GetCamposModeloMatricula(long[] listIdsPessoas, long? IdTipoAto, long? IdPrenotacao, long? IdMatricula)
        {
            DtoDadosImovel dtoTmp = new DtoDadosImovel();

            //Geral 
            dtoTmp.listaCamposValor.Add(new DtoCamposValor
            {
                Campo = "DataAtualExtenso",
                Valor = DataHelper.GetDataPorExtenso("São Paulo")
            });

            //Prenotacao
            dtoTmp.listaCamposValor.AddRange(GetCamposPrenotacao(IdTipoAto, IdPrenotacao, IdMatricula));

            //Matricula
            dtoTmp.listaCamposValor.Add(new DtoCamposValor
            {
                Campo = "Matricula",
                Valor = IdMatricula.ToString()
            });

            //pessoas
            dtoTmp.Pessoas.AddRange(GetListaPessoas(listIdsPessoas, IdTipoAto, IdPrenotacao));

            //Imovel
            dtoTmp.listaCamposValor.AddRange(GetCamposImovel(IdTipoAto, IdPrenotacao, IdMatricula));

            return dtoTmp;
        }

        private List<DtoCamposValor> GetCamposPrenotacao(long? IdTipoAto, long? IdPrenotacao, long? IdMatricula)
        {
            DateTime dataTmp = DateTime.ParseExact("01/01/1800", "dd/MM/yyyy", CultureInfo.InvariantCulture);
            string valorTmp = string.Empty;

            List<DtoCamposValor> listaTmp = new List<DtoCamposValor>();
            List<CamposArquivoModeloDocx> listaCampos =  GetListaCamposIdTipoAto(IdTipoAto).Where(l => l.Entidade == "PRENOTACAO").ToList();

            PREMAD premad = _unitOfWorkCar16.Repositories.GenericRepository<PREMAD>().
                GetWhere(p => (p.SEQPRED == IdPrenotacao) && (p.TIPODATA.Trim() == "R")).OrderByDescending(o => o.DATA).FirstOrDefault();

            if (premad != null)
            {
                foreach (var item in listaCampos)
                {
                    var prop = premad.GetType().GetProperty(item.Campo);

                    if (prop != null)
                    {
                        valorTmp = (prop.GetValue(premad) == null)? "" : prop.GetValue(premad).ToString();

                        if (item.Campo == "IdPrenotacao")
                        {
                            valorTmp = IdPrenotacao.ToString();
                        }

                        if (item.Campo == "DATA")
                        {
                            valorTmp = dataTmp.AddDays(premad.DATA).ToString("dd/MM/yyyy");
                        }

                        listaTmp.Add(new DtoCamposValor
                        {
                            Campo = item.NomeCampo,
                            Valor = valorTmp
                        });
                    }
                }
            }

            return listaTmp;
        }

        private List<DtoCamposValor> GetCamposImovel(long? IdTipoAto, long? IdPrenotacao, long? IdMatricula)
        {
            List<DtoCamposValor> listaTmp = new List<DtoCamposValor>();
            List<CamposArquivoModeloDocx> listaCampos = GetListaCamposIdTipoAto(IdTipoAto).Where(l => l.Entidade == "IMOVEL").ToList();

            PREIMO Imovel = _unitOfWorkCar16.Repositories.GenericRepository<PREIMO>().
                GetWhere(i => i.SEQPRE == IdPrenotacao && i.MATRI == IdMatricula).FirstOrDefault();

            if (Imovel != null)
            {
                foreach (var item in listaCampos)
                {
                    var prop = Imovel.GetType().GetProperty(item.Campo);

                    if (prop != null)
                    {
                        listaTmp.Add(new DtoCamposValor
                        {
                            Campo = item.NomeCampo,
                            Valor = (prop.GetValue(Imovel) == null) ? "" : prop.GetValue(Imovel).ToString()
                        });
                    }
                }
            }

            return listaTmp;
        } 

        private List<DtoPessoaPesxPre> GetPessoasPorPrenotacao(long? IdPrenotacao)
        {
            if (listaDtoPessoaPesxPre == null)
            {
                var listaPessoas =
                    from pre in _unitOfWorkCar16.Repositories.GenericRepository<PESXPRE>().Get().Where(pr => (pr.SEQPRE == IdPrenotacao) && (Relacoes.Contains(pr.REL)))
                    join pes in _unitOfWorkCar16.Repositories.GenericRepository<PESSOA>().Get() on pre.SEQPES equals pes.SEQPES 
                    orderby pes.NOM
                    select new DtoPessoaPesxPre
                    {
                        IdPessoa = pes.SEQPES,
                        TipoPessoa = pre.REL == "O" ? "Outorgado": "Outorgante",
                        Relacao = pre.REL,
                        Bairro = pes.BAI,
                        Cidade = pes.CID,
                        CEP = pes.CEP,
                        Endereco = pes.ENDER,
                        Nome = pes.NOM,
                        TipoDoc1 = pes.TIPODOC1,
                        Numero1 = pes.NRO1,
                        TipoDoc2 = pes.TIPODOC2,
                        Numero2 = pes.NRO2,
                        Telefone = pes.TEL,
                        UF = pes.UF
                    };
                listaDtoPessoaPesxPre = listaPessoas.ToList();
            }

            return listaDtoPessoaPesxPre;
        }

        private List<CamposArquivoModeloDocx> GetListaCamposIdTipoAto(long? IdTipoAto)
        {
            List<CamposArquivoModeloDocx> listaTmp = new List<CamposArquivoModeloDocx>();

            if (listaCamposArquivoModeloDocx == null) {

                var listaCampos =
                    from campos in _unitOfWorkCar16New.Repositories.GenericRepository<CamposArquivoModeloDocx>().Get()
                    .Where(c => c.IdTipoAto == IdTipoAto)
                    orderby campos.NomeCampo
                    select new 
                    {
                        campos.Id,
                        campos.IdAcessoSistema,
                        campos.IdTipoAto,
                        campos.NomeCampo,
                        campos.PlaceHolder,
                        campos.Entidade,
                        campos.Campo
                    };

                foreach (var item in listaCampos)
                {
                    listaTmp.Add(new CamposArquivoModeloDocx {
                        Id = item.Id,
                        IdAcessoSistema = item.IdAcessoSistema,
                        IdTipoAto = item.IdTipoAto,
                        NomeCampo = item.NomeCampo,
                        Campo = item.Campo,
                        Entidade = item.Entidade,
                        PlaceHolder = item.PlaceHolder
                    });
                }

                listaCamposArquivoModeloDocx = listaTmp;
            }

            return listaCamposArquivoModeloDocx;
        }

        private List<DtoPessoaPesxPre> GetListaPessoas(long[] listIdsPessoas, long? IdTipoAto, long? IdPrenotacao)
        {
            bool povoarCampo = false;

            string[] CamposOutorgado = {
                "NomeOutorgado",
                "EnderecoOutorgado",
                "BairroOutorgado",
                "CidadeOutorgado",
                "UFOutorgado",
                "CEPOutorgado",
                "TelefoneOutorgado",
                "TipoDoc1Outorgado",
                "NumDoc1Outorgado",
                "TipoDoc2Outorgado",
                "NumDoc2Outorgado",
                "NomeOutorgado",
                "EnderecoOutorgado",
                "BairroOutorgado",
                "CidadeOutorgado",
                "UFOutorgado",
                "CEPOutorgado",
                "TelefoneOutorgado",
                "TipoDoc1Outorgado",
                "NumDoc1Outorgado",
                "TipoDoc2Outorgado",
                "NumDoc2Outorgado"
            };

            string[] CamposOutorgante = {
                "NomeOutorgante",
                "EnderecoOutorgante",
                "BairroOutorgante",
                "CidadeOutorgante",
                "UFOutorgante",
                "CEPOutorgante",
                "TelefoneOutorgante",
                "TipoDoc1Outorgante",
                "NumDoc1Outorgante",
                "TipoDoc2Outorgante",
                "NumDoc2Outorgante",
                "NomeOutorgante",
                "EnderecoOutorgante",
                "BairroOutorgante",
                "CidadeOutorgante",
                "UFOutorgante",
                "CEPOutorgante",
                "TelefoneOutorgante",
                "TipoDoc1Outorgante",
                "NumDoc1Outorgante",
                "TipoDoc2Outorgante",
                "NumDoc2Outorgante"
            };

            List<DtoPessoaPesxPre> listaPessoasTmp = GetPessoasPorPrenotacao(IdPrenotacao).Where(p => listIdsPessoas.Contains(p.IdPessoa)).ToList();
            List<CamposArquivoModeloDocx> listaCampos = GetListaCamposIdTipoAto(IdTipoAto).Where(l => l.Entidade == "PESSOA").ToList();

            foreach (var pessoa in listaPessoasTmp)
            {
                foreach (var CampoPessoa in listaCampos)
                {
                    povoarCampo = false;
                    //Sse for outorgado
                    if (pessoa.Relacao == "O")
                    {
                        povoarCampo = CamposOutorgado.Contains(CampoPessoa.NomeCampo); 

                    } else if (pessoa.Relacao == "E") //outrogante
                    {
                        povoarCampo = CamposOutorgante.Contains(CampoPessoa.NomeCampo);
                    }

                    if (povoarCampo)
                    {
                        var prop = pessoa.GetType().GetProperty(CampoPessoa.Campo);

                        if (prop != null)
                        {
                            pessoa.listaCamposValor.Add(new DtoCamposValor
                            {
                                Campo = CampoPessoa.NomeCampo,
                                Valor = (prop.GetValue(pessoa) == null) ? "" : prop.GetValue(pessoa).ToString()
                            });
                        }
                    }
                }
            }

            return listaPessoasTmp;
        }
    }
}
