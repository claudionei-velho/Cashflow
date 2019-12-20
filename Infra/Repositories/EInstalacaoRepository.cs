using System;
using System.Data.Common;
using System.Linq;
using System.Linq.Expressions;

using Microsoft.EntityFrameworkCore;

using Domain.Interfaces.Repositories;
using Domain.Models;

namespace Infra.Repositories {
  public class EInstalacaoRepository : RepositoryBase<EInstalacao>, IEInstalacaoRepository {
    public EInstalacaoRepository(DataContext context) : base(context) { }

    protected override IQueryable<EInstalacao> Get(Expression<Func<EInstalacao, bool>> condition = null, 
        Func<IQueryable<EInstalacao>, IOrderedQueryable<EInstalacao>> order = null) {                
      try {
        IQueryable<EInstalacao> query = _context.Set<EInstalacao>().AsNoTracking().Include(e => e.Instalacao)
                                            .Include(e => e.FInstalacao).Include(e => e.Conta);
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
