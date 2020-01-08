using System;
using System.Data.Common;
using System.Linq;
using System.Linq.Expressions;

using Microsoft.EntityFrameworkCore;

using Domain.Interfaces.Repositories;
using Domain.Models;

namespace Infra.Repositories {
  public class TarifaRepository : RepositoryBase<Tarifa>, ITarifaRepository {
    public TarifaRepository(DataContext context) : base(context) { }

    protected override IQueryable<Tarifa> Get(Expression<Func<Tarifa, bool>> condition = null, 
        Func<IQueryable<Tarifa>, IOrderedQueryable<Tarifa>> order = null) {                
      try {
        return base.Get(condition, order).Include(t => t.Empresa).AsNoTracking();
      }
      catch (DbException ex) {
        throw new Exception(ex.Message);
      }
    }
  }
}
