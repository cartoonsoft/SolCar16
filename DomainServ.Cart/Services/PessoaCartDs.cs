using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Domain.Cart.Interfaces.UnitOfWork;
using DomainServ.Cart.Base;
using DomainServ.CartNew.Interfaces;
using Domain.Cart.Entities;
using Domain.Cart.Entities.Diversos;

namespace DomainServ.CartNew.Services
{
    public class PessoaCartDs : DomainServiceCartorio<PessoaCart>, IPessoaCartDs
    {
        //O - Outorgado, E - Outorgante
        private readonly string[] Relacoes = { "O", "E" };
   
        //private IEnumerable<CamposArquivoModeloDocx> listaCamposArquivoModeloDocx = null;

        public PessoaCartDs(IUnitOfWorkDataBaseCartorio UfwCart) : base(UfwCart)
        {
            //
        }

        private IEnumerable<PessoaPesxPre> GetPessoasPrenotacao(long IdPrenotacao)
        {
            var listaPessoas =
                from pre in this.UfwCart.Repositories.GenericRepository<PESXPRE>().Get().Where(pr => (pr.SEQPRE == IdPrenotacao) && (Relacoes.Contains(pr.REL)))
                join pes in this.UfwCart.Repositories.GenericRepository<PessoaCart>().Get() on pre.SEQPES equals pes.SEQPES
                orderby pes.NOM
                select new PessoaPesxPre
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
        public IEnumerable<PessoaPesxPre> GetPessoasPorPrenotacao(long IdPrenotacao)
        {
            return GetPessoasPrenotacao(IdPrenotacao);
        }

        public IEnumerable<PessoaPesxPre> GetListaOutorgadosOutorgantes(long[] listIdsPessoas, long? IdTipoAto, long IdPrenotacao)
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

            List<PessoaPesxPre> listaPessoasTmp = this.GetPessoasPrenotacao(IdPrenotacao).Where(p => listIdsPessoas.Contains(p.IdPessoa)).ToList();

            //List<CamposArquivoModeloDocx> listaCampos = this.UfwCartNew.Repositories.RepositoryArquivoModeloDocx.GetListaCamposIdTipoAto(IdTipoAto).Where(l => l.Entidade == "PESSOA").ToList();
            
            /*
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
            */
            return listaPessoasTmp;
        }
    }
}
