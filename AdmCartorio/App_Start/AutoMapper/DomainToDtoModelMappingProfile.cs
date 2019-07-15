using AutoMapper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Domain.Cartorio.Entities.CartorioNew;
using Dto.Cartorio.Entities.Cadastros;
using Domain.Cartorio.Entities.Diversas;
using Dto.Cartorio.Entities.Diversos;
using Dto.Car16.Entities.Cadastros;
using Domain.Cartorio.Entities.Cartorio;

namespace AdmCartorio.App_Start.AutoMapper
{
    public class DomainToDtoModelMappingProfile : Profile
    {
        public DomainToDtoModelMappingProfile()
        {
            CreateMap<Pais, DtoPaisModel>();
            CreateMap<Uf, DtoUfModel>();
            CreateMap<Municipio, DtoMunicipioModel>();
            CreateMap<PessoaCartorioNew , DtoPessoaCartorioNew >();
            CreateMap<ArquivoModeloDocxList, DtoArquivoModeloDocxList>();
            CreateMap<ACESSO, DtoAcesso>();

        }
    }
}
