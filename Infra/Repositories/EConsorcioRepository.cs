using System;
using System.Data.Common;
using System.Linq;
using System.Linq.Expressions;

using Microsoft.EntityFrameworkCore;

using Domain.Interfaces.Repositories;
using Domain.Models;

namespace Infra.Repositories {
  public class EConsorcioRepository : RepositoryBase<EConsorcio>, IEConsorcioRepository {
    public EConsorcioRepository(DataContext context) : base(context) { }

    public decimal? TotalRatio(Expression<Func<EConsorcio, bool>> condition = null) {
      return Get(condition).Sum(p => p.Ratio);
    }

    protected override IQueryable<EConsorcio> Get(Expression<Func<EConsorcio, bool>> condition = null, 
        Func<IQueryable<EConsorcio>, IOrderedQueryable<EConsorcio>> order = null) {
      try {
        return base.Get(condition, order)
                   .Include(c => c.Consorcio).Include(c => c.Empresa)
                   .Where(e => e.Ativo);
      }
      catch (DbException ex) {
        throw new Exception(ex.Message);
      }
    }
  }
}
