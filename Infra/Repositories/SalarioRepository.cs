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
        IQueryable<Salario> query = _context.Set<Salario>().AsNoTracking()
                                        .Include(s => s.Funcao.Cargo);
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
