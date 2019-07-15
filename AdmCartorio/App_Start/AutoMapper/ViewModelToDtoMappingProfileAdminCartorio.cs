using AutoMapper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using AdmCartorio.ViewModels;
using Dto.Car16.Entities.Cadastros;
using Dto.Cartorio.Entities.Diversos;

namespace AdmCartorio.App_Start.AutoMapper
{
    public class ViewModelToDtoMappingProfileAdminCartorio: Profile
    {
        public ViewModelToDtoMappingProfileAdminCartorio()
        {
            CreateMap<ArquivoModeloDocxListViewModel, DtoArquivoModeloDocxList>();
            CreateMap<AtoListViewModel, DtoAtoList>();
            CreateMap<ACESSOViewModel, DtoAcesso>();
        }
    }
}