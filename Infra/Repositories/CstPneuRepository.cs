using System;
using System.Data.Common;
using System.Linq;
using System.Linq.Expressions;

using Microsoft.EntityFrameworkCore;

using Domain.Interfaces.Repositories;
using Domain.Models;

namespace Infra.Repositories {
  public class CstPneuRepository : RepositoryBase<CstPneu>, ICstPneuRepository {
    public CstPneuRepository(DataContext context) : base(context) { }

    protected override IQueryable<CstPneu> Get(Expression<Func<CstPneu, bool>> condition = null,
        Func<IQueryable<CstPneu>, IOrderedQueryable<CstPneu>> order = null) {
      try {
        IQueryable<CstPneu> query = _context.Set<CstPneu>().AsNoTracking()
                                        .Include(p => p.Empresa).Include(p => p.CVeiculo);
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
