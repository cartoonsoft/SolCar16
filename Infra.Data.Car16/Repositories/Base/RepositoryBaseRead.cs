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
using System.Data.Entity;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using Domain.Core.Entities.Base;
using Domain.Core.Interfaces.Data;
using Domain.Core.Interfaces.Repositories;

namespace Infra.Data.Car16.Repositories.Base
{
    public class RepositoryBaseRead<TEntity> : IRepositoryBaseRead<TEntity> where TEntity : class
    {
        private readonly IContextCore _context;
        private readonly IDbSet<TEntity> _dbSet;

        public RepositoryBaseRead(IContextCore context)
        {
            _context = context;
            _dbSet = _context.Set<TEntity>();
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
                    if (_context != null)
                    {
                        _context.Dispose();
                    }
                }

                // free unmanaged resources (unmanaged objects) and override a finalizer below.
                // set large fields to null.

                disposedValue = true;
            }
        }

        // override a finalizer only if Dispose(bool disposing) above has code to free unmanaged resources.
        // ~RepositoryBase() {
        //   // Do not change this code. Put cleanup code in Dispose(bool disposing) above.
        //   Dispose(false);
        // }

        // This code added to correctly implement the disposable pattern.
        public virtual void Dispose()
        {
            // Do not change this code. Put cleanup code in Dispose(bool disposing) above.
            Dispose(true);
            // uncomment the following line if the finalizer is overridden above.
            // GC.SuppressFinalize(this);
        }
        #endregion

        protected IContextCore Context
        {
            get { return _context;}

        }

        protected IDbSet<TEntity> DbSet  
        {
            get { return _dbSet; }
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

        /// <summary>
        /// Bucar por chave primaria (long id) padrao no modelo novo
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public TEntity GetById(long id)
        {
            return _dbSet.Find(id);
        }

        /// <summary>
        /// Buscar por qq tipo de chave primária e chave composta
        /// </summary>
        /// <param name="keyValues"></param>
        /// <returns></returns>
        public TEntity GetById(params object[] keyValues)
        {
            return _dbSet.Find(keyValues);
        }


        public IEnumerable<TEntity> GetWhere(Expression<Func<TEntity, bool>> expression)
        {
            return _dbSet
                .Where(expression)
                .AsEnumerable();
        }

        public IEnumerable<TEntity> GetWhere(ISpecification<TEntity> specification)
        {
            return _dbSet
                .Where(specification.SatisfiedBy())
                .AsEnumerable();
        }

        public IEnumerable<TEntity> GetWhereOrderBy<KProperty>(Expression<Func<TEntity, bool>> expression, Expression<Func<TEntity, KProperty>> orderByExpression, bool ascending)
        {
            if (ascending)
            {
                return _dbSet
                    .Where(expression)
                    .OrderBy(orderByExpression)
                    .AsEnumerable();
            }
            else
            {
                return _dbSet
                    .Where(expression)
                    .OrderByDescending(orderByExpression)
                   .AsEnumerable();
            }
        }

        public IEnumerable<TEntity> GetWhereOrderBy<KProperty>(ISpecification<TEntity> specification, Expression<Func<TEntity, KProperty>> orderByExpression, bool ascending)
        {
            if (ascending)
            {
                return _dbSet.Where(specification.SatisfiedBy())
                    .OrderBy(orderByExpression)
                    .AsEnumerable();
            }
            else
            {
                return _dbSet.Where(specification.SatisfiedBy())
                    .OrderByDescending(orderByExpression)
                    .AsEnumerable();
            }
        }

        public Paged<TEntity> GetWhereOrderByPaged(int pageIndex, int pageCount, Expression<Func<TEntity, bool>> expression, string fieldSort, bool ascending)
        {
            //todo: ronaldo fazer
            Paged<TEntity> paged = new Paged<TEntity>();

            if (string.IsNullOrWhiteSpace(fieldSort))
                fieldSort = "Id";

            //Type t = typeof(TEntity);

            /*if (!t.GetProperties().Select(c => c.Name).Contains(fieldSort))
                fieldSort = "Id";*/

            //todo: ronaldo arrumar
            var param = Expression.Parameter(typeof(TEntity), "c");
            MemberExpression property = null;

            string[] fieldNames = fieldSort.Contains(".") ? fieldSort.Split('.') : Regex.Split(fieldSort, @"(?<!^)(?=[A-Z])");
            foreach (string filed in fieldNames)
            {
                if (property == null)
                {
                    property = Expression.Property(param, filed);
                }
                else
                {
                    property = Expression.Property(property, filed);
                }
            }

            //Expression conversion = Expression.Convert(Expression.Property(param, fieldSort), typeof(object));
            Expression conversion = Expression.Convert(property, typeof(object));
            var sortExpression = Expression.Lambda<Func<TEntity, object>>(conversion, param).Compile();

            if (ascending)
            {
                paged.listEntities = _dbSet
                    .Where(expression)
                    .OrderBy(sortExpression)
                    .Skip(pageCount * (pageIndex - 1))
                    .Take(pageCount)
                    .AsEnumerable();
            }
            else
            {
                paged.listEntities = _dbSet
                    .Where(expression)
                    .OrderByDescending(sortExpression)
                    .Skip(pageCount * (pageIndex - 1))
                    .Take(pageCount)
                    .AsEnumerable();
            }

            return paged;
        }

        public Paged<TEntity> GetWhereOrderByPaged<KProperty>(int pageIndex, int pageCount, ISpecification<TEntity> specification, Expression<Func<TEntity, KProperty>> orderByExpression, bool ascending)
        {
            Paged<TEntity> paged = new Paged<TEntity>();
            if ((pageIndex > 0 ) && (pageCount > 0))
            {
                List<TEntity> entities = _dbSet.Where(specification.SatisfiedBy()).ToList<TEntity>();
                double pgesTmp = (entities.Count() / pageCount);
                paged.TotalPages = (pgesTmp < 1)? 1: (int)Math.Truncate(pgesTmp);
                paged.CurrentPage = pageIndex;

                if (ascending)
                {
                    paged.listEntities = _dbSet
                        .Where(specification.SatisfiedBy())
                        .OrderBy(orderByExpression)
                        .Skip(pageCount * (pageIndex - 1))
                        .Take(pageCount)
                        .AsEnumerable();
                }
                else
                {
                    paged.listEntities = _dbSet
                        .Where(specification.SatisfiedBy())
                        .OrderByDescending(orderByExpression)
                        .Skip(pageCount * (pageIndex - 1))
                        .Take(pageCount)
                        .AsEnumerable();
                }
            }

            return paged;
        }

    }
}