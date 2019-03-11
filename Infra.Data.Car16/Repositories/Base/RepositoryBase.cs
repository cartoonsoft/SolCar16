﻿/*----------------------------------------------------------------------------
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
using System.Data.Entity;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;
using Domain.Core.Entities.Base;
using Domain.Core.Interfaces.Data;
using Domain.Core.Interfaces.Repositories;
using Oracle.ManagedDataAccess.Client;
using Oracle.ManagedDataAccess.Types;

namespace Infra.Data.Car16.Repositories.Base
{
    public class RepositoryBase<TEntity> : IRepositoryBase<TEntity> where TEntity : class
    {
        private readonly IContextCore _context;
        private readonly IDbSet<TEntity> _dbSet;

        public RepositoryBase(IContextCore context)
        {
            _context = context;
            _dbSet = _context.Set<TEntity>();
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
                    this._context.Dispose();
                }
            }

            disposed = true;
        }
        #endregion

        public void Add(TEntity entity)
        {
            _dbSet.Add(entity);
        }

        public void AddRange(IEnumerable<TEntity> itens)
        {
            (_dbSet as DbSet).AddRange(itens);
        }
              
        public IEnumerable<TEntity> GetAll()
        {
            List<TEntity> listEntity = null;
            try
            {
                listEntity = _dbSet.ToList();
            }
            catch (Exception ex)
            {
                var e = ex;
                throw;
            }
            return listEntity;
        }

        public TEntity GetById(long id)
        {
            return _dbSet.Find(id);
        }

        public void Remove(long id)
        {
            var item = _dbSet.Find(id);
            if (item != null)
            {
                this.Remove(item);
            }
        }

        public void Remove(TEntity item)
        {
            if (_dbSet.Find(item) != null)
            {
                _dbSet.Remove(item);
            }
        }

        public void RemoveRange(IEnumerable<TEntity> itens)
        {
            (_dbSet as DbSet).RemoveRange(itens);
        }

        public int SaveChanges()
        {

            throw new NotImplementedException();
        }

        public void Update(TEntity item)
        {
            if (item != null)
            {
                var entry = _dbSet.Find(item);
                if ( entry != null)
                {
                    _dbSet.Attach(item);
                    _context.Entry(item).State = EntityState.Modified;
                }
            }
        }

        public IEnumerable<TEntity> GetWhere(Expression<Func<TEntity, bool>> expression)
        {
            throw new NotImplementedException();
        }

        public IEnumerable<TEntity> GetWhere(ISpecification<TEntity> specification)
        {
            throw new NotImplementedException();
        }

        public IEnumerable<TEntity> GetWhereOrderBy<KProperty>(Expression<Func<TEntity, bool>> expression, Expression<Func<TEntity, KProperty>> orderByExpression, bool ascending)
        {
            throw new NotImplementedException();
        }

        public IEnumerable<TEntity> GetWhereOrderBy<KProperty>(ISpecification<TEntity> specification, Expression<Func<TEntity, KProperty>> orderByExpression, bool ascending)
        {
            throw new NotImplementedException();
        }

        public Paged<TEntity> GetWhereOrderByPaged<KProperty>(int pageIndex, int pageCount, ISpecification<TEntity> specification, Expression<Func<TEntity, KProperty>> orderByExpression, bool ascending)
        {
            throw new NotImplementedException();
        }

        public Paged<TEntity> GetWhereOrderByPaged(int pageIndex, int pageCount, Expression<Func<TEntity, bool>> expression, string fieldSort, bool ascending)
        {
            throw new NotImplementedException();
        }
    }
}