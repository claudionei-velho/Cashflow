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
        return base.Get(condition, order).Include(c => c.Empresa)
                   .Include(c => c.Combustivel).AsNoTracking();
      }
      catch (DbException ex) {
        throw new Exception(ex.Message);
      }
    }
  }
}
