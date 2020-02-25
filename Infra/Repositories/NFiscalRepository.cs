using System;
using System.Data.Common;
using System.Linq;
using System.Linq.Expressions;

using Microsoft.EntityFrameworkCore;

using Domain.Interfaces.Repositories;
using Domain.Models;

namespace Infra.Repositories {
  public class NFiscalRepository : RepositoryBase<NFiscal>, INFiscalRepository {
    public NFiscalRepository(DataContext context) : base(context) { }

    protected override IQueryable<NFiscal> Get(Expression<Func<NFiscal, bool>> condition = null, 
        Func<IQueryable<NFiscal>, IOrderedQueryable<NFiscal>> order = null) {
      try {
        return base.Get(condition, order).Include(nf => nf.Empresa).Include(nf => nf.Fornecedor);
      }
      catch (DbException ex) {
        throw new Exception(ex.Message);
      }
    }
  }
}
