using AppServices.Car16.AppServices.Base;
using AppServices.Car16.Interfaces;
using Domain.Car16.Entities.Car16New;
using Domain.Car16.Interfaces.UnitOfWork;
using Dto.Car16.Entities.Cadastros;
using LibFunctions.Functions;
using Microsoft.Office.Interop.Word;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AppServices.Car16.AppServices
{
    public class AppServiceMatriculaAto : AppServiceCar16<DtoMatriculaAto, MatriculaAto>, IAppServiceMatriculaAto
    {
        public AppServiceMatriculaAto(IUnitOfWorkCar16 unitOfWorkCar16) : base(unitOfWorkCar16)
        {
        }

        public bool EscreverAtoNoWord(DtoMatriculaAto modelo, string filePath)
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
                    app.Visible = true;
                    doc = app.Documents.Add();

                    //Configuração do documento
                    WordParagraphHelper.ParapraphAlignment(doc, WdParagraphAlignment.wdAlignParagraphJustify);
                    WordPageHelper.InicialConfiguration(doc, WdPaperSize.wdPaperB5, 14, "Times New Roman", true);

                    //Pegando o numero da pagina para configurar o layout
                    numeroPagina = WordPageHelper.GetNumeroPagina(doc);
                    WordPageHelper.ConfigurePageLayout(doc, numeroPagina);
                    WordLayoutPageHelper.InserirCabecalho(modelo, doc, true);

                }
                else
                {
                    //Abre o arquivo para escrever o ATO e faz as configurações iniciais
                    app.Visible = true;
                    doc = app.Documents.Open(filePath);
                    WordPageHelper.InicialConfiguration(doc, WdPaperSize.wdPaperB5, 14, "Times New Roman", true);

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
                            WordHelper.DesviarCentimetros(doc, modelo, modelo.QuantidadeCentimetrosDaBorda, ref numeroPagina, ref posicaoCursor, true);
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
                            WordParagraphHelper.InserirTextoEmRange(doc, posicaoCursor, $"R-12/{modelo.MatriculaID} - ");
                            numeroPagina = WordPageHelper.GetNumeroPagina(doc);

                        }
                        else
                        {
                            doc.Paragraphs.Last.Range.Delete();
                            posicaoCursor = WordPageHelper.GetContentEnd(doc, 1);
                            WordParagraphHelper.InserirTextoEmRange(doc, posicaoCursor, $"R-12/{modelo.MatriculaID} - ");

                        }
                    }
                }
                #region | Metodo para escrever o ATO |
                //Pega a posição do cursor do final do documento
                posicaoCursor = WordPageHelper.GetContentEnd(doc, 1);
                //Não deixa o texto começar com negrito
                WordTextStyleHelper.Bold(doc, posicaoCursor, false);
                //Escreve o ato e ajusta o documento, caso necessário
                WordHelper.EscreverAto(modelo, doc, ref numeroPagina, ref posicaoCursor, modelo.IrParaFicha > 0);
                WordLayoutPageHelper.AjustarFinalDocumento(doc, numeroPagina, posicaoCursor, modelo);

                #endregion

                //Salvando e finalizando documento
                doc.SaveAs2(filePath);
                doc.Close();
                
            }
            catch (Exception)
            {
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
