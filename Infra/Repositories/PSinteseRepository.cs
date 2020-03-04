using System;
using System.Data.Common;
using System.Linq;
using System.Linq.Expressions;

using Microsoft.EntityFrameworkCore;

using Domain.Interfaces.Repositories;
using Domain.Models;

namespace Infra.Repositories {
  public class PSinteseRepository : RepositoryBase<PSintese>, IPSinteseRepository {
    public PSinteseRepository(DataContext context) : base(context) { }

    protected override IQueryable<PSintese> Get(Expression<Func<PSintese, bool>> condition = null,
        Func<IQueryable<PSintese>, IOrderedQueryable<PSintese>> order = null) {
      try {
        return base.Get(condition, order).Include(p => p.Empresa);
      }
      catch (DbException ex) {
        throw new Exception(ex.Message);
      }
    }
  }
}
