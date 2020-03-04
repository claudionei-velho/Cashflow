using System;
using System.Data.Common;
using System.Linq;
using System.Linq.Expressions;

using Microsoft.EntityFrameworkCore;

using Domain.Interfaces.Repositories;
using Domain.Models;

namespace Infra.Repositories {
  public class ProducaoRepository : RepositoryBase<Producao>, IProducaoRepository {
    public ProducaoRepository(DataContext context) : base(context) { }

    protected override IQueryable<Producao> Get(Expression<Func<Producao, bool>> condition = null,
        Func<IQueryable<Producao>, IOrderedQueryable<Producao>> order = null) {
      try {
        return base.Get(condition, order).Include(p => p.Empresa)
                   .Include(p => p.TCategoria).Include(p => p.Linha);
      }
      catch (DbException ex) {
        throw new Exception(ex.Message);
      }
    }
  }
}
