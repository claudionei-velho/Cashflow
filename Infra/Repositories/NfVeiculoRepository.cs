using System;
using System.Data.Common;
using System.Linq;
using System.Linq.Expressions;

using Microsoft.EntityFrameworkCore;

using Domain.Interfaces.Repositories;
using Domain.Models;

namespace Infra.Repositories {
  public class NfVeiculoRepository : RepositoryBase<NfVeiculo>, INfVeiculoRepository {
    public NfVeiculoRepository(DataContext context) : base(context) { }

    protected override IQueryable<NfVeiculo> Get(Expression<Func<NfVeiculo, bool>> condition = null, 
        Func<IQueryable<NfVeiculo>, IOrderedQueryable<NfVeiculo>> order = null) {
      try {
        return base.Get(condition, order).Include(nf => nf.NFiscal).AsNoTracking();
      }
      catch (DbException ex) {
        throw new Exception(ex.Message);
      }
    }
  }
}
