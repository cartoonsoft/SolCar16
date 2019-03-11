using System;
using System.Collections.Generic;
using Dto.Car16.Entities.Base;

namespace Dto.Car16.Entities.Cadastros
{
    public class DtoMunicipioModel: DtoEntityBaseModel
    {
        public override long Id { get; set; }
        public int IdUf { get; set; }
        public string CodIbge { get; set; }
        public string NomeMunicipio { get; set; }
    }
}
