using System;
using System.Data.Common;
using System.Linq;
using System.Linq.Expressions;

using Microsoft.EntityFrameworkCore;

using Domain.Interfaces.Repositories;
using Domain.Models;

namespace Infra.Repositories {
  public class ConsorcioRepository : RepositoryBase<Consorcio>, IConsorcioRepository {
    public ConsorcioRepository(DataContext context) : base(context) { }

    protected override IQueryable<Consorcio> Get(Expression<Func<Consorcio, bool>> condition = null,
        Func<IQueryable<Consorcio>, IOrderedQueryable<Consorcio>> order = null) {
      try {
        return base.Get(condition, order).Include(e => e.Municipio.Uf).Include(e => e.Pais);
      }
      catch (DbException ex) {
        throw new Exception(ex.Message);
      }
    }
  }
}
