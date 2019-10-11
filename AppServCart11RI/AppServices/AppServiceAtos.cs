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

        public AppServiceAtos(IUnitOfWorkDataBaseCartNew UfwCartNew) : base(UfwCartNew)
        {
            //

        }

        #region Private Methods
        /// <summary>
        /// 
        /// </summary>
        /// <param name="IdTipoAto"></param>
        /// <param name="IdPrenotacao"></param>
        /// <param name="NumMatricula"></param>
        /// <returns></returns>
        private List<DtoCamposValor> GetCamposAto(long? IdTipoAto, long? IdPrenotacao, long IdCtaAcessoSist, string NumMatricula)
        {
            DateTime dataTmp = DateTime.ParseExact("01/01/1800", "dd/MM/yyyy", CultureInfo.InvariantCulture);
            string dataPremadTmp = string.Empty;
            string valorTmp = string.Empty;

            int matTmp = 0;
            int.TryParse(NumMatricula, out matTmp);

            List<DtoCamposValor> listaTmp = new List<DtoCamposValor>();
            List<CampoTipoAto> listaCampos = this.UfwCartNew.Repositories.RepositoryModeloDocx.GetListaCamposIdTipoAto(IdTipoAto, IdCtaAcessoSist)
                .Where(l => ((l.Entidade == "ATO") || (l.Entidade == "PRENOTACAO"))).ToList();

            Ato ato = this.UfwCartNew.Repositories.RepositoryAto.GetWhere(a => (a.IdPrenotacao == IdPrenotacao) && (a.NumMatricula == NumMatricula)).FirstOrDefault();
            PREIMO preimo = this.UfwCartNew.Repositories.GenericRepository<PREIMO>().GetWhere(p => (p.SEQPRE == IdPrenotacao) && (p.MATRI == matTmp)).FirstOrDefault();

            //para obter data da prenotacao
            PREMAD premad = this.UfwCartNew.Repositories.GenericRepository<PREMAD>()
                .GetWhere(p => (p.SEQPRE == IdPrenotacao) && (p.TIPODATA.Trim() == "R"))
                .OrderByDescending(o => o.DATA).FirstOrDefault();

            if (premad != null)
            {
                dataPremadTmp = dataTmp.AddDays(premad.DATA).ToString("dd/MM/yyyy");
            }

            foreach (var item in listaCampos)
            {
                if ((ato != null) && (item.Entidade == "ATO"))
                {
                    var prop = ato.GetType().GetProperty(item.Campo);
                    if (prop != null)
                    {
                        valorTmp = (prop.GetValue(premad) == null) ? "" : prop.GetValue(premad).ToString();

                        listaTmp.Add(new DtoCamposValor
                        {
                            Campo = item.Campo,
                            Valor = valorTmp
                        });
                    }
                }

                if ((preimo != null) && (item.Entidade == "PRENOTACAO"))
                {
                    if (item.Campo == "IdPrenotacao")
                    {
                        listaTmp.Add(new DtoCamposValor
                        {
                            Campo = item.Campo,
                            Valor = IdPrenotacao.ToString()
                        });
                    }

                    if (item.Campo == "DATA")
                    {
                        listaTmp.Add(new DtoCamposValor
                        {
                            Campo = item.Campo,
                            Valor = dataPremadTmp
                        });
                    }
                }
            }

            return listaTmp;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="IdTipoAto"></param>
        /// <param name="IdPrenotacao"></param>
        /// <param name="NumMatricula"></param>
        /// <returns></returns>
        private List<DtoCamposValor> GetCamposImovel(long? IdTipoAto, long? IdPrenotacao, long IdCtaAcessoSist, string NumMatricula)
        {
            List<DtoCamposValor> listaTmp = new List<DtoCamposValor>();
            List<CampoTipoAto> listaCampos = this.UfwCartNew.Repositories.RepositoryModeloDocx.GetListaCamposIdTipoAto(IdTipoAto, IdCtaAcessoSist).Where(l => l.Entidade == "IMOVEL").ToList();

            PREIMO Imovel = this.UfwCartNew.Repositories.GenericRepository<PREIMO>().GetWhere(i => i.SEQPRE == IdPrenotacao && i.MATRI.ToString() == NumMatricula).FirstOrDefault();

            if (Imovel != null)
            {
                foreach (var item in listaCampos)
                {
                    var prop = Imovel.GetType().GetProperty(item.Campo);

                    if (prop != null)
                    {
                        listaTmp.Add(new DtoCamposValor
                        {
                            Campo = item.NomeCampo,
                            Valor = (prop.GetValue(Imovel) == null) ? "" : prop.GetValue(Imovel).ToString()
                        });
                    }
                }
            }

            return listaTmp;
        }

        private List<DtoPessoaPesxPre> GetPessoas(long IdTipoAto, long IdPrenotacao, long IdCtaAcessoSist,  long[] ListIdsPessoas)
        {
            var pessoas =
                from pre in this.UfwCartNew.Repositories.GenericRepository<PESXPRE>().Get().Where(pp => (ListIdsPessoas.Contains(pp.SEQPES) && (pp.SEQPRE == IdPrenotacao)))
                join pes in this.UfwCartNew.Repositories.GenericRepository<PESSOAS>().Get() on pre.SEQPES equals pes.SEQPES
                orderby pes.NOM
                select new DtoPessoaPesxPre
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
                    TipoPessoaPrenotacao = 
                        pre.REL == "E" ? TipoPessoaPrenotacao.outorgado: 
                        pre.REL == "O" ? TipoPessoaPrenotacao.outorgante : TipoPessoaPrenotacao.indefinido,
                };

            List<CampoTipoAto> listaCampos = this.UfwCartNew.Repositories.RepositoryModeloDocx.GetListaCamposIdTipoAto(IdTipoAto, IdCtaAcessoSist).Where(l => l.Entidade == "PESSOA").ToList();

            foreach (var pessoa in pessoas)
            {
                foreach (var item in listaCampos)
                {
                    var prop = pessoa.GetType().GetProperty(item.Campo);

                    if (prop != null)
                    {
                        pessoa.ListaCamposValor.Add(new DtoCamposValor
                        {
                            Campo = item.NomeCampo,
                            Valor = (prop.GetValue(pessoa) == null) ? "" : prop.GetValue(pessoa).ToString()
                        });
                    }
                }
            }

            return pessoas.ToList();
        }

        /// <summary>
        /// RemoveUltimaMarcacao
        /// </summary>
        /// <param name="ato"></param>
        /// <returns></returns>
        private static string RemoveUltimaMarcacao(string ato)
        {
            var atoString = ato.Substring(0, ato.Length - 1);
            atoString = atoString.Replace('\n', ' ').Replace("&nbsp;", "");
            return atoString;
        }
        #endregion

        public DtoAto NovoAto(DtoAto Ato, string textoHtml)
        {
            throw new NotImplementedException();
        }

        public bool EditarAto(long IdAto, string textoHtml)
        {
            throw new NotImplementedException();
        }

        public void ConferirAto(long IdAto, TipoConferenciaAto tipoConferencia)
        {
            throw new NotImplementedException();
        }

        public void Desativar(long IdAto)
        {
            throw new NotImplementedException();
        }

        public bool FinalizarAto(long IdAto)
        {
            throw new NotImplementedException();
        }

        public IEnumerable<DtoAtoDocx> GerarFichas(long IdAto)
        {
            throw new NotImplementedException();
        }

        public void ImprimirAto(long IdAto)
        {
            throw new NotImplementedException();
        }

        public void ImprimirFicha(long IdDocx)
        {
            throw new NotImplementedException();
        }

        public void UploadFicha(long IdDocx)
        {
            throw new NotImplementedException();
        }

        public void Bloquear(long IdAto)
        {
            throw new NotImplementedException();
        }

        /// <summary>
        /// Listar atos de um período
        /// </summary>
        /// <param name="dataIni"></param>
        /// <param name="dataFim"></param>
        /// <param name="IdUsuario"></param>
        /// <returns></returns>
        public IEnumerable<DtoAtoList> GetListaAtos(DateTime dataIni, DateTime dataFim, string IdUsuario = null)
        {
            IEnumerable<DtoAtoList> lista = new List<DtoAtoList>();
            //todo: ronaldo fazer GetListAtos

            return lista;
        }

        /// <summary>
        /// Busca Dados do Ato por Id
        /// </summary>
        /// <param name="IdAto"></param>
        /// <returns></returns>
        public DtoDadosAto GetDadosAto(long IdAto)
        {
            DtoDadosAto dtoDadosAto = new DtoDadosAto();
            try
            {
                Ato ato = this.DsFactoryCartNew.AtoDs.GetById(IdAto);
                dtoDadosAto = Mapper.Map<Ato, DtoDadosAto>(ato);
            }
            catch (Exception ex)
            {
                throw new Exception("Falha GetDadosAto: " + ex.Message);
            }

            return dtoDadosAto;
        }

        /// <summary>
        /// Busca Dados do Ato por IdPrenotacao
        /// </summary>
        /// <param name="IdPrenotacao"></param>
        /// <returns></returns>
        public DtoDadosAto GetDadosAtoPrenotacao(long IdPrenotacao)
        {
            DtoDadosAto dtoDadosAto = new DtoDadosAto();
            try
            {
                Ato ato = this.DsFactoryCartNew.AtoDs.GetWhere(p => p.IdPrenotacao == IdPrenotacao).FirstOrDefault();
                if (ato != null)
                {
                    dtoDadosAto = Mapper.Map<Ato, DtoDadosAto>(ato);
                }
            }
            catch (Exception ex)
            {
                throw new Exception("Falha GetDadosAtoPorIdPrenotacao: " + ex.Message);
            }

            return dtoDadosAto;
        }

        /// <summary>
        /// Busca Dados do imovel por prenotação/matricula
        /// </summary>
        /// <param name="IdPrenotacao"></param>
        /// <param name="NumMatricula"></param>
        /// <returns></returns>
        public DtoDadosImovel GetDadosImovelPrenotacao(long IdPrenotacao)
        {
            DtoDadosImovel dtoDadosImovel = new DtoDadosImovel();
            try
            {
                dtoDadosImovel = this.DsFactoryCartNew.AtoDs.GetDadosImovelPrenotacao(IdPrenotacao);
            }
            catch (Exception ex)
            {
                throw new Exception("Falha GetDadosImovelPrenotacao: " + ex.Message);
            }

            return dtoDadosImovel;
        }

        /// <summary>
        /// Lista de Pessoa por Prenotação
        /// </summary>
        /// <param name="numeroPrenotacao"></param>
        /// <returns></returns>
        public IEnumerable<DtoPessoaPesxPre> GetPessoasPrenotacao(long numeroPrenotacao)
        {
            if (listaPessoasPrenotacao == null)
            {
                try
                {
                    listaPessoasPrenotacao = this.DsFactoryCartNew.AtoDs.GetPessoasPrenotacao(numeroPrenotacao).ToList();
                }
                catch (Exception ex)
                {
                    throw new Exception("Falha GetPessoasPrenotacao: " + ex.Message);
                }
            }

            return listaPessoasPrenotacao;
        }

        /// <summary>
        /// GetListDtoDocxAto
        /// </summary>
        /// <param name="NumMatricula"></param>
        /// <returns></returns>
        public IEnumerable<DtoDocxList> GetListDtoDocxAto(string NumMatricula)
        {
            IEnumerable<DtoDocxList> lista = new List<DtoDocxList>();

            try
            {
                lista = this.DsFactoryCartNew.AtoDs.GetListDtoDocxAto(NumMatricula);
            }
            catch (Exception ex)
            {
                throw new Exception("Falha GetListDtoDocxAto: " + ex.Message);
            }

            return lista;
        }

        /// <summary>
        /// GetTextoWordDocModelo
        /// </summary>
        /// <param name="IdModeloDoc"></param>
        /// <returns></returns>
        public StringBuilder GetTextoWordDocModelo(string filePathName)
        {
            StringBuilder textoDoc = new StringBuilder();

            using (var stream = new MemoryStream())
            {
                // Convert input file to RTF stream.
                stream.Position = 0;

                using (AtoWordDocx atoWordDocx = new AtoWordDocx(this, filePathName, 1))
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

        /// <summary>
        /// 
        /// </summary>
        /// <param name="dtoInfAto"></param>
        /// <returns></returns>
        public StringBuilder GetTextoAto(DtoInfAto dtoInfAto)
        {
            StringBuilder textoDoc = new StringBuilder();

            DtoDadosAto dtoDadosAto = new DtoDadosAto();
            DtoDadosImovel dtoDadosImovel = new DtoDadosImovel();

            if (dtoInfAto.IdAto > 0)
            {
                //dtoDadosAto = GetDadosAto(dtoInfAto.IdAto ?? 0);
                //todo: catregar do ato fazer 
            }
            else
            {
                dtoDadosAto.ListaCamposValor = this.GetCamposAto(dtoInfAto.IdTipoAto, dtoInfAto.IdPrenotacao, dtoInfAto.IdCtaAcessoSist, dtoInfAto.NumMatricula);



                //dtoDadosAto.Pessoas = this.GerarFichas

                using (AtoWordDocx atoWordDocx = new AtoWordDocx(this, dtoInfAto.ModeloPathName, dtoInfAto.IdCtaAcessoSist))
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
