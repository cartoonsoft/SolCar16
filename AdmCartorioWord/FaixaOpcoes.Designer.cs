namespace AdmCartorioWord
{
    partial class FaixaOpcoes : Microsoft.Office.Tools.Ribbon.RibbonBase
    {
        /// <summary>
        /// Variável de designer necessária.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        public FaixaOpcoes()
            : base(Globals.Factory.GetRibbonFactory())
        {
            InitializeComponent();
        }

        /// <summary> 
        /// Limpar os recursos que estão sendo usados.
        /// </summary>
        /// <param name="disposing">true se for necessário descartar os recursos gerenciados; caso contrário, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Código gerado pelo Designer de Componentes

        /// <summary>
        /// Método necessário para suporte ao Designer - não modifique 
        /// o conteúdo deste método com o editor de código.
        /// </summary>
        private void InitializeComponent()
        {
            this.tab1 = this.Factory.CreateRibbonTab();
            this.tab1.SuspendLayout();
            this.SuspendLayout();
            // 
            // tab1
            // 
            this.tab1.Label = "Adm Cartório";
            this.tab1.Name = "tab1";
            // 
            // FaixaOpcoes
            // 
            this.Name = "FaixaOpcoes";
            this.Load += new Microsoft.Office.Tools.Ribbon.RibbonUIEventHandler(this.FaixaOpcoes_Load_1);
            this.tab1.ResumeLayout(false);
            this.tab1.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        internal Microsoft.Office.Tools.Ribbon.RibbonTab tab1;
        internal Microsoft.Office.Tools.Ribbon.RibbonGroup GerenciarCamposGroup;
        internal Microsoft.Office.Tools.Ribbon.RibbonComboBox CbNatureza;
        internal Microsoft.Office.Tools.Ribbon.RibbonComboBox CbCampo;
        internal Microsoft.Office.Tools.Ribbon.RibbonButton BtnAdicionar;
    }

    partial class ThisRibbonCollection
    {
        internal FaixaOpcoes FaixaOpcoes
        {
            get { return this.GetRibbon<FaixaOpcoes>(); }
        }
    }
}
