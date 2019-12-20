using System;
using System.Data.Common;
using System.Linq;
using System.Linq.Expressions;

using Microsoft.EntityFrameworkCore;

using Domain.Interfaces.Repositories;
using Domain.Models;

namespace Infra.Repositories {
  public class FrotaHoraRepository : RepositoryBase<FrotaHora>, IFrotaHoraRepository {
    public FrotaHoraRepository(DataContext context) : base(context) { }

    protected override IQueryable<FrotaHora> Get(Expression<Func<FrotaHora, bool>> condition = null, 
        Func<IQueryable<FrotaHora>, IOrderedQueryable<FrotaHora>> order = null) {
      try {
        IQueryable<FrotaHora> query = _context.Set<FrotaHora>().AsNoTracking()
                                          .Include(f => f.Empresa);
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
