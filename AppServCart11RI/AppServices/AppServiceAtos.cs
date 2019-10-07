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
        private List<DtoCamposValor> GetCamposAto(long? IdTipoAto, long? IdPrenotacao, string NumMatricula)
        {
            DateTime dataTmp = DateTime.ParseExact("01/01/1800", "dd/MM/yyyy", CultureInfo.InvariantCulture);
            string dataPremadTmp = string.Empty;
            string valorTmp = string.Empty;

            List<DtoCamposValor> listaTmp = new List<DtoCamposValor>();
            List<CamposModeloDoc> listaCampos = this.UfwCartNew.Repositories.RepositoryModeloDocx.GetListaCamposIdTipoAto(IdTipoAto)
                .Where(l => ((l.Entidade == "ATO") || (l.Entidade == "PRENOTACAO") || (l.Entidade == "IMOVEL"))).ToList();

            Ato ato = this.UfwCartNew.Repositories.RepositoryAto.GetWhere(a => (a.IdPrenotacao == IdPrenotacao) && (a.NumMatricula == NumMatricula)).FirstOrDefault();
            PREIMO preimo = this.UfwCartNew.Repositories.GenericRepository<PREIMO>().GetWhere(p => (p.SEQPRE == IdPrenotacao) && (p.MATRI.ToString() == NumMatricula)).FirstOrDefault();


            //para obter data da prenotacao
            PREMAD premad = this.UfwCartNew.Repositories.GenericRepository<PREMAD>()
                .GetWhere(p => (p.SEQPRED == IdPrenotacao) && (p.TIPODATA.Trim() == "R"))
                .OrderByDescending(o => o.DATA).FirstOrDefault();

            if (premad != null)
            {
                dataPremadTmp = dataTmp.AddDays(premad.DATA).ToString("dd/MM/yyyy");
            }

            foreach (var item in listaCampos)
            {
                var prop = premad.GetType().GetProperty(item.Campo);

                if (prop != null)
                {
                    valorTmp = (prop.GetValue(premad) == null) ? "" : prop.GetValue(premad).ToString();

                    if (item.Campo == "IdPrenotacao")
                    {
                        valorTmp = IdPrenotacao.ToString();
                    }

                    if (item.Campo == "DATA")
                    {
                        valorTmp = dataTmp.AddDays(premad.DATA).ToString("dd/MM/yyyy");
                    }

                    listaTmp.Add(new DtoCamposValor
                    {
                        Campo = item.NomeCampo,
                        Valor = valorTmp
                    });
                }
            }


            if (premad != null)
            {
                foreach (var item in listaCampos)
                {
                    var prop = premad.GetType().GetProperty(item.Campo);

                    if (prop != null)
                    {
                        valorTmp = (prop.GetValue(premad) == null) ? "" : prop.GetValue(premad).ToString();

                        if (item.Campo == "IdPrenotacao")
                        {
                            valorTmp = IdPrenotacao.ToString();
                        }

                        if (item.Campo == "DATA")
                        {
                            valorTmp = dataTmp.AddDays(premad.DATA).ToString("dd/MM/yyyy");
                        }

                        listaTmp.Add(new DtoCamposValor
                        {
                            Campo = item.NomeCampo,
                            Valor = valorTmp
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
        private List<DtoCamposValor> GetCamposImovel(long? IdTipoAto, long? IdPrenotacao, string NumMatricula)
        {
            List<DtoCamposValor> listaTmp = new List<DtoCamposValor>();
            List<CamposModeloDoc> listaCampos = this.UfwCartNew.Repositories.RepositoryModeloDocx.GetListaCamposIdTipoAto(IdTipoAto).Where(l => l.Entidade == "IMOVEL").ToList();

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

        /// <summary>
        /// 
        /// </summary>
        /// <param name="pessoa"></param>
        /// <param name="campoQuery"></param>
        /// <returns></returns>
        private string GetValorCampoPessoa(DtoPessoaPesxPre pessoa, string campoQuery)
        {
            string Campotmp = string.Empty;

            try
            {
                //foreach (var Campo in pessoa.listaCamposValor)
                //{
                //    if (Campo.Campo.Equals(campoQuery))
                //    {
                //        Campotmp = Campo.Valor;
                //    }
                //}

                //Retorna o dados das pessoas
                return string.IsNullOrEmpty(Campotmp.Trim()) ? $"[{campoQuery}]" : Campotmp;
            }
            catch (Exception)
            {
                return "[NÃO ENCONTRADO]";
                throw;
            }
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="dtoDados"></param>
        /// <param name="campoQuery"></param>
        /// <returns></returns>
        private string GetValorCampoModeloMatricula(DtoDadosImovel dtoDados, string campoQuery)
        {
            string Campotmp = string.Empty;
            bool CampoEncontrado = false;

            try
            {
                //PESQUISA DADOS IMÓVEL
                //foreach (var item in dtoDados.listaCamposValor)
                //{
                //    if (item.Campo.Equals(campoQuery))
                //    {
                //        //Retorna o campo
                //        Campotmp = item.Valor;
                //        CampoEncontrado = true;
                //    }
                //}

                //PESQUISA DADOS PESSOA
                //if (!CampoEncontrado)
                //{
                //    foreach (var pessoas in dtoDados.Pessoas)
                //    {
                //        foreach (var pessoa in pessoas.listaCamposValor)
                //        {
                //            if (pessoa.Campo.Equals(campoQuery))
                //            {
                //                Campotmp = pessoa.Valor;
                //            }
                //        }
                //    }
                //}

                //Retorna o dados das pessoas
                return string.IsNullOrEmpty(Campotmp.Trim()) ? $"[{campoQuery}]" : Campotmp;

            }
            catch (Exception)
            {
                return "[NÃO ENCONTRADO]";
                throw;
            }
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="texto"></param>
        /// <param name="pessoa"></param>
        /// <returns></returns>
        private string PopularCamposDoTexto(string texto, DtoPessoaPesxPre pessoa)
        {
            StringBuilder stringBuilder = new StringBuilder();
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
                            //Response.StatusCode = 500;
                            //Response.StatusDescription = "Arquivo com campos corrompidos, verifique o modelo";
                            //return Response.StatusDescription;
                        }
                    }
                    //Buscar dado da pessoa aqui
                    //resultadoQuery = "teste query";
                    resultadoQuery = this.GetValorCampoPessoa(pessoa, nomeCampo);

                    //atualiza o texto formatado
                    stringBuilder.Append(resultadoQuery);
                }
                else
                {
                    //caso não seja um campo somente adiciona o caractere
                    stringBuilder.Append(texto[i].ToString());
                }
            }

            return stringBuilder.ToString();
        }

        /// <summary>
        /// 
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
                DocumentModel.Load(filePathName).Save(stream, SaveOptions.HtmlDefault);
                
                using (var reader = new StreamReader(stream))
                {
                    textoDoc.Append(reader.ReadToEnd());
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
                dtoDadosAto = GetDadosAtoPrenotacao(dtoInfAto.IdPrenotacao);

            }
            /*
            using (FileStream fileStream = new FileStream(fullName, FileMode.Open, FileAccess.Read))
            {
                //Carrega o Modelo
                DocumentModel document = DocumentModel.Load(fileStream, LoadOptions.DocxDefault);

                // Get Word document's plain text.
                string text = document.Content.ToString();
                if (text != "")
                {
                    StringBuilder textoParagrafo = new StringBuilder();
                    for (int i = 0; i < text.Length; i++)
                    {
                        if (text[i] == '[')
                        {
                            i++;
                            string nomeCampo = string.Empty;
                            string resultadoQuery = string.Empty;
                            while (text[i] != ']')
                            {
                                nomeCampo += text[i].ToString().Trim();
                                i++;
                                if (i >= text.Length || text[i] == '[')
                                {
                                    throw new InvalidDataException("Arquivo com campos corrompidos, verifique o modelo");
                                }
                            }
                            //Buscar dado da pessoa aqui
                            //resultadoQuery = "teste query";
                            resultadoQuery = this.GetValorCampoModeloMatricula(dadosImovel, nomeCampo);

                            //atualiza o texto formatado
                            textoParagrafo.Append(resultadoQuery);
                        }
                        else if (paragrafo.Text[i] == '<')
                        {
                            i++;
                            var tipoTag = string.Empty;
                            while (paragrafo.Text[i] != '>')
                            {
                                tipoTag += paragrafo.Text[i].ToString().Trim();
                                i++;
                                if (i >= paragrafo.Text.Length || paragrafo.Text[i] == '<')
                                {
                                    Response.StatusCode = 500;
                                    Response.StatusDescription = "Tags de repetição corrompidas, verifique o modelo";
                                    return Response.StatusDescription;
                                }
                            }
                            i++;
                            if (tipoTag.Equals("outorgantes"))
                            {
                                i = Repetir(dadosImovel, paragrafo, textoParagrafo, i);
                            }
                            else if (tipoTag.Equals("outorgados"))
                            {
                                i = Repetir(dadosImovel, paragrafo, textoParagrafo, i, false);
                            }
                        }
                        else
                        {
                            //caso não seja um campo somente adiciona o caractere
                            textoParagrafo.Append(paragrafo.Text[i].ToString());
                        }

                    }
                    // Populando campo de retorno
                    textoFormatado.Append($"<p>{textoParagrafo}</p>");
                }





                using (DocumentModel docX = DocumentModel.Load(fileStream))
                {
                    //Varre todos os paragrafos do Modelo
                    foreach (var paragrafo in docX.Paragraphs)
                    {
                        if (paragrafo.Text != "")
                        {
                            StringBuilder textoParagrafo = new StringBuilder();
                            for (int i = 0; i < paragrafo.Text.Length; i++)
                            {
                                if (paragrafo.Text[i] == '[')
                                {
                                    i++;
                                    string nomeCampo = string.Empty;
                                    string resultadoQuery = string.Empty;
                                    while (paragrafo.Text[i] != ']')
                                    {
                                        nomeCampo += paragrafo.Text[i].ToString().Trim();
                                        i++;
                                        if (i >= paragrafo.Text.Length || paragrafo.Text[i] == '[')
                                        {
                                            Response.StatusCode = 500;
                                            Response.StatusDescription = "Arquivo com campos corrompidos, verifique o modelo";
                                            return Response.StatusDescription;
                                        }
                                    }
                                    //Buscar dado da pessoa aqui
                                    //resultadoQuery = "teste query";
                                    resultadoQuery = GetValorCampoModeloMatricula(dadosImovel, nomeCampo);

                                    //atualiza o texto formatado
                                    textoParagrafo.Append(resultadoQuery);
                                }
                                else if (paragrafo.Text[i] == '<')
                                {
                                    i++;
                                    var tipoTag = string.Empty;
                                    while (paragrafo.Text[i] != '>')
                                    {
                                        tipoTag += paragrafo.Text[i].ToString().Trim();
                                        i++;
                                        if (i >= paragrafo.Text.Length || paragrafo.Text[i] == '<')
                                        {
                                            Response.StatusCode = 500;
                                            Response.StatusDescription = "Tags de repetição corrompidas, verifique o modelo";
                                            return Response.StatusDescription;
                                        }
                                    }
                                    i++;
                                    if (tipoTag.Equals("outorgantes"))
                                    {
                                        i = Repetir(dadosImovel, paragrafo, textoParagrafo, i);
                                    }
                                    else if (tipoTag.Equals("outorgados"))
                                    {
                                        i = Repetir(dadosImovel, paragrafo, textoParagrafo, i, false);
                                    }
                                }
                                else
                                {
                                    //caso não seja um campo somente adiciona o caractere
                                    textoParagrafo.Append(paragrafo.Text[i].ToString());
                                }

                            }
                            // Populando campo de retorno
                            textoFormatado.Append($"<p>{textoParagrafo}</p>");
                        }
                    }
                }
            }

            DtoDadosImovel imovel = appServiceAto.GetDadosImovelPrenotacao(dadosAtoViewModel.IdPrenotacao);
            */


            //DtoDadosImovel dadosImovel = appServiceAto.GetDadosImovelPrenotacao .GetCamposModeloMatricula(DadosPostModelo.listIdsPessoas, DadosPostModelo.IdTipoAto, DadosPostModelo.IdPrenotacao, DadosPostModelo.NumMatricula);
            return textoDoc;
        }

    }
}
