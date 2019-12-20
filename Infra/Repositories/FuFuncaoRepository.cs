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
        IQueryable<FuFuncao> query = _context.Set<FuFuncao>().AsNoTracking()
                                         .Include(f => f.Empresa).Include(f => f.Funcao.Cargo);
        if (condition != null) {
          query = query.Where(condition);
        }
        if (order != null) {
          query = order(query);
        }
        return query;
      }
      catch (DbException ex) {
        throw new Exception(ex.Message);
      }
    }
  }
}
