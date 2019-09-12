using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Domain.Cart11RI.Entities;
using Domain.CartNew.Entities;
using Domain.CartNew.Entities.Diversos;
using Domain.CartNew.Interfaces.Repositories;
using Domain.CartNew.Interfaces.UnitOfWork;
using DomainServ.CartNew.Base;
using DomainServ.CartNew.Interfaces;
using Dto.CartNew.Entities.Cart_11RI.Diversos;

namespace DomainServ.CartNew.Services
{
    public class TipoAtoDs : DomainServiceCartNew<TipoAto>, ITipoAtoDs
    {
        //private readonly IRepositoryTipoAto _repositoryTipoAto;

        public TipoAtoDs(IUnitOfWorkDataBaseCartNew UfwCartNew) : base(UfwCartNew)
        {
            //_repositoryTipoAto = UfwCartNew.Repositories.RepositoryTipoAto;
        }

        public IEnumerable<DtoTipoAtoList> ListaTipoAtos(long? idTipoAtoPai)
        {
            List<DtoTipoAtoList> listaDtoTipoAtoList = new List<DtoTipoAtoList>();
            List<TipoAtoList> lista = this.UfwCartNew.Repositories.RepositoryTipoAto.ListaTipoAtos(idTipoAtoPai).ToList();

            foreach (var item in lista)
            {
                DtoTipoAtoList dtoTipoAtoList = new  DtoTipoAtoList
                {
                    Id = item.Id,
                    IdCtaAcessoSist = item.IdCtaAcessoSist,
                    IdTipoAtoPai = item.IdTipoAtoPai,
                    Descricao = item.Descricao,
                    Orientacao = item.Orientacao,
                };

                foreach (var itemlist in item.ListaTipoAtosFihos)
                {
                    dtoTipoAtoList.ListaTipoAtosFihos.Add(new DtoTipoAtoList {
                        Id = itemlist.Id,
                        IdCtaAcessoSist = itemlist.IdCtaAcessoSist,
                        IdTipoAtoPai = itemlist.IdTipoAtoPai,
                        Descricao = itemlist.Descricao,
                        Orientacao = itemlist.Orientacao
                    });
                } 
            }

            return listaDtoTipoAtoList;
        }
    }
}
