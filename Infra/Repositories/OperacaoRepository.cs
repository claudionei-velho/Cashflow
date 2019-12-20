using System;
using System.Data.Common;
using System.Linq;
using System.Linq.Expressions;

using Microsoft.EntityFrameworkCore;

using Domain.Interfaces.Repositories;
using Domain.Models;

namespace Infra.Repositories {
  public class OperacaoRepository : RepositoryBase<Operacao>, IOperacaoRepository {
    public OperacaoRepository(DataContext context) : base(context) { }

    protected override IQueryable<Operacao> Get(Expression<Func<Operacao, bool>> condition = null, 
        Func<IQueryable<Operacao>, IOrderedQueryable<Operacao>> order = null) {                
      try {
        IQueryable<Operacao> query = _context.Set<Operacao>().AsNoTracking()
                                         .Include(o => o.Empresa).Include(o => o.OpLinha);
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
