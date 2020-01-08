using System;
using System.Data.Common;
using System.Linq;
using System.Linq.Expressions;

using Microsoft.EntityFrameworkCore;

using Domain.Interfaces.Repositories;
using Domain.Models;

namespace Infra.Repositories {
  public class PlanoRepository : RepositoryBase<Plano>, IPlanoRepository {
    public PlanoRepository(DataContext context) : base(context) { }

    protected override IQueryable<Plano> Get(Expression<Func<Plano, bool>> condition = null, 
        Func<IQueryable<Plano>, IOrderedQueryable<Plano>> order = null) {
      try {
        return base.Get(condition, order).Include(p => p.Linha)
                   .Include(p => p.Atendimento).AsNoTracking();
      }
      catch (DbException ex) {
        throw new Exception(ex.Message);
      }
    }
  }
}
