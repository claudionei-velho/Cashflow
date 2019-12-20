using System;
using System.Data.Common;
using System.Linq;
using System.Linq.Expressions;

using Microsoft.EntityFrameworkCore;

using Domain.Interfaces.Repositories;
using Domain.Models;

namespace Infra.Repositories {
  public class CstCarroceriaRepository : RepositoryBase<CstCarroceria>, ICstCarroceriaRepository {
    public CstCarroceriaRepository(DataContext context) : base(context) { }

    protected override IQueryable<CstCarroceria> Get(Expression<Func<CstCarroceria, bool>> condition = null,
        Func<IQueryable<CstCarroceria>, IOrderedQueryable<CstCarroceria>> order = null) {
      try {
        IQueryable<CstCarroceria> query = _context.Set<CstCarroceria>().AsNoTracking()
                                              .Include(c => c.Empresa).Include(c => c.CVeiculo);
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
