using System;
using System.Data.Common;
using System.Linq;
using System.Linq.Expressions;

using Microsoft.EntityFrameworkCore;

using Domain.Interfaces.Repositories;
using Domain.Models;

namespace Infra.Repositories {
  public class FuncaoRepository : RepositoryBase<Funcao>, IFuncaoRepository {
    public FuncaoRepository(DataContext context) : base(context) { }

    protected override IQueryable<Funcao> Get(Expression<Func<Funcao, bool>> condition = null, 
        Func<IQueryable<Funcao>, IOrderedQueryable<Funcao>> order = null) {                
      try {
        IQueryable<Funcao> query = _context.Set<Funcao>().AsNoTracking()
                                       .Include(f => f.Cargo).Include(f => f.Departamento)
                                       .Include(f => f.Centro).Include(f => f.Conta);
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
