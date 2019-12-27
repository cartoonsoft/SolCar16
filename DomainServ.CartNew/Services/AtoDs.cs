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
using Infra.Cross.Identity.Models;
using LibFunctions.Functions.IOAdmCartorio;

namespace DomainServ.CartNew.Services
{
    public class AtoDs : DomainServiceCartNew<Ato>, IAtoDs
    {
        public AtoDs(IUnitOfWorkDataBaseCartNew UfwCartNew) : base(UfwCartNew)
        {
            //
        }

        /*--------------------------------------------------------------------*/
        #region add, Update, InsertOrUpdateAto 
        public override Ato Add(Ato item)
        {
            return base.Add(item);
        }

        public override void Update(Ato item)
        {
            base.Update(item);
        }

        public DtoExecProc InsertOrUpdateAto(DtoAto atoDto, ApplicationUser usuario)
        {
            DtoExecProc execProc = new DtoExecProc();
            string msg = string.Empty;
            string descEvento = string.Empty;

            if (atoDto == null)
            {
                throw new ArgumentNullException(MethodBase.GetCurrentMethod().Name, "AtoDTO é nulo. Verifique!");
            }

            if (usuario == null)
            {
                throw new ArgumentNullException(MethodBase.GetCurrentMethod().Name, "Usuário é nulo. Verifique!");
            }

            string StatusAnt = atoDto.StatusAto;

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

                if (ato.Id == null)
                {
                    //verificar se prenotacao e matricula já fora salvos
                    if (this.AtoJaCadastrado(ato.IdPrenotacao, ato.NumMatricula))
                    {
                        throw new Exception("Já foi gerado um ato para esta Prenotação e matricula de imóvel!");
                    }

                    ato.Id = this.UfwCartNew.Repositories.RepositoryAto.GetNextValFromOracleSequence("SQ_ATO");
                    ato.StatusAto = "AC1";
                    ato.DataCadastro = DateTime.Now;
                    ato.IdUsuarioCadastro = usuario.IdUsuario;
                    execProc.Operacao = DataBaseOperacoes.insert;

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
                } else {
                    execProc.Operacao = DataBaseOperacoes.update;
                    ato.DataAlteracao = DateTime.Now;
                    ato.IdUsuarioAlteracao = usuario.IdUsuario;
                    
                    //regra para mudar o status do ato
                    ato.StatusAto = "AE";

                    this.Update(ato);
                }

                switch (execProc.Operacao)
                {
                    case DataBaseOperacoes.insert:
                        msg = "Ato inserido com sucesso";
                        descEvento = string.Format("Ato Inserido. usuario {0} em {1}", usuario.Nome, DateTime.Now.ToString());
                        break;
                    case DataBaseOperacoes.update:
                        msg = "Ato Editado com sucesso";
                        descEvento = string.Format("Ato Editado, usuário {0} em {1}", usuario.Nome, DateTime.Now.ToString());
                        break;
                }

                //ato evento
                this.UfwCartNew.Repositories.GenericRepository<AtoEvento>().Add(
                    new AtoEvento
                    {
                        Id = this.UfwCartNew.Repositories.RepositoryAto.GetNextValFromOracleSequence("SQ_ATO_EVENTO"),
                        IdAto = ato.Id ?? 0,
                        IdUsuario = usuario.IdUsuario,
                        TipoEvento = execProc.Operacao,
                        DataEvento = DateTime.Now,
                        Descricao = descEvento,
                        Status = ato.StatusAto,
                        StatusAnterior = StatusAnt,
                        Observacoes = ""
                    }
                );

                this.UfwCartNew.SaveChanges();
                this.UfwCartNew.CommitTransaction();

                execProc.Entidade = ato;
                execProc.Msg = msg;
                execProc.TipoMsg = TipoMsgResposta.ok;
                execProc.Resposta = true;
            }
            catch (Exception ex)
            {
                this.UfwCartNew.RollBackTransaction();
                execProc.TipoMsg = TipoMsgResposta.error;
                execProc.Msg = string.Format("{0}.{1} [{2} {3}]", GetType().FullName, MethodBase.GetCurrentMethod().Name, ex.Message, (ex.InnerException != null) ? ex.InnerException.Message : "");
            }

            return execProc;
        }
        #endregion

        public bool AtoJaCadastrado(long idPrenotacao, string numMatricula) 
        {
            var atoTmp = this.GetWhere(a => (a.IdPrenotacao == idPrenotacao) && (a.NumMatricula == numMatricula)).FirstOrDefault();

            return atoTmp != null;
        }

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
            List<Ato> listaAto = this.UfwCartNew.Repositories.RepositoryAto.GetListAtosPeriodo(DataIni, DataFim).Where(a => a.Ativo == true).ToList();
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
