using System;
using System.Data.Common;
using System.Linq;
using System.Linq.Expressions;

using Microsoft.EntityFrameworkCore;

using Domain.Interfaces.Repositories;
using Domain.Models;

namespace Infra.Repositories {
  public class FrotaHorariaRepository : RepositoryBase<FrotaHoraria>, IFrotaHorariaRepository {
    public FrotaHorariaRepository(DataContext context) : base(context) { }

    public decimal? MediaHoras(Expression<Func<FrotaHoraria, bool>> condition = null) {
      try {
        return (decimal)base.Get(condition).Sum(p => p.Veiculos) /
                   base.Get(condition).Max(p => p.Veiculos);
      }
      catch (DivideByZeroException) {
        return null;
      }
    }

    protected override IQueryable<FrotaHoraria> Get(Expression<Func<FrotaHoraria, bool>> condition = null,
        Func<IQueryable<FrotaHoraria>, IOrderedQueryable<FrotaHoraria>> order = null) {
      try {
        return base.Get(condition, order).Include(f => f.Empresa);
      }
      catch (DbException ex) {
        throw new Exception(ex.Message);
      }
    }
  }
}
