﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;
using AutoMapper;
using AppServices.Car16.Interfaces.Base;
using Domain.Core.Entities.Base;
using Domain.Core.Interfaces.UnitOfWork;
using Domain.Core.Interfaces.Repositories;
using Domain.Core.Interfaces.DomainServices;

namespace AppServices.Car16.AppServices.Base
{
    public class AppServiceBase<TDtoEntityModel, TEntity> : IAppServiceBase<TDtoEntityModel, TEntity> where TDtoEntityModel : class where TEntity : class
    {
        private readonly IUnitOfWork _unitOfWork;

        public AppServiceBase(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;

            //todo: ronaldo verificar automapper
            //   Mapper.Initialize(cfg => cfg.CreateMap<TDtoEntityModel, TEntity>());
        }

        #region IDisposable Support
        private bool disposedValue = false; // To detect redundant calls

        protected virtual void Dispose(bool disposing)
        {
            if (!disposedValue)
            {
                if (disposing)
                {
                    // dispose managed state (managed objects).
                }

                // free unmanaged resources (unmanaged objects) and override a finalizer below.
                // set large fields to null.
                disposedValue = true;
            }
        }

        // override a finalizer only if Dispose(bool disposing) above has code to free unmanaged resources.
        // ~AppServiceBase() {
        //   // Do not change this code. Put cleanup code in Dispose(bool disposing) above.
        //   Dispose(false);
        // }

        // This code added to correctly implement the disposable pattern.
        public void Dispose()
        {
            // Do not change this code. Put cleanup code in Dispose(bool disposing) above.
            Dispose(true);
            // uncomment the following line if the finalizer is overridden above.
            // GC.SuppressFinalize(this);
        }
        #endregion

        public virtual IDomainServicesFactoryBase DomainServices { get; set; }

        public virtual void Add(TDtoEntityModel dtoItem)
        {
            //var config = new MapperConfiguration(cfg => cfg.CreateMap<TDtoEntityModel, TEntity>());
            TEntity entityTmp = Mapper.Map<TDtoEntityModel, TEntity>(dtoItem);
            this.DomainServices.GenericDomainService<TEntity>().Add(entityTmp);
        }

        public virtual void AddRange(IEnumerable<TDtoEntityModel> dtoItens)
        {
            IEnumerable<TEntity> listEntities = Mapper.Map<IEnumerable<TDtoEntityModel>, IEnumerable<TEntity>>(dtoItens);
            this.DomainServices.GenericDomainService<TEntity>().AddRange(listEntities);
        }
        public virtual TDtoEntityModel GetById(long id)
        {
            TEntity entityTmp = this.DomainServices.GenericDomainService<TEntity>().GetById(id);
            TDtoEntityModel dtoEntityTmp = Mapper.Map<TEntity, TDtoEntityModel>(entityTmp);
            return dtoEntityTmp;
        }
        public TDtoEntityModel GetById(params object[] keyValues)
        {
            TEntity entityTmp = this.DomainServices.GenericDomainService<TEntity>().GetById(keyValues);
            TDtoEntityModel dtoEntityTmp = Mapper.Map<TEntity, TDtoEntityModel>(entityTmp);
            return dtoEntityTmp;
        }

        public virtual IEnumerable<TDtoEntityModel> GetAll()
        {
            IEnumerable<TEntity> listEntities = this.DomainServices.GenericDomainService<TEntity>().GetAll();
            IEnumerable<TDtoEntityModel> listDtoEntities = Mapper.Map<IEnumerable<TEntity>, IEnumerable<TDtoEntityModel>>(listEntities);
            return listDtoEntities;
        }

        public virtual void Update(TDtoEntityModel dtoItem)
        {
            throw new NotImplementedException();
        }

        public virtual void Remove(long id)
        {
            this.DomainServices.GenericDomainService<TEntity>().Remove(id);
        }

        public virtual void Remove(TDtoEntityModel dtoitem)
        {
            TEntity entityTmp = Mapper.Map<TDtoEntityModel, TEntity>(dtoitem);
            this.DomainServices.GenericDomainService<TEntity>().Remove(entityTmp);
        }

        public virtual void RemoveRange(IEnumerable<TDtoEntityModel> dtoItens)
        {
            IEnumerable<TEntity> listEntities = Mapper.Map<IEnumerable<TDtoEntityModel>, IEnumerable<TEntity>>(dtoItens);
            this.DomainServices.GenericDomainService<TEntity>().RemoveRange(listEntities);
        }

        public virtual IEnumerable<TDtoEntityModel> GetWhere(Expression<Func<TEntity, bool>> expression)
        {
            IEnumerable<TEntity> listEntities = this.DomainServices.GenericDomainService<TEntity>().GetWhere(expression);
            IEnumerable<TDtoEntityModel> listDto = Mapper.Map<IEnumerable<TEntity>, IEnumerable<TDtoEntityModel>>(listEntities);
            return listDto;
        }

        public virtual IEnumerable<TDtoEntityModel> GetWhere(ISpecification<TEntity> specification)
        {
            IEnumerable<TEntity> listEntities = this.DomainServices.GenericDomainService<TEntity>().GetWhere(specification);
            IEnumerable<TDtoEntityModel> listDto = Mapper.Map<IEnumerable<TEntity>, IEnumerable<TDtoEntityModel>>(listEntities);
            return listDto;
        }

        public virtual IEnumerable<TDtoEntityModel> GetWhereOrderBy<KProperty>(Expression<Func<TEntity, bool>> expression, Expression<Func<TEntity, KProperty>> orderByExpression, bool ascending = true)
        {
            IEnumerable<TEntity> listEntities = this.DomainServices.GenericDomainService<TEntity>().GetWhereOrderBy(expression, orderByExpression, ascending);
            IEnumerable<TDtoEntityModel> listDto = Mapper.Map<IEnumerable<TEntity>, IEnumerable<TDtoEntityModel>>(listEntities);
            return listDto;
        }

        public virtual IEnumerable<TDtoEntityModel> GetWhereOrderBy<KProperty>(ISpecification<TEntity> specification, Expression<Func<TEntity, KProperty>> orderByExpression, bool ascending = true)
        {
            IEnumerable<TEntity> listEntities = this.DomainServices.GenericDomainService<TEntity>().GetWhereOrderBy(specification, orderByExpression, ascending);
            IEnumerable<TDtoEntityModel> listDto = Mapper.Map<IEnumerable<TEntity>, IEnumerable<TDtoEntityModel>>(listEntities);
            return listDto;
        }

        public virtual Paged<TDtoEntityModel> GetWhereOrderByPaged<KProperty>(int pageIndex, int pageCount, ISpecification<TEntity> specification, Expression<Func<TEntity, KProperty>> orderByExpression, bool ascending = true)
        {
            //Paged<TDtoEntityModel> paged = new Paged<TDtoEntityModel>();
            //this.DomainService<TEntity>().GetWhereOrderByPaged<TEntity>
            //IEnumerable<TEntity> listEntities = this.DomainService<TEntity>().GetWhereOrderBy(specification, orderByExpression, ascending);
            //IEnumerable<TDtoEntityModel> listDto = Mapper.Map<IEnumerable<TEntity>, IEnumerable<TDtoEntityModel>>(listEntities);
            //return listDto;
            throw new NotImplementedException();
        }

        public virtual Paged<TDtoEntityModel> GetWhereOrderByPaged(int pageIndex, int pageCount, Expression<Func<TEntity, bool>> expression, string fieldSort, bool ascending = true)
        {
            //IEnumerable<TEntity> listEntities = this.DomainService<TEntity>().GetWhereOrderBy(specification, orderByExpression, ascending);
            //IEnumerable<TDtoEntityModel> listDto = Mapper.Map<IEnumerable<TEntity>, IEnumerable<TDtoEntityModel>>(listEntities);
            //return listDto;
            throw new NotImplementedException();
        }

    }
}