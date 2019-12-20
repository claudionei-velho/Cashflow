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
        IQueryable<Fornecedor> query = _context.Set<Fornecedor>().AsNoTracking()
                                           .Include(f => f.Municipio).Include(f => f.Pais);
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
