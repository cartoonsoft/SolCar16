using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xceed.Words.NET;
using System.Drawing;
using Microsoft.Office.Interop.Word;
using AppServices.Car16.AppServices.Base;
using AppServices.Car16.Interfaces;
using Domain.Car16.Entities.Diversas;
using Domain.Car16.Interfaces.UnitOfWork;
using Dto.Car16.Entities.Cadastros;
using LibFunctions.Functions.Word;
using LibFunctions.Functions.DatesFunc;
using LibFunctions.Functions.IOAdmCartorio;

namespace AppServices.Car16.AppServices
{
    public class AppServiceCadastroDeAto : AppServiceBase<DtoCadastroDeAto, CadastroDeAto>, IAppServiceCadastroAto
    {
        public AppServiceCadastroDeAto(IUnitOfWorkDataBaseCar16New unitOfWork) : base(unitOfWork)
        {
        }

        public bool EscreverAtoNoWord(DtoCadastroDeAto modelo, string filePath, long numSequenciaAto)
        {
            //Representa o documento e o numero de pagina
            Application app = new Application();
            Document doc = null;
            int numeroPagina;
            int posicaoCursor;
            try
            {
                // 3 = ATO INICIAL
                if (modelo.IdTipoAto == (int)Domain.Car16.enums.TipoAtoEnum.AtoInicial)
                {
                    //Inicia a variavel que representa o documento
                    app.Visible = false;
                    doc = app.Documents.Add();

                    //Configuração do documento
                    WordParagraphHelper.ParapraphAlignment(doc, WdParagraphAlignment.wdAlignParagraphJustify);
                    WordPageHelper.InicialConfiguration(doc, WdPaperSize.wdPaperB5, 14, "Times New Roman", true);

                    //Pegando o numero da pagina para configurar o layout
                    numeroPagina = WordPageHelper.GetNumeroPagina(doc);
                    WordPageHelper.ConfigurePageLayout(doc, numeroPagina);
                    //WordLayoutPageHelper.InserirCabecalho(modelo, doc, true);
                    WordParagraphHelper.InserirParagrafo(doc, new string(' ', 5) + modelo.PREIMO.MATRI + new string(' ', 17 + (15 - modelo.PREIMO.MATRI.ToString().Length)) +
                    WordPageHelper.GetNumeroFicha(doc, true) + new string(' ', 18 + (5 - WordPageHelper.GetNumeroFicha(doc, true).ToString().Length)) + new string(' ', 14) + DataHelper.GetDataPorExtenso() + "."
                    , false);
                    WordParagraphHelper.InserirParagrafoEmBranco(doc);
                    WordParagraphHelper.InserirParagrafoEmBranco(doc);
                    WordParagraphHelper.SpaceAfterParagraphs(doc, 0);
                }
                else
                {
                    //Abre o arquivo para escrever o ATO e faz as configurações iniciais
                    app.Visible = false;
                    try
                    {
                        var caminho = filePath.Replace("_pendente", "").Replace("AtosPendentes", "Atos");
                        doc = app.Documents.Open(caminho);
                        foreach (Microsoft.Office.Interop.Word.Paragraph paragrafo in doc.Paragraphs)
                        {
                            if (paragrafo.Range.Text.Contains('\u0001'))
                            {
                                paragrafo.Range.Text = " ";
                                WordParagraphHelper.InserirParagrafoEmBranco(doc);
                            }
                        }
                        doc.SaveAs(caminho);
                        doc.Close();
                        using (var docx = DocX.Load(caminho))
                        {
                            foreach (var item in docx.Paragraphs)
                            {
                                item.Color(Color.Transparent);
                            }
                            docx.Paragraphs.Last().Color(Color.Black);
                            docx.SaveAs(filePath);
                        }
                        doc.SaveAs(filePath);
                        doc.Close();


                        //using (var docx = DocX.Load(caminho))
                        //{
                        //    foreach (var item in docx.Paragraphs)
                        //    {
                        //        if (string.IsNullOrEmpty(item.Text) && item != docx.Paragraphs.Last())
                        //        {
                        //            item.Remove(false);
                        //            docx.InsertParagraph();
                        //        }
                        //        else
                        //        {
                        //            item.Color(Color.Transparent);
                        //        }

                        //    }
                        //    docx.Paragraphs.Last().Color(Color.Black);
                        //    docx.SaveAs(filePath);
                        //}
                        doc = app.Documents.Open(filePath);
                    }
                    catch (Exception ex)
                    {
                        doc = app.Documents.Add();
                    }

                    //if (!modelo.ExisteNoSistema)
                    //{

                    //}
                    //else
                    //{ 
                    //    doc = app.Documents.Open(filePath);
                    //}

                    string sigla = string.Empty;
                    switch (modelo.IdTipoAto)
                    {
                        case 2:
                            sigla = "AV";
                            break;
                        case 1:
                            sigla = "R";
                            break;
                        default:
                            sigla = "";
                            break;
                    }

                    WordPageHelper.InicialConfiguration(doc, WdPaperSize.wdPaperB5, 14, "Times New Roman",true);

                    //Numero de paginas do documento e a posição do cursor
                    numeroPagina = WordPageHelper.GetNumeroPagina(doc);
                    posicaoCursor = WordPageHelper.GetContentEnd(doc, 1);

                    if (modelo.IrParaFicha > 0)
                    {
                        WordPageHelper.DeslocarAte(doc, modelo.IrParaFicha, modelo.IrParaVerso);

                        if (modelo.QuantidadeCentimetrosDaBorda > 0)
                        {
                            ///Desloca os centimetros e escreve o cabeçalho, se necessario. 
                            ///Atualiza o numero da pagina e a posição do cursor
                            WordHelper.DesviarCentimetros(doc, modelo, sigla, numSequenciaAto, modelo.QuantidadeCentimetrosDaBorda, ref numeroPagina, ref posicaoCursor, true);
                        }
                        else
                        {
                            WordLayoutPageHelper.InserirCabecalho(modelo, doc, true, false);
                            numeroPagina = WordPageHelper.GetNumeroPagina(doc);
                        }
                    }
                    else
                    {
                        WordParagraphHelper.InserirParagrafoEmBranco(doc);
                        if (WordPageHelper.GetNumeroPagina(doc) > numeroPagina)
                        {
                            doc.Paragraphs.Last.Range.Delete();
                            WordLayoutPageHelper.InserirCabecalho(modelo, doc, false);
                            doc.Paragraphs.Last.Range.Delete();
                            WordLayoutPageHelper.InserirContinuacaoFicha(doc);

                            posicaoCursor = WordPageHelper.GetContentEnd(doc, 1);
                            WordTextStyleHelper.Bold(doc, posicaoCursor, false);

                            //TO DO : Pegar o numero do até em sequencia.
                            //Escreve o tipo de ato (R ou AV), além disso, escreve o numero da sequencia e a Ato
                            WordParagraphHelper.InserirTextoEmRange(doc, posicaoCursor, $"{sigla}-{numSequenciaAto}/{modelo.PREIMO.MATRI} - ");
                            numeroPagina = WordPageHelper.GetNumeroPagina(doc);

                        }
                        else
                        {
                            doc.Paragraphs.Last.Range.Delete();
                            posicaoCursor = WordPageHelper.GetContentEnd(doc, 1);
                            WordParagraphHelper.InserirTextoEmRange(doc, posicaoCursor, $"{sigla}-{numSequenciaAto}/{modelo.PREIMO.MATRI} - ");

                        }
                    }
                    if (!modelo.ExisteNoSistema && modelo.QuantidadeCentimetrosDaBorda == 0)
                    {
                        posicaoCursor = WordPageHelper.GetContentEnd(doc, 1);
                        WordTextStyleHelper.Bold(doc, posicaoCursor, false);
                        WordParagraphHelper.InserirTextoEmRange(doc, posicaoCursor, $"{sigla}-{numSequenciaAto}/{modelo.PREIMO.MATRI} - ");
                        numeroPagina = WordPageHelper.GetNumeroPagina(doc);
                    }
                }
                #region | Metodo para escrever o ATO |
                //Pega a posição do cursor do final do documento
                posicaoCursor = WordPageHelper.GetContentEnd(doc, 1);
                //Não deixa o texto começar com negrito
                WordTextStyleHelper.Bold(doc, posicaoCursor, false);
                //Escreve o ato e ajusta o documento, caso necessário
                WordHelper.EscreverAto(modelo, doc, ref numeroPagina, ref posicaoCursor);
                WordLayoutPageHelper.AjustarFinalDocumento(doc, numeroPagina, posicaoCursor, modelo);

                #endregion

                //Salvando e finalizando documento
                doc.SaveAs2(filePath);
                doc.Close();

            }
            catch (Exception ex)
            {
                IOFunctions.GerarLogErro(ex);

                doc.Close(WdSaveOptions.wdDoNotSaveChanges);
                return false;
                throw;
            }
            finally
            {
                app.Quit();
                app = null;
                doc = null;
                GC.Collect();
            }
            return true;
        }

    }
}
