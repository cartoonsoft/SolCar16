using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AutoMapper;
using Domain.Cart11RI.Entities;
using Domain.CartNew.Entities;
using Domain.CartNew.Entities.Diversos;
using Dto.CartNew.Entities.Cart_11RI;
using Dto.CartNew.Entities.Cart_11RI.Diversos;
using Dto.CartNew.Entities.TodosCart;

namespace Cartorio11RI.App_Start.AutoMapper
{
    public class DtoToDomainMappingProfile: Profile
    {
        public DtoToDomainMappingProfile()
        {
            CreateMap<DtoAcao, Acao>();
            CreateMap<DtoAto, Ato>();
            CreateMap<DtoAtoEvento, AtoEvento>();
            CreateMap<DtoAtoPessoa, AtoPessoa>();
            CreateMap<DtoDadosAto, Ato>();
            CreateMap<DtoDadosImovel, DadosImovel>();
            CreateMap<DtoDocx, Docx>();
            CreateMap<DtoMenuAcaoList, MenuAcaoList>();
            CreateMap<DtoModeloDoc, ModeloDoc>();
            CreateMap<DtoModeloDocxList, ModeloDocxList>();
            CreateMap<DtoMunicipio, Municipio>();
            CreateMap<DtoPREIMO, PREIMO>();
            CreateMap<DtoPais, Pais>();
            CreateMap<DtoPessoaCartNew, PessoaCartNew>();
            CreateMap<DtoPessoaPesxPre, PessoaPesxPre>();
            CreateMap<DtoUf, Uf>();
        }
    }
}
