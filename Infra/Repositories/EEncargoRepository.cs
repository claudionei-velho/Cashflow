using System;
using System.Data.Common;
using System.Linq;
using System.Linq.Expressions;

using Microsoft.EntityFrameworkCore;

using Domain.Interfaces.Repositories;
using Domain.Models;

namespace Infra.Repositories {
  public class EEncargoRepository : RepositoryBase<EEncargo>, IEEncargoRepository {
    public EEncargoRepository(DataContext context) : base(context) { }

    protected override IQueryable<EEncargo> Get(Expression<Func<EEncargo, bool>> condition = null, 
        Func<IQueryable<EEncargo>, IOrderedQueryable<EEncargo>> order = null) {                
      try {
        return base.Get(condition, order)
                   .Include(e => e.Empresa).Include(e => e.Encargo);
      }
      catch (DbException ex) {
        throw new Exception(ex.Message);
      }
    }
  }
}
