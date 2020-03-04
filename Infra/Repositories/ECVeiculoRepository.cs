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
        return base.Get(condition, order).Include(v => v.Empresa).Include(v => v.CVeiculo);
      }
      catch (DbException ex) {
        throw new Exception(ex.Message);
      }
    }
  }
}
