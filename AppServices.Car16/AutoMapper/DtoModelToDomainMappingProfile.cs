using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AutoMapper;
using Domain.Car16.Entities;
using Dto.Car16.Entities.Cadastros;

namespace AppServices.Car16.AutoMapper
{
    public class DtoModelToDomainMappingProfile : Profile
    {
        public DtoModelToDomainMappingProfile()
        {
            CreateMap<DtoPaisModel, Pais>();
            CreateMap<DtoUfModel, Uf>();
            CreateMap<DtoMunicipioModel, Municipio>();
            CreateMap<DtoPessoaModel, Pessoa>();
            CreateMap<DtoArquivoModeloDocxModel, ArquivoModeloDocx>();
        }
    }
}
