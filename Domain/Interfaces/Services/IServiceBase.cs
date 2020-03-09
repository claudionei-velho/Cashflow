using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;

namespace Domain.Interfaces.Services {
  public interface IServiceBase<TEntity> : IDisposable where TEntity : class {
    IQueryable<TEntity> Query(Expression<Func<TEntity, bool>> condition = null, 
        Func<IQueryable<TEntity>, IOrderedQueryable<TEntity>> order = null);

    IEnumerable<TEntity> List(Expression<Func<TEntity, bool>> condition = null,
        Func<IQueryable<TEntity>, IOrderedQueryable<TEntity>> order = null);
    Task<IEnumerable<TEntity>> ListAsync(Expression<Func<TEntity, bool>> condition = null,
        Func<IQueryable<TEntity>, IOrderedQueryable<TEntity>> order = null);
    IEnumerable<dynamic> SelectList(Expression<Func<TEntity, dynamic>> columns,
        Expression<Func<TEntity, bool>> condition = null,
        Func<IQueryable<TEntity>, IOrderedQueryable<TEntity>> order = null);
    Task<IEnumerable<dynamic>> SelectListAsync(Expression<Func<TEntity, dynamic>> columns,
        Expression<Func<TEntity, bool>> condition = null,
        Func<IQueryable<TEntity>, IOrderedQueryable<TEntity>> order = null);
    IEnumerable<TEntity> PagedList(Expression<Func<TEntity, bool>> condition = null,
        Func<IQueryable<TEntity>, IOrderedQueryable<TEntity>> order = null, int skip = 1, int take = 8);
    Task<IEnumerable<TEntity>> PagedListAsync(Expression<Func<TEntity, bool>> condition = null,
        Func<IQueryable<TEntity>, IOrderedQueryable<TEntity>> order = null, int skip = 1, int take = 8);

    TEntity GetById(int id);
    TEntity GetById(object id);
    Task<TEntity> GetByIdAsync(int id);
    Task<TEntity> GetByIdAsync(object id);
    TEntity GetFirst(Expression<Func<TEntity, bool>> condition);
    Task<TEntity> GetFirstAsync(Expression<Func<TEntity, bool>> condition);

    bool Exists(Expression<Func<TEntity, bool>> condition);
    Task<bool> ExistsAsync(Expression<Func<TEntity, bool>> condition);

    int Count(Expression<Func<TEntity, bool>> condition = null);
    Task<int> CountAsync(Expression<Func<TEntity, bool>> condition = null);
    int Pages(Expression<Func<TEntity, bool>> condition = null, int size = 0);

    Task Insert(TEntity obj);
    Task Update(TEntity obj);
    Task Delete(TEntity obj);
    Task AddOrUpdate(TEntity obj);    
  }
}
