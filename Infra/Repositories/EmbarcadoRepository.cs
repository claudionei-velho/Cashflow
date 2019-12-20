using System;
using System.Data.Common;
using System.Linq;
using System.Linq.Expressions;

using Microsoft.EntityFrameworkCore;

using Domain.Interfaces.Repositories;
using Domain.Models;

namespace Infra.Repositories {
  public class EmbarcadoRepository : RepositoryBase<Embarcado>, IEmbarcadoRepository {
    public EmbarcadoRepository(DataContext context) : base(context) { }

    protected override IQueryable<Embarcado> Get(Expression<Func<Embarcado, bool>> condition = null, 
        Func<IQueryable<Embarcado>, IOrderedQueryable<Embarcado>> order = null) {                
      try {
        IQueryable<Embarcado> query = _context.Set<Embarcado>().AsNoTracking()
                                          .Include(e => e.Veiculo).Include(e => e.Equipamento);
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
