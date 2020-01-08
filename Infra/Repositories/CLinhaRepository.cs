using System;
using System.Data.Common;
using System.Linq;
using System.Linq.Expressions;

using Microsoft.EntityFrameworkCore;

using Domain.Interfaces.Repositories;
using Domain.Models;

namespace Infra.Repositories {
  public class CLinhaRepository : RepositoryBase<CLinha>, ICLinhaRepository {
    public CLinhaRepository(DataContext context) : base(context) { }

    protected override IQueryable<CLinha> Get(Expression<Func<CLinha, bool>> condition = null, 
        Func<IQueryable<CLinha>, IOrderedQueryable<CLinha>> order = null) {
      try {
        return base.Get(condition, order).Include(c => c.Empresa)
                   .Include(c => c.ClassLinha).AsNoTracking();
      }
      catch (DbException ex) {
        throw new Exception(ex.Message);
      }
    }
  }
}
