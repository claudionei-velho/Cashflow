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
        IQueryable<EEncargo> query = _context.Set<EEncargo>().AsNoTracking()
                                         .Include(e => e.Empresa).Include(e => e.Encargo);
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
