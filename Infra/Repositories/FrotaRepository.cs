using System;
using System.Data.Common;
using System.Linq;
using System.Linq.Expressions;

using Microsoft.EntityFrameworkCore;

using Domain.Interfaces.Repositories;
using Domain.Models;

namespace Infra.Repositories {
  public class FrotaRepository : RepositoryBase<Frota>, IFrotaRepository {
    public FrotaRepository(DataContext context) : base(context) { }

    protected override IQueryable<Frota> Get(Expression<Func<Frota, bool>> condition = null, 
        Func<IQueryable<Frota>, IOrderedQueryable<Frota>> order = null) {
      try {
        return base.Get(condition, order).Include(f => f.Empresa)
                   .Include(f => f.CVeiculo).Include(f => f.FxEtaria).AsNoTracking();
      }
      catch (DbException ex) {
        throw new Exception(ex.Message);
      }
    }
  }
}
