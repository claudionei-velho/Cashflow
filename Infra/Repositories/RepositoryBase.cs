using System;
using System.Data.Common;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;

using Microsoft.EntityFrameworkCore;

using Domain.Interfaces.Repositories;

namespace Infra.Repositories {
  public class RepositoryBase<TEntity> : IRepositoryBase<TEntity> where TEntity : class {
    protected readonly DataContext _context;

    public RepositoryBase(DataContext context) {
      _context = context;
    }

    protected virtual IQueryable<TEntity> Get(Expression<Func<TEntity, bool>> condition = null,
        Func<IQueryable<TEntity>, IOrderedQueryable<TEntity>> order = null) {
      try {
        IQueryable<TEntity> query = _context.Set<TEntity>().AsNoTracking();
        if (condition != null) {
          query = query.Where(condition);
        }
        if (order != null) {
          query = order(query);
        }
        return query;
      }
      catch (DbException ex) {
        throw new Exception(ex.Message);
      }
    }

    public IQueryable<TEntity> GetData(Expression<Func<TEntity, bool>> condition = null,
        Func<IQueryable<TEntity>, IOrderedQueryable<TEntity>> order = null) {
      return Get(condition, order);
    }

    public IQueryable<dynamic> SelectList(Expression<Func<TEntity, dynamic>> columns,
        Expression<Func<TEntity, bool>> condition = null,
        Func<IQueryable<TEntity>, IOrderedQueryable<TEntity>> order = null) {
      return Get(condition, order).Select(columns);
    }

    public TEntity GetById(int id) {
      try {
        return _context.Set<TEntity>().Find(id);
      }
      catch (DbException ex) {
        throw new Exception(ex.Message);
      }
    }

    public TEntity GetById(object id) {
      try {
        return _context.Set<TEntity>().Find(id);
      }
      catch (DbException ex) {
        throw new Exception(ex.Message);
      }
    }

    public async Task<TEntity> GetByIdAsync(int id) {
      try {
        return await _context.Set<TEntity>().FindAsync(id);
      }
      catch (DbException ex) {
        throw new Exception(ex.Message);
      }
    }

    public async Task<TEntity> GetByIdAsync(object id) {
      try {
        return await _context.Set<TEntity>().FindAsync(id);
      }
      catch (DbException ex) {
        throw new Exception(ex.Message);
      }
    }

    public TEntity GetFirst(Expression<Func<TEntity, bool>> condition = null) {
      return Get(condition).FirstOrDefault();
    }

    public async Task<TEntity> GetFirstAsync(Expression<Func<TEntity, bool>> condition = null) {
      return await Get(condition).FirstOrDefaultAsync();
    }

    public bool Exists(Expression<Func<TEntity, bool>> condition) {
      try {
        return _context.Set<TEntity>().Any(condition);
      }
      catch (DbException ex) {
        throw new Exception(ex.Message);
      }
    }

    public async Task<bool> ExistsAsync(Expression<Func<TEntity, bool>> condition) {
      try {
        return await _context.Set<TEntity>().AnyAsync(condition);
      }
      catch (DbException ex) {
        throw new Exception(ex.Message);
      }
    }

    public int Count(Expression<Func<TEntity, bool>> condition = null) {
      return Get(condition).Count();
    }

    public async Task<int> CountAsync(Expression<Func<TEntity, bool>> condition = null) {
      return await Get(condition).CountAsync();
    }

    public async Task Insert(TEntity obj) {
      await using (_context) {
        try {
          _context.Set<TEntity>().Add(obj);
          await _context.SaveChangesAsync();
        }
        catch (DbException ex) {
          throw new Exception(ex.Message);
        }
      }
    }

    public async Task Update(TEntity obj) {
      await using (_context) {
        try {
          _context.Entry(obj).State = EntityState.Modified;
          await _context.SaveChangesAsync();
        }
        catch (DbUpdateConcurrencyException ex) {
          throw new Exception(ex.Message);
        }
        catch (DbException ex) {
          throw new Exception(ex.Message);
        }
      }
    }

    public async Task Delete(TEntity obj) {
      await using (_context) {
        try {
          _context.Set<TEntity>().Remove(obj);
          await _context.SaveChangesAsync();
        }
        catch (DbException ex) {
          throw new Exception(ex.Message);
        }
      }
    }

    #region IDisposable Support
    private bool disposedValue;    // To detect redundant calls

    protected virtual void Dispose(bool disposing) {
      if (!disposedValue) {
        if (disposing) {
          _context.Dispose();
        }
        disposedValue = true;
      }
    }

    ~RepositoryBase() {
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
