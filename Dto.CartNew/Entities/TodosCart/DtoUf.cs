using System;
using System.Collections.Generic;

namespace Dto.CartNew.Entities.TodosCart
{
    public class DtoUf : DtoEntityBaseModel
    {
        public override long? Id { get; set; }
        public long IdPais { get; set; }
        public string CodigoIbge { get; set; }
        public string Sigla { get; set; }
        public string NomeUf { get; set; }
    }
}
