using System;
using System.Collections.Generic;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AppServCart11RI.Base;
using AppServCart11RI.Cartorio;
using AppServices.Cartorio.Interfaces;
using AutoMapper;
using Domain.Cart11RI.Entities;
using Domain.CartNew.Entities;
using Domain.CartNew.Enumerations;
using Domain.CartNew.Interfaces.UnitOfWork;
using Dto.CartNew.Entities.Cart_11RI;
using Dto.CartNew.Entities.Cart_11RI.Diversos;
using GemBox.Document;

namespace AppServCart11RI.AppServices
{
    public class AppServiceAtos : AppServiceCartorio11RI<DtoAto, Ato>, IAppServiceAtos
    {
        private List<DtoPessoaPesxPre> listaPessoasPrenotacao = null;  //PESXPRE

        /// <summary>
        /// Construtor
        /// </summary>
        /// <param name="UfwCartNew"></param>
        public AppServiceAtos(IUnitOfWorkDataBaseCartNew UfwCartNew) : base(UfwCartNew)
        {
            //
        }

        public void AtualizarAto(DtoAto Ato)
        {
            throw new NotImplementedException();
        }

        public bool BloquearAto(long IdAto)
        {
            throw new NotImplementedException();
        }

        public bool BloquearMatricula(string NumMatricula)
        {
            throw new NotImplementedException();
        }

        public bool ConfirmarAjusteImpressaoAto(long IdAto)
        {
            throw new NotImplementedException();
        }

        public bool ConfirmarFicha(long IdDocx)
        {
            throw new NotImplementedException();
        }

        public void DesativarAto(long IdAto)
        {
            throw new NotImplementedException();
        }

        public bool ExisteAtoCadastrado(string NumMatricula)
        {
            throw new NotImplementedException();
        }

        public IEnumerable<DtoDocx> GerarFichas(long IdAto)
        {
            throw new NotImplementedException();
        }

        public IEnumerable<DtoAto> GetListAtosMatricula(string NumMatricula)
        {
            throw new NotImplementedException();
        }

        public IEnumerable<DtoAto> GetListAtosPeriodo(DateTime DataIni, DateTime DataFim)
        {
            throw new NotImplementedException();
        }

        public IEnumerable<DtoCamposValor> GetListCamposAto(long IdAto)
        {
            throw new NotImplementedException();
        }

        public IEnumerable<DtoCamposValor> GetListCamposImovel(string NumMatricula)
        {
            throw new NotImplementedException();
        }

        public IEnumerable<DtoCamposValor> GetListCamposPessoa(long IdPessoa)
        {
            throw new NotImplementedException();
        }

        public IEnumerable<DtoDocx> GetListDocxAto(long? IdAto)
        {
            throw new NotImplementedException();
        }

        public IEnumerable<DtoDadosImovel> GetListImoveisPrenotacao(long IdPrenotacao)
        {
            List<DtoDadosImovel> listaImoveis = new List<DtoDadosImovel>();

            listaImoveis = this.DsFactoryCartNew.AtoDs.GetListImoveisPrenotacao(IdPrenotacao).ToList();


            return listaImoveis;
        }

        public IEnumerable<DtoPessoaAto> GetListPessoasAto(long? IdAto)
        {
            throw new NotImplementedException();
        }

        public IEnumerable<DtoPessoaPesxPre> GetListPessoasPrenotacao(long IdPrenotacao)
        {
            throw new NotImplementedException();
        }

        public DtoDadosImovel GetDadosImovel(long IdPrenotacao, string NumMatricula)
        {
            return this.DsFactoryCartNew.AtoDs.GetDadosImovel(IdPrenotacao, NumMatricula);
        }

        public long? GetNumSequenciaTipoAto(string NumMatricula, long IdTipoAto)
        {
            throw new NotImplementedException();
        }

        public void ImprimirFicha(long IdDocx)
        {
            throw new NotImplementedException();
        }

        public void ImprimirFichasAto(long IdAto)
        {
            throw new NotImplementedException();
        }

        public void ImprimirMinutaAto(long IdAto)
        {
            throw new NotImplementedException();
        }

        public void NovoAto(DtoAto Ato)
        {
            throw new NotImplementedException();
        }

        public bool ReabrirAto(long IdAto)
        {
            throw new NotImplementedException();
        }

        public void UploadFicha(long IdDocx)
        {
            throw new NotImplementedException();
        }

        public DtoReservaImovel ProcReservarMatImovel(TipoReservaMatImovel TipoReserva, long IdPrenotacao, string NumMatricula, string IdUsuario)
        {
            DtoReservaImovel reserva = new DtoReservaImovel();
            DtoDadosImovel Imovel = this.GetDadosImovel(IdPrenotacao, NumMatricula);
            PrenotacaoImovel PreImo;

            reserva.Resposta = true;

            if (Imovel != null)
            {
                reserva.Imovel = Imovel;

                switch (TipoReserva)
                {
                    case TipoReservaMatImovel.Reservar:

                        PreImo = this.UfwCartNew.Repositories.GenericRepository<PrenotacaoImovel>().GetWhere(p => 
                            (p.IdPrenotacao == IdPrenotacao) && 
                            (p.NumMatricula == NumMatricula) &&
                            (p.IdUsuario != IdUsuario)).FirstOrDefault();

                        if (PreImo != null)
                        {
                            reserva.TipoMsg = TipoMsgResposta.error;
                            reserva.Msg = "Imóvel já reservado por outro usuário!";
                            reserva.Resposta = false;
                        }

                        if (reserva.Resposta)
                        {
                            PreImo = this.UfwCartNew.Repositories.GenericRepository<PrenotacaoImovel>().GetWhere(p => 
                                (p.IdPrenotacao == IdPrenotacao) && 
                                (p.NumMatricula == NumMatricula) && 
                                (p.IdUsuario == IdUsuario)).FirstOrDefault();

                            if (PreImo != null)
                            {
                                reserva.TipoMsg = TipoMsgResposta.warning;
                                reserva.Msg = string.Format("Você já tinha reservado a matrícula: {0}", PreImo.NumMatricula);
                                reserva.Resposta = true;
                            } else
                            {
                                PreImo = new PrenotacaoImovel();
                                PreImo.IdPrenotacao = IdPrenotacao;
                                PreImo.NumMatricula = NumMatricula;
                                PreImo.IdUsuario = IdUsuario;
                                reserva.Operacao = DataBaseOperacoes.insert;
                                this.UfwCartNew.Repositories.GenericRepository<PrenotacaoImovel>().Add(PreImo);
                                this.UfwCartNew.SaveChanges();

                                reserva.TipoMsg = TipoMsgResposta.ok;
                                reserva.Msg = string.Format("Matrícula {0} reservada com sucesso!", NumMatricula);
                            }
                        }
                        break;
                    case TipoReservaMatImovel.Liberar:

                        PreImo = this.UfwCartNew.Repositories.GenericRepository<PrenotacaoImovel>().GetWhere(p => 
                            (p.IdPrenotacao == IdPrenotacao) && 
                            (p.NumMatricula == NumMatricula) && 
                            (p.IdUsuario == IdUsuario)).FirstOrDefault();

                        if (PreImo == null)
                        {
                            reserva.TipoMsg = TipoMsgResposta.warning;
                            reserva.Msg = string.Format("Matrícula {0} já está liberada!", PreImo.NumMatricula);
                            reserva.Resposta = false;
                        } else
                        {
                            this.UfwCartNew.Repositories.GenericRepository<PrenotacaoImovel>().Remove(PreImo);
                            this.UfwCartNew.SaveChanges();
                            reserva.Operacao = DataBaseOperacoes.delete;
                            reserva.TipoMsg = TipoMsgResposta.ok;
                            reserva.Msg = string.Format("Matrícula {0} liberada com sucesso!", NumMatricula); ;
                        }
                        break;
                    default:
                        break;
                }
            } else {
                reserva.Resposta = false;
                reserva.TipoMsg = TipoMsgResposta.error;
                reserva.Msg = "Imóvel não localizado";
            }

            return reserva;
        }
    }
}
