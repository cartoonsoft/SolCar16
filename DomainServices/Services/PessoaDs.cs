using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Domain.Car16.Entities.Car16;
using Domain.Car16.Entities.Car16New;
using Domain.Cartorio.Interfaces.UnitOfWork;
using DomainServices.Base;
using DomainServices.Interfaces;
using Dto.Cartorio.Entities.Diversos;

namespace DomainServices.Services
{
    public class PessoaDs : DomainServiceCartorioNew<PessoaCartorioNew>, IPessoaDs
    {
        //O - Outorgado, E - Outorgante
        private readonly string[] Relacoes = { "O", "E" };
        //private IEnumerable<CamposArquivoModeloDocx> listaCamposArquivoModeloDocx = null;


        public PessoaDs(IUnitOfWorkDataBaseCartorio UfwCart, IUnitOfWorkDataBaseCartorioNew UfwCartNew) : base(UfwCart, UfwCartNew)
        {
            //
        }

        private IEnumerable<DtoPessoaPesxPre> GetPessoasPrenotacao(long IdPrenotacao)
        {
            var listaPessoas =
                from pre in this.UfwCart.Repositories.GenericRepository<PESXPRE>().Get().Where(pr => (pr.SEQPRE == IdPrenotacao) && (Relacoes.Contains(pr.REL)))
                join pes in this.UfwCart.Repositories.GenericRepository<PessoaCartorio>().Get() on pre.SEQPES equals pes.SEQPES
                orderby pes.NOM
                select new DtoPessoaPesxPre
                {
                    IdPessoa = pes.SEQPES,
                    TipoPessoa = pre.REL == "O" ? "Outorgado" : "Outorgante",
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

            return listaPessoas.ToList();
        }

        /// <summary>
        /// Lista de Pessoa por Prenotação
        /// </summary>
        /// <param name="numeroPrenotacao"></param>
        /// <returns></returns>
        public IEnumerable<DtoPessoaPesxPre> GetPessoasPorPrenotacao(long IdPrenotacao)
        {
            return GetPessoasPrenotacao(IdPrenotacao);
        }

        public IEnumerable<DtoPessoaPesxPre> GetListaOutorgadosOutorgantes(long[] listIdsPessoas, long? IdTipoAto, long IdPrenotacao)
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

            List<DtoPessoaPesxPre> listaPessoasTmp = this.GetPessoasPrenotacao(IdPrenotacao).Where(p => listIdsPessoas.Contains(p.IdPessoa)).ToList();
            List<CamposArquivoModeloDocx> listaCampos = this.UfwCartNew.Repositories.RepositoryArquivoModeloDocx.GetListaCamposIdTipoAto(IdTipoAto).Where(l => l.Entidade == "PESSOA").ToList();
            
            foreach (var pessoa in listaPessoasTmp)
            {
                foreach (var CampoPessoa in listaCampos)
                {
                    povoarCampo = false;
                    //Sse for outorgado
                    if (pessoa.Relacao == "O")
                    {
                        povoarCampo = CamposOutorgado.Contains(CampoPessoa.NomeCampo);

                    }
                    else if (pessoa.Relacao == "E") //outrogante
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
