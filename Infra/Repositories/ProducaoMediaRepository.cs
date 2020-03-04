using System;
using System.Data.Common;
using System.Linq;
using System.Linq.Expressions;

using Microsoft.EntityFrameworkCore;

using Domain.Interfaces.Repositories;
using Domain.Models;

namespace Infra.Repositories {
  public class ProducaoMediaRepository : RepositoryBase<ProducaoMedia>, IProducaoMediaRepository {
    public ProducaoMediaRepository(DataContext context) : base(context) { }

    protected override IQueryable<ProducaoMedia> Get(Expression<Func<ProducaoMedia, bool>> condition = null,
        Func<IQueryable<ProducaoMedia>, IOrderedQueryable<ProducaoMedia>> order = null) {
      try {
        return base.Get(condition, order).Include(p => p.Empresa).Include(p => p.TCategoria);
      }
      catch (DbException ex) {
        throw new Exception(ex.Message);
      }
    }
  }
}
