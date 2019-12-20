using System;
using System.Data.Common;
using System.Linq;
using System.Linq.Expressions;

using Microsoft.EntityFrameworkCore;

using Domain.Interfaces.Repositories;
using Domain.Models;

namespace Infra.Repositories {
  public class ProdutoRepository : RepositoryBase<Produto>, IProdutoRepository {
    public ProdutoRepository(DataContext context) : base(context) { }

    protected override IQueryable<Produto> Get(Expression<Func<Produto, bool>> condition = null, 
        Func<IQueryable<Produto>, IOrderedQueryable<Produto>> order = null) {
      try {
        IQueryable<Produto> query = _context.Set<Produto>().AsNoTracking()
                                        .Include(p => p.Ncm).Include(p => p.UComercial);
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
