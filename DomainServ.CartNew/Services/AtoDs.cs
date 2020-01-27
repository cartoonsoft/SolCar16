﻿using System;
using System.Collections.Generic;
using System.Globalization;
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
        //status que permitem a edição do texto do ato, data ato 
        private readonly string[] _statusEdtTexto = { "AC1", "AC2", "AE", "AR" };

        //status que permitem a edição doos campos: Livro, ficha, num seq., frente/verso, distancia topo, descrição e observaçoes  
        private readonly string[] _statusEdtDadosImp = { "CT", "GF" };

        //status finais
        private readonly string[] _statusAtoFinalizado = { "CL", "AF" };

        public AtoDs(IUnitOfWorkDataBaseCartNew UfwCartNew) : base(UfwCartNew)
        {
            //
        }

        public string[] StatusEdtTexto()
        {
            return _statusEdtTexto;
        }
        public string[] StatusEdtDadosImp()
        {
            return _statusEdtDadosImp;
        }
        public string[] StatusAtoFinalizado()
        {
            return _statusAtoFinalizado;
        }

        public DtoExecProc SetStatusAto(long? idAto, string statusAto, ApplicationUser usuario)
        {
            DtoExecProc execProc = new DtoExecProc();
            bool AlterouStatus = false;

            if (usuario == null)
            {
                throw new ArgumentNullException(MethodBase.GetCurrentMethod().Name, "Usuário é nulo. Verifique!");
            }

            try
            {
                if (idAto != null)
                {
                    Ato ato = this.UfwCartNew.Repositories.RepositoryAto.GetById(idAto);

                    if (ato != null)
                    {
                        this.UfwCartNew.BeginTransaction();
                        execProc.Operacao = DataBaseOperacoes.update;
                        AlterouStatus = this.UfwCartNew.Repositories.RepositoryAto.SetStatusAto(idAto, usuario.Id);
                        string codigoAto = ato.NumMatricula + "/" + ato.SiglaSeqAto + ": " + ato.NumSequenciaAto.ToString();

                        if (AlterouStatus)
                        {
                            this.UfwCartNew.Repositories.GenericRepository<AtoEvento>().Add(
                                new AtoEvento
                                {
                                    Id = this.UfwCartNew.Repositories.RepositoryAto.GetNextValFromOracleSequence("SQ_ATO_EVENTO"),
                                    IdAto = idAto ?? 0,
                                    IdUsuario = usuario.Id,
                                    TipoEvento = execProc.Operacao,
                                    DataEvento = DateTime.Now,
                                    Descricao = string.Format(CultureInfo.CurrentCulture, "Ato {0} alterado para status {1} pelo usuário {2}.", codigoAto, statusAto, usuario.Nome),
                                    Status = statusAto,
                                    StatusAnterior = ato.StatusAto
                                }
                            );

                            execProc.Resposta = true;
                            execProc.Msg = string.Format(CultureInfo.CurrentCulture, "Status alterado de {0} para {1} com sucesso!", ato.StatusAto, statusAto);
                            execProc.TipoMsg = TipoMsgResposta.ok;
                        }

                        this.UfwCartNew.SaveChanges();
                        this.UfwCartNew.CommitTransaction();
                    }
                }
            }
            catch (Exception ex)
            {
                if (execProc.Operacao == DataBaseOperacoes.update)
                {
                    this.UfwCartNew.RollBackTransaction();
                }
                execProc.TipoMsg = TipoMsgResposta.error;
                execProc.Msg = string.Format(CultureInfo.CurrentCulture, "{0}.{1} [{2} {3}]", GetType().FullName, MethodBase.GetCurrentMethod().Name, ex.Message, (ex.InnerException != null) ? ex.InnerException.Message : "");
            }

            return execProc;
        }

        #region add, Update, InsertOrUpdateAto 
        public override Ato Add(Ato item)
        {
            if (item != null)
            {
                item.Ativo = true;
                // verificar se prenotacao e matricula já fora salvos
                if (this.AtoJaCadastrado(item.IdPrenotacao, item.NumMatricula))
                {
                    throw new Exception(string.Format(CultureInfo.CurrentCulture, "Já foi gerado um ato para esta Prenotação {0} e a matricula {1} de imóvel!", item.IdPrenotacao, item.NumMatricula));
                }

                // verificar se existe ato em andamento para matricula.
                Ato atoTmp = this.GetWhere(a => (a.NumMatricula == item.NumMatricula) && (_statusAtoFinalizado.Contains(a.StatusAto))).FirstOrDefault();
                if (atoTmp != null)
                {
                    string msg = string.Format(CultureInfo.CurrentCulture, "O Ato {0} para a matricula {1} está em andamento, e deve ser finalizado para que se possa incluir um novo ato para o imóvel!", atoTmp.Id.ToString(), atoTmp.NumMatricula);
                    throw new ArgumentException(msg);
                }
            }

            return base.Add(item);
        }

        public override void Update(Ato item)
        {
            if (item != null)
            {
                if (!_statusEdtTexto.Contains(item.StatusAto))
                {
                    throw new ArgumentException(string.Format(CultureInfo.CurrentCulture, "O Ato {0} no status {1} não pode ser atualizado!", item.Id.ToString(), item.StatusAto));
                }
            }

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
            bool AlterouTexto = atoDto.TextoAnterior != atoDto.Texto;

            /*-- status ato ----------------------------------------------------
            AC1	Ato Criado
            AC2	Ato Criado importacao
            AE	Ato foi editado e está em Escrita
            AR	Ato em Escrita - colocado em reedicao na confereincia (achou algum erro de escrita).
            CT  Ato conferido texto
            GF	Gerado Ficha
            CI	Ato conferido Impressão (docx)
            CL	Ato cancelado
            AF	Ato Finalizado
            ----------------------------------------------------------------- */

            try
            {
                this.UfwCartNew.BeginTransaction();
                var ato = Mapper.Map<DtoAto, Ato>(atoDto);

                if (ato.Id == null)
                {
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
                    
                    if ((StatusAnt == "AC1") || (StatusAnt == "AC2")) 
                    {
                        ato.StatusAto = "AE";
                    }

                    ato.DataAlteracao = DateTime.Now;
                    ato.IdUsuarioAlteracao = usuario.IdUsuario;

                    this.Update(ato);
                }

                switch (execProc.Operacao)
                {
                    case DataBaseOperacoes.insert:
                        msg = "Ato inserido com sucesso";
                        descEvento = string.Format(CultureInfo.CurrentCulture, "Ato Inserido. usuario {0} em {1}", usuario.Nome, DateTime.Now.ToString(CultureInfo.CurrentCulture));
                        break;
                    case DataBaseOperacoes.update:
                        msg = "Ato Editado com sucesso";
                        descEvento = string.Format(CultureInfo.CurrentCulture, "Ato Editado, usuário {0} em {1}", usuario.Nome, DateTime.Now.ToString(CultureInfo.CurrentCulture));
                        break;
                }

                //ato evento
                if ((StatusAnt != ato.StatusAto) ||(AlterouTexto))
                {
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
                            Observacoes = AlterouTexto? "Texto do ato foi alterado pelo usuário": ""
                        }
                    );
                }

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
                execProc.Msg = string.Format(CultureInfo.CurrentCulture, "{0}.{1} [{2} {3}]", GetType().FullName, MethodBase.GetCurrentMethod().Name, ex.Message, (ex.InnerException != null) ? ex.InnerException.Message : "");
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

        public IEnumerable<DtoDocx> GetListDocxAto(long? IdAto)
        {
            List<DtoDocx> listaDtoDocx = new List<DtoDocx>();
            var lista = this.UfwCartNew.Repositories.RepositoryAto.GetListDocxAto(IdAto).ToList();
            listaDtoDocx = Mapper.Map<List<Docx>, List<DtoDocx>>(lista);

            return listaDtoDocx;
        }

        public IEnumerable<DtoAtoEvento> GetListHistoricoAto(long? IdAto)
        {
            List<DtoAtoEvento> listaDto = new List<DtoAtoEvento>();
            var lista = this.UfwCartNew.Repositories.RepositoryAto.GetListHistoricoAto(IdAto).ToList();
            listaDto = Mapper.Map<List<AtoEvento>, List<DtoAtoEvento>>(lista);

            return listaDto;
        }

        public DtoPessoaPesxPre GetPessoa(long idPessoa, long? idPrenotacao)
        {
            DtoPessoaPesxPre dtoPessoaPesxPre = new DtoPessoaPesxPre();
            PessoaPesxPre pessoaPesxPre = this.UfwCartNew.Repositories.RepositoryAto.GetPessoa(idPessoa, idPrenotacao);
            dtoPessoaPesxPre = Mapper.Map<PessoaPesxPre, DtoPessoaPesxPre>(pessoaPesxPre);

            return dtoPessoaPesxPre;
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
