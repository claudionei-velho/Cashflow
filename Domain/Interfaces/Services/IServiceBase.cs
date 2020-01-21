using System;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;

using Domain.Extensions;

namespace Domain.Interfaces.Services {
  public interface IServiceBase<TEntity> : IDisposable where TEntity : class {
    IQueryable<TEntity> GetData(Expression<Func<TEntity, bool>> condition = null,
        Func<IQueryable<TEntity>, IOrderedQueryable<TEntity>> order = null);
    IQueryable<dynamic> SelectList(Expression<Func<TEntity, dynamic>> columns,
        Expression<Func<TEntity, bool>> condition = null,
        Func<IQueryable<TEntity>, IOrderedQueryable<TEntity>> order = null);

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
    Task<int> PagesAsync(Expression<Func<TEntity, bool>> condition = null, int size = 0);

    Expression<Func<TEntity, bool>> GetExpression(int? id);
    Expression<Func<TEntity, bool>> GetExpression(ForeignKey key, int? id);
    Expression<Func<TEntity, bool>> GetExpression(ForeignKey key, object id);

    Task Insert(TEntity obj);
    Task Update(TEntity obj);
    Task Delete(TEntity obj);
  }
}
