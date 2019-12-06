/*----------------------------------------------------------------------------
  _____            _                    _____        __ _   
/  __ \          | |                  /  ___|      / _| |  
| /  \/ __ _ _ __| |_ ___   ___  _ __ \ `--.  ___ | |_| |_ 
| |    / _` | '__| __/ _ \ / _ \| '_ \ `--. \/ _ \|  _| __|
| \__/\ (_| | |  | || (_) | (_) | | | /\__/ / (_) | | | |_ 
 \____/\__,_|_|   \__\___/ \___/|_| |_\____/ \___/|_|  \__|
Todos os direitos reservados ®                       
-----------------------------------------------------------------------------*/
using Domain.Core.Entities.Base;
using Domain.Core.Interfaces.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Core.Interfaces.DomainServices
{
    public interface IDomainServiceBase<TEntity>: IDisposable where TEntity : class
    {
        TEntity Add(TEntity item);
        void AddRange(IEnumerable<TEntity> itens);

        void Update(TEntity item);

        void Remove(long id);
        void Remove(TEntity item);
        void RemoveRange(IEnumerable<TEntity> itens);

        //void Merge(TEntity persisted, TEntity current);

        TEntity GetById(long id);
        TEntity GetById(params object[] keyValues);

        IEnumerable<TEntity> GetAll();

        IEnumerable<TEntity> GetWhere(Expression<Func<TEntity, bool>> expression);
        IEnumerable<TEntity> GetWhere(ISpecification<TEntity> specification);
        IEnumerable<TEntity> GetWhereOrderBy<KProperty>(Expression<Func<TEntity, bool>> expression, Expression<Func<TEntity, KProperty>> orderByExpression, bool ascending = true);
        IEnumerable<TEntity> GetWhereOrderBy<KProperty>(ISpecification<TEntity> specification, Expression<Func<TEntity, KProperty>> orderByExpression, bool ascending = true);
        Paged<TEntity> GetWhereOrderByPaged<KProperty>(int pageIndex, int pageCount, ISpecification<TEntity> specification, Expression<Func<TEntity, KProperty>> orderByExpression, bool ascending = true);
        Paged<TEntity> GetWhereOrderByPaged(int pageIndex, int pageCount, Expression<Func<TEntity, bool>> expression, string fieldSort, bool ascending = true);

        long? GetNextValFromOracleSequence(string SequenceName);
    }
}
