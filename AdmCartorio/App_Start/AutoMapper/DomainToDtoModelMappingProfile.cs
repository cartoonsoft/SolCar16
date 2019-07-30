using AutoMapper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Domain.CartNew.Entities;
using Domain.CartNew.Entities.Diversos;
using Dto.Cartorio.Entities.Cadastros;
using Dto.Cartorio.Entities.Diversos;
using Dto.Car16.Entities.Cadastros;

namespace AdmCartorio.App_Start.AutoMapper
{
    public class DomainToDtoModelMappingProfile : Profile
    {
        public DomainToDtoModelMappingProfile()
        {
            CreateMap<Pais, DtoPaisModel>();
            CreateMap<Uf, DtoUfModel>();
            CreateMap<Municipio, DtoMunicipioModel>();
            CreateMap<PessoaCartNew , DtoPessoaCartorioNew >();
            CreateMap<ArquivoModeloDocxList, DtoArquivoModeloDocxList>();
            CreateMap<Acao, DtoAcao>();

        }
    }
}
