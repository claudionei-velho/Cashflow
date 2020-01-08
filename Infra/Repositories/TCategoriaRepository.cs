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
        return base.Get(condition, order).Include(c => c.Empresa).AsNoTracking();
      }
      catch (DbException ex) {
        throw new Exception(ex.Message);
      }
    }
  }
}
