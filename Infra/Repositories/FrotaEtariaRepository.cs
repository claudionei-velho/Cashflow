using System;
using System.Data.Common;
using System.Linq;
using System.Linq.Expressions;

using Microsoft.EntityFrameworkCore;

using Domain.Interfaces.Repositories;
using Domain.Models;

namespace Infra.Repositories {
  public class FrotaEtariaRepository : RepositoryBase<FrotaEtaria>, IFrotaEtariaRepository {
    public FrotaEtariaRepository(DataContext context) : base(context) { }

    protected override IQueryable<FrotaEtaria> Get(Expression<Func<FrotaEtaria, bool>> condition = null, 
        Func<IQueryable<FrotaEtaria>, IOrderedQueryable<FrotaEtaria>> order = null) {
      try {
        return base.Get(condition, order)
                   .Include(f => f.Empresa).Include(f => f.FxEtaria);
      }
      catch (DbException ex) {
        throw new Exception(ex.Message);
      }
    }
  }
}
