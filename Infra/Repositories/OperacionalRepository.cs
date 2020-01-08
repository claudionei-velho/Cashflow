using System;
using System.Data.Common;
using System.Linq;
using System.Linq.Expressions;

using Microsoft.EntityFrameworkCore;

using Domain.Interfaces.Repositories;
using Domain.Models;

namespace Infra.Repositories {
  public class OperacionalRepository : RepositoryBase<Operacional>, IOperacionalRepository {
    public OperacionalRepository(DataContext context) : base(context) { }

    protected override IQueryable<Operacional> Get(Expression<Func<Operacional, bool>> condition = null, 
        Func<IQueryable<Operacional>, IOrderedQueryable<Operacional>> order = null) {                
      try {
        return base.Get(condition, order).Include(p => p.Empresa)
                   .Include(p => p.Linha).Include(p => p.Atendimento).AsNoTracking();
      }
      catch (DbException ex) {
        throw new Exception(ex.Message);
      }
    }
  }
}
