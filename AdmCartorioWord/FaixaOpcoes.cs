using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Office.Interop.Word;
using Microsoft.Office.Tools.Ribbon;
using Oracle.ManagedDataAccess.Client;

namespace AdmCartorioWord
{
    public partial class FaixaOpcoes : RibbonBase
    {
        Dictionary<string, int> naturezaCampo = new Dictionary<string, int>();
        int IdNatureza;
        string NomeCampo;

        // Função que inicia o Ribbon
        private void FaixaOpcoes_Load(object sender, RibbonUIEventArgs e)
        {
            RibbonDropDownItem selecione
                        = Globals
                        .Factory
                        .GetRibbonFactory()
                        .CreateRibbonDropDownItem();

            selecione.Label = "Selecione";

            naturezaCampo.Add("Selecione", 0);

            CbNatureza.Items.Add(selecione);
            BtnAdicionar.Enabled = false;

            IniciarComboBoxNatureza();

        }

        private void CbNatureza_TextChanged(object sender, RibbonControlEventArgs e)
        {
            RibbonComboBox drop = sender as RibbonComboBox;

            IdNatureza = naturezaCampo[drop.Text];

            RecarregarCamposPorNatureza(IdNatureza);

            DesbloquearBloquearBotao();

        }

        private void CbCampo_TextChanged(object sender, RibbonControlEventArgs e)
        {
            RibbonComboBox drop = sender as RibbonComboBox;
            NomeCampo = drop.Text;

            DesbloquearBloquearBotao();
        }

        private void BtnAdicionar_Click(object sender, RibbonControlEventArgs e)
        {
            //Pegando posicao do cursor do teclado
            int posicao = GetPosicaoCursorDoTeclado();

            //Insere o campo no Word
            InserirCampoNoWord(posicao, NomeCampo);

            FormFields itens = Globals.ThisAddIn.Application.ActiveDocument.FormFields;
            foreach (var item in itens)
            {
                Console.WriteLine(item);
            }
        }
        #region |Funções Auxiliares com Banco |

        /// <summary>
        /// Função que recarrega os campos do combobox a partir do id do tipo do ato
        /// </summary>
        /// <param name="IdTipoAto">Identificador do tipo do ato (inicial,registro etc.)</param>
        private void RecarregarCamposPorNatureza(int IdTipoAto)
        {
            using (var con = new OracleConnection("Data Source=(DESCRIPTION =(ADDRESS = (PROTOCOL = TCP)(HOST = 192.168.1.16)(PORT =1521))(CONNECT_DATA = (SERVER = DEDICATED)(SERVICE_NAME =car12)));Password=senha16ri;User ID=dezesseis_new"))
            {
                OracleCommand command
                    = new OracleCommand("SELECT NOME_CAMPO FROM TB_CAMPOS_TP_MOD WHERE ID_TP_MODELO = :ID_TP_MODELO", con);
                con.Open();

                command.Parameters.Add(new OracleParameter("ID_TP_MODELO", IdTipoAto));

                CbCampo.Items.Clear();
                OracleDataReader reader = command.ExecuteReader();
                while (reader.Read())
                {
                    RibbonDropDownItem item
                        = Globals
                        .Factory
                        .GetRibbonFactory()
                        .CreateRibbonDropDownItem();

                    item.Label = reader.GetValue(0).ToString();
                    CbCampo.Items.Add(item);

                }
            }
        }

        #endregion

        #region | Funções Auxiliares |

        /// <summary>
        /// Pega a posição absoluta do cursor do teclado
        /// </summary>
        /// <returns>posicao do teclado (int) </returns>
        private static int GetPosicaoCursorDoTeclado()
        {
            return Globals.ThisAddIn
                .Application.Selection.Start;
        }

        /// <summary>
        /// Insere um campo no word de acordo com o NomeCampo
        /// </summary>
        /// <param name="posicao">posicao que inicia o campo</param>
        /// <param name="nomeCampo">Nome que fica no placeholder</param>
        private static void InserirCampoNoWord(int posicao, string nomeCampo)
        {

            Range posicaoRange;

            //Determina a posicao inicial que o componente sera criado
            //Trata o erro inesperado colocando o campo na posição do cursor e usando o espaço dele
            try
            {
                posicaoRange
                    = Globals.ThisAddIn
                    .Application
                    .ActiveDocument.Range(posicao, posicao);
            }
            catch
            {
                posicaoRange
                    = Globals.ThisAddIn
                    .Application
                    .ActiveDocument.Range(posicao);
            }


            //Pega o documento ativo
            Document documentoAtivo
                = Globals.ThisAddIn.Application.ActiveDocument;
            if (documentoAtivo == null)
                return;

            //Adiciona o campo na lista de campo e na posicao determinada
            FormField input = documentoAtivo.FormFields.Add(posicaoRange,
                WdFieldType.wdFieldFormTextInput);

            // Campo inicialmente vazio, então ele é considerado erro.
            input.Enabled = false;
            input.Name = nomeCampo;
            input.TextInput.Default = $"[{nomeCampo}]";
            input.HelpText = nomeCampo;
            input.Result = $"[{nomeCampo}]";
        }

        private void DesbloquearBloquearBotao()
        {
            if (IdNatureza != 0 && NomeCampo != null)
            {
                BtnAdicionar.Enabled = true;
            }
            else
            {
                BtnAdicionar.Enabled = false;
            }
        }

        /// <summary>
        /// Função que inicia o combobox Natureza pegando os dados do banco disponiveis
        /// </summary>
        private void IniciarComboBoxNatureza()
        {
            using (var con = new OracleConnection("Data Source=(DESCRIPTION =(ADDRESS = (PROTOCOL = TCP)(HOST = 192.168.1.16)(PORT =1521))(CONNECT_DATA = (SERVER = DEDICATED)(SERVICE_NAME =car12)));Password=senha16ri;User ID=dezesseis_new"))
            {
                OracleCommand command
                    = new OracleCommand("SELECT ID_TP_MODELO,DESCRICAO FROM TB_TP_MODELO", con);
                con.Open();
                OracleDataReader reader = command.ExecuteReader();
                while (reader.Read())
                {
                    RibbonDropDownItem item
                        = Globals
                        .Factory
                        .GetRibbonFactory()
                        .CreateRibbonDropDownItem();

                    item.Label = reader.GetValue(1).ToString();

                    naturezaCampo.Add(
                        reader.GetValue(1).ToString(),
                        int.Parse(reader.GetValue(0).ToString())
                        );

                    CbNatureza.Items.Add(item);

                }
            }
        }

        #endregion

    }
}
