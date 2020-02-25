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
        return base.Get(condition, order).Include(p => p.Empresa).Include(p => p.CVeiculo);
      }
      catch (DbException ex) {
        throw new Exception(ex.Message);
      }
    }
  }
}
