using System;
using System.Data.Common;
using System.Linq;
using System.Linq.Expressions;

using Microsoft.EntityFrameworkCore;

using Domain.Interfaces.Repositories;
using Domain.Models;

namespace Infra.Repositories {
  public class TurnoRepository : RepositoryBase<Turno>, ITurnoRepository {
    public TurnoRepository(DataContext context) : base(context) { }

    protected override IQueryable<Turno> Get(Expression<Func<Turno, bool>> condition = null, 
        Func<IQueryable<Turno>, IOrderedQueryable<Turno>> order = null) {                
      try {
        return base.Get(condition, order).Include(t => t.Empresa);
      }
      catch (DbException ex) {
        throw new Exception(ex.Message);
      }
    }
  }
}
