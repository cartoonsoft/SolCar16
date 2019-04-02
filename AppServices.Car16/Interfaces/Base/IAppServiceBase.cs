using Domain.Core.Entities.Base;
using Domain.Core.Interfaces.DomainServices.Base;
using Domain.Core.Interfaces.Repositories;
using Domain.Core.Interfaces.UnitOfWork;
using Dto.Car16.Entities.Base;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace AppServices.Car16.Interfaces.Base
{
    public interface IAppServiceBase <TDtoEntityModel, TEntity>: IDisposable where TDtoEntityModel : class where TEntity : class
    {
        void Add(TDtoEntityModel dtoItem);
        void AddRange(IEnumerable<TDtoEntityModel> dtoItens);

        void Update(TDtoEntityModel dtoItem);

        void Remove(long id);
        void Remove(TDtoEntityModel dtoitem);
        void RemoveRange(IEnumerable<TDtoEntityModel> dtoItens);

        //void Merge(TEntity persisted, TEntity current);

        IDomainServiceBase<T> DomainService<T>() where T: class;

        TDtoEntityModel GetById(long id);
        IEnumerable<TDtoEntityModel> GetAll();
        IEnumerable<TDtoEntityModel> GetWhere(Expression<Func<TEntity, bool>> expression);
        IEnumerable<TDtoEntityModel> GetWhere(ISpecification<TEntity> specification);
        IEnumerable<TDtoEntityModel> GetWhereOrderBy<KProperty>(Expression<Func<TEntity, bool>> expression, Expression<Func<TEntity, KProperty>> orderByExpression, bool ascending = true);
        IEnumerable<TDtoEntityModel> GetWhereOrderBy<KProperty>(ISpecification<TEntity> specification, Expression<Func<TEntity, KProperty>> orderByExpression, bool ascending = true);
        Paged<TDtoEntityModel> GetWhereOrderByPaged<KProperty>(int pageIndex, int pageCount, ISpecification<TEntity> specification, Expression<Func<TEntity, KProperty>> orderByExpression, bool ascending = true);
        Paged<TDtoEntityModel> GetWhereOrderByPaged(int pageIndex, int pageCount, Expression<Func<TEntity, bool>> expression, string fieldSort, bool ascending = true);
    }
}
