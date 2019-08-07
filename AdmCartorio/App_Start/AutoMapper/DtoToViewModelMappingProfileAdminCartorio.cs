using AutoMapper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using AdmCartorio.ViewModels;
using Dto.CartNew.Entities.Cart_11RI;
using Dto.CartNew.Entities.Cart_11RI.Diversos;

namespace AdmCartorio.App_Start.AutoMapper
{
    public class DtoToViewModelMappingProfileAdminCartorio: Profile
    {
        public DtoToViewModelMappingProfileAdminCartorio()
        {
            CreateMap<DtoModeloDocxList, ModeloDocxListViewModel>();
            CreateMap<DtoAtoList, AtoListViewModel>();
            CreateMap<DtoAcao, AcaoViewModel>();
        }
    }
}