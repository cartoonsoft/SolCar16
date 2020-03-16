using System;
using System.Collections.Generic;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Text;
using GemBox.Document;
using Infra.Cross.Identity.Models;
using LibFunctions.Functions.StringsFunc;
using LibFunctions.Functions.IOAdmCartorio;
using Domain.CartNew.Entities;
using Domain.CartNew.Enumerations;
using Domain.CartNew.Interfaces.UnitOfWork;
using Dto.CartNew.Base;
using Dto.CartNew.Entities.Cart_11RI;
using Dto.CartNew.Entities.Cart_11RI.Diversos;
using AppServCart11RI.Base;
using AppServCart11RI.Cartorio;
using AppServices.Cartorio.Interfaces;
using DomainServ.CartNew.Interfaces.Factory;
using AutoMapper;
using LibFunctions.Functions.BusinessFuncs;

namespace AppServCart11RI.AppServices
{
    public class AppServiceAtos : AppServiceCartorio11RI<DtoAto, Ato>, IAppServiceAtos
    {
        /// <summary>
        /// Construtor
        /// </summary>
        /// <param name="UfwCartNew"></param>
        public AppServiceAtos(IUnitOfWorkDataBaseCartNew UfwCartNew, long IdCtaAcessoSist, IDomainServicesFactoryCartNew dsFactoryCartNew = null) : base(UfwCartNew, IdCtaAcessoSist, dsFactoryCartNew)
        {
            //
        }

        #region Private Methods
        /// <summary>
        /// Get Lista de campos povoados com os valores 
        /// </summary>
        /// <param name="entidade"></param>
        /// <param name="IdTipoAto"></param>
        /// <returns></returns>
        private List<DtoCamposValor> GetListCamposPovoados(string entidade, DtoDadosAto dadosAto)
        {
            List<DtoCamposValor> listaCamposValor = new List<DtoCamposValor>();

            if (entidade.ToLower() == "ato")
            {
                var listaCamposAto = this.UfwCartNew.Repositories.RepositoryAto.GetListCamposAto(dadosAto.IdTipoAto, this.IdCtaAcessoSist);
                foreach (var campo in listaCamposAto)
                {
                    if (campo.Campo.ToLower() == "idlivro")
                    {
                        listaCamposValor.Add(new DtoCamposValor
                        {
                            Campo = campo.NomeCampo,
                            Valor = dadosAto.IdLivro.ToString()
                        });
                    }

                    if (campo.Campo.ToLower() == "dataato")
                    {
                        listaCamposValor.Add(new DtoCamposValor
                        {
                            Campo = campo.NomeCampo,
                            Valor = dadosAto.DataAto.HasValue ? dadosAto.DataAto.Value.ToString("dd/MM/yyyy", CultureInfo.InvariantCulture) : ""
                        });
                    }
                }
            }

            if (entidade.ToLower() == "prenotacao")
            {
                var listaCamposPrenotacao = this.UfwCartNew.Repositories.RepositoryAto.GetListCamposPrenotacao(dadosAto.IdTipoAto, this.IdCtaAcessoSist);

                foreach (var campo in listaCamposPrenotacao)
                {
                    if (campo.Campo.ToLower() == "idprenotacao")
                    {
                        listaCamposValor.Add(new DtoCamposValor
                        {
                            Campo = campo.NomeCampo,
                            Valor = dadosAto.IdPrenotacao.ToString()
                        });
                    }

                    if (campo.Campo.ToLower() == "dataregprenotacao")
                    {
                        listaCamposValor.Add(new DtoCamposValor
                        {
                            Campo = campo.NomeCampo,
                            Valor = dadosAto.DataRegPrenotacao.HasValue ? dadosAto.DataRegPrenotacao.Value.ToString("dd/MM/yyyy", CultureInfo.InvariantCulture) : ""
                        });
                    }
                }
            }

            if (entidade.ToLower() == "imovel")
            {
                var listaCamposImovel = this.UfwCartNew.Repositories.RepositoryAto.GetListCamposImovel(dadosAto.IdTipoAto, this.IdCtaAcessoSist);
                DtoDadosImovel imovel = this.GetDadosImovel(dadosAto.IdPrenotacao, dadosAto.NumMatricula);

                Type imovelType = imovel.GetType();
                PropertyInfo[] propertyInfo = imovelType.GetProperties();

                foreach (var campo in listaCamposImovel)
                {
                    var prop = propertyInfo.Where(p => p.Name == campo.Campo).FirstOrDefault();

                    if (prop != null)
                    {
                        var propValue = prop.GetValue(imovel);

                        if (propValue != null)
                        {
                            listaCamposValor.Add(new DtoCamposValor
                            {
                                Campo = campo.NomeCampo,
                                Valor = propValue.ToString().Trim()
                            });
                        }
                    }
                }
            }

            return listaCamposValor;
        }

        private List<DtoCamposValor> GetListCamposPovoadosPessoa(long idTipoAto, DtoPessoaPesxPre pessoa, long? idPrenotacao)
        {
            List<DtoCamposValor> listaCamposValor = new List<DtoCamposValor>();
            List<CampoTipoAto> listaCamposPessoa = null;

            string nomeCampo = pessoa.TipoPessoa == TipoPessoaPrenotacao.outorgante ? "Outorgante" : "Outorgado";

            listaCamposPessoa = this.UfwCartNew.Repositories.RepositoryAto.GetListCamposPessoa(idTipoAto, this.IdCtaAcessoSist)
                .Where(p => p.NomeCampo.Substring(0, nomeCampo.Length) == nomeCampo).ToList();

            //DtoPessoaPesxPre pessoa = this.DsFactoryCartNew.AtoDs.GetPessoa(idPessoa, idPrenotacao);

            if (listaCamposPessoa != null)
            {
                Type pessoaType = pessoa.GetType();
                PropertyInfo[] propertyInfo = pessoaType.GetProperties();

                foreach (var campo in listaCamposPessoa)
                {
                    var prop = propertyInfo.Where(p => p.Name == campo.Campo).FirstOrDefault();

                    if (prop != null)
                    {
                        var propValue = prop.GetValue(pessoa);

                        if (propValue != null)
                        {
                            listaCamposValor.Add(new DtoCamposValor
                            {
                                Campo = campo.NomeCampo,
                                Valor = propValue.ToString().Trim()
                            });
                        }
                    }
                }
            }

            return listaCamposValor;
        }

        private StringBuilder GetTexto(DtoDadosAto dtoDadosAto, string texto) 
        {
            StringBuilder textoTmp = new StringBuilder();
            string strAto = texto;

            strAto = PovoarGrpPessoa(dtoDadosAto.Pessoas, TipoPessoaPrenotacao.outorgante, strAto);
            strAto = PovoarGrpPessoa(dtoDadosAto.Pessoas, TipoPessoaPrenotacao.outorgado, strAto);
            strAto = PovoarCamposTexto(dtoDadosAto.ListaCamposValor, strAto);

            textoTmp.Append(strAto);

            return textoTmp;
        }

        private string PovoarGrpPessoa(List<DtoPessoaPesxPre> listaPessoas, TipoPessoaPrenotacao tipoPessoa, string texto)
        {
            string nomeGrupo = (tipoPessoa == TipoPessoaPrenotacao.outorgado) ? "[GrupoOutorgado]" : (tipoPessoa == TipoPessoaPrenotacao.outorgante) ? "[GrupoOutorgante]" : "";

            if (!string.IsNullOrEmpty(nomeGrupo))
            {
                for (int i = 0; i < texto.Length; i++)
                {
                    int posIniBloco = texto.IndexOf(nomeGrupo, i);

                    if (posIniBloco > 0)
                    {
                        int posFimBloco = texto.IndexOf("[FimGrupo]", posIniBloco);

                        if (posFimBloco == -1)
                        {
                            throw new FormatException("Arquivo com campos corrompidos, verifique o modelo");
                        }

                        posFimBloco += 10;
                        string strBloco = texto.Substring(posIniBloco, (posFimBloco - posIniBloco));
                        string strAux = strBloco.Replace(nomeGrupo, string.Empty).Replace("[FimGrupo]", string.Empty);
                        texto = texto.Replace(strBloco, GetTextoBloco(strAux, listaPessoas, tipoPessoa));
                        i = posFimBloco;
                    }
                }
            }

            return texto;
        }

        private string GetTextoBloco(string texto, List<DtoPessoaPesxPre> listaPessoas, TipoPessoaPrenotacao tipoPessoaPrenotacao)
        {
            string resp = string.Empty;
            List<DtoPessoaPesxPre> lista = listaPessoas.Where(p => p.TipoPessoa == tipoPessoaPrenotacao).ToList();
            int cont = 0;
            foreach (var item in lista)
            {
                cont++;
                if (!string.IsNullOrEmpty(texto))
                {
                    resp += PovoarCamposTexto(item.ListaCamposValor, texto);
                    if (cont > 0)
                    {
                        resp += "</br>";
                    }
                }
            }

            return resp;
        }

        private string PovoarCamposTexto(List<DtoCamposValor> listaCamposValor, string texto) 
        {
            string strAux = texto;

            foreach (var campoValor in listaCamposValor)
            {
                string nomeCampo = "[" + campoValor.Campo + "]";
                if (strAux.Contains(nomeCampo))
                {
                    strAux = strAux.Replace(nomeCampo, StringFunctions.Capitalize(campoValor.Valor));
                }
            }

            return strAux;
        }

        #endregion

        public string[] StatusPodeEditar()
        {
            return this.DsFactoryCartNew.AtoDs.StatusPodeEditar();
        }

        public string[] StatusPodeGerarFicha()
        {
            return this.DsFactoryCartNew.AtoDs.StatusPodeGerarFicha();
        }
        public string[] StatusPodeConfigImp()
        {
            return this.DsFactoryCartNew.AtoDs.StatusPodeConfigImp();
        }
        public string[] StatusAtoFinalizado()
        {
            return this.DsFactoryCartNew.AtoDs.StatusAtoFinalizado();
        }

        public DtoExecProc SetTextoConferido(long? idAto, string idUsuario, bool conferido)
        {
            DtoExecProc execProc = new DtoExecProc();

            execProc = this.DsFactoryCartNew.AtoDs.SetTextoConferido(idAto, idUsuario, conferido);

            return execProc;
        }

        #region Add, insert, update
        public override void Add(DtoAto dtoItem)
        {
            //regra de negocio insert
            Ato atoTmp = Mapper.Map<DtoAto, Ato>(dtoItem);

            this.DsFactoryCartNew.AtoDs.Add(atoTmp);
        }

        public override void Update(DtoAto dtoItem)
        {
            Ato atoTmp = Mapper.Map<DtoAto, Ato>(dtoItem);

            this.DsFactoryCartNew.AtoDs.Update(atoTmp);
        }

        public DtoExecProc InsertOrUpdateAto(DtoAto ato, ApplicationUser usuario)
        {
            return this.DsFactoryCartNew.AtoDs.InsertOrUpdateAto(ato, usuario);
        }
        #endregion

        public bool ConfirmarAjusteImpressaoAto(long IdAto)
        {
            throw new NotImplementedException();
        }

        public bool ConfirmarFicha(long IdDocx)
        {
            throw new NotImplementedException();
        }

        public bool AtoJaCadastrado(long idPrenotacao, string numMatricula)
        {
            return this.DsFactoryCartNew.AtoDs.AtoJaCadastrado(idPrenotacao, numMatricula);
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
            IEnumerable<DtoAto> lista = new List<DtoAto>();

            if (this.VerificarIntervaloDatas(DataIni, DataFim))
            {
                lista = this.DsFactoryCartNew.AtoDs.GetListAtosPeriodo(DataIni, DataFim);
            }

            return lista;
        }

        public IEnumerable<DtoDocx> GetListDocxAto(long? IdAto)
        {
            throw new NotImplementedException();
        }

        /// <summary>
        /// Data do registro da prenotacao na base onzeri
        /// </summary>
        public DateTime? DataRegPrenotacao(long IdPrenotacao) 
        {
            return this.UfwCartNew.Repositories.RepositoryAto.DataRegPrenotacao(IdPrenotacao);
        }

        public IEnumerable<string> GetListMatriculasPrenotacao(long IdPrenotacao)
        {
            List<string> listaMatriculas = DsFactoryCartNew.AtoDs.GetListMatriculasPrenotacao(IdPrenotacao).ToList();

            return listaMatriculas;
        }

        public IEnumerable<DtoDadosImovel> GetListImoveisPrenotacao(long IdPrenotacao)
        {
            List<DtoDadosImovel> listaImoveis = DsFactoryCartNew.AtoDs.GetListImoveisPrenotacao(IdPrenotacao).ToList();

            return listaImoveis;
        }

        /// <summary>
        /// Lista de pessoa por Ato (as pessoas selecionadas na prenotação)
        /// </summary>
        /// <param name="IdAto"></param>
        /// <returns></returns>
        public IEnumerable<DtoPessoaAto> GetListPessoasAto(long? IdAto)
        {
            List<DtoPessoaAto> pessoasAto = DsFactoryCartNew.AtoDs.GetListPessoasAto(IdAto).ToList();

            return pessoasAto;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="IdPrenotacao"></param>
        /// <returns></returns>
        public IEnumerable<DtoPessoaPesxPre> GetListPessoasPrenotacao(long IdPrenotacao)
        {
            List<DtoPessoaPesxPre> pessoasPrenotacao = DsFactoryCartNew.AtoDs.GetListPessoasPrenotacao(IdPrenotacao).ToList();

            foreach (var pessoa in pessoasPrenotacao)
            {
                string retValTmp;
                if (StringFunctions.SomenteNumeros(pessoa.Numero1).Length == 11)
                {
                    pessoa.Valido = BusinessFunctions.ValidarCPF(pessoa.Numero1);
                    retValTmp = "está com CPF " + (pessoa.Valido ? "Validado" : "Inválido");

                } else if (StringFunctions.SomenteNumeros(pessoa.Numero1).Length == 14)
                {
                    pessoa.Valido = BusinessFunctions.ValidarCNPJ(pessoa.Numero1);
                    retValTmp = "está com CNPJ " + (pessoa.Valido ? "Validado" : "Inválido");
                } else
                {
                    pessoa.Valido = false;
                    retValTmp = "está com CPF/CNPJ indeterminado!";
                }

                pessoa.RetornoValidacao = string.Format(
                    "{0} {1}-{2}, {3}.",
                    pessoa.TipoPessoa == TipoPessoaPrenotacao.outorgado ? "Outorgado" : "Outorgante",
                    pessoa.IdPessoa,
                    pessoa.Nome,
                    retValTmp
                );
            }

            return pessoasPrenotacao;
        }

        public IEnumerable<DtoPessoaPesxPre> GetListPessoas(long idTipoAto, List<DtoAtoPessoa> listaAtoPessoa, long? idPrenotacao)
        {
            List<DtoPessoaPesxPre> dtoPessoaPesxPres = DsFactoryCartNew.AtoDs.GetListPessoas(listaAtoPessoa, idPrenotacao).ToList();

            foreach (var pessoa in dtoPessoaPesxPres)
            {
                pessoa.ListaCamposValor = this.GetListCamposPovoadosPessoa(idTipoAto, pessoa, idPrenotacao);
            }

            return dtoPessoaPesxPres;
        }

        public IEnumerable<DtoAtoEvento> GetListHistoricoAto(long? IdAto)
        {
            List<DtoAtoEvento> listaDtoAtoEvento = DsFactoryCartNew.AtoDs.GetListHistoricoAto(IdAto).ToList();

            return listaDtoAtoEvento;
        }

        public DtoPessoaPesxPre GetPessoa(long idPessoa, long? idPrenotacao)
        {
            return DsFactoryCartNew.AtoDs.GetPessoa(idPessoa, idPrenotacao);
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

        public void UploadFicha(long IdDocx)
        {
            throw new NotImplementedException();
        }

        public DtoReservaImovel ProcReservarMatImovel(TipoReservaMatImovel TipoReserva, long IdPrenotacao, string NumMatricula, string IdUsuario)
        {
            DtoReservaImovel reserva = new DtoReservaImovel();
            DtoDadosImovel Imovel = null; 
            PrenotacaoImovel PreImo = null;
            reserva.Resposta = true;

            if (this.AtoJaCadastrado(IdPrenotacao, NumMatricula))
            {
                reserva.TipoMsg = TipoMsgResposta.error;
                reserva.Resposta = false;
                reserva.Msg = "Ato já foi gerado para esta prenotacao e matrícula!";
            } else {

                UsuarioIdentity usr = this.UfwCartNew.Repositories.GenericRepository<UsuarioIdentity>().GetWhere(u => u.Id == IdUsuario).FirstOrDefault();

                if (usr == null)
                {
                    throw new ArgumentNullException(MethodBase.GetCurrentMethod().Name, "Usuário inválido. Verifique!");
                }

                PreImo = this.UfwCartNew.Repositories.GenericRepository<PrenotacaoImovel>().GetWhere(p =>
                    (p.IdPrenotacao == IdPrenotacao) &&
                    (p.NumMatricula == NumMatricula) &&
                    (p.IdUsuario != IdUsuario)).FirstOrDefault();

                if (PreImo == null)
                {
                    Imovel = this.GetDadosImovel(IdPrenotacao, NumMatricula);

                    if (Imovel != null)
                    {
                        reserva.Imovel = Imovel;

                        switch (TipoReserva)
                        {
                            case TipoReservaMatImovel.Reservar:

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
                } else {
                    reserva.TipoMsg = TipoMsgResposta.error;
                    UsuarioIdentity usr2 = this.UfwCartNew.Repositories.GenericRepository<UsuarioIdentity>().GetWhere(u => u.Id == PreImo.IdUsuario).FirstOrDefault();

                    if (usr2 != null)
                    {
                        reserva.Msg = string.Format("Imóvel já reservado pelo usuário: {0}!", "[" + usr2.UserName + "] " + usr.Nome);
                    } else {
                        reserva.Msg = "Imóvel já reservado por outro usuário";
                    }

                    reserva.Resposta = false;
                }
            }

            return reserva;
        }

        public StringBuilder GetTextoModeloDoc(long IdModeloDoc)
        {
            StringBuilder textoDoc = new StringBuilder();

            ModeloDoc modelo = this.DsFactoryCartNew.ModeloDocxDs.GetById(IdModeloDoc);

            if (modelo != null)
            {
                textoDoc.Append(modelo.Texto);
            }

            return textoDoc;
        }

        /// <summary>
        /// Gera o texto a partir de um modelo
        /// </summary>
        /// <param name="dtoInfAto"></param>
        /// <returns></returns>
        public StringBuilder GetTextoAto(DtoInfAto dtoInfAto)
        {
            StringBuilder textoDoc = new StringBuilder();

            DtoDadosAto dtoDadosAto = new DtoDadosAto {
                Id = dtoInfAto.IdAto,
                IdTipoAto = dtoInfAto.IdTipoAto,
                IdLivro = dtoInfAto.IdLivro,
                IdModeloDoc = dtoInfAto.IdModeloDoc,
                IdPrenotacao = dtoInfAto.IdPrenotacao,
                NumMatricula = dtoInfAto.NumMatricula,
                DataRegPrenotacao = dtoInfAto.DataRegPrenotacao,
                DataAto = dtoInfAto.DataAto
            };

            //pegar texto modelo
            ModeloDoc modelo = this.UfwCartNew.Repositories.RepositoryModeloDocx.GetById(dtoDadosAto.IdModeloDoc);

            if (modelo != null) 
            {
                string textoModelo = modelo.Texto;

                if (dtoInfAto.IdAto.HasValue && (dtoInfAto.IdAto > 0))
                {
                    //dtoDadosAto = GetDadosAto(dtoInfAto.IdAto ?? 0);
                    //todo: carregar do ato fazer 
                } else
                {
                    dtoDadosAto.ListaCamposValor.AddRange(this.GetListCamposPovoados("Ato", dtoDadosAto));
                    dtoDadosAto.ListaCamposValor.AddRange(this.GetListCamposPovoados("Prenotacao", dtoDadosAto));
                    dtoDadosAto.ListaCamposValor.AddRange(this.GetListCamposPovoados("Imovel", dtoDadosAto));
                    dtoDadosAto.Pessoas = this.GetListPessoas(dtoInfAto.IdTipoAto, dtoInfAto.ListaAtoPessoa, dtoInfAto.IdPrenotacao).ToList();

                    textoDoc = this.GetTexto(dtoDadosAto, textoModelo);
                }
            } else {
                throw new ArgumentNullException(string.Format("Modelo {0} inválido! Verificar", dtoDadosAto.IdModeloDoc));
            }

            return textoDoc;
        }

    }
}
