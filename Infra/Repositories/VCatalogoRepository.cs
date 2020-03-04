using System;
using System.Data.Common;
using System.Linq;
using System.Linq.Expressions;

using Microsoft.EntityFrameworkCore;

using Domain.Interfaces.Repositories;
using Domain.Models;

namespace Infra.Repositories {
  public class VCatalogoRepository : RepositoryBase<VCatalogo>, IVCatalogoRepository {
    public VCatalogoRepository(DataContext context) : base(context) { }

    protected override IQueryable<VCatalogo> Get(Expression<Func<VCatalogo, bool>> condition = null,
        Func<IQueryable<VCatalogo>, IOrderedQueryable<VCatalogo>> order = null) {
      try {
        return base.Get(condition, order).Include(c => c.Empresa)
                   .Include(c => c.CVeiculo).Include(c => c.Fornecedor);
      }
      catch (DbException ex) {
        throw new Exception(ex.Message);
      }
    }
  }
}
