using System;
using System.Data.Common;
using System.Linq;
using System.Linq.Expressions;

using Microsoft.EntityFrameworkCore;

using Domain.Interfaces.Repositories;
using Domain.Models;

namespace Infra.Repositories {
  public class TarifaModRepository : RepositoryBase<TarifaMod>, ITarifaModRepository {
    public TarifaModRepository(DataContext context) : base(context) { }

    protected override IQueryable<TarifaMod> Get(Expression<Func<TarifaMod, bool>> condition = null, 
        Func<IQueryable<TarifaMod>, IOrderedQueryable<TarifaMod>> order = null) {                
      try {
        IQueryable<TarifaMod> query = _context.Set<TarifaMod>().AsNoTracking()
                                          .Include(t => t.Empresa);
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
