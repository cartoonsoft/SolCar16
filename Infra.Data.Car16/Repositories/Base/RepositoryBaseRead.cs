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
using System.Configuration;
using System.Data.Entity;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using System.Web.Configuration;
using Domain.Core.Entities.Base;
using Domain.Core.Interfaces.Data;
using Domain.Core.Interfaces.Repositories;
using Oracle.ManagedDataAccess.Client;

namespace Infra.Data.Car16.Repositories.Base
{
    public class RepositoryBaseRead<TEntity> : IRepositoryBaseRead<TEntity> where TEntity : class
    {
        private readonly IContextCore _context;
        private readonly IDbSet<TEntity> _dbContextSet;

        public RepositoryBaseRead(IContextCore context)
        {
            _context = context;
            _dbContextSet = _context.Set<TEntity>();
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

        /// <summary>
        /// Adiciona uma cláusula were na query
        /// </summary>
        /// <param name="sqlWhere"></param>
        /// <param name="logicOperator">pode ser: and ou or</param>
        /// <param name="firstWhere"></param>
        /// <returns></returns>
        protected string AddWhereClause(string sqlWhere, string logicOperator, bool firstWhere)
        {
            string whereTmp = string.Empty; 

            if (firstWhere)
            {
                whereTmp = "where" + System.Environment.NewLine + sqlWhere;
            } else {
                whereTmp = System.Environment.NewLine + logicOperator + " " + sqlWhere;
            }

            return whereTmp;
        }

        protected IContextCore Context
        {
            get { return _context;}

        }

        protected IDbSet<TEntity> DbSet  
        {
            get { return _dbContextSet; }
        }

        public IDbSet<TEntity> Get
        {
            get { return _dbContextSet; } 
        }

        public IEnumerable<TEntity> GetAll()
        {
            List<TEntity> listEntity = null;
            try
            {
                listEntity = _dbContextSet.ToList();
            }
            catch (Exception)
            {
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
            return _dbContextSet.Find(id);
        }

        /// <summary>
        /// Buscar por qq tipo de chave primária e chave composta
        /// </summary>
        /// <param name="keyValues"></param>
        /// <returns></returns>
        public TEntity GetById(params object[] keyValues)
        {
            return _dbContextSet.Find(keyValues);
        }


        public IEnumerable<TEntity> GetWhere(Expression<Func<TEntity, bool>> expression)
        {
            return _dbContextSet
                .Where(expression)
                .AsEnumerable();
        }

        public IEnumerable<TEntity> GetWhere(ISpecification<TEntity> specification)
        {
            return _dbContextSet
                .Where(specification.SatisfiedBy())
                .AsEnumerable();
        }

        public IEnumerable<TEntity> GetWhereOrderBy<KProperty>(Expression<Func<TEntity, bool>> expression, Expression<Func<TEntity, KProperty>> orderByExpression, bool ascending = true)
        {
            if (ascending)
            {
                return _dbContextSet
                    .Where(expression)
                    .OrderBy(orderByExpression)
                    .AsEnumerable();
            }
            else
            {
                return _dbContextSet
                    .Where(expression)
                    .OrderByDescending(orderByExpression)
                    .AsEnumerable();
            }
        }

        public IEnumerable<TEntity> GetWhereOrderBy<KProperty>(ISpecification<TEntity> specification, Expression<Func<TEntity, KProperty>> orderByExpression, bool ascending = true)
        {
            if (ascending)
            {
                return _dbContextSet.Where(specification.SatisfiedBy())
                    .OrderBy(orderByExpression)
                    .AsEnumerable();
            }
            else
            {
                return _dbContextSet.Where(specification.SatisfiedBy())
                    .OrderByDescending(orderByExpression)
                    .AsEnumerable();
            }
        }

        public Paged<TEntity> GetWhereOrderByPaged(int pageIndex, int pageCount, Expression<Func<TEntity, bool>> expression, string fieldSort, bool ascending = true)
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
                paged.listEntities = _dbContextSet
                    .Where(expression)
                    .OrderBy(sortExpression)
                    .Skip(pageCount * (pageIndex - 1))
                    .Take(pageCount)
                    .AsEnumerable();
            }
            else
            {
                paged.listEntities = _dbContextSet
                    .Where(expression)
                    .OrderByDescending(sortExpression)
                    .Skip(pageCount * (pageIndex - 1))
                    .Take(pageCount)
                    .AsEnumerable();
            }

            return paged;
        }

        public Paged<TEntity> GetWhereOrderByPaged<KProperty>(int pageIndex, int pageCount, ISpecification<TEntity> specification, Expression<Func<TEntity, KProperty>> orderByExpression, bool ascending = true)
        {
            Paged<TEntity> paged = new Paged<TEntity>();
            if ((pageIndex > 0 ) && (pageCount > 0))
            {
                List<TEntity> entities = _dbContextSet.Where(specification.SatisfiedBy()).ToList<TEntity>();
                double pgesTmp = (entities.Count() / pageCount);
                paged.TotalPages = (pgesTmp < 1)? 1: (int)Math.Truncate(pgesTmp);
                paged.CurrentPage = pageIndex;

                if (ascending)
                {
                    paged.listEntities = _dbContextSet
                        .Where(specification.SatisfiedBy())
                        .OrderBy(orderByExpression)
                        .Skip(pageCount * (pageIndex - 1))
                        .Take(pageCount)
                        .AsEnumerable();
                }
                else
                {
                    paged.listEntities = _dbContextSet
                        .Where(specification.SatisfiedBy())
                        .OrderByDescending(orderByExpression)
                        .Skip(pageCount * (pageIndex - 1))
                        .Take(pageCount)
                        .AsEnumerable();
                }
            }

            return paged;
        }

        /// <summary>
        /// Busca o NextVal de uma sequence
        /// </summary>
        /// <param name="SequenceName"></param>
        /// <returns></returns>
        public long GetNextValFromOracleSequence(string SequenceName)
        {
            long SeqTmp = 0;
            ConnectionStringsSection connectionStringsSection = WebConfigurationManager.GetSection("connectionStrings") as ConnectionStringsSection;
            var connStr = connectionStringsSection.ConnectionStrings[this.Context.ContextName].ConnectionString;

            using (OracleConnection conn = new OracleConnection(connStr))
            {
                conn.Open();
                using (OracleCommand command = new OracleCommand(string.Format("select {0}.NEXTVAL from dual ", SequenceName), conn))
                {
                    command.CommandType = System.Data.CommandType.Text;
                    command.BindByName = true;
                    using (OracleDataReader row = command.ExecuteReader())
                    {
                        while (row.Read())
                        {
                            SeqTmp = row.GetOracleDecimal(0).ToInt64();
                        }
                    }
                }
                conn.Close();
            }

            return SeqTmp;
        }
    }
}