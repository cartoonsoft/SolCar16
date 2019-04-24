using System;
using System.Collections.Generic;
using Dto.Car16.Entities.Base;

namespace Dto.Car16.Entities.Cadastros
{
    public class DtoUfModel : DtoEntityBaseModel
    {
        public override long? Id { get; set; }
        public long IdPais { get; set; }
        public string CodigoIbge { get; set; }
        public string Sigla { get; set; }
        public string NomeUf { get; set; }
    }
}
