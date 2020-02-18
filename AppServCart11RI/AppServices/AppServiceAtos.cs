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
using LibFunctions.Functions.CommonFunc;

namespace AppServCart11RI.AppServices
{
    public class AppServiceAtos : AppServiceCartorio11RI<DtoAto, Ato>, IAppServiceAtos
    {
        private List<ApplicationUser> listaUsrSist;
        private List<DtoPessoaPesxPre> listaPessoasPrenotacao = null;  //PESXPRE

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
        private List<DtoCamposValor> GetListCamposPovoados(string entidade, DadosAtoSimplificado dadosAto)
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

        private string GetTextoBloco(string strBloco, List<DtoPessoaPesxPre> listaPessoas)
        {
            string resp = string.Empty;

            foreach (var item in listaPessoas)
            {
                string texto = strBloco;

                if (texto != "")
                {
                    string strAto = string.Empty;

                    for (int i = 0; i < texto.Length; i++)
                    {
                        if (texto[i] == '[')
                        {
                            i++;
                            string nomeCampo = string.Empty;
                            string resultadoQuery = string.Empty;
                            while (texto[i] != ']')
                            {
                                nomeCampo += texto[i].ToString().Trim();
                                i++;
                                if (i >= texto.Length || texto[i] == '[')
                                {
                                    throw new FormatException("Arquivo com campos corrompidos, verifique o modelo");
                                }
                            }

                            var CampoValor = item.ListaCamposValor.Where(c => c.Campo == nomeCampo).FirstOrDefault();

                            if (CampoValor != null)
                            {
                                resultadoQuery = StringFunctions.Capitalize(CampoValor.Valor);
                            }

                            if (!string.IsNullOrEmpty(resultadoQuery))
                            {
                                //atualiza o textoo formatado
                                strAto += resultadoQuery;
                            }
                        } else {
                            //caso não seja um campo somente adiciona o caractere
                            strAto += texto[i].ToString();
                        }

                    }
                    // Populando campo de retorno
                    resp += "<p>{strAto}</p>";
                }
            }

            return resp;
        }
        #endregion

        public string[] StatusEdtTexto()
        {
            return this.DsFactoryCartNew.AtoDs.StatusEdtTexto();
        }
        public string[] StatusEdtDadosImp()
        {
            return this.DsFactoryCartNew.AtoDs.StatusEdtDadosImp();
        }
        public string[] StatusAtoFinalizado()
        {
            return this.DsFactoryCartNew.AtoDs.StatusAtoFinalizado();
        }

        public DtoExecProc SetTextoConferido(long? idAto, string idUsuario, bool conferido)
        {
            DtoExecProc execProc = new DtoExecProc();
            var usr = this.listaUsrSist.Where(u => u.IdUsuario == idUsuario).FirstOrDefault();

            execProc = this.DsFactoryCartNew.AtoDs.SetTextoConferido(idAto, usr, conferido);

            return execProc;
        }

        #region Add, update, InsertOrUpdateAto
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

        public List<ApplicationUser> ListaUsuariosSistema
        { 
            get { return listaUsrSist; }
            set { listaUsrSist = value; }
        }

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
            List<string> listaMatriculas = new List<string>();
            listaMatriculas = this.DsFactoryCartNew.AtoDs.GetListMatriculasPrenotacao(IdPrenotacao).ToList();

            return listaMatriculas;
        }

        public IEnumerable<DtoDadosImovel> GetListImoveisPrenotacao(long IdPrenotacao)
        {
            List<DtoDadosImovel> listaImoveis = new List<DtoDadosImovel>();
            listaImoveis = this.DsFactoryCartNew.AtoDs.GetListImoveisPrenotacao(IdPrenotacao).ToList();

            return listaImoveis;
        }

        /// <summary>
        /// Lista de pessoa por Ato (as pessoas selecionadas na prenotação)
        /// </summary>
        /// <param name="IdAto"></param>
        /// <returns></returns>
        public IEnumerable<DtoPessoaAto> GetListPessoasAto(long? IdAto)
        {
            List<DtoPessoaAto> pessoasAto = new List<DtoPessoaAto>();
            pessoasAto = this.DsFactoryCartNew.AtoDs.GetListPessoasAto(IdAto).ToList();

            return pessoasAto;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="IdPrenotacao"></param>
        /// <returns></returns>
        public IEnumerable<DtoPessoaPesxPre> GetListPessoasPrenotacao(long IdPrenotacao)
        {
            string retValTmp = string.Empty;

            List<DtoPessoaPesxPre> pessoasPrenotacao = new List<DtoPessoaPesxPre>();
            pessoasPrenotacao = this.DsFactoryCartNew.AtoDs.GetListPessoasPrenotacao(IdPrenotacao).ToList();

            foreach (var pessoa in pessoasPrenotacao)
            {
                if (CommonFunctions.SomenteNumeros(pessoa.Numero1).Length == 11)
                {
                    pessoa.Valido = BusinessFunctions.ValidarCPF(pessoa.Numero1);
                    retValTmp = "está com CPF " + (pessoa.Valido ? "Validado" : "Inválido");

                } else if (CommonFunctions.SomenteNumeros(pessoa.Numero1).Length == 14) {  
                    pessoa.Valido = BusinessFunctions.ValidarCNPJ(pessoa.Numero1);
                    retValTmp = "está com CNPJ " + (pessoa.Valido ? "Validado" : "Inválido");
                } else {
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

        public IEnumerable<DtoPessoaPesxPre> GetListPessoas(long idTipoAto, long[] idsPessoas, long? idPrenotacao)
        {
            List<DtoPessoaPesxPre> dtoPessoaPesxPres = new List<DtoPessoaPesxPre>();
            dtoPessoaPesxPres = DsFactoryCartNew.AtoDs.GetListPessoas(idsPessoas, idPrenotacao).ToList();

            foreach (var pessoa in dtoPessoaPesxPres)
            {
                pessoa.ListaCamposValor = this.GetListCamposPovoadosPessoa(idTipoAto, pessoa, idPrenotacao);
            }

            return dtoPessoaPesxPres;
        }

        public IEnumerable<DtoAtoEvento> GetListHistoricoAto(long? IdAto)
        {
            List<DtoAtoEvento> listaDtoAtoEvento = DsFactoryCartNew.AtoDs.GetListHistoricoAto(IdAto).ToList();

            foreach (var item in listaDtoAtoEvento)
            {
                item.NomeUsuario = this.ListaUsuariosSistema.Where(u => u.IdUsuario == item.IdUsuario).FirstOrDefault().Nome;
            }

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

                    if (this.listaUsrSist != null)
                    {
                        var usr = this.listaUsrSist.Where(u => u.IdUsuario == PreImo.IdUsuario).FirstOrDefault();
                        reserva.Msg = string.Format("Imóvel já reservado pelo usuário: {0}!", "[" + usr.LoginName + "] " + usr.Nome);
                    } else {
                        reserva.Msg = "Imóvel já reservado por outro usuário";
                    }
                    reserva.Resposta = false;
                }
            }

            return reserva;
        }

        public StringBuilder GetTextoWordDocModelo(long IdModeloDoc, string ServerPath)
        {
            StringBuilder textoDoc = new StringBuilder();
            FilesConfig fileConfig = new FilesConfig();
            string fileName = Path.Combine(ServerPath, fileConfig.GetModeloDocFileName(IdModeloDoc));

            using (AtoWordDocx atoWordDocx = new AtoWordDocx(this, this.IdCtaAcessoSist, fileName))
            {
                foreach (Paragraph paragraph in atoWordDocx.WordDocument.GetChildElements(true, ElementType.Paragraph))
                {
                    textoDoc.Append(paragraph.Content.ToString());
                }
            }

            return textoDoc;
        }

        public StringBuilder GetTextoAto(DtoInfAto dtoInfAto)
        {
            StringBuilder textoDoc = new StringBuilder();
            FilesConfig fileConfig = new FilesConfig();

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

            DadosAtoSimplificado dadosAto = new DadosAtoSimplificado
            {
                IdAto = dtoInfAto.IdAto,
                IdTipoAto = dtoInfAto.IdTipoAto,
                IdLivro = dtoInfAto.IdLivro,
                IdModeloDoc = dtoInfAto.IdModeloDoc,
                IdPrenotacao = dtoInfAto.IdPrenotacao,
                NumMatricula = dtoInfAto.NumMatricula,
                DataRegPrenotacao = dtoInfAto.DataRegPrenotacao,
                DataAto = dtoInfAto.DataAto
            };

            if (dtoInfAto.IdAto.HasValue && (dtoInfAto.IdAto > 0))
            {
                //dtoDadosAto = GetDadosAto(dtoInfAto.IdAto ?? 0);
                //todo: catregar do ato fazer 
            } else {

                dtoDadosAto.ListaCamposValor.AddRange(this.GetListCamposPovoados("Ato", dadosAto));
                dtoDadosAto.ListaCamposValor.AddRange(this.GetListCamposPovoados("Prenotacao", dadosAto));
                dtoDadosAto.ListaCamposValor.AddRange(this.GetListCamposPovoados("Imovel", dadosAto));
                dtoDadosAto.Pessoas = this.GetListPessoas(dtoInfAto.IdTipoAto, dtoInfAto.ListIdsPessoas, dtoInfAto.IdPrenotacao).ToList();

                //obter filename
                string fileName = Path.Combine(dtoInfAto.ServerPath, fileConfig.GetModeloDocFileName(dtoInfAto.IdModeloDoc));

                //dtoDadosAto.Pessoas = this.GerarFichas
                using (AtoWordDocx atoWordDocx = new AtoWordDocx(this, dtoInfAto.IdCtaAcessoSist, fileName))
                {
                    StringBuilder textoTmp = new StringBuilder();

                    // Get content from each paragraph
                    foreach (Paragraph paragraph in atoWordDocx.WordDocument.GetChildElements(true, ElementType.Paragraph))
                    {
                        textoTmp.Append(paragraph.Content.ToString());
                    }

                    string texto = textoTmp.ToString();
                    if (texto != "")
                    {
                        string strAto = string.Empty;
                        string strBloco = string.Empty;
                        bool flagBloco = false;
                        char tipoPes = '0';

                        for (int i = 0; i < texto.Length; i++)
                        {
                            if (texto[i] == '[')
                            {
                                i++;
                                string nomeCampo = string.Empty;
                                string resultadoQuery = string.Empty;
                                while (texto[i] != ']')
                                {
                                    nomeCampo += texto[i].ToString().Trim();
                                    i++;
                                    if (i >= texto.Length || texto[i] == '[')
                                    {
                                        throw new FormatException("Arquivo com campos corrompidos, verifique o modelo");
                                    }
                                }

                                //Buscar dado da pessoa aqui
                                //resultadoQuery = "teste query";
                                var CampoValor = dtoDadosAto.ListaCamposValor.Where(c => c.Campo == nomeCampo).FirstOrDefault();
                                
                                if (CampoValor != null)
                                {
                                    resultadoQuery = StringFunctions.Capitalize(CampoValor.Valor);
                                } else {
                                    if ((nomeCampo.Length >= 10) && (nomeCampo.Substring(0, 10) == "Outorgante"))
                                    {
                                        var CampoValorOutorgante = dtoDadosAto.Pessoas.Where( p => p.TipoPessoa == TipoPessoaPrenotacao.outorgante).FirstOrDefault()
                                            .ListaCamposValor.Where(c => c.Campo == nomeCampo).FirstOrDefault();
                                        if (CampoValorOutorgante != null)
                                        {
                                            resultadoQuery = StringFunctions.Capitalize(CampoValorOutorgante.Valor);
                                        }
                                    }

                                    if ((nomeCampo.Length >= 9 ) && (nomeCampo.Substring(0, 9) == "Outorgado"))
                                    {
                                        var CampoValorOutorgado = dtoDadosAto.Pessoas.Where(p => p.TipoPessoa == TipoPessoaPrenotacao.outorgado).FirstOrDefault()
                                            .ListaCamposValor.Where(c => c.Campo == nomeCampo).FirstOrDefault();
                                        if (CampoValorOutorgado != null)
                                        {
                                            resultadoQuery = StringFunctions.Capitalize(CampoValorOutorgado.Valor);
                                        }
                                    }
                                }

                                if (!string.IsNullOrEmpty(resultadoQuery))
                                {
                                    //atualiza o textoo formatado
                                    strAto += resultadoQuery;
                                }
                            }
                            else if (texto[i] == '<')
                            {
                                i++;
                                string tipoTag = string.Empty;

                                while (texto[i] != '>')
                                {
                                    tipoTag += texto[i].ToString().Trim();
                                    i++;
                                    if (i >= texto.Length || texto[i] == '<')
                                    {
                                        throw new FormatException("Tags de repetição corrompidas, verifique o modelo");
                                    }
                                }
                                i++;

                                if (flagBloco)
                                {
                                    strAto += GetTextoBloco(strBloco, dtoDadosAto.Pessoas);
                                }

                                if (tipoTag.ToLower().Equals("outorgantes"))
                                {
                                    tipoPes = '1';
                                }
                                else if (tipoTag.Equals("outorgados"))
                                {
                                    tipoPes = '2';
                                }

                                strBloco = string.Empty;
                                flagBloco = !flagBloco;
                            }
                            else
                            {
                                //caso não seja um campo somente adiciona o caractere
                                strAto += texto[i].ToString();
                            }

                            strBloco += texto[i].ToString();
                        }
                        // Populando campo de retorno
                        textoDoc.Append($"<p>{strAto}</p>");
                    }
                    
                }
            }

            return textoDoc;
        }

    }
}
