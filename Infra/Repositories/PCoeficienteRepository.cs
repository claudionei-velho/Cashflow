using System;
using System.Data.Common;
using System.Linq;
using System.Linq.Expressions;

using Microsoft.EntityFrameworkCore;

using Domain.Interfaces.Repositories;
using Domain.Models;

namespace Infra.Repositories {
  public class PCoeficienteRepository : RepositoryBase<PCoeficiente>, IPCoeficienteRepository {
    public PCoeficienteRepository(DataContext context) : base(context) { }

    protected override IQueryable<PCoeficiente> Get(Expression<Func<PCoeficiente, bool>> condition = null, 
        Func<IQueryable<PCoeficiente>, IOrderedQueryable<PCoeficiente>> order = null) {
      try {
        IQueryable<PCoeficiente> query = _context.Set<PCoeficiente>().AsNoTracking()
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
