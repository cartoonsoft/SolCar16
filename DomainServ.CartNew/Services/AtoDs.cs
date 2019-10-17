using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AutoMapper;
using Domain.Cart11RI.Entities;
using Domain.CartNew.Entities;
using Domain.CartNew.Entities.Diversos;
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
            List<DtoPessoaPesxPre> listaDtoPessoaPesxPre = new List<DtoPessoaPesxPre>();
            List<PessoaPesxPre> listaPessoaPesxPre = this.UfwCartNew.Repositories.RepositoryAto.GetPessoasPrenotacao(numeroPrenotacao).ToList();
            listaDtoPessoaPesxPre = Mapper.Map<List<PessoaPesxPre>, List<DtoPessoaPesxPre>>(listaPessoaPesxPre);

            return listaDtoPessoaPesxPre;
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


        public IEnumerable<DtoPessoaAto> GetListPessoasAto(long? IdAto) 
        {

            return null;
        }

        public IEnumerable<DtoDocx> GetListDocxAto(long? IdAto)
        {

            return null;
        }

        public IEnumerable<DtoDadosImovel> GetDadosImoveisPrenotacao(long IdPrenotacao)
        {
            IEnumerable<DtoDadosImovel> listaDtoImoveis = new List<DtoDadosImovel>();

            var listaImoveis = this.UfwCartNew.Repositories.RepositoryAto.GetDadosImoveisPrenotacao(IdPrenotacao).ToList();
            listaDtoImoveis = Mapper.Map<List<DadosImovel>, List<DtoDadosImovel>>(listaImoveis);


            return listaDtoImoveis;
        }

        public long? GetNumSequenciaAto(long numeroMatricula)
        {
            throw new NotImplementedException();
        }

        IEnumerable<DtoDocx> IAtoDs.GetListDocxAto(long? IdAto)
        {
            throw new NotImplementedException();
        }
    }
}
