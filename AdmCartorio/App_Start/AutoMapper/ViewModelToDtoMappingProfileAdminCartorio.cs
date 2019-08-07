using AutoMapper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using AdmCartorio.ViewModels;
using Dto.CartNew.Entities.Cart_11RI.Diversos;
using Dto.CartNew.Entities.Cart_11RI;

namespace AdmCartorio.App_Start.AutoMapper
{
    public class ViewModelToDtoMappingProfileAdminCartorio: Profile
    {
        public ViewModelToDtoMappingProfileAdminCartorio()
        {
            CreateMap<ModeloDocxListViewModel, DtoModeloDocxList>();
            CreateMap<AtoListViewModel, DtoAtoList>();
            CreateMap<AcaoViewModel, DtoAcao>();
        }
    }
}