using System;
using System.Data.Common;
using System.Linq;
using System.Linq.Expressions;

using Microsoft.EntityFrameworkCore;

using Domain.Interfaces.Repositories;
using Domain.Models;

namespace Infra.Repositories {
  public class TarifaRepository : RepositoryBase<Tarifa>, ITarifaRepository {
    public TarifaRepository(DataContext context) : base(context) { }

    protected override IQueryable<Tarifa> Get(Expression<Func<Tarifa, bool>> condition = null, 
        Func<IQueryable<Tarifa>, IOrderedQueryable<Tarifa>> order = null) {                
      try {
        IQueryable<Tarifa> query = _context.Set<Tarifa>().AsNoTracking()
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
