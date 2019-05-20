using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.Office.Interop.Word;
using System.Text;
using System.Threading.Tasks;

namespace LibFunctions.Functions.Word
{
    public static class WordSelectionHelper
    {
        /// <summary>
        /// Função que desloca para algum item, para certa direção e uma certa quantidade de deslocamento
        /// </summary>
        /// <param name="doc">Documento Ativo</param>
        /// <param name="wdGoToItem">Item que deseja (Page,line etc.)</param>
        /// <param name="wdGoToDirection">Direção que deslocamento</param>
        /// <param name="quantidadeDeslocamento">Quantidade de deslocamento</param>
        public static void Goto(Document doc, WdGoToItem wdGoToItem, WdGoToDirection wdGoToDirection, int quantidadeDeslocamento)
        {
            if (doc == null)
            {
                throw new ArgumentNullException("doc", "Documento não pode ser nulo!");
            }
            doc.Application.Selection.GoTo(wdGoToItem, wdGoToDirection, quantidadeDeslocamento);

        }
        /// <summary>
        /// Seleciona o texto até o final de uma unidade especifica ou apenas se move para o final de dela.
        /// </summary>
        /// <param name="doc">Documento Ativo</param>
        /// <param name="wdUnits">Unidade especifica (WdUnits) </param>
        /// <param name="wdMovementType">Tipo de movimento (Extend- Seleciona, Move - Move sem selecionar)</param>
        public static void EndOf(Document doc, WdUnits wdUnits, WdMovementType wdMovementType)
        {
            if (doc == null)
            {
                throw new ArgumentNullException("doc", "Documento não pode ser nulo!");
            }
            doc.Application.Selection.EndOf(wdUnits, wdMovementType);
        }

        /// <summary>
        /// Função que pega o texto selecionado
        /// </summary>
        /// <param name="doc">Documento ativo</param>
        /// <returns>Texto selecionado</returns>
        public static string GetSelectionText(Document doc)
        {
            if (doc == null)
            {
                throw new ArgumentNullException("doc", "Documento não pode ser nulo!");
            }
            return doc.Application.Selection.Text;
        }
        /// <summary>
        /// Delete o texto selecionado
        /// </summary>
        /// <param name="doc">Documento ativo</param>
        public static void DeleteSelectionText(Document doc)
        {
            if (doc == null)
            {
                throw new ArgumentNullException("doc", "Documento não pode ser nulo!");
            }
            doc.Application.Selection.Delete();
        }
    }
}
