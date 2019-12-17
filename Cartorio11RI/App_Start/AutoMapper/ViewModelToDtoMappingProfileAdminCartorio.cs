using AutoMapper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Dto.CartNew.Entities.Cart_11RI.Diversos;
using Dto.CartNew.Entities.Cart_11RI;
using Cartorio11RI.ViewModels;

namespace Cartorio11RI.App_Start.AutoMapper
{
    public class ViewModelToDtoMappingProfileAdminCartorio: Profile
    {
        public ViewModelToDtoMappingProfileAdminCartorio()
        {
            CreateMap<ModeloDocxListViewModel, DtoModeloDocxList>();
            CreateMap<AtoListViewModel, DtoAto>();
            CreateMap<AcaoViewModel, DtoAcao>();
            CreateMap<AtoViewModel, DtoAto>();
        }
    }
}