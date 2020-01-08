using System;
using System.Data.Common;
using System.Linq;
using System.Linq.Expressions;

using Microsoft.EntityFrameworkCore;

using Domain.Interfaces.Repositories;
using Domain.Models;

namespace Infra.Repositories {
  public class PremissaRepository : RepositoryBase<Premissa>, IPremissaRepository {
    public PremissaRepository(DataContext context) : base(context) { }

    protected override IQueryable<Premissa> Get(Expression<Func<Premissa, bool>> condition = null,
        Func<IQueryable<Premissa>, IOrderedQueryable<Premissa>> order = null) {
      try {
        return base.Get(condition, order).Include(p => p.Empresa).AsNoTracking();
      }
      catch (DbException ex) {
        throw new Exception(ex.Message);
      }
    }
  }
}
