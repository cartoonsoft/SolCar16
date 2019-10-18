using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AutoMapper;
using Domain.CartNew.Entities;
using Domain.CartNew.Entities.Diversos;
using Domain.CartNew.Interfaces.UnitOfWork;
using DomainServ.CartNew.Base;
using DomainServ.CartNew.Interfaces;
using Dto.CartNew.Entities.Cart_11RI;
using Dto.CartNew.Entities.Cart_11RI.Diversos;

namespace DomainServ.CartNew.Services
{
    public class AtoDs : DomainServiceCartNew<Ato>, IAtoDs
    {

        public AtoDs(IUnitOfWorkDataBaseCartNew UfwCartNew) : base(UfwCartNew)
        {
            //
        }

        public bool ExisteAtoCadastrado(long numMatricula)
        {
            throw new NotImplementedException();
        }

        public IEnumerable<DtoDocx> GerarFichas(DtoAto ato)
        {
            throw new NotImplementedException();
        }

        public IEnumerable<DtoAto> GetListAtosMatricula(string NumMatricula)
        {
            List<DtoAto> dtoDocxList = new List<DtoAto>();

            return dtoDocxList;
        }

        public IEnumerable<DtoAto> GetListAtosPeriodo(DateTime DataIni, DateTime DataFim)
        {
            List<DtoAto> dtoDocxList = new List<DtoAto>();

            return dtoDocxList;
        }

        public IEnumerable<DtoCamposValor> GetListCamposAto(long IdAto)
        {
            List<DtoCamposValor> dtoCampoValor = new List<DtoCamposValor>();

            return dtoCampoValor;
        }

        public IEnumerable<DtoCamposValor> GetListCamposImovel(long numeroMatricula)
        {
            List<DtoCamposValor> dtoCampoValor = new List<DtoCamposValor>();

            return dtoCampoValor;
        }

        public IEnumerable<DtoCamposValor> GetListCamposPessoa(long IdPessoa)
        {
            List<DtoCamposValor> dtoCampoValor = new List<DtoCamposValor>();

            return dtoCampoValor;
        }

        public IEnumerable<DtoDocx> GetListDocxAto(long? IdAto)
        {
            List<DtoDocx> listaDtoDocx = new List<DtoDocx>();

            var lista = this.UfwCartNew.Repositories.RepositoryAto.GetListDocxAto(IdAto).ToList();
            listaDtoDocx = Mapper.Map<List<Docx>, List<DtoDocx>>(lista);

            return listaDtoDocx;
        }

        public IEnumerable<DtoDadosImovel> GetListImoveisPrenotacao(long IdPrenotacao)
        {
            IEnumerable<DtoDadosImovel> listaDtoImoveis = new List<DtoDadosImovel>();

            var listaImoveis = this.UfwCartNew.Repositories.RepositoryAto.GetListImoveisPrenotacao(IdPrenotacao).ToList();
            listaDtoImoveis = Mapper.Map<List<DadosImovel>, List<DtoDadosImovel>>(listaImoveis);

            return listaDtoImoveis;
        }

        public IEnumerable<DtoPessoaAto> GetListPessoasAto(long? IdAto)
        {
            throw new NotImplementedException();
        }

        public IEnumerable<DtoPessoaPesxPre> GetListPessoasPrenotacao(long numeroPrenotacao)
        {
            List<DtoPessoaPesxPre> listaDtoPessoaPesxPre = new List<DtoPessoaPesxPre>();
            List<PessoaPesxPre> listaPessoaPesxPre = this.UfwCartNew.Repositories.RepositoryAto.GetListPessoasPrenotacao(numeroPrenotacao).ToList();
            listaDtoPessoaPesxPre = Mapper.Map<List<PessoaPesxPre>, List<DtoPessoaPesxPre>>(listaPessoaPesxPre);

            return listaDtoPessoaPesxPre;
        }

        public long? GetNumSequenciaAto(long numeroMatricula)
        {
            throw new NotImplementedException();
        }
    }
}
