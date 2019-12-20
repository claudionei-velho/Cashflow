using System;
using System.Data.Common;
using System.Linq;
using System.Linq.Expressions;

using Microsoft.EntityFrameworkCore;

using Domain.Interfaces.Repositories;
using Domain.Models;

namespace Infra.Repositories {
  public class TurnoRepository : RepositoryBase<Turno>, ITurnoRepository {
    public TurnoRepository(DataContext context) : base(context) { }

    protected override IQueryable<Turno> Get(Expression<Func<Turno, bool>> condition = null, 
        Func<IQueryable<Turno>, IOrderedQueryable<Turno>> order = null) {                
      try {
        IQueryable<Turno> query = _context.Set<Turno>().AsNoTracking()
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
