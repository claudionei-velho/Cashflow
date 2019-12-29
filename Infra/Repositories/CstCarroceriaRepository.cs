using System;
using System.Data.Common;
using System.Linq;
using System.Linq.Expressions;

using Microsoft.EntityFrameworkCore;

using Domain.Interfaces.Repositories;
using Domain.Models;

namespace Infra.Repositories {
  public class CstCarroceriaRepository : RepositoryBase<CstCarroceria>, ICstCarroceriaRepository {
    public CstCarroceriaRepository(DataContext context) : base(context) { }

    protected override IQueryable<CstCarroceria> Get(Expression<Func<CstCarroceria, bool>> condition = null,
        Func<IQueryable<CstCarroceria>, IOrderedQueryable<CstCarroceria>> order = null) {
      try {
        return base.Get(condition, order)
                   .Include(c => c.Empresa).Include(c => c.CVeiculo);
      }
      catch (DbException ex) {
        throw new Exception(ex.Message);
      }
    }
  }
}
