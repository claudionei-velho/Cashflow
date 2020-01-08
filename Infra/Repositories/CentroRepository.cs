using System;
using System.Data.Common;
using System.Linq;
using System.Linq.Expressions;

using Microsoft.EntityFrameworkCore;

using Domain.Interfaces.Repositories;
using Domain.Models;

namespace Infra.Repositories {
  public class CentroRepository : RepositoryBase<Centro>, ICentroRepository {
    public CentroRepository(DataContext context) : base(context) { }

    protected override IQueryable<Centro> Get(Expression<Func<Centro, bool>> condition = null, 
        Func<IQueryable<Centro>, IOrderedQueryable<Centro>> order = null) {
      try {
        return base.Get(condition, order).Include(c => c.Empresa)
                   .Include(c => c.Vinculo).AsNoTracking();
      }
      catch (DbException ex) {
        throw new Exception(ex.Message);
      }
    }
  }
}
