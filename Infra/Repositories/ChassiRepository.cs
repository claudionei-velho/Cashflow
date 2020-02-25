using System;
using System.Data.Common;
using System.Linq;
using System.Linq.Expressions;

using Microsoft.EntityFrameworkCore;

using Domain.Interfaces.Repositories;
using Domain.Models;

namespace Infra.Repositories {
  public class ChassiRepository : RepositoryBase<Chassi>, IChassiRepository {
    public ChassiRepository(DataContext context) : base(context) { }

    protected override IQueryable<Chassi> Get(Expression<Func<Chassi, bool>> condition = null, 
        Func<IQueryable<Chassi>, IOrderedQueryable<Chassi>> order = null) {
      try {
        return base.Get(condition, order).Include(c => c.Veiculo);
      }
      catch (DbException ex) {
        throw new Exception(ex.Message);
      }
    }
  }
}
