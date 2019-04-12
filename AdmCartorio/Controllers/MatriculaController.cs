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
                            var app = new Application();
                            app.Visible = true;

                            //var doc = app.Documents.Open(Server.MapPath($"~/App_Data/Arquivos/{modelo.MatriculaID}_.docx"));
                            var doc = app.Documents.Add();
                            doc.Paragraphs.Format.Alignment = WdParagraphAlignment.wdAlignParagraphJustify;

                            //teste para impressao;
                            //app.ActiveDocument.Paragraphs.Add().Range.Text = "1111111111111";
                            //app.ActiveDocument.Paragraphs.Add();
                            //app.ActiveDocument.Paragraphs.Add().Range.Text="2222222222222";
                            //app.ActiveDocument.Paragraphs.Add().Range.Text = "2222222222222";
                            //app.ActiveDocument.Paragraphs.Add();
                            //app.ActiveDocument.Paragraphs.Add();
                            //app.ActiveDocument.Paragraphs.Add().Range.Text = "3333333333333";
                            //app.ActiveDocument.Paragraphs.Add().Range.Text = "3333333333333";
                            //app.ActiveDocument.Paragraphs.Add().Range.Text = "3333333333333";


                            var numeroPagina = doc.Application.Selection.Information[WdInformation.wdNumberOfPagesInDocument];
                            int posicao;

                            //Configuração de borda
                            doc.PageSetup.TopMargin = 20;
                            doc.PageSetup.BottomMargin = 40;
                            doc.Application.ActiveDocument.AutoHyphenation = true;

                            //var numeroPagina = doc.PageSetup.LineNumbering;
                            doc.Paragraphs.Add().Range.Text = "LIVRO N.° 2 - REGISTRO" + new string(' ', 52) + "16.° CARTÓRIO DE REGISTRO DE IMÓVEIS";

                            

                            //NEGRITO E SUBLINHADO
                            var auxLenght = ("LIVRO N.° 2 - REGISTRO".ToString().Length + 52);
                            var auxLenght2 = auxLenght + ("16.° CARTÓRIO DE REGISTRO DE IMÓVEIS".ToString().Length);
                            doc.Application.ActiveDocument.Range(auxLenght, auxLenght2).Bold = 1;
                            doc.Application.ActiveDocument.Range(auxLenght, auxLenght2 + 1).Font.Underline = WdUnderline.wdUnderlineSingle;
                            doc.Paragraphs.Add().Range.InsertAfter(new string(' ', 30) + "GERAL" + new string(' ', 79) + "de São Paulo");
                            //NEGRITO E SUBLINHADO
                            doc.Application.ActiveDocument.Range(auxLenght2 + 1).Font.Underline = WdUnderline.wdUnderlineNone;
                            auxLenght = auxLenght2 + 30 + "GERAL".ToString().Length + 79;
                            auxLenght2 = auxLenght + "de São Paulo".ToString().Length;
                            doc.Application.ActiveDocument.Range(auxLenght, auxLenght2 + 1).Bold = 1;

                            doc.Paragraphs.SpaceAfter = 0;

                            doc.Paragraphs.Add().Range.InsertAfter(new string(' ', 5) + "matrícula" + new string(' ', 30) + "ficha");


                            //FUNDO BRANCO PARA ILUSAO DE LEGEND DO CAMPO
                            doc.Application.ActiveDocument.Range(auxLenght2 + 1).Font.Underline = WdUnderline.wdUnderlineNone;
                            auxLenght = auxLenght2 + 5;
                            auxLenght2 = auxLenght + "matrícula".ToString().Length;
                            doc.Application.ActiveDocument.Range(auxLenght, auxLenght2 + 3).HighlightColorIndex = WdColorIndex.wdWhite;
                            auxLenght = auxLenght2 + 30;
                            auxLenght2 = auxLenght + "ficha".ToString().Length;
                            doc.Application.ActiveDocument.Range(auxLenght, auxLenght2 + 3).HighlightColorIndex = WdColorIndex.wdWhite;

                            //Matricula, ficha, local e data
                            doc.Paragraphs.Add().Range.InsertAfter(new string(' ', 5) +
                                GetNumeroMatricula(modelo) + new string(' ', 30 + (15 - GetNumeroMatricula(modelo).ToString().Length)) +
                                $"{GetNumeroFicha(doc)}" + new string(' ', 36 + (5 - GetNumeroFicha(doc).ToString().Length)) +
                                "São Paulo," + new string(' ', 4) +
                                GetDataPorExtenso());

                            doc.Application.ActiveDocument.Range(auxLenght2 + 3).HighlightColorIndex = WdColorIndex.wdNoHighlight;
                            auxLenght = auxLenght2 + 5 + "matrícula".ToString().Length + 30 + "ficha".ToString().Length + 33 + "São Paulo,".ToString().Length + 4;
                            auxLenght2 = auxLenght + DateTime.Today.ToString().Length;
                            doc.Application.ActiveDocument.Range(auxLenght + 9, auxLenght2 + 12).Bold = 1;

                            #region | Configurando Shape | 
                            var shapes = doc.Paragraphs.Add().Application.ActiveDocument.Shapes;
                            //Insere os shapes de matricula e ficha
                            InserirShapeMatriculaFicha(shapes);
                            //Insere a ilusão de margem
                            InserirShapeMargem(shapes);
                            #endregion

                            doc.Paragraphs.Add().Range.InsertParagraphAfter();


                            System.Threading.Tasks.Task.Run(() =>
                            {
                                posicao = app.ActiveDocument.Content.End - 1;
                                while (true)
                                {
                                    //Imprimir modelo R-{numeroAtoSequencia}/{MATRICULA} - 
                                    app.ActiveDocument.Range(posicao).Text = $"R-12/{modelo.MatriculaID} - ";
                                    posicao = app.ActiveDocument.Content.End - 1;
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
                                            InserirParagrafoEmBranco(doc);
                                            doc.Application.Selection.EndOf(WdUnits.wdParagraph, WdMovementType.wdExtend);
                                            textoParaSalvar += app.Selection.Text;
                                            app.Selection.Delete();
                                            InserirParagrafoEmBranco(doc);

                                            InserirRodape(doc);
                                            if (!IsVerso(GetNumeroPagina(doc)))
                                            {
                                                doc.Paragraphs.Add().Range.Text = "LIVRO N.° 2 - REGISTRO" + new string(' ', 45) + "16.° Oficial de Registro de Imóveis da Capital";
                                                //Deixa em negrito e sublinhado
                                                doc.Application.ActiveDocument.Range(doc.Application.ActiveDocument.Range().End - 47, doc.Application.ActiveDocument.Range().End).Bold = 1;
                                                doc.Application.ActiveDocument.Range(doc.Application.ActiveDocument.Range().End - 47, doc.Application.ActiveDocument.Range().End).Font.Underline = WdUnderline.wdUnderlineSingle;
                                               
                                                doc.Paragraphs.Last.Alignment = WdParagraphAlignment.wdAlignParagraphJustify;
                                                doc.Paragraphs.Add().Range.InsertAfter(new string(' ', 30) + "GERAL");
                                                doc.Application.ActiveDocument.Range(doc.Application.ActiveDocument.Range().End-36,doc.Application.ActiveDocument.Range().End).Bold = 0;
                                                doc.Application.ActiveDocument.Range(doc.Application.ActiveDocument.Range().End-36,doc.Application.ActiveDocument.Range().End).Font.Underline = WdUnderline.wdUnderlineNone;

                                                doc.Paragraphs.SpaceAfter = 0;
                                                //doc.Paragraphs.Last.Alignment = WdParagraphAlignment.wdAlignParagraphJustify;

                                            }
                                            else
                                            {
                                                InserirParagrafoEmBranco(doc);
                                            }
                                            //INSERINDO TEXTO PADRÃO
                                            doc.Application.Selection.GoTo(WdGoToItem.wdGoToPage, WdGoToDirection.wdGoToNext, 1);

                                            var shapesTask = doc.Paragraphs.Add().Application.ActiveDocument.Shapes;
                                            //Insere os shapes de matricula e ficha
                                            InserirShapeMatriculaFicha(shapesTask);
                                            //Ilusão de margem
                                            InserirShapeMargem(shapes);

                                            InserirTextoMatriculaFicha(doc);

                                            doc.Paragraphs.Last.Alignment = WdParagraphAlignment.wdAlignParagraphJustify;

                                            var rangeFinal = doc.Application.ActiveDocument.Range().End;
                                            AjustarBackGroundShapeMatriculaFicha(doc, rangeFinal);

                                            //Numero da matricula e ficha
                                            if (IsVerso(GetNumeroPagina(doc)))
                                            {
                                                doc.Paragraphs.Add().Range.InsertAfter(new string(' ', 5) +
                                                    GetNumeroMatricula(modelo) + new string(' ', 30 + (15 - GetNumeroMatricula(modelo).ToString().Length)) +
                                                    $"{GetNumeroFicha(doc)}" + new string(' ', 5 - GetNumeroFicha(doc).ToString().Length));

                                            }
                                            //numero da matricula,ficha e data.
                                            else
                                            {
                                                doc.Paragraphs.Add().Range.InsertAfter(new string(' ', 5) +
                                                    GetNumeroMatricula(modelo) + new string(' ', 30 + (15 - GetNumeroMatricula(modelo).ToString().Length)) +
                                                    GetNumeroFicha(doc) + new string(' ', 30 + (5 - GetNumeroFicha(doc).ToString().Length)) +
                                                    "São Paulo," + new string(' ', 4) +
                                                    GetDataPorExtenso());
                                                doc.Application.ActiveDocument.Range(rangeFinal).HighlightColorIndex = WdColorIndex.wdNoHighlight;
                                                doc.Application.ActiveDocument.Range(doc.Application.ActiveDocument.Range().End - 24, doc.Application.ActiveDocument.Range().End).Bold = 1;
                                            }

                                            doc.Application.ActiveDocument.Range(rangeFinal).HighlightColorIndex = WdColorIndex.wdNoHighlight;
                                            if (IsVerso(GetNumeroPagina(doc)))
                                            {
                                                doc.Paragraphs.Add().Range.InsertAfter(new string(' ', 59) + "verso");

                                                rangeFinal = doc.Application.ActiveDocument.Range().End;
                                                doc.Application.ActiveDocument.Range(rangeFinal - 7, rangeFinal).HighlightColorIndex = WdColorIndex.wdWhite;

                                            }
                                            else
                                            {
                                                InserirParagrafoEmBranco(doc);
                                            }
                                            InserirParagrafoEmBranco(doc);
                                            doc.Paragraphs.Last.Range.Bold = 0;
                                            InserirParagrafoEmBranco(doc);
                                            if (!IsVerso(GetNumeroPagina(doc)) && GetNumeroFicha(doc) > 1)
                                            {
                                                doc.Paragraphs.Add().Range.Text = new string(' ',4) + $"( CONTINUAÇÃO DA FICHA N°. {GetNumeroFicha(doc) - 1} )";
                                                doc.Paragraphs.Last.Range.Bold = 1;
                                                InserirParagrafoEmBranco(doc);
                                                doc.Paragraphs.Last.Range.Bold = 0;
                                            }
                                            posicao = app.ActiveDocument.Content.End - 1;
                                            numeroPagina = doc.Application.Selection.Information[WdInformation.wdNumberOfPagesInDocument];

                                            for (int j = 0; j < textoParaSalvar.Length; j++)
                                            {
                                                app.ActiveDocument.Range(posicao++).Text = textoParaSalvar[j].ToString();
                                            }
                                            posicao = app.ActiveDocument.Content.End - 3;
                                            i--;
                                        }
                                        else
                                        {
                                            app.ActiveDocument.Range(posicao++).Text = modelo.Ato[i].ToString();
                                        }
                                    }

                                    app.ActiveDocument.Paragraphs.Add().Range.InlineShapes.AddHorizontalLineStandard();
                                    if (doc.Application.Selection.Information[WdInformation.wdNumberOfPagesInDocument] > numeroPagina)
                                    {
                                        //AJUSTAR PARAGRAFO
                                        app.ActiveDocument.InlineShapes[app.ActiveDocument.InlineShapes.Count].Delete();
                                    }
                                    else
                                    {
                                        InserirParagrafoEmBranco(doc);
                                        if (doc.Application.Selection.Information[WdInformation.wdNumberOfPagesInDocument] > numeroPagina)
                                        {
                                            //AJUSTAR PARAGRAFO
                                            app.ActiveDocument.InlineShapes[app.ActiveDocument.InlineShapes.Count].Delete();
                                        }
                                        else
                                        {
                                            app.ActiveDocument.Paragraphs.Last.Range.Delete();
                                        }

                                    }

                                    break;
                                }

                            });





                            System.Threading.Tasks.Task.WaitAll();
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

                            var app = new Application();
                            app.Visible = true;

                            var doc = app.Documents.Open("C:\\Users\\pedro.henrique\\Desktop\\TesteInterrupt.docx");
                            
                            var numeroPagina = doc.Application.Selection.Information[WdInformation.wdNumberOfPagesInDocument];

                            System.Threading.Tasks.Task.Run(() =>
                            {
                                var posicao = app.ActiveDocument.Content.End - 1;
                                while (true)
                                {
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
                                            InserirParagrafoEmBranco(doc);
                                            doc.Application.Selection.EndOf(WdUnits.wdParagraph, WdMovementType.wdExtend);
                                            textoParaSalvar += app.Selection.Text;
                                            app.Selection.Delete();
                                            InserirParagrafoEmBranco(doc);

                                            InserirRodape(doc);
                                            if (!IsVerso(GetNumeroPagina(doc)))
                                            {
                                                doc.Paragraphs.Add().Range.Text = "LIVRO N.° 2 - REGISTRO" + new string(' ', 45) + "16.° Oficial de Registro de Imóveis da Capital";
                                                //Deixa em negrito e sublinhado
                                                doc.Application.ActiveDocument.Range(doc.Application.ActiveDocument.Range().End - 47, doc.Application.ActiveDocument.Range().End).Bold = 1;
                                                doc.Application.ActiveDocument.Range(doc.Application.ActiveDocument.Range().End - 47, doc.Application.ActiveDocument.Range().End).Font.Underline = WdUnderline.wdUnderlineSingle;

                                                doc.Paragraphs.Last.Alignment = WdParagraphAlignment.wdAlignParagraphJustify;
                                                doc.Paragraphs.Add().Range.InsertAfter(new string(' ', 30) + "GERAL");
                                                doc.Application.ActiveDocument.Range(doc.Application.ActiveDocument.Range().End - 36, doc.Application.ActiveDocument.Range().End).Bold = 0;
                                                doc.Application.ActiveDocument.Range(doc.Application.ActiveDocument.Range().End - 36, doc.Application.ActiveDocument.Range().End).Font.Underline = WdUnderline.wdUnderlineNone;

                                                doc.Paragraphs.SpaceAfter = 0;
                                                //doc.Paragraphs.Last.Alignment = WdParagraphAlignment.wdAlignParagraphJustify;

                                            }
                                            else
                                            {
                                                InserirParagrafoEmBranco(doc);
                                            }
                                            //INSERINDO TEXTO PADRÃO
                                            doc.Application.Selection.GoTo(WdGoToItem.wdGoToPage, WdGoToDirection.wdGoToNext, WdMovementType.wdExtend);

                                            var shapesTask = doc.Paragraphs.Add().Application.ActiveDocument.Shapes;
                                            //Insere os shapes de matricula e ficha
                                            InserirShapeMatriculaFicha(shapesTask);
                                            //Shapes shapes = doc.Paragraphs.Add().Application.ActiveDocument.Shapes;

                                            //Ilusão de margem
                                            InserirShapeMargem(shapesTask);

                                            InserirTextoMatriculaFicha(doc);

                                            doc.Paragraphs.Last.Alignment = WdParagraphAlignment.wdAlignParagraphJustify;

                                            var rangeFinal = doc.Application.ActiveDocument.Range().End;
                                            AjustarBackGroundShapeMatriculaFicha(doc, rangeFinal);

                                            //Numero da matricula e ficha
                                            if (IsVerso(GetNumeroPagina(doc)))
                                            {
                                                doc.Paragraphs.Add().Range.InsertAfter(new string(' ', 5) +
                                                    GetNumeroMatricula(modelo) + new string(' ', 30 + (15 - GetNumeroMatricula(modelo).ToString().Length)) +
                                                    $"{GetNumeroFicha(doc)}" + new string(' ', 5 - GetNumeroFicha(doc).ToString().Length));

                                            }
                                            //numero da matricula,ficha e data.
                                            else
                                            {
                                                doc.Paragraphs.Add().Range.InsertAfter(new string(' ', 5) +
                                                    GetNumeroMatricula(modelo) + new string(' ', 30 + (15 - GetNumeroMatricula(modelo).ToString().Length)) +
                                                    GetNumeroFicha(doc) + new string(' ', 30 + (5 - GetNumeroFicha(doc).ToString().Length)) +
                                                    "São Paulo," + new string(' ', 4) +
                                                    GetDataPorExtenso());
                                                doc.Application.ActiveDocument.Range(rangeFinal).HighlightColorIndex = WdColorIndex.wdNoHighlight;
                                                doc.Application.ActiveDocument.Range(doc.Application.ActiveDocument.Range().End - 24, doc.Application.ActiveDocument.Range().End).Bold = 1;
                                            }

                                            doc.Application.ActiveDocument.Range(rangeFinal).HighlightColorIndex = WdColorIndex.wdNoHighlight;
                                            if (IsVerso(GetNumeroPagina(doc)))
                                            {
                                                doc.Paragraphs.Add().Range.InsertAfter(new string(' ', 59) + "verso");

                                                rangeFinal = doc.Application.ActiveDocument.Range().End;
                                                doc.Application.ActiveDocument.Range(rangeFinal - 7, rangeFinal).HighlightColorIndex = WdColorIndex.wdWhite;

                                            }
                                            else
                                            {
                                                InserirParagrafoEmBranco(doc);
                                            }
                                            InserirParagrafoEmBranco(doc);
                                            doc.Paragraphs.Last.Range.Bold = 0;
                                            InserirParagrafoEmBranco(doc);
                                            if (!IsVerso(GetNumeroPagina(doc)) && GetNumeroFicha(doc) > 1)
                                            {
                                                doc.Paragraphs.Add().Range.Text = new string(' ', 4) + $"( CONTINUAÇÃO DA FICHA N°. {GetNumeroFicha(doc) - 1} )";
                                                doc.Paragraphs.Last.Range.Bold = 1;
                                                InserirParagrafoEmBranco(doc);
                                                doc.Paragraphs.Last.Range.Bold = 0;
                                            }
                                            posicao = app.ActiveDocument.Content.End - 1;
                                            numeroPagina = doc.Application.Selection.Information[WdInformation.wdNumberOfPagesInDocument];




                                            for (int j = 0; j < textoParaSalvar.Length; j++)
                                            {
                                                app.ActiveDocument.Range(posicao++).Text = textoParaSalvar[j].ToString();
                                            }
                                            posicao = app.ActiveDocument.Content.End - 3;
                                            i--;
                                        }
                                        else
                                        {
                                            app.ActiveDocument.Range(posicao++).Text = modelo.Ato[i].ToString();
                                        }
                                    }

                                    app.ActiveDocument.Paragraphs.Add().Range.InlineShapes.AddHorizontalLineStandard();
                                    if (doc.Application.Selection.Information[WdInformation.wdNumberOfPagesInDocument] > numeroPagina)
                                    {
                                        //AJUSTAR PARAGRAFO
                                        app.ActiveDocument.InlineShapes[app.ActiveDocument.InlineShapes.Count].Delete();
                                    }
                                    else
                                    {
                                        InserirParagrafoEmBranco(doc);
                                        if (doc.Application.Selection.Information[WdInformation.wdNumberOfPagesInDocument] > numeroPagina)
                                        {
                                            //AJUSTAR PARAGRAFO
                                            app.ActiveDocument.InlineShapes[app.ActiveDocument.InlineShapes.Count].Delete();
                                        }
                                        else
                                        {
                                            app.ActiveDocument.Paragraphs.Last.Range.Delete();
                                        }

                                    }

                                    //HIFENIZAR OS PARAGRAFOS
                                    var hyphenation = doc.Application.ActiveDocument.Paragraphs.Format.Hyphenation;
                                    hyphenation = 1;
                                    break;
                                }

                            });







                            
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
        /// Imprime após uma quantidade de centimetros em relação ao SHAPE
        /// </summary>
        /// <param name="centimetros">Centimetros</param>
        /// <param name="doc">Documento Ativo</param>
        private static void ImprimirAposCentrimetros(float centimetros,Document doc)
        {
            int quantidadeDeEspacos = (int)Math.Ceiling(centimetros / 0.6);
            while (quantidadeDeEspacos > 0)
                doc.Paragraphs.Add();
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
        /// Inserir o texto de matricula e ficha dos shapes
        /// </summary>
        /// <param name="doc">Documento Ativo</param>
        private static void InserirTextoMatriculaFicha(Document doc)
        {
            doc.Paragraphs.Add().Range.Text = new string(' ', 5) + "matrícula" + new string(' ', 30) + "ficha";
        }

        private static void AjustarBackGroundShapeMatriculaFicha(Document doc, int rangeFinal)
        {
            doc.Application.ActiveDocument.Range(rangeFinal - 7, rangeFinal).HighlightColorIndex = WdColorIndex.wdWhite;
            doc.Application.ActiveDocument.Range(rangeFinal - 46, rangeFinal - 35).HighlightColorIndex = WdColorIndex.wdWhite;

            doc.Application.ActiveDocument.Range(rangeFinal - 35, rangeFinal - 7).HighlightColorIndex = WdColorIndex.wdNoHighlight;
            doc.Application.ActiveDocument.Range(rangeFinal - 54, rangeFinal - 46).HighlightColorIndex = WdColorIndex.wdNoHighlight;
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
        /// Pega o numero da página do documento ativo
        /// </summary>
        /// <param name="doc">Documento ativo</param>
        /// <returns>Numero da pagina (INT) </returns>
            private static int GetNumeroPagina(Document doc)
        {
            return doc.Application.Selection.Information[WdInformation.wdNumberOfPagesInDocument];
        }
        /// <summary>
        /// Método que imprime o rodapé de acordo com o numero da pagina
        /// </summary>
        /// <param name="doc">Documento Ativo</param>
        private static void InserirRodape(Document doc)
        {
            if (IsVerso(GetNumeroPagina(doc)))
                ImprimirTextoRodape(doc, $"(CONTINUA NA FICHA N°. {GetNumeroFicha(doc) + 1})");
            else
                ImprimirTextoRodape(doc, "(CONTINUA NO VERSO)");

            doc.Application.ActiveDocument.Paragraphs.Last.Alignment = WdParagraphAlignment.wdAlignParagraphRight;
            InserirParagrafoEmBranco(doc);
            //InserirParagrafoEmBranco(doc);
        }
        /// <summary>
        /// Método que escreve o texto do rodape
        /// </summary>
        /// <param name="doc">Documento Ativo</param>
        /// <param name="textoRodape">Texto do rodapé</param>
        private static void ImprimirTextoRodape(Document doc, string textoRodape)
        {
            doc.Application.ActiveDocument.Paragraphs.Add().Range.InsertAfter(textoRodape);
        }
        /// <summary>
        /// Verifica se é o verso da ficha de acordo com o numero da página
        /// </summary>
        /// <param name="numeroPagina">Numero da pagina</param>
        /// <returns>Se é o verso da pagina (TRUE) se a frente (FALSE)</returns>
        private static bool IsVerso(int numeroPagina)
        {
            return numeroPagina % 2 == 0;
        }
        /// <summary>
        /// Pega o numero da ficha de acordo com o numero da pagina
        /// </summary>
        /// <param name="doc">Documento ativo</param>
        /// <returns>Retorna o numero da ficha para a pagina corrente</returns>
        private static int GetNumeroFicha(Document doc)
        {
            return GetNumeroPagina(doc) % 2 + GetNumeroPagina(doc) / 2;
        }

        /// <summary>
        /// Imprime uma linha em branco
        /// </summary>
        /// <param name="doc">Documento ativo</param>
        private static void InserirParagrafoEmBranco(Document doc)
        {
            doc.Application.ActiveDocument.Paragraphs.Add();
        }

        private static void InserirShapeMatriculaFicha(Microsoft.Office.Interop.Word.Shapes shapes)
        {
            int index;
            //Shape do numero de matricula
            shapes.AddShape((int)MsoAutoShapeType.msoShapeRoundedRectangle, 50, 50, 80, 30)
                                                        .ZOrder(MsoZOrderCmd.msoSendBehindText);
            index = shapes.Count;

            shapes[index].Fill.ForeColor.RGB = (int)XlRgbColor.xlWhite;
            shapes[index].Line.ForeColor.RGB = (int)XlRgbColor.xlBlack;
            shapes[index].RelativeHorizontalPosition = WdRelativeHorizontalPosition.wdRelativeHorizontalPositionPage;
            shapes[index].RelativeVerticalPosition = WdRelativeVerticalPosition.wdRelativeVerticalPositionPage;
            shapes[index].Left = 85;
            shapes[index].Top = 57;

            //Shape da ficha
            shapes.AddShape((int)MsoAutoShapeType.msoShapeRoundedRectangle, 50, 50, 65, 30)
                .ZOrder(MsoZOrderCmd.msoSendBehindText);
            index = shapes.Count;

            shapes[index].Fill.ForeColor.RGB = (int)XlRgbColor.xlWhite;
            shapes[index].Line.ForeColor.RGB = (int)XlRgbColor.xlBlack;
            shapes[index].RelativeHorizontalPosition = WdRelativeHorizontalPosition.wdRelativeHorizontalPositionPage;
            shapes[index].RelativeVerticalPosition = WdRelativeVerticalPosition.wdRelativeVerticalPositionPage;
            shapes[index].Left = 200;
            shapes[index].Top = 57;
        }

        private static void InserirShapeMargem(Microsoft.Office.Interop.Word.Shapes shapes)
        {

            shapes.AddShape((int)MsoAutoShapeType.msoShapeRectangle, 50, 50, 428, 705)
                                            .ZOrder(MsoZOrderCmd.msoSendBehindText);
            int index = shapes.Count;

            shapes[index].Fill.ForeColor.RGB = (int)XlRgbColor.xlWhite;
            shapes[index].Line.ForeColor.RGB = (int)XlRgbColor.xlBlack;
            shapes[index].RelativeHorizontalPosition = WdRelativeHorizontalPosition.wdRelativeHorizontalPositionPage;
            shapes[index].RelativeVerticalPosition = WdRelativeVerticalPosition.wdRelativeVerticalPositionPage;
            shapes[index].Left = 84;
            shapes[index].Top = 100;
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
}
