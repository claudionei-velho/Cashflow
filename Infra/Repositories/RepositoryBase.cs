using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Common;
using System.Linq;
using System.Linq.Expressions;
using System.Reflection;
using System.Threading.Tasks;

using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.ChangeTracking;

using Domain.Interfaces.Repositories;

namespace Infra.Repositories {
  public class RepositoryBase<TEntity> : IRepositoryBase<TEntity> where TEntity : class {
    protected readonly DataContext _context;

    public RepositoryBase(DataContext context) {
      _context = context;
    }

    public IQueryable<TEntity> Query(Expression<Func<TEntity, bool>> condition = null, 
        Func<IQueryable<TEntity>, IOrderedQueryable<TEntity>> order = null) {
      return Get(condition, order);
    }

    public IEnumerable<TEntity> List(Expression<Func<TEntity, bool>> condition = null,
        Func<IQueryable<TEntity>, IOrderedQueryable<TEntity>> order = null) {
      return Get(condition, order).ToList();
    }

    public async Task<IEnumerable<TEntity>> ListAsync(Expression<Func<TEntity, bool>> condition = null,
        Func<IQueryable<TEntity>, IOrderedQueryable<TEntity>> order = null) {
      return await Get(condition, order).ToListAsync();
    }

    public IEnumerable<dynamic> SelectList(Expression<Func<TEntity, dynamic>> columns,
        Expression<Func<TEntity, bool>> condition = null,
        Func<IQueryable<TEntity>, IOrderedQueryable<TEntity>> order = null) {
      return Get(condition, order).Select(columns).ToList();
    }

    public async Task<IEnumerable<dynamic>> SelectListAsync(Expression<Func<TEntity, dynamic>> columns,
        Expression<Func<TEntity, bool>> condition = null,
        Func<IQueryable<TEntity>, IOrderedQueryable<TEntity>> order = null) {
      return await Get(condition, order).Select(columns).ToListAsync();
    }

    public IEnumerable<TEntity> PagedList(Expression<Func<TEntity, bool>> condition = null,
        Func<IQueryable<TEntity>, IOrderedQueryable<TEntity>> order = null, int skip = 1, int take = 8) {
      if (skip < 1 || take < 1) {
        return Get(condition, order).ToList();
      }
      return Get(condition, order).Skip((skip - 1) * take).Take(take).ToList();
    }

    public async Task<IEnumerable<TEntity>> PagedListAsync(Expression<Func<TEntity, bool>> condition = null,
        Func<IQueryable<TEntity>, IOrderedQueryable<TEntity>> order = null, int skip = 1, int take = 8) {
      if (skip < 1 || take < 1) {
        return await Get(condition, order).ToListAsync();
      }
      return await Get(condition, order).Skip((skip - 1) * take).Take(take).ToListAsync();
    }

    public virtual TEntity GetById(int id) {
      try {
        return _context.Set<TEntity>().Find(id);
      }
      catch (DbException ex) {
        throw new Exception(ex.Message);
      }
    }

    public virtual TEntity GetById(object id) {
      try {
        return _context.Set<TEntity>().Find(id);
      }
      catch (DbException ex) {
        throw new Exception(ex.Message);
      }
    }

    public virtual async Task<TEntity> GetByIdAsync(int id) {
      try {
        return await _context.Set<TEntity>().FindAsync(id);
      }
      catch (DbException ex) {
        throw new Exception(ex.Message);
      }
    }

    public virtual async Task<TEntity> GetByIdAsync(object id) {
      try {
        return await _context.Set<TEntity>().FindAsync(id);
      }
      catch (DbException ex) {
        throw new Exception(ex.Message);
      }
    }

    public virtual TEntity GetFirst(Expression<Func<TEntity, bool>> condition) {
      return Get().FirstOrDefault(condition);
    }

    public virtual async Task<TEntity> GetFirstAsync(Expression<Func<TEntity, bool>> condition) {
      return await Get().FirstOrDefaultAsync(condition);
    }

    public virtual bool Exists(Expression<Func<TEntity, bool>> condition) {
      try {
        return _context.Set<TEntity>().Any(condition);
      }
      catch (DbException ex) {
        throw new Exception(ex.Message);
      }
    }

    public virtual async Task<bool> ExistsAsync(Expression<Func<TEntity, bool>> condition) {
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
        catch (ConstraintException ex) {
          throw new Exception(ex.Message);
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
        catch (DeletedRowInaccessibleException ex) {
          throw new Exception(ex.Message);
        }
        catch (DbException ex) {
          throw new Exception(ex.Message);
        }
      }
    }

    public async Task AddOrUpdate(TEntity obj) {
      EntityEntry<TEntity> entityEntry = _context.Entry(obj);
      string primaryKeyName = entityEntry.Context.Model.FindEntityType(typeof(TEntity))
                                  .FindPrimaryKey().Properties.Select(x => x.Name).Single();

      PropertyInfo primaryKeyField = obj.GetType().GetProperty(primaryKeyName);
      if (primaryKeyField == null) {
        throw new Exception($"{typeof(TEntity).FullName} does not have a primary key specified. Unable to exec AddOrUpdate call.");
      }
      object keyVal = primaryKeyField.GetValue(obj);

      TEntity dbVal = await _context.Set<TEntity>().FindAsync(keyVal);
      if (dbVal != null) {
        _context.Entry(dbVal).CurrentValues.SetValues(obj);
        await Update(dbVal);
      }
      else {
        await Insert(obj);
      }
    }

    protected virtual IQueryable<TEntity> Get(Expression<Func<TEntity, bool>> condition = null,
        Func<IQueryable<TEntity>, IOrderedQueryable<TEntity>> order = null) {
      try {
        IQueryable<TEntity> query = _context.Set<TEntity>();
        if (condition != null) {
          query = query.Where(condition);
        }
        if (order != null) {
          return order(query);
        }
        return query;
      }
      catch (DbException ex) {
        throw new Exception(ex.Message);
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
