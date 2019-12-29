using System;
using System.Data.Common;
using System.Linq;
using System.Linq.Expressions;

using Microsoft.EntityFrameworkCore;

using Domain.Interfaces.Repositories;
using Domain.Models;

namespace Infra.Repositories {
  public class EDominioRepository : RepositoryBase<EDominio>, IEDominioRepository {
    public EDominioRepository(DataContext context) : base(context) { }

    protected override IQueryable<EDominio> Get(Expression<Func<EDominio, bool>> condition = null, 
        Func<IQueryable<EDominio>, IOrderedQueryable<EDominio>> order = null) {                
      try {
        return base.Get(condition, order)
                   .Include(d => d.Empresa).Include(d => d.Dominio);
      }
      catch (DbException ex) {
        throw new Exception(ex.Message);
      }
    }
  }
}
