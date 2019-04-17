using AdmCartorio.Models;
using HtmlAgilityPack;
using Microsoft.Office.Core;
using Microsoft.Office.Interop.Word;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;
using Xceed.Words.NET;
using Shapes = Microsoft.Office.Interop.Word.Shapes;

namespace AdmCartorio.Controllers
{
    public class MatriculaController : Controller
    {
        // GET: Matricula
        public ActionResult Index()
        {
            var dados = new MatriculaAtoViewModel()
            {
                MatriculasViewModel = getMatriculaViewModel(),
                ModelosSimplificadoViewModel = getModeloSimplificadoViewModel()
            };
            return View(dados);
        }

        private static List<ArquivoModeloSimplificadoViewModel> getModeloSimplificadoViewModel()
        {
            return new List<ArquivoModeloSimplificadoViewModel>()
                {
                    new ArquivoModeloSimplificadoViewModel()
                    {
                        Id = 1,
                        DescricaoTipoAto = "Ato Inicial",
                        NomeModelo = "TesteModelo"
                    },
                    new ArquivoModeloSimplificadoViewModel()
                    {
                        Id = 2,
                        DescricaoTipoAto = "Registro",
                        NomeModelo = "testeWord"
                    }
                };
        }

        private static List<MatriculaViewModel> getMatriculaViewModel()
        {
            return new List<MatriculaViewModel>()
                {
                    new MatriculaViewModel()
                    {
                        EnderecoImovel = "Endereço 1",
                        MatriculaId = 1,
                        NomeImovel = "Imovel 1",
                        NomeProprietarioAtual = "Proprietario 1"

                    },
                    new MatriculaViewModel()
                    {
                        EnderecoImovel = "Endereço 2",
                        MatriculaId = 2,
                        NomeImovel = "Imovel 2",
                        NomeProprietarioAtual = "Proprietario 2"

                    }
                };
        }


        [HttpPost]
        [ValidateInput(false)]
        public ActionResult Index(MatriculaAtoViewModel modelo)
        {
            string filePath = Server.MapPath($"~/App_Data/Arquivos/{modelo.MatriculaID}_.docx");
            try
            {

                if (modelo.Ato == null)
                {
                    modelo.MatriculasViewModel = getMatriculaViewModel();
                    modelo.ModelosSimplificadoViewModel = getModeloSimplificadoViewModel();
                    ViewBag.erro = "O Ato é obrigatório";
                    return View(nameof(Index), modelo);
                }
                //Ajusta a string de ato(HTML) -> ato(String)
                modelo.Ato = ConvertHtmlToString(modelo.Ato);

                if (ModelState.IsValid)
                {
                    using (FileStream fileStream = new FileStream(filePath, FileMode.OpenOrCreate, FileAccess.ReadWrite))
                    {
                        if (modelo.ModeloTipoAto == "Ato Inicial")
                        {
                            #region | INICIALIZACAO E CONFIGURACAO INICIAL |
                            var app = new Application
                            {
                                Visible = true
                            };

                            var doc = app.Documents.Add();
                            //Configuração do documento
                            doc.Paragraphs.Format.Alignment = WdParagraphAlignment.wdAlignParagraphJustify;
                            doc.PageSetup.PaperSize = WdPaperSize.wdPaperB5;
                            doc.PageSetup.BottomMargin = 35;
                            doc.PageSetup.LeftMargin = 62.3f;
                            doc.PageSetup.RightMargin = 36.5f;
                            doc.Application.ActiveDocument.AutoHyphenation = true;

                            var numeroPagina = WordPageHelper.GetNumeroPagina(doc);
                            int posicaoCursor;
                            #endregion
                            ////Inserir paragrafo
                            //WordParagraphHelper.InserirParagrafo(doc, "LIVRO N.° 2 - REGISTRO" + new string(' ', 52) + "16.° CARTÓRIO DE REGISTRO DE IMÓVEIS"
                            //    , false);

                            ////NEGRITO E SUBLINHADO                            
                            //WordTextStyleHelper.Bold(doc, 74, 110);
                            //WordTextStyleHelper.Underline(doc, 74, 111, WdUnderline.wdUnderlineSingle);
                            ////Inserir paragrafo após
                            //WordParagraphHelper.InserirParagrafo(doc, new string(' ', 30) + "GERAL" + new string(' ', 79) + "de São Paulo", true);

                            //WordTextStyleHelper.Underline(doc, 111, WdUnderline.wdUnderlineNone);
                            //WordTextStyleHelper.Bold(doc, 224, 237, true);
                            //doc.Paragraphs.SpaceAfter = 0;
                            //WordParagraphHelper.InserirParagrafo(doc, new string(' ', 5) + "matrícula" + new string(' ', 30) + "ficha", true);

                            ////FUNDO BRANCO PARA ILUSAO DE LEGEND DO CAMPO
                            //WordTextStyleHelper.Underline(doc, 286, WdUnderline.wdUnderlineNone);
                            //WordTextStyleHelper.SetHighlightColor(doc, 241, 253, WdColorIndex.wdWhite);
                            //WordTextStyleHelper.SetHighlightColor(doc, 280, 288, WdColorIndex.wdWhite);

                            ////Matricula, ficha, local e data
                            WordParagraphHelper.InserirParagrafo(doc, new string(' ', 5) + GetNumeroMatricula(modelo) + new string(' ', 33 + (15 - GetNumeroMatricula(modelo).ToString().Length)) +
                                WordPageHelper.GetNumeroFicha(doc) + new string(' ', 33 + (5 - WordPageHelper.GetNumeroFicha(doc).ToString().Length)) + "São Paulo," + new string(' ', 4) + GetDataPorExtenso()
                                , true);


                            //WordTextStyleHelper.SetHighlightColor(doc, 288, WdColorIndex.wdNoHighlight);
                            //WordTextStyleHelper.Bold(doc, 390, 412, true);

                            //#region | Configurando Shape | 
                            //var shapes = doc.Paragraphs.Add().Application.ActiveDocument.Shapes;
                            ////Insere os shapes de matricula e ficha
                            //WordShapeHelper.InserirShapeMatriculaFicha(shapes);
                            ////Insere a ilusão de margem
                            //WordShapeHelper.InserirShapeMargem(shapes, 84, 100);
                            //#endregion

                            doc.Paragraphs.Add().Range.InsertParagraphAfter();



                            posicaoCursor = app.ActiveDocument.Content.End - 1;
                            
                                //Imprimir modelo R-{numeroAtoSequencia}/{MATRICULA} - 
                                app.ActiveDocument.Range(posicaoCursor).Text = $"R-12/{modelo.MatriculaID} - ";
                                posicaoCursor = app.ActiveDocument.Content.End - 1;
                                for (int i = 0; i < modelo.Ato.Length; i++)
                                {
                                    if (doc.Application.Selection.Information[WdInformation.wdNumberOfPagesInDocument] > numeroPagina)
                                    {

                                        //SELECIONANDO TEXTO PARA SALVAR
                                        doc.Application.Selection.GoTo(WdGoToItem.wdGoToPage, WdGoToDirection.wdGoToNext, 1);
                                        doc.Application.Selection.GoToPrevious(WdGoToItem.wdGoToLine);

                                        doc.Application.Selection.EndOf(WdUnits.wdParagraph, WdMovementType.wdExtend);
                                        var textoParaSalvar = app.Selection.Text;

                                        app.Selection.Delete();
                                        WordParagraphHelper.InserirParagrafoEmBranco(doc);
                                        doc.Application.Selection.EndOf(WdUnits.wdParagraph, WdMovementType.wdExtend);
                                        textoParaSalvar += app.Selection.Text;
                                        app.Selection.Delete();
                                        WordParagraphHelper.InserirParagrafoEmBranco(doc);

                                        WordParagraphHelper.InserirRodape(doc);

                                        //INSERE PARAGRAFOS EM BRANCO ATÉ IR PARA A PROXIMA PÁGINA
                                        while (doc.Application.Selection.Information[WdInformation.wdNumberOfPagesInDocument] <= numeroPagina)
                                        {
                                            WordParagraphHelper.InserirParagrafoEmBranco(doc);
                                        }
                                        if (!WordPageHelper.IsVerso(WordPageHelper.GetNumeroPagina(doc)))
                                        {
                                            //CASO ACONTECEÇA ALGUM PROBLEMA COM A INSERÇÃO DO RODAPÉ INSERE UMA NOVA LINHA
                                            if (doc.Application.Selection.Information[WdInformation.wdNumberOfPagesInDocument] == numeroPagina)
                                                WordParagraphHelper.InserirParagrafoEmBranco(doc);

                                            //SELECIONA O INICIO DA PROXIMA PÁGINA PARA NÃO TER ERRO
                                            doc.Application.Selection.GoTo(WdGoToItem.wdGoToPage, WdGoToDirection.wdGoToNext, 1);


                                            doc.Paragraphs.Add().Range.Text = "LIVRO N.° 2 - REGISTRO" + new string(' ', 45) + "16.° Oficial de Registro de Imóveis da Capital";
                                            //Deixa em negrito e sublinhado
                                            WordTextStyleHelper.Bold(doc, WordPageHelper.GetRangeEnd(doc, 47), WordPageHelper.GetRangeEnd(doc));
                                            WordTextStyleHelper.Underline(doc, WordPageHelper.GetRangeEnd(doc, 47), WordPageHelper.GetRangeEnd(doc), WdUnderline.wdUnderlineSingle);

                                            doc.Paragraphs.Last.Alignment = WdParagraphAlignment.wdAlignParagraphJustify;
                                            WordParagraphHelper.InserirParagrafo(doc, new string(' ', 30) + "GERAL", true);

                                            WordTextStyleHelper.Bold(doc, WordPageHelper.GetRangeEnd(doc, 36), WordPageHelper.GetRangeEnd(doc), false);
                                            WordTextStyleHelper.Underline(doc, WordPageHelper.GetRangeEnd(doc, 36), WordPageHelper.GetRangeEnd(doc), WdUnderline.wdUnderlineNone);

                                            doc.Paragraphs.SpaceAfter = 0;
                                        }
                                        else
                                        {
                                            WordParagraphHelper.InserirParagrafoEmBranco(doc);

                                        }
                                        //INSERINDO TEXTO PADRÃO
                                        doc.Application.Selection.GoTo(WdGoToItem.wdGoToPage, WdGoToDirection.wdGoToNext, 1);

                                        var shapesTask = doc.Paragraphs.Add().Application.ActiveDocument.Shapes;
                                        //Insere os shapes de matricula e ficha
                                        WordShapeHelper.InserirShapeMatriculaFicha(shapesTask);
                                        //Ilusão de margem
                                        //WordShapeHelper.InserirShapeMargem(shapes, 84, 100);

                                        WordShapeHelper.InserirTextoMatriculaFicha(doc);

                                        doc.Paragraphs.Last.Alignment = WdParagraphAlignment.wdAlignParagraphJustify;

                                        var rangeFinal = doc.Application.ActiveDocument.Range().End;
                                        WordShapeHelper.AjustarBackGroundShapeMatriculaFicha(doc, rangeFinal);

                                        //Numero da matricula e ficha
                                        if (WordPageHelper.IsVerso(WordPageHelper.GetNumeroPagina(doc)))
                                        {
                                            WordParagraphHelper.InserirParagrafo(doc, new string(' ', 5) + GetNumeroMatricula(modelo) + new string(' ', 30 + (15 - GetNumeroMatricula(modelo).ToString().Length)) +
                                                WordPageHelper.GetNumeroFicha(doc) + new string(' ', 5 - WordPageHelper.GetNumeroFicha(doc).ToString().Length)
                                                , true);

                                        }
                                        //numero da matricula,ficha e data.
                                        else
                                        {
                                            WordParagraphHelper.InserirParagrafo(doc, new string(' ', 5) + GetNumeroMatricula(modelo) + new string(' ', 30 + (15 - GetNumeroMatricula(modelo).ToString().Length)) +
                                                WordPageHelper.GetNumeroFicha(doc) + new string(' ', 30 + (5 - WordPageHelper.GetNumeroFicha(doc).ToString().Length)) + "São Paulo," + new string(' ', 4) + GetDataPorExtenso()
                                                , true);

                                            WordTextStyleHelper.Bold(doc, WordPageHelper.GetRangeEnd(doc, 24), WordPageHelper.GetRangeEnd(doc), true);
                                        }
                                        WordTextStyleHelper.SetHighlightColor(doc, rangeFinal, WdColorIndex.wdNoHighlight);

                                        if (WordPageHelper.IsVerso(WordPageHelper.GetNumeroPagina(doc)))
                                        {
                                            WordParagraphHelper.InserirParagrafo(doc, new string(' ', 59) + "verso", true);
                                            WordTextStyleHelper.SetHighlightColor(doc, WordPageHelper.GetRangeEnd(doc, 7), WordPageHelper.GetRangeEnd(doc), WdColorIndex.wdWhite);
                                        }
                                        else
                                        {
                                            WordParagraphHelper.InserirParagrafoEmBranco(doc);
                                        }
                                        WordParagraphHelper.InserirParagrafoEmBranco(doc);
                                        //Tira do Ultimo Paragrafo o negrito
                                        doc.Paragraphs.Last.Range.Bold = 0;
                                        WordParagraphHelper.InserirParagrafoEmBranco(doc);

                                        if (!WordPageHelper.IsVerso(WordPageHelper.GetNumeroPagina(doc)) && WordPageHelper.GetNumeroFicha(doc) > 1)
                                        {
                                            WordParagraphHelper.InserirParagrafoEmRange(doc, new string(' ', 4) + $"( CONTINUAÇÃO DA FICHA N°. { WordPageHelper.GetNumeroFicha(doc) - 1} )");
                                            
                                            doc.Paragraphs.Last.Range.Bold = 1;
                                            WordParagraphHelper.InserirParagrafoEmBranco(doc);
                                            doc.Paragraphs.Last.Range.Bold = 0;
                                        }
                                        
                                        posicaoCursor = WordPageHelper.GetContentEnd(doc, 1);
                                        numeroPagina = WordPageHelper.GetNumeroPagina(doc);

                                        //Reescreve o texto que foi perdido pelo rodapé e retorna a posição do cursor atualizada
                                        posicaoCursor = WordParagraphHelper.ReescreverTextoDeFinalDePagina(doc, posicaoCursor, textoParaSalvar);

                                        i--;
                                    }
                                    else
                                    {
                                        WordParagraphHelper.InserirTextoEmRange(doc, posicaoCursor++, modelo.Ato[i].ToString());
                                    }
                                }

                                app.ActiveDocument.Paragraphs.Add().Range.InlineShapes.AddHorizontalLineStandard();
                                if (WordPageHelper.GetNumeroPagina(doc) > numeroPagina)
                                {
                                    //AJUSTAR PARAGRAFO
                                    WordShapeHelper.DeleteLastShape(doc);
                                    app.ActiveDocument.Paragraphs.Last.Range.Delete();
                                }
                                else
                                {
                                    WordParagraphHelper.InserirParagrafoEmBranco(doc);
                                    if (WordPageHelper.GetNumeroPagina(doc) > numeroPagina)
                                    {
                                        //AJUSTAR PARAGRAFO
                                        app.ActiveDocument.Paragraphs.Last.Range.Delete();
                                        WordShapeHelper.DeleteLastShape(doc);

                                    }
                                    else
                                    {
                                        app.ActiveDocument.Paragraphs.Last.Range.Delete();

                                    }

                                }

                                
                            

                            //doc.Save();


#if false
                            using (DocX docX = DocX.Create(fileStream, DocumentTypes.Document))
                            {
                                docX.InsertParagraph().Append(modelo.Ato).SpacingAfter(5);

                                //docX.InsertParagraph().InsertText(modelo.Ato);

                                //Espaço de segurança
                                docX.InsertParagraph();
                                docX.InsertParagraph().InsertHorizontalLine();
                                
                                //docX.InsertParagraph().InsertPageNumber(PageNumberFormat.normal);

                                //docX.Sections.First().InsertParagraph().AppendPageNumber(PageNumberFormat.normal);

                                //docX.InsertParagraph().AppendPageNumber(PageNumberFormat.normal);

                                fileStream.Close();
                                docX.SaveAs(filePath);
                            }
#endif
                        }
                        else
                        {

                            var app = new Application
                            {
                                Visible = true
                            };

                            var doc = app.Documents.Open("C:\\Users\\pedro.henrique\\Desktop\\TesteInterrupt.docx");
                            doc.AutoHyphenation = true;
                            var numeroPagina = WordPageHelper.GetNumeroPagina(doc);
                            var posicao = WordPageHelper.GetContentEnd(doc, 1);
                            WordParagraphHelper.InserirTextoEmRange(doc, posicao, $"R-12/{modelo.MatriculaID} - ");
                            posicao = WordPageHelper.GetContentEnd(doc, 1);



                            for (int i = 0; i < modelo.Ato.Length; i++)
                            {
                                if (WordPageHelper.GetNumeroPagina(doc) > numeroPagina)
                                {

                                    //SELECIONANDO TEXTO PARA SALVAR
                                    doc.Application.Selection.GoTo(WdGoToItem.wdGoToPage, WdGoToDirection.wdGoToNext, doc.Application.Selection.Information[WdInformation.wdNumberOfPagesInDocument] - 1);
                                    doc.Application.Selection.GoToPrevious(WdGoToItem.wdGoToLine);
                                    doc.Application.Selection.EndOf(WdUnits.wdParagraph, WdMovementType.wdExtend);
                                    var textoParaSalvar = app.Selection.Text;


                                    //AJUSTAR
                                    if (String.IsNullOrEmpty(textoParaSalvar.Replace('\r', ' ').Trim()))
                                    {
                                        app.Selection.Delete();
                                        doc.Application.Selection.EndOf(WdUnits.wdParagraph, WdMovementType.wdExtend);
                                        textoParaSalvar += app.Selection.Text;
                                        app.Selection.Delete();
                                        WordParagraphHelper.InserirParagrafoEmBranco(doc);
                                        WordParagraphHelper.InserirRodape(doc);
                                    }
                                    else
                                    {
                                        app.Selection.Delete();
                                        WordParagraphHelper.InserirParagrafoEmBranco(doc);
                                        doc.Application.Selection.EndOf(WdUnits.wdParagraph, WdMovementType.wdExtend);
                                        textoParaSalvar += app.Selection.Text;
                                        app.Selection.Delete();
                                        WordParagraphHelper.InserirParagrafoEmBranco(doc);

                                        WordParagraphHelper.InserirRodape(doc);
                                    }
                                    //INSERE PARAGRAFOS EM BRANCO ATÉ IR PARA A PROXIMA PÁGINA
                                    while (WordPageHelper.GetNumeroPagina(doc) <= numeroPagina)
                                    {
                                        WordParagraphHelper.InserirParagrafoEmBranco(doc);
                                    }
                                    if (WordPageHelper.IsVerso(WordPageHelper.GetNumeroPagina(doc)))
                                    {
                                        WordParagraphHelper.InserirParagrafoEmRange(doc, "LIVRO N.° 2 - REGISTRO" + new string(' ', 45) + "16.° Oficial de Registro de Imóveis da Capital");

                                        //Deixa em negrito e sublinhado
                                        WordTextStyleHelper.Bold(doc, WordPageHelper.GetRangeEnd(doc, 47), WordPageHelper.GetRangeEnd(doc), true);
                                        WordTextStyleHelper.Underline(doc, WordPageHelper.GetRangeEnd(doc, 47), WordPageHelper.GetRangeEnd(doc), WdUnderline.wdUnderlineSingle);

                                        doc.Paragraphs.Last.Alignment = WdParagraphAlignment.wdAlignParagraphJustify;
                                        WordParagraphHelper.InserirParagrafo(doc, new string(' ', 30) + "GERAL", true);

                                        //Tira o negrito e sublinhado
                                        WordTextStyleHelper.Bold(doc, WordPageHelper.GetRangeEnd(doc, 36), WordPageHelper.GetRangeEnd(doc), false);
                                        WordTextStyleHelper.Underline(doc, WordPageHelper.GetRangeEnd(doc, 36), WordPageHelper.GetRangeEnd(doc), WdUnderline.wdUnderlineNone);

                                        doc.Paragraphs.SpaceAfter = 0;

                                    }
                                    else
                                    {
                                        WordParagraphHelper.InserirParagrafoEmBranco(doc);
                                    }
                                    //INSERINDO TEXTO PADRÃO
                                    doc.Application.Selection.GoTo(WdGoToItem.wdGoToPage, WdGoToDirection.wdGoToNext, WdMovementType.wdExtend);

                                    var shapesTask = WordShapeHelper.GetShapes(doc);
                                    //Insere os shapes de matricula e ficha
                                    WordShapeHelper.InserirShapeMatriculaFicha(shapesTask);

                                    //Ilusão de margem
                                    WordShapeHelper.InserirShapeMargem(shapesTask, 84, 100);

                                    WordShapeHelper.InserirTextoMatriculaFicha(doc);

                                    doc.Paragraphs.Last.Alignment = WdParagraphAlignment.wdAlignParagraphJustify;

                                    var rangeFinal = WordPageHelper.GetRangeEnd(doc);
                                    WordShapeHelper.AjustarBackGroundShapeMatriculaFicha(doc, rangeFinal);

                                    //Numero da matricula e ficha
                                    if (WordPageHelper.IsVerso(WordPageHelper.GetNumeroPagina(doc)))
                                    {
                                        WordParagraphHelper.InserirParagrafo(doc, new string(' ', 5) + GetNumeroMatricula(modelo) + new string(' ', 30 + (15 - GetNumeroMatricula(modelo).ToString().Length)) +
                                            $"{WordPageHelper.GetNumeroFicha(doc)}" + new string(' ', 5 - WordPageHelper.GetNumeroFicha(doc).ToString().Length),
                                            true);

                                    }
                                    //numero da matricula,ficha e data.
                                    else
                                    {
                                        WordParagraphHelper.InserirParagrafo(doc, new string(' ', 5) + GetNumeroMatricula(modelo) + new string(' ', 30 + (15 - GetNumeroMatricula(modelo).ToString().Length)) +
                                            WordPageHelper.GetNumeroFicha(doc) + new string(' ', 30 + (5 - WordPageHelper.GetNumeroFicha(doc).ToString().Length)) +
                                            "São Paulo," + new string(' ', 4) +
                                            GetDataPorExtenso(),
                                            true);

                                        WordTextStyleHelper.SetHighlightColor(doc, rangeFinal, WdColorIndex.wdNoHighlight);
                                        WordTextStyleHelper.Bold(doc, WordPageHelper.GetRangeEnd(doc, 24), WordPageHelper.GetRangeEnd(doc));
                                    }

                                    doc.Application.ActiveDocument.Range(rangeFinal).HighlightColorIndex = WdColorIndex.wdNoHighlight;
                                    if (WordPageHelper.IsVerso(WordPageHelper.GetNumeroPagina(doc)))
                                    {
                                        WordParagraphHelper.InserirParagrafo(doc, new string(' ', 59) + "verso", true);
                                        WordTextStyleHelper.SetHighlightColor(doc, WordPageHelper.GetRangeEnd(doc, 7), WordPageHelper.GetRangeEnd(doc),
                                            WdColorIndex.wdWhite);

                                    }
                                    else
                                    {
                                        WordParagraphHelper.InserirParagrafoEmBranco(doc);
                                    }
                                    WordParagraphHelper.InserirParagrafoEmBranco(doc);
                                    doc.Paragraphs.Last.Range.Bold = 0;
                                    WordParagraphHelper.InserirParagrafoEmBranco(doc);
                                    if (!WordPageHelper.IsVerso(WordPageHelper.GetNumeroPagina(doc)) && WordPageHelper.GetNumeroFicha(doc) > 1)
                                    {
                                        WordParagraphHelper.InserirParagrafoEmRange(doc, new string(' ', 4) + $"( CONTINUAÇÃO DA FICHA N°. {WordPageHelper.GetNumeroFicha(doc) - 1} )");
                                        doc.Paragraphs.Last.Range.Bold = 1;
                                        WordParagraphHelper.InserirParagrafoEmBranco(doc);
                                        doc.Paragraphs.Last.Range.Bold = 0;
                                    }
                                    posicao = app.ActiveDocument.Content.End - 1;
                                    numeroPagina = WordPageHelper.GetNumeroPagina(doc);

                                    // Reescreve o texto final que foi sobreescrito pelo rodapé na quebra de página
                                    posicao = WordParagraphHelper.ReescreverTextoDeFinalDePagina(doc, posicao, textoParaSalvar);
                                    i--;
                                }
                                else
                                {
                                    app.ActiveDocument.Range(posicao++).Text = modelo.Ato[i].ToString();
                                }
                            }

                            app.ActiveDocument.Paragraphs.Add().Range.InlineShapes.AddHorizontalLineStandard();
                            if (WordPageHelper.GetNumeroPagina(doc) > numeroPagina)
                            {
                                //AJUSTAR PARAGRAFO
                                WordShapeHelper.DeleteLastShape(doc);
                                app.ActiveDocument.Paragraphs.Last.Range.Delete();
                            }
                            else
                            {
                                WordParagraphHelper.InserirParagrafoEmBranco(doc);
                                if (WordPageHelper.GetNumeroPagina(doc) > numeroPagina)
                                {
                                    WordShapeHelper.DeleteLastShape(doc);
                                }
                                else
                                {
                                    app.ActiveDocument.Paragraphs.Last.Range.Delete();
                                }

                            }


                            //using (DocX docX = DocX.Load(fileStream))
                            //{
                            //    //deixa texto transparente
                            //    SetTextColorTransparent(docX);

                            //    //Cadastro do texto e registro do arquivo
                            //    docX.InsertParagraph().Append(modelo.Ato).SpacingAfter(5);

                            //    //espaço de segurança
                            //    docX.InsertParagraph();
                            //    docX.InsertParagraph().InsertHorizontalLine();

                            //    fileStream.Close();
                            //    docX.Save();
                            //    docX.SaveAs(filePath);
                            //}
                        }

                        // Gravar no banco o array de bytes
                        //var arrayBytesNovo = System.IO.File.ReadAllBytes(filePath);
                        // Pegar a ultima "versão" do ato e somar

                        // Gravar o ato e buscar o selo e gravar o selo


                    }
                }
                modelo.MatriculasViewModel = getMatriculaViewModel();
                modelo.ModelosSimplificadoViewModel = getModeloSimplificadoViewModel();
                ViewBag.sucesso = "Ato cadastrado com sucesso!";

                return View(nameof(Index), modelo);
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
                return new HttpStatusCodeResult(HttpStatusCode.InternalServerError);
                throw;
            }
        }








        /// <summary>
        /// Retorna o numero de matricula do modelo
        /// </summary>
        /// <param name="modelo">Modelo</param>
        /// <returns>N° da Matricula</returns>
        private static long GetNumeroMatricula(MatriculaAtoViewModel modelo)
        {
            return modelo.MatriculaID;
        }

        /// <summary>
        /// Retorna a data de hoje por extenso
        /// </summary>
        /// <returns>DD de MM de YYYY</returns>
        private static string GetDataPorExtenso()
        {
            var date = DateTime.Now.ToLongDateString().Split(',');

            return date[1].Trim();
        }

        /// <summary>
        /// Deixa o texto transparente do arquivo
        /// </summary>
        /// <param name="docX">Representa o documento</param>
        private static void SetTextColorTransparent(DocX docX)
        {
            var texto = docX.Paragraphs;
            foreach (var item in texto)
            {
                item.Color(Color.Transparent);
                item.UnderlineStyle(UnderlineStyle.none);
            }
        }


        /// <summary>
        /// Função que retorna o ato de html para string
        /// </summary>
        /// <param name="ato">ATO como HTML</param>
        /// <returns>ato como string</returns>
        private static string ConvertHtmlToString(string ato)
        {
            var documentoHtml = new HtmlDocument();
            StringBuilder st = new StringBuilder();

            documentoHtml.LoadHtml(ato);

            foreach (var linhaHtml in documentoHtml.DocumentNode.ChildNodes)
            {

                st.Append(linhaHtml.InnerHtml);
                st.AppendLine();

            }

            return st.ToString();
        }

        /// <summary>
        /// Função que monta uma string HTML para mostrar na tela exatamente 
        /// oque esta escrito no documento
        /// </summary>
        /// <returns>string HTML</returns>
        public string UsaModeloParaAto([Bind(Include = "ModeloNome")]string ModeloNome)
        {
            StringBuilder textoFormatado = new StringBuilder();

            string filePath = Server.MapPath($"~/App_Data/Arquivos/{ModeloNome}.docx");
            try
            {
                using (FileStream fileStream = new FileStream(filePath, FileMode.Open, FileAccess.Read))
                {
                    //Carrega o Modelo
                    using (DocX docX = DocX.Load(fileStream))
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
                                                return "Arquivo com campos corrompidos, verifique o modelo";
                                            }
                                        }
                                        //Buscar dado da pessoa aqui
                                        resultadoQuery = "teste query";

                                        //atualiza o texto formatado
                                        textoParagrafo.Append(resultadoQuery);
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
                return textoFormatado.ToString();
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
                throw new Exception("Ocorreu algum erro ao utilizar o modelo");
            }
        }




        public PartialViewResult BuscaMatricula()
        {
            return PartialView();
        }
        public PartialViewResult BuscaModelo(int? idMatricula)
        {
            if (idMatricula.HasValue) { return PartialView(); }
            return PartialView(nameof(BuscaMatricula));
        }

    }

    public static class WordParagraphHelper
    {
        /// <summary>
        /// Imprime uma linha em branco
        /// </summary>
        /// <param name="doc">Documento ativo</param>
        public static void InserirParagrafoEmBranco(Document doc)
        {
            doc.Paragraphs.Add();
        }

        /// <summary>
        /// Insere um novo paragrafo com texto em range
        /// </summary>
        /// <param name="doc">Documento Ativo</param>
        /// <param name="text">Texto para inserção</param>
        public static void InserirParagrafoEmRange(Document doc, string text)
        {
            if (!String.IsNullOrEmpty(text))
                doc.Paragraphs.Add().Range.Text = text;
        }
        /// <summary>
        /// Adiciona o texto a partir do range
        /// </summary>
        /// <param name="doc">Documento Ativo</param>
        /// <param name="posicaoInicial">Posição Inicial</param>
        /// <param name="text">Texto</param>
        public static void InserirTextoEmRange(Document doc, int posicaoInicial, string text)
        {
            if (!String.IsNullOrEmpty(text))

                doc.Application.ActiveDocument.Range(posicaoInicial).Text = text;
        }
        /// <summary>
        /// Adiciona o texto a partir do range
        /// </summary>
        /// <param name="doc">Documento Ativo</param>
        /// <param name="posicaoInicial">Posição Inicial</param>
        /// <param name="posicaoFinal">Posição Final</param>
        /// <param name="text">Texto</param>
        public static void InserirTextoEmRange(Document doc, int posicaoInicial, int posicaoFinal, string text)
        {
            if (!String.IsNullOrEmpty(text) && posicaoFinal >= posicaoInicial)

                doc.Application.ActiveDocument.Range(posicaoInicial, posicaoFinal).Text = text;
        }

        /// <summary>
        /// Insere um paragrafo, na linha atual ou na próxima
        /// </summary>
        /// <param name="doc">Documento ativo</param>
        /// <param name="text">Texto do parágrafo</param>
        /// <param name="insertAfter">Flag que identifica se o paragrafo é uma continuação</param>
        public static void InserirParagrafo(Document doc, string text, bool insertAfter)
        {
            if (!String.IsNullOrEmpty(text.Trim()) && doc != null)
            {
                if (!insertAfter)
                {
                    doc.Paragraphs.Add().Range.Text = text;
                    return;
                }
                doc.Paragraphs.Add().Range.InsertAfter(text);
            }
        }
        /// <summary>
        /// Método que imprime o rodapé de acordo com o numero da pagina
        /// </summary>
        /// <param name="doc">Documento Ativo</param>
        public static void InserirRodape(Document doc)
        {
            if (WordPageHelper.IsVerso(WordPageHelper.GetNumeroPagina(doc)))
                WordParagraphHelper.InserirParagrafo(doc, $"(CONTINUA NA FICHA N°. { WordPageHelper.GetNumeroFicha(doc) + 1})", true);

            else
                WordParagraphHelper.InserirParagrafo(doc, "(CONTINUA NO VERSO)", true);

            doc.Application.ActiveDocument.Paragraphs.Last.Alignment = WdParagraphAlignment.wdAlignParagraphRight;
            WordParagraphHelper.InserirParagrafoEmBranco(doc);
        }
        /// <summary>
        /// Reescreve o texto final de página que foi perdido pelo rodapé
        /// </summary>
        /// <param name="doc">Documento Ativo</param>
        /// <param name="posicaoCursor">Posição do cursor</param>
        /// <param name="textoParaSalvar">Texto para salvar</param>
        /// <returns>Posição do cursor após a inserção</returns>
        public static int ReescreverTextoDeFinalDePagina(Document doc, int posicaoCursor, string textoParaSalvar)
        {
            for (int j = 0; j < textoParaSalvar.Length; j++)
            {
                doc.Application.ActiveDocument.Range(posicaoCursor++).Text = textoParaSalvar[j].ToString();
            }
            /*Reposiciona o cursor no final do arquivo, pois foram reescrita as ultimas linhas
            devido a inserção de rodapé dinâmica.*/
            posicaoCursor = doc.Application.ActiveDocument.Content.End - 3;
            return posicaoCursor;
        }

    }
    public static class WordPageHelper
    {
        /// <summary>
        /// Pega o numero da página do documento ativo
        /// </summary>
        /// <param name="doc">Documento ativo</param>
        /// <returns>Numero da pagina (INT) </returns>
        public static int GetNumeroPagina(Document doc)
        {
            return doc.Application.Selection.Information[WdInformation.wdNumberOfPagesInDocument];
        }

        /// <summary>
        /// Pega o range final do documento ativo
        /// </summary>
        /// <param name="doc">Documento ativo</param>
        /// <param name="backOFF">Posições que recuam do range final do documento (recuar N posições)</param>
        /// <returns>Posição do range final do documento</returns>
        public static int GetRangeEnd(Document doc, int backOFF = 0)
        {
            return doc.Application.ActiveDocument.Range().End - backOFF;
        }
        /// <summary>
        /// Pega a posição final do content do Documento
        /// </summary>
        /// <param name="doc">Documento ativo</param>
        /// <returns>Posição final do content</returns>
        public static int GetContentEnd(Document doc, int backOFF = 0)
        {
            return doc.Application.ActiveDocument.Content.End - backOFF;
        }

        /// <summary>
        /// Verifica se é o verso da ficha de acordo com o numero da página
        /// </summary>
        /// <param name="numeroPagina">Numero da pagina</param>
        /// <returns>Se é o verso da pagina (TRUE) se a frente (FALSE)</returns>
        public static bool IsVerso(int numeroPagina)
        {
            return numeroPagina % 2 == 0;
        }
        /// <summary>
        /// Pega o numero da ficha de acordo com o numero da pagina
        /// </summary>
        /// <param name="doc">Documento ativo</param>
        /// <returns>Retorna o numero da ficha para a pagina corrente</returns>
        public static int GetNumeroFicha(Document doc)
        {
            return WordPageHelper.GetNumeroPagina(doc) % 2 + WordPageHelper.GetNumeroPagina(doc) / 2;
        }
        /// <summary>
        /// Desloca uma quantidade de centimetros em relação ao SHAPE
        /// </summary>
        /// <param name="centimetros">Centimetros</param>
        /// <param name="doc">Documento Ativo</param>
        public static void DeslocarCentimetros(float centimetros, Document doc)
        {
            //Cada paragrafo desloca aproximadamente 0.6 centrimetros
            int quantidadeDeEspacos = (int)Math.Ceiling(centimetros / 0.6);
            while (quantidadeDeEspacos > 0)
                WordParagraphHelper.InserirParagrafoEmBranco(doc);
        }
    }
    public static class WordTextStyleHelper
    {
        /// <summary>
        /// Função responsável por controlar o negrito
        /// </summary>
        /// <param name="doc">Documento Ativo</param>
        /// <param name="posicaoInicial">Posição Inicial</param>
        /// <param name="posicaoFinal">Posição Final</param>
        /// <param name="negrito"> TRUE - NEGRITO; FALSE - TIRAR NEGRITO </param>
        public static void Bold(Document doc, int posicaoInicial, int posicaoFinal, bool negrito = true)
        {
            //Coloca o texto em negrito se negrito = true, se não, tira o negrito
            doc.Application.ActiveDocument.Range(posicaoInicial, posicaoFinal).Bold = negrito ? 1 : 0;
        }

        /// <summary>
        /// Função que configura a cor de fundo das letras a partir da posição inicial até a posição final
        /// </summary>
        /// <param name="doc">Documento ativo</param>
        /// <param name="posicaoInicial">Posição Inicial</param>
        /// <param name="posicaoFinal">Posição Final</param>
        /// <param name="wdColorIndex">Configuração da cor de fundo</param>
        public static void SetHighlightColor(Document doc, int posicaoInicial, int posicaoFinal, WdColorIndex wdColorIndex)
        {
            doc.Application.ActiveDocument.Range(posicaoInicial, posicaoFinal).HighlightColorIndex = wdColorIndex;
        }
        /// <summary>
        /// Função que configura a cor de fundo das letras a partir da posição inicial até o final do documento
        /// </summary>
        /// <param name="doc">Documento ativo</param>
        /// <param name="posicaoInicial">Posição Inicial</param>
        /// <param name="wdColorIndex">Configuração da cor de fundo</param>
        public static void SetHighlightColor(Document doc, int posicaoInicial, WdColorIndex wdColorIndex)
        {
            doc.Application.ActiveDocument.Range(posicaoInicial).HighlightColorIndex = wdColorIndex;

        }
        /// <summary>
        /// Função que controla o sublinhado do texto e configura até o final do documento
        /// </summary>
        /// <param name="doc">Documento ativo</param>
        /// <param name="posicaoInicial">Posição inicial</param>
        /// <param name="wdUnderline">Tipo de configuração para o sublinhado</param>
        public static void Underline(Document doc, int posicaoInicial, WdUnderline wdUnderline)
        {
            doc.Application.ActiveDocument.Range(posicaoInicial).Font.Underline = wdUnderline;
        }
        /// <summary>
        /// Função que controla o sublinhado do texto de uma posição inicial até a final
        /// </summary>
        /// <param name="doc">Documento Ativo</param>
        /// <param name="posicaoInicial">Posição Inicial</param>
        /// <param name="posicaoFinal">Posição Final</param>
        /// <param name="wdUnderline">Configuração para o sublinhado</param>
        public static void Underline(Document doc, int posicaoInicial, int posicaoFinal, WdUnderline wdUnderline)
        {
            doc.Application.ActiveDocument.Range(posicaoInicial, posicaoFinal).Font.Underline = wdUnderline;
        }
    }
    public static class WordShapeHelper
    {
        /// <summary>
        /// Função que ajusta a cor de fundo do texto 'Matricula' e 'ficha' para dar ilusão de legend do shape
        /// Utilizar somente após de inserir o texto da ficha
        /// </summary>
        /// <param name="doc">Documento Ativo</param>
        /// <param name="rangeFinal">Posição Final do texto 'ficha'</param>
        public static void AjustarBackGroundShapeMatriculaFicha(Document doc, int rangeFinal)
        {
            //Fundo Branco
            WordTextStyleHelper.SetHighlightColor(doc, rangeFinal - 7, rangeFinal, WdColorIndex.wdWhite);
            WordTextStyleHelper.SetHighlightColor(doc, rangeFinal - 46, rangeFinal - 35, WdColorIndex.wdWhite);
            //Sem cor de fundo
            WordTextStyleHelper.SetHighlightColor(doc, rangeFinal - 35, rangeFinal - 7, WdColorIndex.wdNoHighlight);
            WordTextStyleHelper.SetHighlightColor(doc, rangeFinal - 54, rangeFinal - 46, WdColorIndex.wdNoHighlight);
        }
        /// <summary>
        /// Inserir o texto de matricula e ficha dos shapes
        /// </summary>
        /// <param name="doc">Documento Ativo</param>
        public static void InserirTextoMatriculaFicha(Document doc)
        {
            WordParagraphHelper.InserirParagrafoEmRange(doc, new string(' ', 5) + "matrícula" + new string(' ', 30) + "ficha");
        }
        /// <summary>
        /// Get Shapes Object
        /// </summary>
        /// <param name="doc">Documento ativo</param>
        /// <returns>Shapes Object</returns>
        public static Shapes GetShapes(Document doc)
        {
            return doc.Paragraphs.Add().Application.ActiveDocument.Shapes;
        }
        /// <summary>
        /// Simula uma margem
        /// </summary>
        /// <param name="shapes">Shapes Obj</param>
        /// <param name="left">Padding Left</param>
        /// <param name="top">Padding Top</param>
        public static void InserirShapeMargem(Shapes shapes, float left, float top)
        {

            shapes.AddShape((int)MsoAutoShapeType.msoShapeRectangle, 50, 50, 428, 705)
                                            .ZOrder(MsoZOrderCmd.msoSendBehindText);
            int index = shapes.Count;
            //Configuração de estilo de shape
            SetBackgroundColor(shapes, index, XlRgbColor.xlWhite);
            SetBorderColor(shapes, index, XlRgbColor.xlBlack);
            SetRelativePosition(shapes, index, true, true);
            shapes[index].Left = left;
            shapes[index].Top = top;
        }
        /// <summary>
        /// Função que adiciona um shape para matricula e outro para ficha
        /// </summary>
        /// <param name="shapes">Objecto de Shapes</param>
        public static void InserirShapeMatriculaFicha(Shapes shapes)
        {
            int index;

            #region | Shape N° Matricula |
            shapes.AddShape((int)MsoAutoShapeType.msoShapeRoundedRectangle, 50, 50, 80, 30)
                                                        .ZOrder(MsoZOrderCmd.msoSendBehindText);
            index = shapes.Count;

            SetBackgroundColor(shapes, index, XlRgbColor.xlWhite);
            SetBorderColor(shapes, index, XlRgbColor.xlBlack);
            SetRelativePosition(shapes, index, true, true);

            shapes[index].Left = 85;
            shapes[index].Top = 57;
            #endregion
            #region | Shape Ficha |
            shapes.AddShape((int)MsoAutoShapeType.msoShapeRoundedRectangle, 50, 50, 65, 30)
               .ZOrder(MsoZOrderCmd.msoSendBehindText);
            index = shapes.Count;

            SetBackgroundColor(shapes, index, XlRgbColor.xlWhite);
            SetBorderColor(shapes, index, XlRgbColor.xlBlack);
            SetRelativePosition(shapes, index, true, true);
            shapes[index].Left = 200;
            shapes[index].Top = 57;
            #endregion

        }
        /// <summary>
        /// Função que pinta a cor de fundo do shape
        /// </summary>
        /// <param name="shapes">Shapes Obj</param>
        /// <param name="index">Indice do shape no vetor</param>
        /// <param name="color">Cor desejada (XlRgbColor) </param>
        public static void SetBackgroundColor(Shapes shapes, int index, XlRgbColor color)
        {
            shapes[index].Fill.ForeColor.RGB = (int)color;
        }
        /// <summary>
        /// Preenche a cor da borda do shape
        /// </summary>
        /// <param name="shapes">Shapes Obj</param>
        /// <param name="index">Indice do shape no vetor</param>
        /// <param name="color">Cor desejada (XlRgbColor) </param>
        public static void SetBorderColor(Shapes shapes, int index, XlRgbColor color)
        {
            shapes[index].Line.ForeColor.RGB = (int)color;
        }
        /// <summary>
        /// Função que configura a posição relativa do shape
        /// </summary>
        /// <param name="shapes">Shapes</param>
        /// <param name="index">Indice do shape no vetor</param>
        /// <param name="RelativeVerticalPosition">Set RelativeVerticalPosition (true or false)</param>
        /// <param name="RelativeHorizontalPosition">Set RelativeHorizontalPosition (true or false)</param>
        public static void SetRelativePosition(Shapes shapes, int index, bool RelativeVerticalPosition = true, bool RelativeHorizontalPosition = true)
        {
            if (RelativeHorizontalPosition && RelativeVerticalPosition)
            {
                shapes[index].RelativeHorizontalPosition = WdRelativeHorizontalPosition.wdRelativeHorizontalPositionPage;
                shapes[index].RelativeVerticalPosition = WdRelativeVerticalPosition.wdRelativeVerticalPositionPage;
                return;
            }
            else if (RelativeVerticalPosition)
            {
                shapes[index].RelativeVerticalPosition = WdRelativeVerticalPosition.wdRelativeVerticalPositionPage;
                return;
            }
            else if (RelativeHorizontalPosition)
            {
                shapes[index].RelativeHorizontalPosition = WdRelativeHorizontalPosition.wdRelativeHorizontalPositionPage;
                return;
            }
        }
        /// <summary>
        /// Deleta o ultimo shape
        /// </summary>
        /// <param name="doc">Documento ativo</param>
        public static void DeleteLastShape(Document doc)
        {
            doc.Application.ActiveDocument.InlineShapes[GetShapesCount(doc)].Delete();
        }
        /// <summary>
        /// Retorna a quantidade de shapes no documento
        /// </summary>
        /// <param name="doc">Documento ativo</param>
        /// <returns>Quantidade de shapes</returns>
        public static int GetShapesCount(Document doc)
        {
            return doc.Application.ActiveDocument.InlineShapes.Count;
        }
    }
}
