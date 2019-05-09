using AdmCartorio.ViewModels;
using AutoMapper;
using Dto.Car16.Entities.Diversos;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace AdmCartorio.App_Start.AutoMapper
{
    public class ViewModelToDtoMappingProfileAdminCartorio: Profile
    {
        public ViewModelToDtoMappingProfileAdminCartorio()
        {
            CreateMap<ArquivoModeloDocxListViewModel, DtoArquivoModeloDocxList>();
            CreateMap<AtoListViewModel, DtoAtoList>();
        }
    }
}