using AutoMapper;
using Domain.Car16.Entities;
using Dto.Car16.Entities.Cadastros;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AppServices.Car16.AutoMapper
{
    public class DomainToDtoModelMappingProfile : Profile
    {
        public DomainToDtoModelMappingProfile()
        {
            CreateMap<Pais, DtoPaisModel>();
            CreateMap<Uf, DtoUfModel>();
            CreateMap<Municipio, DtoMunicipioModel>();
            CreateMap<Pessoa, DtoPessoaModel>();
            CreateMap<ArquivoModeloDocx, DtoArquivoModeloDocxModel>();

        }
    }
}
