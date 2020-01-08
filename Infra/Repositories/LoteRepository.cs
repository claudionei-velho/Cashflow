using System;
using System.Data.Common;
using System.Linq;
using System.Linq.Expressions;

using Microsoft.EntityFrameworkCore;

using Domain.Interfaces.Repositories;
using Domain.Models;

namespace Infra.Repositories {
  public class LoteRepository : RepositoryBase<Lote>, ILoteRepository {
    public LoteRepository(DataContext context) : base(context) { }

    protected override IQueryable<Lote> Get(Expression<Func<Lote, bool>> condition = null, 
        Func<IQueryable<Lote>, IOrderedQueryable<Lote>> order = null) {
      try {
        return base.Get(condition, order)
                   .Include(l => l.Bacia.Municipio.Uf).AsNoTracking();
      }
      catch (DbException ex) {
        throw new Exception(ex.Message);
      }
    }
  }
}
