using System;
using System.Data.Common;
using System.Linq;
using System.Linq.Expressions;

using Microsoft.EntityFrameworkCore;

using Domain.Interfaces.Repositories;
using Domain.Models;

namespace Infra.Repositories {
  public class NfEntregaRepository : RepositoryBase<NfEntrega>, INfEntregaRepository {
    public NfEntregaRepository(DataContext context) : base(context) { }

    protected override IQueryable<NfEntrega> Get(Expression<Func<NfEntrega, bool>> condition = null,
        Func<IQueryable<NfEntrega>, IOrderedQueryable<NfEntrega>> order = null) {
      try {
        return base.Get(condition, order).Include(nf => nf.NFiscal).Include(nf => nf.Municipio.Uf);
      }
      catch (DbException ex) {
        throw new Exception(ex.Message);
      }
    }
  }
}
