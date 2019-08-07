using System;
using System.Collections.Generic;

namespace Dto.CartNew.Entities.TodosCart
{
    public class DtoMunicipio: DtoEntityBaseModel
    {
        public override long? Id { get; set; }
        public long IdUf { get; set; }
        public string CodIbge { get; set; }
        public string NomeMunicipio { get; set; }
    }
}
