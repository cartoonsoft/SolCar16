using AutoMapper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AppServices.Car16.AutoMapper
{
    class AutoMapperConfig
    {
        public static void RegisterMappings()
        {
            Mapper.Initialize(x =>
            {
                x.AddProfile<DomainToDtoModelMappingProfile>();
                x.AddProfile<DtoModelToDomainMappingProfile>();
            });
        }
    }
}
