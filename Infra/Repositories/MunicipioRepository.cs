using System;
using System.Data.Common;
using System.Linq;
using System.Linq.Expressions;

using Microsoft.EntityFrameworkCore;

using Domain.Interfaces.Repositories;
using Domain.Models;

namespace Infra.Repositories {
  public class MunicipioRepository : RepositoryBase<Municipio>, IMunicipioRepository {
    public MunicipioRepository(DataContext context) : base(context) { }

    protected override IQueryable<Municipio> Get(Expression<Func<Municipio, bool>> condition = null, 
        Func<IQueryable<Municipio>, IOrderedQueryable<Municipio>> order = null) {
      try {
        IQueryable<Municipio> query = _context.Set<Municipio>().AsNoTracking()
                                          .Include(m => m.Uf);
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
