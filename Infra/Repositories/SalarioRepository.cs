using System;
using System.Data.Common;
using System.Linq;
using System.Linq.Expressions;

using Microsoft.EntityFrameworkCore;

using Domain.Interfaces.Repositories;
using Domain.Models;

namespace Infra.Repositories {
  public class SalarioRepository : RepositoryBase<Salario>, ISalarioRepository {
    public SalarioRepository(DataContext context) : base(context) { }

    protected override IQueryable<Salario> Get(Expression<Func<Salario, bool>> condition = null,
        Func<IQueryable<Salario>, IOrderedQueryable<Salario>> order = null) {
      try {
        return base.Get(condition, order).Include(s => s.Funcao.Cargo);
      }
      catch (DbException ex) {
        throw new Exception(ex.Message);
      }
    }
  }
}
