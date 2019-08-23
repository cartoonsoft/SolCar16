﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AutoMapper;
using Domain.CartNew.Entities;
using Domain.CartNew.Entities.Diversos;
using Dto.CartNew.Entities.Cart_11RI.Diversos;
using Dto.CartNew.Entities.TodosCart;

namespace Cartorio11RI.App_Start.AutoMapper
{
    public class DtoModelToDomainMappingProfile: Profile
    {
        public DtoModelToDomainMappingProfile()
        {
            CreateMap<DtoPais, Pais>();
            CreateMap<DtoUf, Uf>();
            CreateMap<DtoMunicipio, Municipio>();
            CreateMap<DtoPessoaCartNew , PessoaCartNew>();
            CreateMap<DtoModeloDocxList, ModeloDocxList>();
        }
    }
}