using System;
using System.Data.Common;
using System.Linq;
using System.Linq.Expressions;

using Microsoft.EntityFrameworkCore;

using Domain.Interfaces.Repositories;
using Domain.Models;

namespace Infra.Repositories {
  public class CarroceriaRepository : RepositoryBase<Carroceria>, ICarroceriaRepository {
    public CarroceriaRepository(DataContext context) : base(context) { }

    protected override IQueryable<Carroceria> Get(Expression<Func<Carroceria, bool>> condition = null, 
        Func<IQueryable<Carroceria>, IOrderedQueryable<Carroceria>> order = null) {
      try {
        IQueryable<Carroceria> query = _context.Set<Carroceria>().AsNoTracking()
                                           .Include(c => c.Veiculo);
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
