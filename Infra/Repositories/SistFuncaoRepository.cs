using System;
using System.Data.Common;
using System.Linq;
using System.Linq.Expressions;

using Microsoft.EntityFrameworkCore;

using Domain.Interfaces.Repositories;
using Domain.Models;

namespace Infra.Repositories {
  public class SistFuncaoRepository : RepositoryBase<SistFuncao>, ISistFuncaoRepository {
    public SistFuncaoRepository(DataContext context) : base(context) { }

    protected override IQueryable<SistFuncao> Get(Expression<Func<SistFuncao, bool>> condition = null, 
        Func<IQueryable<SistFuncao>, IOrderedQueryable<SistFuncao>> order = null) {                
      try {
        return base.Get(condition, order).Include(f => f.ESistema)
                   .Include(f => f.Funcao).AsNoTracking();
      }
      catch (DbException ex) {
        throw new Exception(ex.Message);
      }
    }
  }
}
