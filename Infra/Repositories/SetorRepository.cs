using System;
using System.Data.Common;
using System.Linq;
using System.Linq.Expressions;

using Microsoft.EntityFrameworkCore;

using Domain.Interfaces.Repositories;
using Domain.Models;

namespace Infra.Repositories {
  public class SetorRepository : RepositoryBase<Setor>, ISetorRepository {
    public SetorRepository(DataContext context) : base(context) { }

    protected override IQueryable<Setor> Get(Expression<Func<Setor, bool>> condition = null, 
        Func<IQueryable<Setor>, IOrderedQueryable<Setor>> order = null) {                
      try {
        IQueryable<Setor> query = _context.Set<Setor>().AsNoTracking().Include(s => s.Empresa)
                                      .Include(s => s.Cargo).Include(s => s.Vinculo);
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
