using System;
using System.Data.Common;
using System.Linq;
using System.Linq.Expressions;

using Microsoft.EntityFrameworkCore;

using Domain.Interfaces.Repositories;
using Domain.Models;

namespace Infra.Repositories {
  public class ESistemaRepository : RepositoryBase<ESistema>, IESistemaRepository {
    public ESistemaRepository(DataContext context) : base(context) { }

    protected override IQueryable<ESistema> Get(Expression<Func<ESistema, bool>> condition = null, 
        Func<IQueryable<ESistema>, IOrderedQueryable<ESistema>> order = null) {                
      try {
        return base.Get(condition, order)
                   .Include(e => e.Empresa).Include(e => e.Sistema);
      }
      catch (DbException ex) {
        throw new Exception(ex.Message);
      }
    }
  }
}
