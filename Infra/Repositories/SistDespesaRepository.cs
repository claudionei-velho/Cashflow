using System;
using System.Data.Common;
using System.Linq;
using System.Linq.Expressions;

using Microsoft.EntityFrameworkCore;

using Domain.Interfaces.Repositories;
using Domain.Models;

namespace Infra.Repositories {
  public class SistDespesaRepository : RepositoryBase<SistDespesa>, ISistDespesaRepository {
    public SistDespesaRepository(DataContext context) : base(context) { }

    protected override IQueryable<SistDespesa> Get(Expression<Func<SistDespesa, bool>> condition = null,
        Func<IQueryable<SistDespesa>, IOrderedQueryable<SistDespesa>> order = null) {
      try {
        return base.Get(condition, order).Include(d => d.ESistema).Include(d => d.Conta);
      }
      catch (DbException ex) {
        throw new Exception(ex.Message);
      }
    }
  }
}
