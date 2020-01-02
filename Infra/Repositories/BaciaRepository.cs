using System;
using System.Data.Common;
using System.Linq;
using System.Linq.Expressions;

using Microsoft.EntityFrameworkCore;

using Domain.Interfaces.Repositories;
using Domain.Models;

namespace Infra.Repositories {
  public class BaciaRepository : RepositoryBase<Bacia>, IBaciaRepository {
    public BaciaRepository(DataContext context) : base(context) { }

    protected override IQueryable<Bacia> Get(Expression<Func<Bacia, bool>> condition = null, 
        Func<IQueryable<Bacia>, IOrderedQueryable<Bacia>> order = null) {
      try {
        return base.Get(condition, order).Include(b => b.Municipio.Uf);
      }
      catch (DbException ex) {
        throw new Exception(ex.Message);
      }
    }
  }
}
