namespace Domain.Cart16RI.Entities
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    

    [Table("MENSAL", Schema = "DEZESSEIS")]
    public partial class MENSAL
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        public long DATA { get; set; }

        public int? TITQTDE { get; set; }

        public double? TITOFICIAL { get; set; }

        public double? TITESTADO { get; set; }

        public double? TITIPESP { get; set; }

        public double? TITRC { get; set; }

        public double? TITTJ { get; set; }

        public int? CERQTDE { get; set; }

        public double? CEROFICIAL { get; set; }

        public double? CERESTADO { get; set; }

        public double? CERIPESP { get; set; }

        public double? CERRC { get; set; }

        public double? CERTJ { get; set; }

        public int? ARIQTDE { get; set; }

        public double? ARIOFICIAL { get; set; }

        public double? ARIESTADO { get; set; }

        public double? ARIIPESP { get; set; }

        public double? ARIRC { get; set; }

        public double? ARITJ { get; set; }

        public int? CEDQTDE { get; set; }

        public double? CEDOFICIAL { get; set; }

        public double? CEDESTADO { get; set; }

        public double? CEDIPESP { get; set; }

        public double? CEDRC { get; set; }

        public double? CEDTJ { get; set; }

        public int? ESPQTDE { get; set; }

        public double? ESPOFICIAL { get; set; }

        public double? ESPESTADO { get; set; }

        public double? ESPIPESP { get; set; }

        public double? ESPRC { get; set; }

        public double? ESPTJ { get; set; }

        public int? PREQTDE { get; set; }

        public double? PREOFICIAL { get; set; }

        public double? PREESTADO { get; set; }

        public double? PREIPESP { get; set; }

        public double? PRERC { get; set; }

        public double? PRETJ { get; set; }

        public int? EXAQTDE { get; set; }

        public double? EXAOFICIAL { get; set; }

        public double? EXAESTADO { get; set; }

        public double? EXAIPESP { get; set; }

        public double? EXARC { get; set; }

        public double? EXATJ { get; set; }

        public double? BANCO { get; set; }

        public int? PORQTDE { get; set; }

        public double? POROFICIAL { get; set; }

        public double? PORESTADO { get; set; }

        public double? PORIPESP { get; set; }

        public double? PORRC { get; set; }

        public double? PORTJ { get; set; }

        public int? MICQTDE { get; set; }

        public double? MICVALOR { get; set; }

        public double? TITMP { get; set; }

        public double? CERMP { get; set; }

        public double? ARIMP { get; set; }

        public double? CEDMP { get; set; }

        public double? ESPMP { get; set; }

        public double? PREMP { get; set; }

        public double? EXAMP { get; set; }

        public double? PORMP { get; set; }
    }
}
