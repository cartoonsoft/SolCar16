using AutoMapper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace AdmCartorio.App_Start.AutoMapper
{
    public class AutoMapperConfigAdmCartorio
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