using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using AutoMapper;
using Domain.CartNew.Entities;
using Domain.CartNew.Entities.Diversos;
using Domain.CartNew.Enumerations;
using Domain.CartNew.Interfaces.UnitOfWork;
using DomainServ.CartNew.Base;
using DomainServ.CartNew.Interfaces;
using Dto.CartNew.Base;
using Dto.CartNew.Entities.Cart_11RI;
using Dto.CartNew.Entities.Cart_11RI.Diversos;
using LibFunctions.Functions.IOAdmCartorio;

namespace DomainServ.CartNew.Services
{
    public class AtoDs : DomainServiceCartNew<Ato>, IAtoDs
    {
        public AtoDs(IUnitOfWorkDataBaseCartNew UfwCartNew, string pathErroLog = null) : base(UfwCartNew, pathErroLog)
        {
            //
        }

        /*--------------------------------------------------------------------*/
        #region add, Update, InsertOrUpdateAto 
        public override Ato Add(Ato item)
        {
            //base.Add(item);

            return null;
        }

        public override void Update(Ato item)
        {
            //base.Update(item);
        }

        public DtoExecProc InsertOrUpdateAto(DtoAto atoDto, string idUsuario)
        {
            string nullMsg = "AtoDTO é nulo!";

            if (atoDto == null)
            {
                throw new ArgumentNullException(MethodBase.GetCurrentMethod().Name, nullMsg);
            }

            string StatusAnt = atoDto.StatusAto;
            DtoExecProc execProc = new DtoExecProc();

            /*-- status ato ----------------------------------------------------
            AC1	Ato Criado
            AC2	Ato Criado
            AE	Ato em Escrita
            AI	Confirmado ajuste impressão
            CF	Ato conferido
            CL	Ato cancelado
            GF	Gerado Ficha
            AF	Ato Finalizado
            ----------------------------------------------------------------- */

            try
            {
                this.UfwCartNew.BeginTransaction();
                var ato = Mapper.Map<DtoAto, Ato>(atoDto);

                if (atoDto.Id == null)
                {
                    execProc.IdEntidade = this.UfwCartNew.Repositories.RepositoryAto.GetNextValFromOracleSequence("SQ_ATO");
                    execProc.Operacao = DataBaseOperacoes.insert;
                    atoDto.Id = execProc.IdEntidade;
                    atoDto.StatusAto = "AC1";

                    this.Add(ato);

                    //insert pessoas
                    foreach (var pessoa in atoDto.ListaPessoasAto)
                    {
                        this.UfwCartNew.Repositories.GenericRepository<AtoPessoa>().Add(
                            new AtoPessoa
                            {
                                IdAto = ato.Id ?? 0,
                                Relacao = pessoa.Relacao,
                                SeqPes = pessoa.IdPessoa,
                                TipoPessoa = pessoa.TipoPessoa
                            }
                        );
                    }

                    execProc.Msg = "Dados incluidos com sucesso con sucesso";
                } else
                {
                    execProc.Operacao = DataBaseOperacoes.update;
                    this.Update(ato);
                    execProc.Msg = "Dados Atualizados com sucesso con sucesso";
                }

                //ato evento
                this.UfwCartNew.Repositories.GenericRepository<AtoEvento>().Add(
                    new AtoEvento
                    {
                        Id = null,
                        IdAto = ato.Id ?? 0,
                        IdUsuario = idUsuario,
                        TipoEvento = execProc.Operacao,
                        Observacoes = "",
                        Status = ato.StatusAto,
                        StatusAnterior = StatusAnt,
                        DataEvento = DateTime.Now,
                        Descricao = "",
                    }
                );

                this.UfwCartNew.SaveChanges();
                this.UfwCartNew.CommitTransaction();

                execProc.TipoMsg = TipoMsgResposta.ok;
                execProc.Resposta = true;
            }
            catch (Exception ex)
            {
                this.UfwCartNew.RollBackTransaction();
                execProc.Msg = string.Format("{0}.{1} [{2} {3}]", GetType().FullName, MethodBase.GetCurrentMethod().Name, ex.Message, (ex.InnerException != null) ? ex.InnerException.Message : "");
            }

            return null;
        }
        #endregion

        public bool ExisteAtoCadastrado(string NumMatricula)
        {
            throw new NotImplementedException();
        }
        public long? GetNumSequenciaAto(string NumMatricula)
        {
            throw new NotImplementedException();
        }

        public short GetUltimoNumFicha(string NumMatricula)
        {
            throw new NotImplementedException();
        }

        public IEnumerable<DtoDocx> GerarFichas(DtoAto ato)
        {
            throw new NotImplementedException();
        }

        public IEnumerable<DtoAto> GetListAtosMatricula(string NumMatricula)
        {
            List<DtoAto> listaDtoAto = new List<DtoAto>();
            List<Ato> listaAto = this.UfwCartNew.Repositories.RepositoryAto.GetListAtosMatricula(NumMatricula).ToList();
            listaDtoAto = Mapper.Map<List<Ato>, List<DtoAto>>(listaAto);

            return listaDtoAto;
        }

        public IEnumerable<DtoAto> GetListAtosPeriodo(DateTime DataIni, DateTime DataFim)
        {
            List<DtoAto> listaDtoAto = new List<DtoAto>();
            List<Ato> listaAto = this.UfwCartNew.Repositories.RepositoryAto.GetListAtosPeriodo(DataIni, DataFim).ToList();
            listaDtoAto = Mapper.Map<List<Ato>, List<DtoAto>>(listaAto);

            return listaDtoAto;
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
            List<DtoPessoaAto> lista = new List<DtoPessoaAto>();

            List<PessoaAto> listaPessoaAto = this.UfwCartNew.Repositories.RepositoryAto.GetListPessoasAto(IdAto).ToList();
            lista = Mapper.Map<List<PessoaAto>, List<DtoPessoaAto>>(listaPessoaAto);

            return lista;
        }

        public IEnumerable<DtoPessoaPesxPre> GetListPessoasPrenotacao(long IdPrenotacao)
        {
            List<DtoPessoaPesxPre> listaDtoPessoaPesxPre = new List<DtoPessoaPesxPre>();
            List<PessoaPesxPre> listaPessoaPesxPre = this.UfwCartNew.Repositories.RepositoryAto.GetListPessoasPrenotacao(IdPrenotacao).ToList();
            listaDtoPessoaPesxPre = Mapper.Map<List<PessoaPesxPre>, List<DtoPessoaPesxPre>>(listaPessoaPesxPre);

            return listaDtoPessoaPesxPre;
        }

        public IEnumerable<DtoPessoaPesxPre> GetListPessoas(long[] idsPessoas, long? idPrenotacao)
        {
            List<DtoPessoaPesxPre> listaDtoPessoaPesxPre = new List<DtoPessoaPesxPre>();
            List<PessoaPesxPre> listaPessoaPesxPre = this.UfwCartNew.Repositories.RepositoryAto.GetListPessoas(idsPessoas, idPrenotacao).ToList();
            listaDtoPessoaPesxPre = Mapper.Map<List<PessoaPesxPre>, List<DtoPessoaPesxPre>>(listaPessoaPesxPre);

            return listaDtoPessoaPesxPre;
        }

        public DtoPessoaPesxPre GetPessoa(long idPessoa, long? idPrenotacao)
        {
            DtoPessoaPesxPre dtoPessoaPesxPre = new DtoPessoaPesxPre();
            PessoaPesxPre pessoaPesxPre = this.UfwCartNew.Repositories.RepositoryAto.GetPessoa(idPessoa, idPrenotacao);

            dtoPessoaPesxPre = Mapper.Map<PessoaPesxPre, DtoPessoaPesxPre>(pessoaPesxPre);

            return dtoPessoaPesxPre;
        }

        public IEnumerable<DtoDocx> GetListDocxAto(long? IdAto)
        {
            List<DtoDocx> listaDtoDocx = new List<DtoDocx>();

            var lista = this.UfwCartNew.Repositories.RepositoryAto.GetListDocxAto(IdAto).ToList();
            listaDtoDocx = Mapper.Map<List<Docx>, List<DtoDocx>>(lista);

            return listaDtoDocx;
        }

        public DtoDadosImovel GetDadosImovel(long IdPrenotacao, string NumMatricula)
        {
            DtoDadosImovel dtoImovel = new DtoDadosImovel();
            DadosImovel imovel = this.UfwCartNew.Repositories.RepositoryAto.GetDadosImovel(IdPrenotacao, NumMatricula);
            dtoImovel = Mapper.Map<DadosImovel, DtoDadosImovel>(imovel);

            return dtoImovel;
        }

    }
}
