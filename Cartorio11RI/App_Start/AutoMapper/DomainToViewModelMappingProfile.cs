using AutoMapper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Domain.CartNew.Entities;
using Domain.CartNew.Entities.Diversos;
using Domain.Cart11RI.Entities;
using Cartorio11RI.ViewModels;

namespace Cartorio11RI.App_Start.AutoMapper
{
    public class DomainToViewModelMappingProfile : Profile
    {
        public DomainToViewModelMappingProfile()
        {
            //CreateMap<Pais, PaisViewModel>();
            //CreateMap<Uf, UfViewModel>();
            //CreateMap<Municipio, MunicipioViewModel>();
            CreateMap<ModeloDoc, ModeloDocxViewModel>();
            CreateMap<ModeloDocxList, ModeloDocxListViewModel>();
            CreateMap<Acao, AcaoViewModel>();
            CreateMap<Ato, AtoViewModel>();

        }
    }
}
