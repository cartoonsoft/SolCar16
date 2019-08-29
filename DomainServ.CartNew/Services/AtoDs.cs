using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Domain.Cart11RI.Entities;
using Domain.CartNew.Entities;
using Domain.CartNew.Interfaces.Repositories;
using Domain.CartNew.Interfaces.UnitOfWork;
using DomainServ.CartNew.Base;
using DomainServ.CartNew.Interfaces;
using Dto.CartNew.Entities.Cart_11RI.Diversos;

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
            var listaPessoas =
                from pre in this.UfwCartNew.Repositories.GenericRepository<PESXPRE>().Get().Where(pr => (pr.SEQPRE == numeroPrenotacao) && (PapelPessoaAto.Contains(pr.REL)))
                join pes in this.UfwCartNew.Repositories.GenericRepository<PESSOAS>().Get() on pre.SEQPES equals pes.SEQPES
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

            return listaPessoas;
        }

    }
}
