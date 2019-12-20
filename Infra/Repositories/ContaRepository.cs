using System;
using System.Data.Common;
using System.Linq;
using System.Linq.Expressions;

using Microsoft.EntityFrameworkCore;

using Domain.Interfaces.Repositories;
using Domain.Models;

namespace Infra.Repositories {
  public class ContaRepository : RepositoryBase<Conta>, IContaRepository {
    public ContaRepository(DataContext context) : base(context) { }

    protected override IQueryable<Conta> Get(Expression<Func<Conta, bool>> condition = null, 
        Func<IQueryable<Conta>, IOrderedQueryable<Conta>> order = null) {
      try {
        IQueryable<Conta> query = _context.Set<Conta>().AsNoTracking()
                                      .Include(c => c.Empresa).Include(c => c.Vinculo);
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
