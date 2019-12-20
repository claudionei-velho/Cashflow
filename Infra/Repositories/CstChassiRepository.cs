using System;
using System.Data.Common;
using System.Linq;
using System.Linq.Expressions;

using Microsoft.EntityFrameworkCore;

using Domain.Interfaces.Repositories;
using Domain.Models;

namespace Infra.Repositories {
  public class CstChassiRepository : RepositoryBase<CstChassi>, ICstChassiRepository {
    public CstChassiRepository(DataContext context) : base(context) { }

    protected override IQueryable<CstChassi> Get(Expression<Func<CstChassi, bool>> condition = null,
        Func<IQueryable<CstChassi>, IOrderedQueryable<CstChassi>> order = null) {
      try {
        IQueryable<CstChassi> query = _context.Set<CstChassi>().AsNoTracking()
                                          .Include(c => c.Empresa).Include(c => c.CVeiculo);
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
