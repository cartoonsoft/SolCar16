namespace Domain.Car16.Entities.Car16
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("NFE_RPS_RET", Schema = "DEZESSEIS")]
    public partial class NFE_RPS_RET
    {
        [Key]
        [Column(Order = 0)]
        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        public long SEQNFE { get; set; }

        [Key]
        [Column(Order = 1)]
        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        public int NRO_NFE { get; set; }

        public DateTime? DT_NFE { get; set; }

        [Key]
        [Column(Order = 2)]
        [StringLength(8)]
        public string COD_VER { get; set; }

        [StringLength(5)]
        public string TIPO_RPS { get; set; }

        [Key]
        [Column(Order = 3)]
        [StringLength(5)]
        public string SERIE { get; set; }

        [Key]
        [Column(Order = 4)]
        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        public long NUMERO { get; set; }

        [StringLength(8)]
        public string DT_EMISSAO { get; set; }

        public int? CAMPO_09 { get; set; }

        public bool? CAMPO_10 { get; set; }

        public long? CAMPO_11 { get; set; }

        [StringLength(75)]
        public string CAMPO_12 { get; set; }

        [StringLength(3)]
        public string CAMPO_13 { get; set; }

        [StringLength(50)]
        public string CAMPO_14 { get; set; }

        [StringLength(10)]
        public string CAMPO_15 { get; set; }

        [StringLength(30)]
        public string CAMPO_16 { get; set; }

        [StringLength(30)]
        public string CAMPO_17 { get; set; }

        [StringLength(50)]
        public string CAMPO_18 { get; set; }

        [StringLength(2)]
        public string CAMPO_19 { get; set; }

        public int? CAMPO_20 { get; set; }

        [StringLength(75)]
        public string CAMPO_21 { get; set; }

        public bool? CAMPO_22 { get; set; }

        [StringLength(1)]
        public string CAMPO_23 { get; set; }

        [StringLength(8)]
        public string CAMPO_24 { get; set; }

        public long? CAMPO_25 { get; set; }

        [StringLength(8)]
        public string CAMPO_26 { get; set; }

        public long? CAMPO_27 { get; set; }

        public long? CAMPO_28 { get; set; }

        public short? CAMPO_29 { get; set; }

        public short? CAMPO_30 { get; set; }

        public long? CAMPO_31 { get; set; }

        public long? CAMPO_32 { get; set; }

        [StringLength(1)]
        public string CAMPO_33 { get; set; }

        public bool? CAMPO_34 { get; set; }

        public long? CAMPO_35 { get; set; }

        public int? CAMPO_36 { get; set; }

        public long? CAMPO_37 { get; set; }

        [StringLength(75)]
        public string CAMPO_38 { get; set; }

        [StringLength(3)]
        public string CAMPO_39 { get; set; }

        [StringLength(50)]
        public string CAMPO_40 { get; set; }

        [StringLength(10)]
        public string CAMPO_41 { get; set; }

        [StringLength(30)]
        public string CAMPO_42 { get; set; }

        [StringLength(30)]
        public string CAMPO_43 { get; set; }

        [StringLength(50)]
        public string CAMPO_44 { get; set; }

        [StringLength(2)]
        public string CAMPO_45 { get; set; }

        public int? CAMPO_46 { get; set; }

        [StringLength(75)]
        public string CAMPO_47 { get; set; }

        [StringLength(1000)]
        public string CAMPO_48 { get; set; }

        [Key]
        [Column(Order = 5)]
        public DateTime DT_INC { get; set; }

        [Key]
        [Column(Order = 6)]
        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        public long SEQINC { get; set; }
    }
}
