namespace Domain.Cart16RI.Entities
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    

    [Table("EXAME", Schema = "DEZESSEIS")]
    public partial class EXAME
    {
        [Key]
        [Column(Order = 0)]
        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        public long SEQEXA { get; set; }

        public long? ESP { get; set; }

        public long? CONFERENTE { get; set; }

        [Key]
        [Column(Order = 1)]
        [StringLength(120)]
        public string OUTDO { get; set; }

        [Key]
        [Column(Order = 2)]
        [StringLength(20)]
        public string OUTDOX { get; set; }

        [Key]
        [Column(Order = 3)]
        [StringLength(120)]
        public string OUTTE { get; set; }

        [Key]
        [Column(Order = 4)]
        [StringLength(20)]
        public string OUTTEX { get; set; }

        [Key]
        [Column(Order = 5)]
        [StringLength(120)]
        public string APRES { get; set; }

        [StringLength(50)]
        public string TEL { get; set; }

        [StringLength(1000)]
        public string OBS { get; set; }

        [Key]
        [Column(Order = 6)]
        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        public long ENTRADA { get; set; }

        [Column("EXAME")]
        public long? EXAME1 { get; set; }

        public long? DEVOLUCAO { get; set; }

        public long? SAIDA { get; set; }

        public long? RETIRADA { get; set; }
    }
}
