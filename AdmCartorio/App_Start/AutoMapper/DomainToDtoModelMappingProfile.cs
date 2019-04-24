using AutoMapper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Domain.Car16.Entities.Car16New;
using Dto.Car16.Entities.Cadastros;
using Domain.Car16.Entities.Diversas;
using Dto.Car16.Entities.Diversos;

namespace AdmCartorio.App_Start.AutoMapper
{
    public class DomainToDtoModelMappingProfile : Profile
    {
        public DomainToDtoModelMappingProfile()
        {
            CreateMap<Pais, DtoPaisModel>();
            CreateMap<Uf, DtoUfModel>();
            CreateMap<Municipio, DtoMunicipioModel>();
            CreateMap<Pessoa, DtoPessoaModel>();
            CreateMap<ArquivoModeloDocxList, DtoArquivoModeloDocxList>();

        }
    }
}
