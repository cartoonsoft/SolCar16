/*----------------------------------------------------------------------------
  _____            _                    _____        __ _   
/  __ \          | |                  /  ___|      / _| |  
| /  \/ __ _ _ __| |_ ___   ___  _ __ \ `--.  ___ | |_| |_ 
| |    / _` | '__| __/ _ \ / _ \| '_ \ `--. \/ _ \|  _| __|
| \__/\ (_| | |  | || (_) | (_) | | | /\__/ / (_) | | | |_ 
 \____/\__,_|_|   \__\___/ \___/|_| |_\____/ \___/|_|  \__|
Todos os direitos reservados ®                       
-----------------------------------------------------------------------------*/
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;
using Domain.Core.Entities.Base;
using Domain.Core.Interfaces.DomainServices;
using Domain.Core.Interfaces.UnitOfWork;

namespace Domain.Core.DomainServices
{
    public class DomainServiceBase<TEntity> : IDomainServiceBase<TEntity> where TEntity : class
    {
        private readonly IUfwCart _unitOfWork;

        public DomainServiceBase(IUfwCart unitOfWork)
        {
            _unitOfWork = unitOfWork;
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
        // ~DomainServiceBase() {
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

        public virtual TEntity Add(TEntity item)
        {
            return _unitOfWork.Repositories.GenericRepository<TEntity>().Add(item);
        }

        public virtual void AddRange(IEnumerable<TEntity> itens)
        {
            _unitOfWork.Repositories.GenericRepository<TEntity>().AddRange(itens);
        }

        public virtual TEntity GetById(long id)
        {
            return _unitOfWork.Repositories.GenericRepository<TEntity>().GetById(id);
        }

        public virtual TEntity GetById(params object[] keyValues)
        {
            return _unitOfWork.Repositories.GenericRepository<TEntity>().GetById(keyValues);
        }

        public virtual IEnumerable<TEntity> GetAll()
        {
            //todo: ronaldo verificar 
            IEnumerable<TEntity> listEntity = _unitOfWork.Repositories.GenericRepository<TEntity>().GetAll();
            return listEntity;
        }

        public virtual void Update(TEntity item)
        {
            _unitOfWork.Repositories.GenericRepository<TEntity>().Update(item);
        }

        public virtual void Remove(long id)
        {
            _unitOfWork.Repositories.GenericRepository<TEntity>().Remove(id);
        }

        public virtual void Remove(TEntity item)
        {
            _unitOfWork.Repositories.GenericRepository<TEntity>().Remove(item);
        }

        public virtual void RemoveRange(IEnumerable<TEntity> itens)
        {
            _unitOfWork.Repositories.GenericRepository<TEntity>().RemoveRange(itens);
        }

        public virtual IEnumerable<TEntity> GetWhere(Expression<Func<TEntity, bool>> expression)
        {
            IEnumerable<TEntity> listEntity = _unitOfWork.Repositories.GenericRepository<TEntity>().GetWhere(expression);
            return listEntity;
        }

        public virtual IEnumerable<TEntity> GetWhere(Interfaces.Repositories.ISpecification<TEntity> specification)
        {
            IEnumerable<TEntity> listEntity = _unitOfWork.Repositories.GenericRepository<TEntity>().GetWhere(specification);
            return listEntity;
        }

        public virtual IEnumerable<TEntity> GetWhereOrderBy<KProperty>(Expression<Func<TEntity, bool>> expression, Expression<Func<TEntity, KProperty>> orderByExpression, bool ascending= true)
        {
            IEnumerable<TEntity> listEntity = _unitOfWork.Repositories.GenericRepository<TEntity>().GetWhereOrderBy(expression, orderByExpression, ascending);
            return listEntity;
        }

        public virtual IEnumerable<TEntity> GetWhereOrderBy<KProperty>(Interfaces.Repositories.ISpecification<TEntity> specification, Expression<Func<TEntity, KProperty>> orderByExpression, bool ascending = true)
        {
            IEnumerable<TEntity> listEntity = _unitOfWork.Repositories.GenericRepository<TEntity>().GetWhereOrderBy(specification, orderByExpression, ascending = true);
            return listEntity;
        }

        public Paged<TEntity> GetWhereOrderByPaged<KProperty>(int pageIndex, int pageCount, Interfaces.Repositories.ISpecification<TEntity> specification, Expression<Func<TEntity, KProperty>> orderByExpression, bool ascending = true)
        {
            Paged<TEntity> pagedTmp = _unitOfWork.Repositories.GenericRepository<TEntity>().GetWhereOrderByPaged(pageIndex, pageCount, specification, orderByExpression, ascending = true);
            return pagedTmp;
        }

        public Paged<TEntity> GetWhereOrderByPaged(int pageIndex, int pageCount, Expression<Func<TEntity, bool>> expression, string fieldSort, bool ascending = true)
        {
            Paged<TEntity> pagedTmp = _unitOfWork.Repositories.GenericRepository<TEntity>().GetWhereOrderByPaged(pageIndex, pageCount, expression, fieldSort, ascending);
            return pagedTmp;
        }

        public long? GetNextValFromOracleSequence(string SequenceName)
        {
            return _unitOfWork.Repositories.GenericRepository<TEntity>().GetNextValFromOracleSequence(SequenceName);
        }
    }
}
