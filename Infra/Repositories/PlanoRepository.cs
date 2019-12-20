using System;
using System.Data.Common;
using System.Linq;
using System.Linq.Expressions;

using Microsoft.EntityFrameworkCore;

using Domain.Interfaces.Repositories;
using Domain.Models;

namespace Infra.Repositories {
  public class PlanoRepository : RepositoryBase<Plano>, IPlanoRepository {
    public PlanoRepository(DataContext context) : base(context) { }

    protected override IQueryable<Plano> Get(Expression<Func<Plano, bool>> condition = null, 
        Func<IQueryable<Plano>, IOrderedQueryable<Plano>> order = null) {
      try {
        IQueryable<Plano> query = _context.Set<Plano>().AsNoTracking()
                                      .Include(p => p.Linha).Include(p => p.Atendimento);
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
