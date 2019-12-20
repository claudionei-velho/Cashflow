using System;
using System.Data.Common;
using System.Linq;
using System.Linq.Expressions;

using Microsoft.EntityFrameworkCore;

using Domain.Interfaces.Repositories;
using Domain.Models;

namespace Infra.Repositories {
  public class CargoRepository : RepositoryBase<Cargo>, ICargoRepository {
    public CargoRepository(DataContext context) : base(context) { }

    protected override IQueryable<Cargo> Get(Expression<Func<Cargo, bool>> condition = null, 
        Func<IQueryable<Cargo>, IOrderedQueryable<Cargo>> order = null) {                
      try {
        IQueryable<Cargo> query = _context.Set<Cargo>().AsNoTracking()
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
