using Microsoft.Office.Core;
using Microsoft.Office.Interop.Word;
using Shapes = Microsoft.Office.Interop.Word.Shapes;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LibFunctions.Functions
{
    public static class WordShapeHelper
    {
        /// <summary>
        /// Função que ajusta a cor de fundo do texto 'Ato' e 'ficha' para dar ilusão de legend do shape
        /// Utilizar somente após de inserir o texto da ficha
        /// </summary>
        /// <param name="doc">Documento Ativo</param>
        /// <param name="rangeFinal">Posição Final do texto 'ficha'</param>
        [Obsolete("Não precisa ser mais usado, pois não há necessidade de colocar mais Shape")]
        public static void AjustarBackGroundShapeAtoFicha(Document doc, int rangeFinal)
        {
            //Fundo Branco
            WordTextStyleHelper.SetHighlightColor(doc, rangeFinal - 7, rangeFinal, WdColorIndex.wdWhite);
            WordTextStyleHelper.SetHighlightColor(doc, rangeFinal - 46, rangeFinal - 35, WdColorIndex.wdWhite);
            //Sem cor de fundo
            WordTextStyleHelper.SetHighlightColor(doc, rangeFinal - 35, rangeFinal - 7, WdColorIndex.wdNoHighlight);
            WordTextStyleHelper.SetHighlightColor(doc, rangeFinal - 54, rangeFinal - 46, WdColorIndex.wdNoHighlight);
        }
        /// <summary>
        /// Inserir o texto de Ato e ficha dos shapes
        /// </summary>
        /// <param name="doc">Documento Ativo</param>
        [Obsolete("Não precisa ser mais usado, pois não há necessidade de colocar mais Shape")]
        public static void InserirTextoAtoFicha(Document doc)
        {
            if (doc == null)
            {
                throw new ArgumentNullException("doc", "Documento não pode ser nulo!");
            }
            WordParagraphHelper.InserirParagrafoEmRange(doc, new string(' ', 5) + "matrícula" + new string(' ', 30) + "ficha");
        }
        /// <summary>
        /// Get Shapes Object
        /// </summary>
        /// <param name="doc">Documento ativo</param>
        /// <returns>Shapes Object</returns>
        public static Shapes GetShapes(Document doc)
        {
            return doc != null ?
                    doc.Paragraphs.Add().Application.ActiveDocument.Shapes
                :
                    throw new ArgumentNullException("doc", "Documento não pode ser nulo");
        }
        /// <summary>
        /// Simula uma margem
        /// </summary>
        /// <param name="shapes">Shapes Obj</param>
        /// <param name="left">Padding Left</param>
        /// <param name="top">Padding Top</param>
        [Obsolete("Não precisa ser mais usado, pois não há necessidade de colocar mais Shape")]
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
        /// Função que adiciona um shape para Ato e outro para ficha
        /// </summary>
        /// <param name="shapes">Objecto de Shapes</param>
        [Obsolete("Não precisa ser mais usado, pois não há necessidade de colocar mais Shape")]
        public static void InserirShapeAtoFicha(Shapes shapes)
        {
            int index;

            #region | Shape N° Ato |
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
        [Obsolete("Não precisa ser mais usado, pois não há necessidade de colocar mais Shape")]
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
        [Obsolete("Não precisa ser mais usado, pois não há necessidade de colocar mais Shape")]
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
        [Obsolete("Não precisa ser mais usado, pois não há necessidade de colocar mais Shape")]
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
            if (doc != null)
                doc.Application.ActiveDocument.InlineShapes[GetShapesCount(doc)].Delete();
            else
                throw new ArgumentNullException("doc", "Erro ao deletar, documento não pode ser nulo!");
        }
        /// <summary>
        /// Retorna a quantidade de shapes no documento
        /// </summary>
        /// <param name="doc">Documento ativo</param>
        /// <returns>Quantidade de shapes</returns>
        public static int GetShapesCount(Document doc)
        {
            return doc == null ?
                    throw new ArgumentNullException("doc", "Documento não pode ser nulo!")
                :
                    doc.Application.ActiveDocument.InlineShapes.Count;
        }
        /// <summary>
        /// Cria uma linha de separação de texto
        /// </summary>
        /// <param name="doc">Documento Ativo</param>
        public static void InserirLinhaHorizontal(Document doc)
        {
            
            if (doc != null)
                doc.Application.ActiveDocument.Paragraphs.Add().Range.InlineShapes.AddHorizontalLineStandard();
            else
                throw new ArgumentNullException("doc", "Erro ao adicionar, documento não pode ser nulo!");
        }
    }
}
