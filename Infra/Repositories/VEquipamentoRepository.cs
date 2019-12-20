using System;
using System.Data.Common;
using System.Linq;
using System.Linq.Expressions;

using Microsoft.EntityFrameworkCore;

using Domain.Interfaces.Repositories;
using Domain.Models;

namespace Infra.Repositories {
  public class VEquipamentoRepository : RepositoryBase<VEquipamento>, IVEquipamentoRepository {
    public VEquipamentoRepository(DataContext context) : base(context) { }

    protected override IQueryable<VEquipamento> Get(Expression<Func<VEquipamento, bool>> condition = null, 
        Func<IQueryable<VEquipamento>, IOrderedQueryable<VEquipamento>> order = null) {                
      try {
        IQueryable<VEquipamento> query = _context.Set<VEquipamento>().AsNoTracking()
                                             .Include(t => t.Empresa);
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
