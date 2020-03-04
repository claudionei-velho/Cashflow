using System;
using System.Data.Common;
using System.Linq;
using System.Linq.Expressions;

using Microsoft.EntityFrameworkCore;

using Domain.Interfaces.Repositories;
using Domain.Models;

namespace Infra.Repositories {
  public class NfCombustivelRepository : RepositoryBase<NfCombustivel>, INfCombustivelRepository {
    public NfCombustivelRepository(DataContext context) : base(context) { }

    protected override IQueryable<NfCombustivel> Get(Expression<Func<NfCombustivel, bool>> condition = null,
        Func<IQueryable<NfCombustivel>, IOrderedQueryable<NfCombustivel>> order = null) {
      try {
        return base.Get(condition, order).Include(nf => nf.NFiscal).Include(nf => nf.AnpProduto);
      }
      catch (DbException ex) {
        throw new Exception(ex.Message);
      }
    }
  }
}
