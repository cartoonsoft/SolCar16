using Microsoft.Office.Interop.Word;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LibFunctions.Functions.Word
{
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
            if (doc == null)
            {
                throw new ArgumentNullException("doc", "Documento não pode ser nulo!");
            }
            //Coloca o texto em negrito se negrito = true, se não, tira o negrito
            doc.Application.ActiveDocument.Range(posicaoInicial, posicaoFinal).Bold = negrito ? 1 : 0;
        }
        /// <summary>
        /// Função responsável por controlar o negrito
        /// </summary>
        /// <param name="doc">Documento Ativo</param>
        /// <param name="posicaoInicial">Posição Inicial</param>
        /// <param name="negrito"> TRUE - NEGRITO; FALSE - TIRAR NEGRITO </param>
        public static void Bold(Document doc, int posicaoInicial, bool negrito = true)
        {
            if (doc == null)
            {
                throw new ArgumentNullException("doc", "Documento não pode ser nulo!");
            }
            //Coloca o texto em negrito se negrito = true, se não, tira o negrito
            doc.Application.ActiveDocument.Range(posicaoInicial).Bold = negrito ? 1 : 0;
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
            if (doc == null)
            {
                throw new ArgumentNullException("doc", "Documento não pode ser nulo!");
            }
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
            if (doc == null)
            {
                throw new ArgumentNullException("doc", "Documento não pode ser nulo!");
            }
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
            if (doc == null)
            {
                throw new ArgumentNullException("doc", "Documento não pode ser nulo!");
            }
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
            if (doc == null)
            {
                throw new ArgumentNullException("doc", "Documento não pode ser nulo!");
            }
            doc.Application.ActiveDocument.Range(posicaoInicial, posicaoFinal).Font.Underline = wdUnderline;
        }

    }
}
