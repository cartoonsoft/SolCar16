using System;
using System.Collections.Generic;
using Dto.Car16.Entities.Base;

namespace Dto.Car16.Entities.Cadastros
{
    public class DtoPessoaModel : DtoEntityBaseModel
    {
        public override long? Id { get; set; }
        public string Codigo { get; set; }
        public string CodExterno1 { get; set; }
        public string CodExterno2 { get; set; }
        public string CodExterno3 { get; set; }
        public DateTime? DataImport { get; set; }
        public bool FlagImport { get; set; }
        public string FonteImport { get; set; }
        public string StatusPessoa { get; set; }
        public string NomePessoa { get; set; }
        public string RazaoSocial { get; set; }
        public string Naturalidade { get; set; }
        public DateTime? DataNasc { get; set; }
        public string FlagFisJur { get; set; }
        public string Sexo { get; set; }
        public string Pai { get; set; }
        public string Mae { get; set; }
        public string Cpf  { get; set; }
        public string Rg { get; set; }
        public string RgOrgaoEmissor { get; set; }
        public DateTime? RgEmissao { get; set; }
        public string Passaport { get; set; }
        public string Cnpj { get; set; }
        public string InscricaoEstadual { get; set; }
        public string InscricaoMunicipal { get; set; }
        public bool OptanteSimplesNacional { get; set; }

    }
}
