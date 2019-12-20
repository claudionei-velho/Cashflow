using System;
using System.Data.Common;
using System.Linq;
using System.Linq.Expressions;

using Microsoft.EntityFrameworkCore;

using Domain.Interfaces.Repositories;
using Domain.Models;

namespace Infra.Repositories {
  public class PSinteseRepository : RepositoryBase<PSintese>, IPSinteseRepository {
    public PSinteseRepository(DataContext context) : base(context) { }

    protected override IQueryable<PSintese> Get(Expression<Func<PSintese, bool>> condition = null, 
        Func<IQueryable<PSintese>, IOrderedQueryable<PSintese>> order = null) {
      try {
        IQueryable<PSintese> query = _context.Set<PSintese>().AsNoTracking()
                                         .Include(p => p.Empresa);
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
