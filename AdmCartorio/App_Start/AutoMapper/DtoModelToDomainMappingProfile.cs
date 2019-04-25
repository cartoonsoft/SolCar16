﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AutoMapper;
using Domain.Car16.Entities.Car16New;
using Domain.Car16.Entities.Diversas;
using Dto.Car16.Entities.Cadastros;
using Dto.Car16.Entities.Diversos;

namespace AdmCartorio.App_Start.AutoMapper
{
    public class DtoModelToDomainMappingProfile: Profile
    {
        public DtoModelToDomainMappingProfile()
        {
            CreateMap<DtoPaisModel, Pais>();
            CreateMap<DtoUfModel, Uf>();
            CreateMap<DtoMunicipioModel, Municipio>();
            CreateMap<DtoPessoaModel, Pessoa>();
            CreateMap<DtoArquivoModeloDocxList, ArquivoModeloDocxList>();

        }
    }
}