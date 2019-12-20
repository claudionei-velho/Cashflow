using System;
using System.Data.Common;
using System.Linq;
using System.Linq.Expressions;

using Microsoft.EntityFrameworkCore;

using Domain.Interfaces.Repositories;
using Domain.Models;

namespace Infra.Repositories {
  public class ECVeiculoRepository : RepositoryBase<ECVeiculo>, IECVeiculoRepository {
    public ECVeiculoRepository(DataContext context) : base(context) { }

    protected override IQueryable<ECVeiculo> Get(Expression<Func<ECVeiculo, bool>> condition = null, 
        Func<IQueryable<ECVeiculo>, IOrderedQueryable<ECVeiculo>> order = null) {                
      try {
        IQueryable<ECVeiculo> query = _context.Set<ECVeiculo>().AsNoTracking()
                                          .Include(v => v.Empresa).Include(v => v.CVeiculo);
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
