using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;
using Domain.Core.Entities.Base;
using Domain.Core.Interfaces.DomainServices;
using Domain.Core.Interfaces.Repositories;

namespace AppServices.Car16.Interfaces.Base
{
    public interface IAppServiceBase <TDtoEntityModel, TEntity>: IDisposable where TDtoEntityModel : class where TEntity : class
    {
        //ronaldo arrumar
        IDomainServicesFactoryBase DomainServices
        {
            get;
            set;
        }

        void Add(TDtoEntityModel dtoItem);
        void AddRange(IEnumerable<TDtoEntityModel> dtoItens);

        void Update(TDtoEntityModel dtoItem);

        void Remove(long id);
        void Remove(TDtoEntityModel dtoitem);
        void RemoveRange(IEnumerable<TDtoEntityModel> dtoItens);

        //void Merge(TEntity persisted, TEntity current);

        TDtoEntityModel GetById(long id);
        TDtoEntityModel GetById(params object[] keyValues);
        IEnumerable<TDtoEntityModel> GetAll();
        IEnumerable<TDtoEntityModel> GetWhere(Expression<Func<TEntity, bool>> expression);
        IEnumerable<TDtoEntityModel> GetWhere(ISpecification<TEntity> specification);
        IEnumerable<TDtoEntityModel> GetWhereOrderBy<KProperty>(Expression<Func<TEntity, bool>> expression, Expression<Func<TEntity, KProperty>> orderByExpression, bool ascending = true);
        IEnumerable<TDtoEntityModel> GetWhereOrderBy<KProperty>(ISpecification<TEntity> specification, Expression<Func<TEntity, KProperty>> orderByExpression, bool ascending = true);
        Paged<TDtoEntityModel> GetWhereOrderByPaged<KProperty>(int pageIndex, int pageCount, ISpecification<TEntity> specification, Expression<Func<TEntity, KProperty>> orderByExpression, bool ascending = true);
        Paged<TDtoEntityModel> GetWhereOrderByPaged(int pageIndex, int pageCount, Expression<Func<TEntity, bool>> expression, string fieldSort, bool ascending = true);
    }
}
