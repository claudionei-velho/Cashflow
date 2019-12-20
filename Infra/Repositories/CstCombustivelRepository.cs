using System;
using System.Data.Common;
using System.Linq;
using System.Linq.Expressions;

using Microsoft.EntityFrameworkCore;

using Domain.Interfaces.Repositories;
using Domain.Models;

namespace Infra.Repositories {
  public class CstCombustivelRepository : RepositoryBase<CstCombustivel>, ICstCombustivelRepository {
    public CstCombustivelRepository(DataContext context) : base(context) { }

    protected override IQueryable<CstCombustivel> Get(Expression<Func<CstCombustivel, bool>> condition = null,
        Func<IQueryable<CstCombustivel>, IOrderedQueryable<CstCombustivel>> order = null) {
      try {
        IQueryable<CstCombustivel> query = _context.Set<CstCombustivel>().AsNoTracking()
                                               .Include(c => c.Empresa).Include(c => c.Combustivel);
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
