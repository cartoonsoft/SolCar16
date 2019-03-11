using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;
using Domain.Core.Entities.Base;
using Domain.Core.Interfaces.DomainServices.Base;
using Domain.Core.Interfaces.Repositories;
using Domain.Core.Interfaces.UnitOfWork;

namespace Domain.Core.DomainServices.Base
{
    public class DomainServiceBase<TEntity> : IDomainServiceBase<TEntity> where TEntity : class
    {
        private readonly IUnitOfWork _unitOfWork;

        public DomainServiceBase(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        #region dispose
        private bool disposed = false;

        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }

        // Protected implementation of Dispose pattern.
        protected virtual void Dispose(bool disposing)
        {
            if (!disposed)
            {
                if (disposing)
                {
                    // Free any other managed objects here.
                    if (this._unitOfWork != null)
                    {
                        this._unitOfWork.Dispose();
                    }
                }
            }

            disposed = true;
        }
        #endregion

        public void Add(TEntity item)
        {
            _unitOfWork.Repository<TEntity>().Add(item);
        }

        public void AddRange(IEnumerable<TEntity> itens)
        {
            _unitOfWork.Repository<TEntity>().AddRange(itens);
        }

        public IEnumerable<TEntity> GetAll()
        {
            IEnumerable<TEntity> listEntity = _unitOfWork.Repository<TEntity>().GetAll();
            return listEntity;
        }

        public TEntity GetById(long id)
        {
            return _unitOfWork.Repository<TEntity>().GetById(id);
        }

        public void Update(TEntity item)
        {
            _unitOfWork.Repository<TEntity>().Update(item);
        }

        public void Remove(long id)
        {
            _unitOfWork.Repository<TEntity>().Remove(id);
        }

        public void Remove(TEntity item)
        {
            _unitOfWork.Repository<TEntity>().Remove(item);
        }

        public void RemoveRange(IEnumerable<TEntity> itens)
        {
            _unitOfWork.Repository<TEntity>().RemoveRange(itens);
        }

        public IEnumerable<TEntity> GetWhere(Expression<Func<TEntity, bool>> expression)
        {
            IEnumerable<TEntity> listEntity = _unitOfWork.Repository<TEntity>().GetWhere(expression);
            return listEntity;
        }

        public IEnumerable<TEntity> GetWhere(ISpecification<TEntity> specification)
        {
            IEnumerable<TEntity> listEntity = _unitOfWork.Repository<TEntity>().GetWhere(specification);
            return listEntity;
        }

        public IEnumerable<TEntity> GetWhereOrderBy<KProperty>(Expression<Func<TEntity, bool>> expression, Expression<Func<TEntity, KProperty>> orderByExpression, bool ascending= true)
        {
            IEnumerable<TEntity> listEntity = _unitOfWork.Repository<TEntity>().GetWhereOrderBy(expression, orderByExpression, ascending);
            return listEntity;
        }

        public IEnumerable<TEntity> GetWhereOrderBy<KProperty>(ISpecification<TEntity> specification, Expression<Func<TEntity, KProperty>> orderByExpression, bool ascending = true)
        {
            IEnumerable<TEntity> listEntity = _unitOfWork.Repository<TEntity>().GetWhereOrderBy(specification, orderByExpression, ascending = true);
            return listEntity;
        }

        public Paged<TEntity> GetWhereOrderByPaged<KProperty>(int pageIndex, int pageCount, ISpecification<TEntity> specification, Expression<Func<TEntity, KProperty>> orderByExpression, bool ascending = true)
        {
            Paged<TEntity> pagedTmp = _unitOfWork.Repository<TEntity>().GetWhereOrderByPaged(pageIndex, pageCount, specification, orderByExpression, ascending = true);
            return pagedTmp;
        }

        public Paged<TEntity> GetWhereOrderByPaged(int pageIndex, int pageCount, Expression<Func<TEntity, bool>> expression, string fieldSort, bool ascending = true)
        {
            Paged<TEntity> pagedTmp = _unitOfWork.Repository<TEntity>().GetWhereOrderByPaged(pageIndex, pageCount, expression, fieldSort, ascending);
            return pagedTmp;
        }
    }
}
