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

namespace AppServCart11RI.AppServices
{
    public class AppServiceAtos : AppServiceCartorio11RI<DtoAto, Ato>, IAppServiceAtos
    {
        private List<ApplicationUser> listaUsrSist;
        private List<DtoPessoaPesxPre> listaPessoasPrenotacao = null;  //PESXPRE

        /*-- status ato --------------------------------------------------------
        AC1	Ato Criado sistema 
        AC2	Ato Criado importacao
        AE	Ato em Escrita
        AI	Confirmado ajuste impressão
        CFT	Ato conferido texto ato
        CFD	Ato conferido docx
        CL	Ato cancelado
        GF	Gerado Ficha
        AF	Ato Finalizado
        --------------------------------------------------------------------- */
        //status que podem mostrar tela de edição
        private readonly string[] _statusEditaveis = { "AC1", "AC2", "AE", "AI", "CF" };

        //status que os campos ficam readony na edição
        private readonly string[] _statusCamposReadOnly = { "AI", "CF", "CL", "GF", "AF" };

        /// <summary>
        /// Construtor
        /// </summary>
        /// <param name="UfwCartNew"></param>
        public AppServiceAtos(IUnitOfWorkDataBaseCartNew UfwCartNew, long IdCtaAcessoSist, IDomainServicesFactoryCartNew dsFactoryCartNew = null) : base(UfwCartNew, IdCtaAcessoSist, dsFactoryCartNew)
        {
            //
        }

        /// <summary>
        /// Get Lista de campos povoados com os valores 
        /// </summary>
        /// <param name="entidade"></param>
        /// <param name="IdTipoAto"></param>
        /// <returns></returns>
        #region Private Methods
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


        public string[] StatusEditaveis
        {
            get { return this._statusEditaveis; }
        }

        public string[] StatusCamposReadOnly
        {
            get { return this._statusCamposReadOnly; }
        }

        /*--------------------------------------------------------------------*/
        #region Add, update, InsertOrUpdateAto
        public override void Add(DtoAto dtoItem)
        {
            base.Add(dtoItem);
        }

        public override void Update(DtoAto dtoItem)
        {
            base.Update(dtoItem);
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
            IEnumerable<DtoAto> lista = this.DsFactoryCartNew.AtoDs.GetListAtosPeriodo(DataIni, DataFim);
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

        /// <summary>
        /// 
        /// </summary>
        /// <param name="IdPrenotacao"></param>
        /// <returns></returns>
        public IEnumerable<DtoPessoaPesxPre> GetListPessoasPrenotacao(long IdPrenotacao)
        {
            List<DtoPessoaPesxPre> pessoasPrenotacao = new List<DtoPessoaPesxPre>();

            pessoasPrenotacao = this.DsFactoryCartNew.AtoDs.GetListPessoasPrenotacao(IdPrenotacao).ToList();

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

        public bool AtoJaCadastrado(long idPrenotacao, string numMatricula)
        {
            return this.DsFactoryCartNew.AtoDs.AtoJaCadastrado(idPrenotacao, numMatricula);
        }

    }

}
