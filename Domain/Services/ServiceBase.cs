using System;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;

using Domain.Extensions;
using Domain.Interfaces.Repositories;
using Domain.Interfaces.Services;

namespace Domain.Services {
  public class ServiceBase<TEntity> : IServiceBase<TEntity> where TEntity : class {
    private readonly IRepositoryBase<TEntity> _repository;

    public ServiceBase(IRepositoryBase<TEntity> repository) {
      _repository = repository;
    }

    public IQueryable<TEntity> GetData(Expression<Func<TEntity, bool>> condition = null,
        Func<IQueryable<TEntity>, IOrderedQueryable<TEntity>> order = null) {
      return _repository.GetData(condition, order);
    }

    public IQueryable<dynamic> SelectList(Expression<Func<TEntity, dynamic>> columns,
        Expression<Func<TEntity, bool>> condition = null,
        Func<IQueryable<TEntity>, IOrderedQueryable<TEntity>> order = null) {
      return _repository.SelectList(columns, condition, order);
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

    public async Task<int> PagesAsync(Expression<Func<TEntity, bool>> condition = null, int size = 0) {
      try {
        int pages = await _repository.CountAsync(condition) / size;
        if (await _repository.CountAsync(condition) % size > 0) {
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
