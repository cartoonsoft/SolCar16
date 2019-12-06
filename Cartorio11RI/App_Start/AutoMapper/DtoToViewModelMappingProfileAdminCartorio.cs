using AutoMapper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Dto.CartNew.Entities.Cart_11RI;
using Dto.CartNew.Entities.Cart_11RI.Diversos;
using Cartorio11RI.ViewModels;

namespace Cartorio11RI.App_Start.AutoMapper
{
    public class DtoToViewModelMappingProfileAdminCartorio: Profile
    {
        public DtoToViewModelMappingProfileAdminCartorio()
        {
            CreateMap<DtoModeloDocxList, ModeloDocxListViewModel>();
            CreateMap<DtoAtoList, AtoListViewModel>();
            CreateMap<DtoAcao, AcaoViewModel>();
            CreateMap<DtoAto, AtoViewModel>();
        }
    }
}