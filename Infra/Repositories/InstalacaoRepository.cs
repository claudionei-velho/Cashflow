using System;
using System.Data.Common;
using System.Linq;
using System.Linq.Expressions;

using Microsoft.EntityFrameworkCore;

using Domain.Interfaces.Repositories;
using Domain.Models;

namespace Infra.Repositories {
  public class InstalacaoRepository : RepositoryBase<Instalacao>, IInstalacaoRepository {
    public InstalacaoRepository(DataContext context) : base(context) { }

    protected override IQueryable<Instalacao> Get(Expression<Func<Instalacao, bool>> condition = null, 
        Func<IQueryable<Instalacao>, IOrderedQueryable<Instalacao>> order = null) {
      try {
        IQueryable<Instalacao> query = _context.Set<Instalacao>().AsNoTracking()
                                           .Include(i => i.Empresa);
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
