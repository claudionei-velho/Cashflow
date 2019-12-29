using System;
using System.Data.Common;
using System.Linq;
using System.Linq.Expressions;

using Microsoft.EntityFrameworkCore;

using Domain.Interfaces.Repositories;
using Domain.Models;

namespace Infra.Repositories {
  public class TarifaModRepository : RepositoryBase<TarifaMod>, ITarifaModRepository {
    public TarifaModRepository(DataContext context) : base(context) { }

    protected override IQueryable<TarifaMod> Get(Expression<Func<TarifaMod, bool>> condition = null, 
        Func<IQueryable<TarifaMod>, IOrderedQueryable<TarifaMod>> order = null) {                
      try {
        return base.Get(condition, order).Include(t => t.Empresa);
      }
      catch (DbException ex) {
        throw new Exception(ex.Message);
      }
    }
  }
}
