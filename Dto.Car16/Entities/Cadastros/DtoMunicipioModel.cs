using System;
using System.Collections.Generic;
using Dto.Car16.Entities.Base;

namespace Dto.Cartorio.Entities.Cadastros
{
    public class DtoMunicipioModel: DtoEntityBaseModel
    {
        public override long? Id { get; set; }
        public long IdUf { get; set; }
        public string CodIbge { get; set; }
        public string NomeMunicipio { get; set; }
    }
}
