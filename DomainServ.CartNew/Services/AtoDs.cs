using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Domain.Cart11RI.Entities;
using Domain.CartNew.Entities;
using Domain.CartNew.Enumerations;
using Domain.CartNew.Interfaces.Repositories;
using Domain.CartNew.Interfaces.UnitOfWork;
using DomainServ.CartNew.Base;
using DomainServ.CartNew.Interfaces;
using Dto.CartNew.Entities.Cart_11RI;
using Dto.CartNew.Entities.Cart_11RI.Diversos;
using LibFunctions.Functions.DatesFunc;

namespace DomainServ.CartNew.Services
{
    public class AtoDs : DomainServiceCartNew<Ato>, IAtoDs
    {
        private readonly string[] PapelPessoaAto = { "O", "E" };  // O - outorgantes E - outorgados 
        private readonly IRepositoryAto _repositoryAto;

        public AtoDs(IUnitOfWorkDataBaseCartNew UfwCartNew) : base(UfwCartNew)
        {
            _repositoryAto = UfwCartNew.Repositories.RepositoryAto;
        }

        public bool CadastrarAto(Ato ato)
        {
            try
            {
                _repositoryAto.Add(ato);
                return true;
            }
            catch (Exception)
            {
                return false;
                throw;
            }
        }

        public bool ExisteAtoCadastrado(long numMatricula)
        {
            throw new NotImplementedException();
        }

        public Docx GetUltimaFichaGravada(string NumMatricula)
        {
            throw new NotImplementedException();
        }

        public short GetUltimoNumFicha(string NumMatricula)
        {
            throw new NotImplementedException();
        }

        public IEnumerable<DtoPessoaPesxPre> GetPessoasPrenotacao(long numeroPrenotacao)
        {
            List<DtoPessoaPesxPre> listaDtoPessoaPesxPres = new List<DtoPessoaPesxPre>();

            var lista =
                from pre in this.UfwCartNew.Repositories.GenericRepository<PESXPRE>().Get().Where(pr => (pr.SEQPRE == numeroPrenotacao) && (PapelPessoaAto.Contains(pr.REL)))
                join pes in this.UfwCartNew.Repositories.GenericRepository<PESSOAS>().Get() on pre.SEQPES equals pes.SEQPES
                orderby pre.REL, pes.NOM
                select new
                {
                    IdPessoa = pes.SEQPES,
                    TipoPessoaPrenotacao =
                        pre.REL == "E" ? TipoPessoaPrenotacao.outorgado :
                        pre.REL == "O" ? TipoPessoaPrenotacao.outorgante : TipoPessoaPrenotacao.indefinido,
                    Nome = pes.NOM,
                    Relacao = pre.REL,
                    Endereco = pes.ENDER,
                    Bairro = pes.BAI,
                    Cidade = pes.CID,
                    Telefone = pes.TEL,
                    Cep = pes.CEP,
                    Uf = pes.UF,
                    TipoDoc1 = pes.TIPODOC1,
                    Numero1 = pes.NRO1,
                    TipoDoc2 = pes.TIPODOC2,
                    Numero2 = pes.NRO2
                };

            foreach (var item in lista)
            {
                listaDtoPessoaPesxPres.Add(new DtoPessoaPesxPre() {
                    IdPessoa = item.IdPessoa,
                    IdPrenotacao = numeroPrenotacao,
                    TipoPessoaPrenotacao = item.TipoPessoaPrenotacao,
                    Nome = item.Nome,
                    Relacao = item.Relacao,
                    Endereco = item.Endereco,
                    Bairro = item.Bairro,
                    Cidade = item.Cidade,
                    Telefone = item.Telefone,
                    Cep = item.Cep,
                    Uf = item.Uf,
                    TipoDoc1 = item.TipoDoc1,
                    Numero1 = item.Numero1,
                    TipoDoc2 = item.TipoDoc2,
                    Numero2 = item.Numero2,
                });
            }

            return listaDtoPessoaPesxPres;
        }

        public IEnumerable<DtoDocxList> GetListDtoDocxAto(string NumMatricula)
        {
            var resposta =
                from Atos in this.UfwCartNew.Repositories.RepositoryAto.Get().Where(a => a.NumMatricula == NumMatricula)
                join AtoDocx in this.UfwCartNew.Repositories.GenericRepository<AtoDocx>().Get() on Atos.Id equals AtoDocx.IdAto
                join Doc in this.UfwCartNew.Repositories.GenericRepository<Docx>().Get() on AtoDocx.IdDocx equals Doc.Id
                orderby Atos.DataAto descending
                select new DtoDocxList
                {
                    IdAto = Atos.Id ?? 0,
                    IdDocx = AtoDocx.IdDocx,
                    IdCtaAcessoSist = Atos.IdCtaAcessoSist,
                    IdTipoAto = Atos.IdTipoAto,
                    IdPrenotacao = Atos.IdPrenotacao,
                    IdUsuarioCadastroDocx = Doc.IdUsuarioCadastro,
                    IdUsuarioAlteracaoDocx = Doc.IdUsuarioAlteracao,
                    IdxParagrafo = AtoDocx.IdxParagrago,
                    DataAlteracaoDocx = Doc.DataAlteracao,
                    DataCadastroDocx = Doc.DataCadastro,
                    DataDocx = Doc.DataDocx,
                    DescricaoAto = Atos.DescricaoAto,
                    NomeArquivo = Doc.NomeArq,
                    NumMatricula = Atos.NumMatricula,
                    NumFicha = Doc.NumFicha,
                    ObsAto = Atos.Observacao,
                    StatusAto = Atos.StatusAto,
                    TextoHtml = AtoDocx.TextoHtml
                };

            return resposta;
        }

        public DtoDadosImovel GetDadosImovelPrenotacao(long IdPrenotacao)
        {
            PREIMO preimo = this.UfwCartNew.Repositories.GenericRepository<PREIMO>().GetWhere(a => a.SEQPRE == IdPrenotacao).FirstOrDefault();
            DtoDadosImovel dtoDadosImovel = new DtoDadosImovel
            {
                APTO = preimo.APTO,
                BLOCO = preimo.BLOCO,
                CONTRIB = preimo.CONTRIB,
                DataAtualExtenso = DataHelper.GetDataPorExtenso("São Paulo"),
                EDIF = preimo.EDIF,
                ENDER = preimo.ENDER,
                HIPO = preimo.HIPO,
                IdPrenotacao = IdPrenotacao,
                INSCR = preimo.INSCR,
                LOTE = preimo.LOTE,
                MATRI = preimo.MATRI,
                NUM = preimo.NUM,
                OUTROS = preimo.OUTROS,
                QUADRA = preimo.QUADRA,
                RD = preimo.RD,
                SEQIMO = preimo.SEQIMO,
                SEQPRE = preimo.SEQPRE,
                SUBD = preimo.SUBD,
                TIPO = preimo.TIPO,
                TITULO = preimo.TITULO,
                TRANS = preimo.TRANS,
                VAGA = preimo.VAGA,
                NumMatricula =preimo.MATRI.ToString()
            };

            return dtoDadosImovel;
        }

    }
}
