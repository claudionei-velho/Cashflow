using System;
using System.Data.Common;
using System.Linq;
using System.Linq.Expressions;

using Microsoft.EntityFrameworkCore;

using Domain.Interfaces.Repositories;
using Domain.Models;

namespace Infra.Repositories {
  public class DepartamentoRepository : RepositoryBase<Departamento>, IDepartamentoRepository {
    public DepartamentoRepository(DataContext context) : base(context) { }

    protected override IQueryable<Departamento> Get(Expression<Func<Departamento, bool>> condition = null, 
        Func<IQueryable<Departamento>, IOrderedQueryable<Departamento>> order = null) {                
      try {
        return base.Get(condition, order).Include(d => d.Setor)
                   .Include(d => d.Cargo).AsNoTracking();
      }
      catch (DbException ex) {
        throw new Exception(ex.Message);
      }
    }
  }
}
