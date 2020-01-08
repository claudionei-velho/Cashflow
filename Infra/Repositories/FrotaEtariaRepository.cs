using System;
using System.Data.Common;
using System.Linq;
using System.Linq.Expressions;

using Microsoft.EntityFrameworkCore;

using Domain.Interfaces.Repositories;
using Domain.Models;

namespace Infra.Repositories {
  public class FrotaEtariaRepository : RepositoryBase<FrotaEtaria>, IFrotaEtariaRepository {
    public FrotaEtariaRepository(DataContext context) : base(context) { }

    public decimal? FrotaIdade(Expression<Func<FrotaEtaria, bool>> condition = null) {
      try {
        return base.Get(condition).Sum(p => p.EqvIdade) / base.Get(condition).Sum(p => p.Frota);
      }
      catch (DivideByZeroException) {
        return null;
      }
    }

    protected override IQueryable<FrotaEtaria> Get(Expression<Func<FrotaEtaria, bool>> condition = null, 
        Func<IQueryable<FrotaEtaria>, IOrderedQueryable<FrotaEtaria>> order = null) {
      try {
        return base.Get(condition, order).Include(f => f.Empresa)
                   .Include(f => f.FxEtaria).AsNoTracking();
      }
      catch (DbException ex) {
        throw new Exception(ex.Message);
      }
    }
  }
}
