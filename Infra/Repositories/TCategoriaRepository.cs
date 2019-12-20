using System;
using System.Data.Common;
using System.Linq;
using System.Linq.Expressions;

using Microsoft.EntityFrameworkCore;

using Domain.Interfaces.Repositories;
using Domain.Models;

namespace Infra.Repositories {
  public class TCategoriaRepository : RepositoryBase<TCategoria>, ITCategoriaRepository {
    public TCategoriaRepository(DataContext context) : base(context) { }

    protected override IQueryable<TCategoria> Get(Expression<Func<TCategoria, bool>> condition = null, 
        Func<IQueryable<TCategoria>, IOrderedQueryable<TCategoria>> order = null) {
      try {
        IQueryable<TCategoria> query = _context.Set<TCategoria>().AsNoTracking()
                                           .Include(c => c.Empresa);
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
  }
}
