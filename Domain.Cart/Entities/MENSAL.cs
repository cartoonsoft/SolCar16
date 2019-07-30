namespace Domain.Cart.Entities
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("MENSAL", Schema = "DEZESSEIS")]
    public partial class MENSAL
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        public int DATA { get; set; }

        public short? TITQTDE { get; set; }

        public decimal? TITOFICIAL { get; set; }

        public decimal? TITESTADO { get; set; }

        public decimal? TITIPESP { get; set; }

        public decimal? TITRC { get; set; }

        public decimal? TITTJ { get; set; }

        public short? CERQTDE { get; set; }

        public decimal? CEROFICIAL { get; set; }

        public decimal? CERESTADO { get; set; }

        public decimal? CERIPESP { get; set; }

        public decimal? CERRC { get; set; }

        public decimal? CERTJ { get; set; }

        public short? ARIQTDE { get; set; }

        public decimal? ARIOFICIAL { get; set; }

        public decimal? ARIESTADO { get; set; }

        public decimal? ARIIPESP { get; set; }

        public decimal? ARIRC { get; set; }

        public decimal? ARITJ { get; set; }

        public short? CEDQTDE { get; set; }

        public decimal? CEDOFICIAL { get; set; }

        public decimal? CEDESTADO { get; set; }

        public decimal? CEDIPESP { get; set; }

        public decimal? CEDRC { get; set; }

        public decimal? CEDTJ { get; set; }

        public short? ESPQTDE { get; set; }

        public decimal? ESPOFICIAL { get; set; }

        public decimal? ESPESTADO { get; set; }

        public decimal? ESPIPESP { get; set; }

        public decimal? ESPRC { get; set; }

        public decimal? ESPTJ { get; set; }

        public short? PREQTDE { get; set; }

        public decimal? PREOFICIAL { get; set; }

        public decimal? PREESTADO { get; set; }

        public decimal? PREIPESP { get; set; }

        public decimal? PRERC { get; set; }

        public decimal? PRETJ { get; set; }

        public short? EXAQTDE { get; set; }

        public decimal? EXAOFICIAL { get; set; }

        public decimal? EXAESTADO { get; set; }

        public decimal? EXAIPESP { get; set; }

        public decimal? EXARC { get; set; }

        public decimal? EXATJ { get; set; }

        public decimal? BANCO { get; set; }

        public short? PORQTDE { get; set; }

        public decimal? POROFICIAL { get; set; }

        public decimal? PORESTADO { get; set; }

        public decimal? PORIPESP { get; set; }

        public decimal? PORRC { get; set; }

        public decimal? PORTJ { get; set; }

        public short? MICQTDE { get; set; }

        public decimal? MICVALOR { get; set; }

        public decimal? TITMP { get; set; }

        public decimal? CERMP { get; set; }

        public decimal? ARIMP { get; set; }

        public decimal? CEDMP { get; set; }

        public decimal? ESPMP { get; set; }

        public decimal? PREMP { get; set; }

        public decimal? EXAMP { get; set; }

        public decimal? PORMP { get; set; }
    }
}
