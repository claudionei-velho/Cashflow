using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;

using Domain.Interfaces.Repositories;
using Domain.Interfaces.Services;

namespace Domain.Services {
  public class ServiceBase<TEntity> : IServiceBase<TEntity> where TEntity : class {
    private readonly IRepositoryBase<TEntity> _repository;

    public ServiceBase(IRepositoryBase<TEntity> repository) {
      _repository = repository;
    }

    public IQueryable<TEntity> Query(Expression<Func<TEntity, bool>> condition = null, 
        Func<IQueryable<TEntity>, IOrderedQueryable<TEntity>> order = null) {
      return _repository.Query(condition, order);
    }

    public IEnumerable<TEntity> List(Expression<Func<TEntity, bool>> condition = null, 
      Func<IQueryable<TEntity>, IOrderedQueryable<TEntity>> order = null) {
      return _repository.List(condition, order);
    }

    public async Task<IEnumerable<TEntity>> ListAsync(Expression<Func<TEntity, bool>> condition = null,
        Func<IQueryable<TEntity>, IOrderedQueryable<TEntity>> order = null) {
      return await _repository.ListAsync(condition, order);
    }

    public IEnumerable<dynamic> SelectList(Expression<Func<TEntity, dynamic>> columns, 
        Expression<Func<TEntity, bool>> condition = null, 
        Func<IQueryable<TEntity>, IOrderedQueryable<TEntity>> order = null) {
      return _repository.SelectList(columns, condition, order);
    }

    public async Task<IEnumerable<dynamic>> SelectListAsync(Expression<Func<TEntity, dynamic>> columns,
        Expression<Func<TEntity, bool>> condition = null,
        Func<IQueryable<TEntity>, IOrderedQueryable<TEntity>> order = null) {
      return await _repository.SelectListAsync(columns, condition, order);
    }

    public IEnumerable<TEntity> PagedList(Expression<Func<TEntity, bool>> condition = null, 
        Func<IQueryable<TEntity>, IOrderedQueryable<TEntity>> order = null, int skip = 1, int take = 8) {
      return _repository.PagedList(condition, order);
    }

    public async Task<IEnumerable<TEntity>> PagedListAsync(Expression<Func<TEntity, bool>> condition = null,
        Func<IQueryable<TEntity>, IOrderedQueryable<TEntity>> order = null, int skip = 1, int take = 8) {
      return await _repository.PagedListAsync(condition, order, skip, take);
    }

    public TEntity GetById(int id) {
      return _repository.GetById(id);
    }

    public TEntity GetById(object id) {
      return _repository.GetById(id);
    }

    public async Task<TEntity> GetByIdAsync(int id) {
      return await _repository.GetByIdAsync(id);
    }

    public async Task<TEntity> GetByIdAsync(object id) {
      return await _repository.GetByIdAsync(id);
    }

    public TEntity GetFirst(Expression<Func<TEntity, bool>> condition) {
      return _repository.GetFirst(condition);
    }

    public async Task<TEntity> GetFirstAsync(Expression<Func<TEntity, bool>> condition) {
      return await _repository.GetFirstAsync(condition);
    }

    public bool Exists(Expression<Func<TEntity, bool>> condition) {
      return _repository.Exists(condition);
    }

    public async Task<bool> ExistsAsync(Expression<Func<TEntity, bool>> condition) {
      return await _repository.ExistsAsync(condition);
    }

    public int Count(Expression<Func<TEntity, bool>> condition = null) {
      return _repository.Count(condition);
    }

    public async Task<int> CountAsync(Expression<Func<TEntity, bool>> condition = null) {
      return await _repository.CountAsync(condition);
    }

    public int Pages(Expression<Func<TEntity, bool>> condition = null, int size = 0) {
      try {
        int pages = _repository.Count(condition) / size;
        if (_repository.Count(condition) % size > 0) {
          ++pages;
        }
        return pages;
      }
      catch (DivideByZeroException ex) {
        throw new Exception(ex.Message);
      }
    }

    public async Task Insert(TEntity obj) {
      try {
        await _repository.Insert(obj);
      }
      catch (Exception ex) {
        throw new Exception(ex.Message);
      }
    }

    public async Task Update(TEntity obj) {
      try {
        await _repository.Update(obj);
      }
      catch (Exception ex) {
        throw new Exception(ex.Message);
      }
    }

    public async Task Delete(TEntity obj) {
      try {
        await _repository.Delete(obj);
      }
      catch (Exception ex) {
        throw new Exception(ex.Message);
      }
    }

    public async Task AddOrUpdate(TEntity obj) {
      try {
        await _repository.AddOrUpdate(obj);
      }
      catch (Exception ex) {
        throw new Exception(ex.Message);
      }
    }

    #region IDisposable Support
    private bool disposedValue;    // To detect redundant calls

    protected virtual void Dispose(bool disposing) {
      if (!disposedValue) {
        if (disposing) {
          _repository.Dispose();
        }
        disposedValue = true;
      }
    }

    ~ServiceBase() {
      Dispose(false);
    }

    // This code added to correctly implement the disposable pattern.
    public void Dispose() {
      Dispose(true);
      GC.SuppressFinalize(this);
    }
    #endregion
  }
}
