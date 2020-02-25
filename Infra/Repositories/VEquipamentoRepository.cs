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
        return base.Get(condition, order).Include(t => t.Empresa);
      }
      catch (DbException ex) {
        throw new Exception(ex.Message);
      }
    }
  }
}
