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
    public class DtoToViewModelMappingProfile: Profile
    {
        public DtoToViewModelMappingProfile()
        {
            CreateMap<DtoAcao, AcaoViewModel>();
            CreateMap<DtoAto, AtoViewModel>();
            CreateMap<DtoAtoEvento, AtoEventoViewModel>();
            CreateMap<DtoAtoPessoa, AtoPessoaViewModel>();
            CreateMap<DtoDadosAto, AtoViewModel>();
            CreateMap<DtoInfAto, InfAtoViewModel>();
            
            //CreateMap<DtoDadosImovel, DadosImovel >();
            //CreateMap<DtoDocx, Docx >();
            //CreateMap<DtoMenuAcaoList, MenuAcaoList >();

            CreateMap<DtoModeloDoc, ModeloDocViewModel>();
            CreateMap<DtoModeloDocxList, ModeloDocListViewModel>();
            //CreateMap<DtoMunicipio, Municipio>();
            //CreateMap<DtoPREIMO, PREIMO>();
            //CreateMap<DtoPais, Pais>();
            //CreateMap<DtoPessoaCartNew, PessoaCartNew>();
            //CreateMap<DtoPessoaPesxPre, PessoaPesxPre>();
            //CreateMap<DtoUf, Uf>();
        }
    }
}