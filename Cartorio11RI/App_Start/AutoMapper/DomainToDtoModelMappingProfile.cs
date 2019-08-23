﻿using AutoMapper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Domain.CartNew.Entities;
using Domain.CartNew.Entities.Diversos;
using Dto.CartNew.Entities.TodosCart;
using Dto.CartNew.Entities.Cart_11RI;
using Dto.CartNew.Entities.Cart_11RI.Diversos;

namespace Cartorio11RI.App_Start.AutoMapper
{
    public class DomainToDtoModelMappingProfile : Profile
    {
        public DomainToDtoModelMappingProfile()
        {
            CreateMap<Pais, DtoPais>();
            CreateMap<Uf, DtoUf>();
            CreateMap<Municipio, DtoMunicipio>();
            CreateMap<PessoaCartNew , DtoPessoaCartNew>();
            CreateMap<ModeloDocxList, DtoModeloDocxList>();
            CreateMap<Acao, DtoAcao>();

        }
    }
}