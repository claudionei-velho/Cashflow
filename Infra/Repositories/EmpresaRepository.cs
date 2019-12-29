using System;
using System.Data.Common;
using System.Linq;
using System.Linq.Expressions;

using Microsoft.EntityFrameworkCore;

using Domain.Interfaces.Repositories;
using Domain.Models;

namespace Infra.Repositories {
  public class EmpresaRepository : RepositoryBase<Empresa>, IEmpresaRepository {
    public EmpresaRepository(DataContext context) : base(context) { }

    protected override IQueryable<Empresa> Get(Expression<Func<Empresa, bool>> condition = null, 
        Func<IQueryable<Empresa>, IOrderedQueryable<Empresa>> order = null) {
      try {
        return base.Get(condition, order)
                   .Include(e => e.Cidade).Include(e => e.Pais);
      }
      catch (DbException ex) {
        throw new Exception(ex.Message);
      }
    }
  }
}
