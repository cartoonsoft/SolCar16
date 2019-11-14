﻿using System;
using System.Collections.Generic;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Text;
using AppServCart11RI.Base;
using AppServCart11RI.Cartorio;
using AppServices.Cartorio.Interfaces;
using Domain.Cart11RI.Entities;
using Domain.CartNew.Entities;
using Domain.CartNew.Enumerations;
using Domain.CartNew.Interfaces.UnitOfWork;
using Dto.CartNew.Entities.Cart_11RI;
using Dto.CartNew.Entities.Cart_11RI.Diversos;
using GemBox.Document;
using Infra.Cross.Identity.Models;

namespace AppServCart11RI.AppServices
{
    public class AppServiceAtos : AppServiceCartorio11RI<DtoAto, Ato>, IAppServiceAtos
    {
        private List<ApplicationUser> listaUsrSist = null;
        private List<DtoPessoaPesxPre> listaPessoasPrenotacao = null;  //PESXPRE

        /// <summary>
        /// Construtor
        /// </summary>
        /// <param name="UfwCartNew"></param>
        public AppServiceAtos(IUnitOfWorkDataBaseCartNew UfwCartNew, long IdCtaAcessoSist) : base(UfwCartNew, IdCtaAcessoSist)
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
        private List<DtoCamposValor> GetListCamposValores(string entidade, DtoInfAto infAto)
        {
            List<DtoCamposValor> listaCamposValor = new List<DtoCamposValor>();

            var listacampos = this.UfwCartNew.Repositories.RepositoryModeloDocx.GetListCamposIdTipoAto(infAto.IdTipoAto, infAto.IdCtaAcessoSist).Where(e => e.Entidade == entidade).ToList();

            if (entidade.ToLower() == "ato")
            {
                listaCamposValor.Add(new DtoCamposValor
                {
                    Campo = "IdLivro",
                    Valor = infAto.IdLivro.ToString()
                });

                listaCamposValor.Add(new DtoCamposValor
                {
                    Campo = "DataAto",
                    Valor = infAto.DataAto.ToString("dd/MM/yyyy", CultureInfo.InvariantCulture)
                });
            }

            if (entidade.ToLower() == "prenotacao")
            {


            }

            /*
            List<CampoTipoAto> campoTipoAtos = new List<CampoTipoAto>();

            var listaCampos =
                from ta in _contextRepository.DbTipoAtoCampo.Where(a => a.IdTipoAto == IdTipoAto)
                join ac in _contextRepository.DbCampoTipoAto.Where(c => c.IdCtaAcessoSist == IdCtaAcessoSist) on ta.IdCampoTipoAto equals ac.Id
                orderby ac.Entidade, ac.NomeCampo
                select new
                {
                    Id = ac.Id,
                    IdCtaAcessoSist = ac.IdCtaAcessoSist,
                    NomeCampo = ac.NomeCampo,
                    PlaceHolder = ac.PlaceHolder,
                    Campo = ac.Campo,
                    Entidade = ac.Entidade
                };

            foreach (var campo in listaCampos)
            {
                campoTipoAtos.Add(
                    new CampoTipoAto
                    {
                        Id = campo.Id,
                        IdCtaAcessoSist = campo.IdCtaAcessoSist,
                        Campo = campo.Campo,
                        NomeCampo = campo.NomeCampo,
                        Entidade = campo.Entidade,
                        PlaceHolder = campo.PlaceHolder
                    }
                );
            }

            return campoTipoAtos;

            */
            return null;
        }
        private List<DtoPessoaPesxPre> GetPessoas(long [] idsPessoas)
        {

            return null;
        }
        #endregion

        public List<ApplicationUser> ListaUsuariosSistema
        { 
            get { return listaUsrSist; }
            set { listaUsrSist = value; }
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

        /// <summary>
        /// 
        /// </summary>
        /// <param name="IdPrenotacao"></param>
        /// <returns></returns>
        public IEnumerable<DtoPessoaPesxPre> GetListPessoasPrenotacao(long IdPrenotacao)
        {
            List<DtoPessoaPesxPre> pessoasPrenotacao = new List<DtoPessoaPesxPre>();

            var pessoas =
                from pre in this.UfwCartNew.Repositories.GenericRepository<PESXPRE>().Get().Where(pp => pp.SEQPRE == IdPrenotacao)
                join pes in this.UfwCartNew.Repositories.GenericRepository<PESSOAS>().Get() on pre.SEQPES equals pes.SEQPES
                orderby pes.NOM
                select new
                {
                    IdPessoa = pes.SEQPES,
                    IdPrenotacao = IdPrenotacao,
                    Bairro = pes.BAI,
                    Cep = pes.CEP,
                    Cidade = pes.CID,
                    Endereco = pes.ENDER,
                    Nome = pes.NOM,
                    Numero1 = pes.NRO1,
                    Numero2 = pes.NRO2,
                    Relacao = pre.REL,
                    Telefone = pes.TEL,
                    TipoDoc1 = pes.TIPODOC1,
                    TipoDoc2 = pes.TIPODOC2,
                    Uf = pes.UF,
                    TipoPessoa =
                        pre.REL == "E" ? TipoPessoaPrenotacao.outorgado :
                        pre.REL == "O" ? TipoPessoaPrenotacao.outorgante : TipoPessoaPrenotacao.indefinido,
                };

            foreach (var pessoa in pessoas)
            {
                pessoasPrenotacao.Add( new DtoPessoaPesxPre {
                    IdPrenotacao = pessoa.IdPrenotacao,
                    IdPessoa = pessoa.IdPessoa,
                    Bairro = pessoa.Bairro,
                    Cep = pessoa.Cep,
                    Cidade = pessoa.Cidade,
                    Endereco = pessoa.Endereco,
                    Nome = pessoa.Nome,
                    Numero1 = pessoa.Numero1,
                    Numero2 = pessoa.Numero2,
                    Relacao = pessoa.Relacao,
                    Telefone = pessoa.Telefone,
                    TipoDoc1 = pessoa.TipoDoc1.ToString(),
                    TipoDoc2 = pessoa.TipoDoc2,
                    TipoPessoa = pessoa.TipoPessoa,
                    Uf = pessoa.Uf
                });
            }

            return pessoasPrenotacao;
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
            return reserva;
        }

        public StringBuilder GetTextoWordDocModelo(long IdModeloDoc, string ServerPath)
        {
            StringBuilder textoDoc = new StringBuilder();
            FilesConfig fileConfig = new FilesConfig();
            string fileName = ServerPath + fileConfig.GetModeloDocFileName(IdModeloDoc);

            using (var stream = new MemoryStream())
            {
                // Convert input file to RTF stream.
                stream.Position = 0;

                using (AtoWordDocx atoWordDocx = new AtoWordDocx(this, fileName, this.IdCtaAcessoSist))
                {
                    atoWordDocx.WordDocument.Save(stream, SaveOptions.HtmlDefault);
                    using (var reader = new StreamReader(stream))
                    {
                        textoDoc.AppendFormat(reader.ReadToEnd());
                    }
                }
            }

            return textoDoc;
        }

        public StringBuilder GetTextoAto(DtoInfAto dtoInfAto)
        {
            StringBuilder textoDoc = new StringBuilder();
            DtoDadosAto dtoDadosAto = new DtoDadosAto();
            DtoDadosImovel dtoDadosImovel = new DtoDadosImovel();

            if (dtoInfAto.IdAto > 0)
            {
                //dtoDadosAto = GetDadosAto(dtoInfAto.IdAto ?? 0);
                //todo: catregar do ato fazer 
            } else
            {
                dtoDadosAto.ListaCamposValor.AddRange(this.GetListCamposValores("Ato", dtoInfAto));
                dtoDadosAto.ListaCamposValor.AddRange(this.GetListCamposValores("Prenotacao", dtoInfAto));
                dtoDadosAto.ListaCamposValor.AddRange(this.GetListCamposValores("Imovel", dtoInfAto));
                dtoDadosAto.Pessoas.AddRange(this.GetPessoas(dtoInfAto.ListIdsPessoas));

                //dtoDadosAto.Pessoas = this.GerarFichas
                using (AtoWordDocx atoWordDocx = new AtoWordDocx(this, dtoInfAto.ServerPath, dtoInfAto.IdCtaAcessoSist))
                {
                    var sb = new StringBuilder();

                    // Get content from each paragraph
                    foreach (Paragraph paragraph in atoWordDocx.WordDocument.GetChildElements(true, ElementType.Paragraph))
                    {
                        textoDoc.Append(paragraph.Content.ToString());
                    }
                    /*
                    string texto = sb.ToString();
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
                                    resultadoQuery = CampoValor.Valor;
                                }

                                if (!string.IsNullOrEmpty(resultadoQuery))
                                {
                                    //atualiza o textoo formatado
                                    textoDoc.Append(resultadoQuery);
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
                                    //strAto += GetTextoBloco(strBloco, tipoPes, )
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
                    */
                }
            }

            return textoDoc;
        }

    }

}
