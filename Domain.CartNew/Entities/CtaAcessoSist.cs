using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Domain.Core.Entities.Base;

namespace Domain.CartNew.Entities
{
    [Table("TB_CTA_ACESSO_SIST", Schema = "DEZESSEIS_NEW")]
    public class CtaAcessoSist: EntityBase
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        [Column("ID_CTA_ACESSO_SIST")]
        public override long? Id { get; set; }

        [Column("ID_NUVEM")]
        public string IdNuvem { get; set; } //id na nuvem

        [Column("DT_ULT_SINC")]
        public DateTime DataUltSinc { get; set; } //Data d ultima sincronização nuvem

        [Column("FLAG_SINC")]
        public bool FlagSinc { get; set; } //sincronizado com a nuven

        [Column("NUM_LICENCA")]
        public string NumLicenca { get; set; }

        [Column("CHAVE_LIC")]
        public string ChaveLicenca { get; set; }

        [Column("STATUS_LIC")]
        public string StatusLicenca { get; set; }

        [Column("DT_CAD")]
        public DateTime DataCadastro { get; set; }

        [Column("DT_ALTER")]
        public DateTime? DataAlteracao { get; set; }

        [Column("NOME_SISTEMA")]
        public string NomeSistema { get; set; }

        [Column("NOME_VERSAO")]
        public string NomeVersao { get; set; }

        [Column("NUM_VERSAO")]
        public string NumVersao { get; set; }

        [Column("NOME_FANTASIA")]
        public string NomeFantasia { get; set; }

        [Column("RAZAO_SOCIAL")]
        public string RazaoSocial { get; set; }

        [Column("DT_NASC")]
        public DateTime DataNasc { get; set; }

        [Column("PES_FIS_JUR")]
        public char PesFisJur { get; set; } // pessoa F- fisica ou J - jurídica

        [Column("CPF")]
        public string Cpf { get; set; }

        [Column("RG")]
        public string Rg { get; set; }

        [Column("RG_ORGAO_EMISSOR")]
        public string RgOrgaoEmissor { get; set; }

        [Column("RG_EMISSAO")]
        public DateTime RgEmissao { get; set; }

        [Column("CNPJ")]
        public string Cnpj { get; set; }

        [Column("INSC_EST")]
        public string InscricaoEstadual { get; set; }

        [Column("INSC_MUN")]
        public string InscricaoMun { get; set; }  //inscricao Municipal

        [Column("CNS")]
        public string Cns { get; set; } // confederacao nascional de serviços

        [Column("OPT_SIMPLES_NAC")]
        public bool OptanteSimplesNasc { get; set; }

        [Column("TIPO_LOG")]
        public string TipoLogradouro { get; set; }

        [Column("LOGRADOURO")]
        public string Logradouro { get; set; }

        [Column("NUMERO")]
        public string Numero { get; set; }

        [Column("COMPLEMENTO")]
        public string Complemento { get; set; }

        [Column("BAIRRO")]
        public string Bairro { get; set; }

        [Column("MUNICIPIO")]
        public string Municipio { get; set; }

        [Column("CEP")]
        public string Cep { get; set; }

        [Column("SIGLA_UF")]
        public string SiglaUF { get; set; }

        [Column("SIGLA_PAIS")]
        public string  SiglaPais { get; set; }

        /*
        [Column("COD_POSTAL_EXT")]
        public string  CodPostalExterior { get; set; }
        [Column("ENDERECO_EXT")]
        [Column("COMPLEMENTO_EXT")]
        [Column("CIDADE_EXT")]
        [Column("ESTADO_EXT")]
        */

        [Column("DDI_TEL_COM1")]
        public string DDI { get; set; }

        [Column("DDD_TEL_COM1")]
        public string DDD { get; set; }

        [Column("TELEFONE_COM1")]
        public string Telefone { get; set; }
        /*
        [Column("RAMAL_TEL_COM1")]
        [Column("NOM_CONT_TEL_COM1")]
        */

    }
}
