using System;
using System.Data.Common;
using System.Linq;
using System.Linq.Expressions;

using Microsoft.EntityFrameworkCore;

using Domain.Interfaces.Repositories;
using Domain.Models;

namespace Infra.Repositories {
  public class NfReferenciaRepository : RepositoryBase<NfReferencia>, INfReferenciaRepository {
    public NfReferenciaRepository(DataContext context) : base(context) { }

    protected override IQueryable<NfReferencia> Get(Expression<Func<NfReferencia, bool>> condition = null,
        Func<IQueryable<NfReferencia>, IOrderedQueryable<NfReferencia>> order = null) {
      try {
        return base.Get(condition, order).Include(nf => nf.NFiscal).Include(nf => nf.Fornecedor);
      }
      catch (DbException ex) {
        throw new Exception(ex.Message);
      }
    }
  }
}
