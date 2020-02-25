using System;
using System.Data.Common;
using System.Linq;
using System.Linq.Expressions;

using Microsoft.EntityFrameworkCore;

using Domain.Interfaces.Repositories;
using Domain.Models;

namespace Infra.Repositories {
  public class FuFuncaoRepository : RepositoryBase<FuFuncao>, IFuFuncaoRepository {
    public FuFuncaoRepository(DataContext context) : base(context) { }

    protected override IQueryable<FuFuncao> Get(Expression<Func<FuFuncao, bool>> condition = null, 
        Func<IQueryable<FuFuncao>, IOrderedQueryable<FuFuncao>> order = null) {                
      try {
        return base.Get(condition, order).Include(f => f.Empresa).Include(f => f.Funcao.Cargo);
      }
      catch (DbException ex) {
        throw new Exception(ex.Message);
      }
    }
  }
}
