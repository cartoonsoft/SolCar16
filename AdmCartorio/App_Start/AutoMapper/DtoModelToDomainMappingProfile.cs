﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AutoMapper;
using Domain.Cartorio.Entities.CartorioNew;
using Domain.Cartorio.Entities.Diversas;
using Dto.Cartorio.Entities.Cadastros;
using Dto.Cartorio.Entities.Diversos;

namespace AdmCartorio.App_Start.AutoMapper
{
    public class DtoModelToDomainMappingProfile: Profile
    {
        public DtoModelToDomainMappingProfile()
        {
            CreateMap<DtoPaisModel, Pais>();
            CreateMap<DtoUfModel, Uf>();
            CreateMap<DtoMunicipioModel, Municipio>();
            CreateMap<DtoPessoaCartorioNew , PessoaCartorioNew>();
            CreateMap<DtoArquivoModeloDocxList, ArquivoModeloDocxList>();

        }
    }
}
