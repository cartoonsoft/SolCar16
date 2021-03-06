﻿using AutoMapper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Cartorio11RI.App_Start.AutoMapper
{
    public class AutoMapperConfigCartorio11RI
    {
        public static void RegisterMappingsCartorio11RI()
        {
            Mapper.Initialize(x =>
            {
                x.AddProfile<DomainToDtoMappingProfile>();
                x.AddProfile<DtoToDomainMappingProfile>();
                x.AddProfile<DtoToViewModelMappingProfile>();
                x.AddProfile<ViewModelToDtoMappingProfile>();
            });
        }
    }
}