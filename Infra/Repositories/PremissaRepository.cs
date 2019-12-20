using System;
using System.Data.Common;
using System.Linq;
using System.Linq.Expressions;

using Microsoft.EntityFrameworkCore;

using Domain.Interfaces.Repositories;
using Domain.Models;

namespace Infra.Repositories {
  public class PremissaRepository : RepositoryBase<Premissa>, IPremissaRepository {
    public PremissaRepository(DataContext context) : base(context) { }

    protected override IQueryable<Premissa> Get(Expression<Func<Premissa, bool>> condition = null,
        Func<IQueryable<Premissa>, IOrderedQueryable<Premissa>> order = null) {
      try {
        IQueryable<Premissa> query = _context.Set<Premissa>().AsNoTracking()
                                         .Include(p => p.Empresa);
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
