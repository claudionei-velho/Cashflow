using System;
using System.Data.Common;
using System.Linq;
using System.Linq.Expressions;

using Microsoft.EntityFrameworkCore;

using Domain.Interfaces.Repositories;
using Domain.Models;

namespace Infra.Repositories {
  public class FornecedorRepository : RepositoryBase<Fornecedor>, IFornecedorRepository {
    public FornecedorRepository(DataContext context) : base(context) { }

    protected override IQueryable<Fornecedor> Get(Expression<Func<Fornecedor, bool>> condition = null,
        Func<IQueryable<Fornecedor>, IOrderedQueryable<Fornecedor>> order = null) {
      try {
        return base.Get(condition, order).Include(f => f.Municipio.Uf).Include(f => f.Pais);
      }
      catch (DbException ex) {
        throw new Exception(ex.Message);
      }
    }
  }
}
