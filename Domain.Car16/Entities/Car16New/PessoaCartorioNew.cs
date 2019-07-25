using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Domain.Core.Entities.Base;

namespace Domain.Car16.Entities.Car16New
{
    [Table("TB_PESSOA", Schema = "DEZESSEIS_NEW")]
    public class PessoaCartorioNew : AuditedEntity
    {
        [Key]
        [Column("ID_PESSOA")]
        public override long? Id { get; set; }

        [Column("CODIGO")]
        [StringLength(20)]
        public string Codigo { get; set; }

        [Column("COD_EXT1")]
        [StringLength(20)]
        public string CodExterno1 { get; set; }

        [Column("COD_EXT2")]
        [StringLength(20)]
        public string CodExterno2 { get; set; }

        [Column("DT_IMPORT")]
        public DateTime? DataImport { get; set; }

        [Column("FLAG_IMPORT")]
        public bool FlagImport { get; set; }

        [Column("FONTE_IMPORT")]
        [StringLength(100)]
        public string FonteImport { get; set; }

        [Column("STATUS_PESSOA")]
        [Required]
        [StringLength(1)]
        public string StatusPessoa { get; set; }

        [Column("NOME_PESSOA")]
        [Required]
        [StringLength(100)]
        public string NomePessoa { get; set; }

        [Column("RAZAO_SOCIAL")]
        [StringLength(100)]
        public string RazaoSocial { get; set; }

        [Column("NATURALIDADE")]
        [StringLength(255)]
        public string Naturalidade { get; set; }

        [Column("DT_NASC")]
        public DateTime? DataNasc { get; set; }

        [Column("PES_FIS_JUR")]
        [StringLength(1)]
        public string PessoaFisJur { get; set; }

        [Column("SEXO")]
        [StringLength(1)]
        public string Sexo { get; set; }

        [Column("PAI")]
        [StringLength(100)]
        public string Pai { get; set; }

        [Column("MAE")]
        [StringLength(100)]
        public string Mae { get; set; }

        [Column("CPF")]
        [StringLength(11)]
        public string Cpf { get; set; }

        [Column("RG")]
        [StringLength(15)]
        public string Rg { get; set; }

        [Column("RG_ORGAO_EMISSOR")]
        [StringLength(10)]
        public string RgOrgaoEmissor { get; set; }

        [Column("RG_EMISSAO")]
        public DateTime? RgEmissao { get; set; }

        [Column("PASSAPORTE")]
        [StringLength(32)]
        public string Passaport { get; set; }

        [Column("CNPJ")]
        [StringLength(15)]
        public string Cnpj { get; set; }

        [Column("INSC_EST")]
        [StringLength(20)]
        public string InscricaoEstadual { get; set; }

        [Column("INSC_MUN")]
        [StringLength(20)]
        public string InscricaoMunicipal { get; set; }

        [Column("OPT_SIMPLES_NAC")]
        public bool FlagOptSimplesNacional { get; set; }
    }
}
