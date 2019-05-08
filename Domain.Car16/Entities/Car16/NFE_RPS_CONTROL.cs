namespace Domain.Car16.Entities.Car16
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("NFE_RPS_CONTROL", Schema = "DEZESSEIS")]
    public partial class NFE_RPS_CONTROL
    {
        [Key]
        [Column(Order = 0)]
        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        public long SEQNFE { get; set; }

        [Key]
        [Column(Order = 1)]
        [StringLength(5)]
        public string SERIE { get; set; }

        [Key]
        [Column(Order = 2)]
        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        public long NUMERO { get; set; }

        [StringLength(50)]
        public string NM_ARQUIVO { get; set; }

        public DateTime? DT_GERACAO { get; set; }

        public byte? SEQUSU { get; set; }

        public DateTime? DT_BASE { get; set; }

        [Key]
        [Column(Order = 3)]
        [StringLength(1)]
        public string TIPO { get; set; }

        [Key]
        [Column(Order = 4)]
        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        public long PROT { get; set; }

        [StringLength(8)]
        public string CAMPO_05 { get; set; }

        [StringLength(1)]
        public string CAMPO_06 { get; set; }

        [StringLength(15)]
        public string CAMPO_07 { get; set; }

        [StringLength(15)]
        public string CAMPO_08 { get; set; }

        [StringLength(5)]
        public string CAMPO_09 { get; set; }

        [StringLength(4)]
        public string CAMPO_10 { get; set; }

        [StringLength(1)]
        public string CAMPO_11 { get; set; }

        [StringLength(1)]
        public string CAMPO_12 { get; set; }

        [StringLength(14)]
        public string CAMPO_13 { get; set; }

        [StringLength(8)]
        public string CAMPO_14 { get; set; }

        [StringLength(12)]
        public string CAMPO_15 { get; set; }

        [StringLength(75)]
        public string CAMPO_16 { get; set; }

        [StringLength(3)]
        public string CAMPO_17 { get; set; }

        [StringLength(50)]
        public string CAMPO_18 { get; set; }

        [StringLength(10)]
        public string CAMPO_19 { get; set; }

        [StringLength(30)]
        public string CAMPO_20 { get; set; }

        [StringLength(30)]
        public string CAMPO_21 { get; set; }

        [StringLength(50)]
        public string CAMPO_22 { get; set; }

        [StringLength(2)]
        public string CAMPO_23 { get; set; }

        [StringLength(8)]
        public string CAMPO_24 { get; set; }

        [StringLength(75)]
        public string CAMPO_25 { get; set; }

        [StringLength(55)]
        public string CAMPO_26 { get; set; }

        public int? NRO_NFE { get; set; }

        public DateTime? DT_NFE { get; set; }

        [StringLength(8)]
        public string COD_VER { get; set; }

        [Key]
        [Column(Order = 5)]
        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        public long SEQINC { get; set; }

        [Key]
        [Column(Order = 6)]
        public DateTime DT_INC { get; set; }
    }
}
