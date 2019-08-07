using AutoMapper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Cartorio11RI.App_Start.AutoMapper
{
    public class AutoMapperConfigCartorio11RI
    {
        public static void RegisterMappingsAdmCartorio()
        {
            Mapper.Initialize(x =>
            {
                x.AddProfile<DomainToDtoModelMappingProfile>();
                x.AddProfile<DtoModelToDomainMappingProfile>();
                x.AddProfile<DtoToViewModelMappingProfileAdminCartorio>();
                x.AddProfile<ViewModelToDtoMappingProfileAdminCartorio>();
            });
        }
    }
}