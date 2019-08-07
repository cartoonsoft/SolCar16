using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Runtime.Serialization;

namespace Dto.CartNew.Entities.TodosCart
{
    [DataContract]
    public class DtoPessoaCartNew : DtoEntityBaseModel
    {
        [Key]
        public override long? Id { get; set; }

        [StringLength(20)]
        public string Codigo { get; set; }

        [StringLength(20)]
        public string CodExterno1 { get; set; }

        [StringLength(20)]
        public string CodExterno2 { get; set; }

        public DateTime? DataImport { get; set; }

        public bool FlagImport { get; set; }

        [StringLength(100)]
        public string FonteImport { get; set; }

        [Required]
        [StringLength(1)]
        public string StatusPessoa { get; set; }

        [Required]
        [StringLength(100)]
        public string NomePessoa { get; set; }

        [StringLength(100)]
        public string RazaoSocial { get; set; }

        [StringLength(255)]
        public string Naturalidade { get; set; }

        public DateTime? DataNasc { get; set; }

        [StringLength(1)]
        public string PessoaFisJur { get; set; }

        [StringLength(1)]
        public string Sexo { get; set; }

        [StringLength(100)]
        public string Pai { get; set; }

        [StringLength(100)]
        public string Mae { get; set; }

        [StringLength(11)]
        public string Cpf { get; set; }

        [StringLength(15)]
        public string Rg { get; set; }

        [StringLength(10)]
        public string RgOrgaoEmissor { get; set; }

        public DateTime? RgEmissao { get; set; }

        [StringLength(32)]
        public string Passaport { get; set; }

        [StringLength(15)]
        public string Cnpj { get; set; }

        [StringLength(20)]
        public string InscricaoEstadual { get; set; }

        [StringLength(20)]
        public string InscricaoMunicipal { get; set; }

        public bool FlagOptSimplesNacional { get; set; }
    }
}
