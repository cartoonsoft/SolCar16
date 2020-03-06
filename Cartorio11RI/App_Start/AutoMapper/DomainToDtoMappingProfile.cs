using AutoMapper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Domain.CartNew.Entities;
using Domain.CartNew.Entities.Diversos;
using Dto.CartNew.Entities.TodosCart;
using Dto.CartNew.Entities.Cart_11RI;
using Dto.CartNew.Entities.Cart_11RI.Diversos;
using Domain.Cart11RI.Entities;

namespace Cartorio11RI.App_Start.AutoMapper
{
    public class DomainToDtoMappingProfile : Profile
    {
        public DomainToDtoMappingProfile()
        {
            CreateMap<Acao, DtoAcao>();
            CreateMap<Ato, DtoAto>();
            CreateMap<Ato, DtoDadosAto>();
            CreateMap<AtoEvento, DtoAtoEvento>();
            CreateMap<AtoPessoa, DtoAtoPessoa>();
            CreateMap<DadosImovel, DtoDadosImovel>();
            CreateMap<Docx, DtoDocx>();
            CreateMap<MenuAcaoList, DtoMenuAcaoList>();
            CreateMap<ModeloDoc, DtoModeloDoc>();
            CreateMap<ModeloDocxList, DtoModeloDocxList>();
            CreateMap<Municipio, DtoMunicipio>();
            CreateMap<PREIMO, DtoPREIMO>();
            CreateMap<Pais, DtoPais>();
            CreateMap<PessoaCartNew, DtoPessoaCartNew>();
            CreateMap<PessoaPesxPre, DtoPessoaPesxPre>();
            CreateMap<Uf, DtoUf>();
        }
    }
}
