using System;
using System.Data.Common;
using System.Linq;
using System.Linq.Expressions;

using Microsoft.EntityFrameworkCore;

using Domain.Interfaces.Repositories;
using Domain.Models;

namespace Infra.Repositories {
  public class SistDespesaRepository : RepositoryBase<SistDespesa>, ISistDespesaRepository {
    public SistDespesaRepository(DataContext context) : base(context) { }

    protected override IQueryable<SistDespesa> Get(Expression<Func<SistDespesa, bool>> condition = null, 
        Func<IQueryable<SistDespesa>, IOrderedQueryable<SistDespesa>> order = null) {                
      try {
        IQueryable<SistDespesa> query = _context.Set<SistDespesa>().AsNoTracking()
                                            .Include(d => d.ESistema).Include(d => d.Conta);
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
