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
    public class ViewModelToDtoMappingProfile: Profile
    {
        public ViewModelToDtoMappingProfile()
        {
            CreateMap<AcaoViewModel, DtoAcao>();
            CreateMap<AtoViewModel, DtoAto>();
            CreateMap<AtoViewModel, DtoDadosAto>();
            CreateMap<AtoListViewModel, DtoAto>();
            CreateMap<AtoEventoViewModel, DtoAtoEvento>();
            CreateMap<AtoPessoaViewModel, DtoAtoPessoa>();
            CreateMap<InfAtoViewModel, DtoInfAto>();

            //CreateMap<DtoDadosImovel, DadosImovel >();
            //CreateMap<DtoDocx, Docx >();
            //CreateMap<DtoMenuAcaoList, MenuAcaoList >();

            CreateMap<ModeloDocViewModel, DtoModeloDoc>();
            CreateMap<ModeloDocListViewModel, DtoModeloDocxList>();
            //CreateMap<DtoMunicipio, Municipio>();
            //CreateMap<DtoPREIMO, PREIMO>();
            //CreateMap<DtoPais, Pais>();
            //CreateMap<DtoPessoaCartNew, PessoaCartNew>();
            //CreateMap<DtoPessoaPesxPre, PessoaPesxPre>();
            //CreateMap<DtoUf, Uf>();
        }
    }
}