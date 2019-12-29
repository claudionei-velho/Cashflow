using System;
using System.Data.Common;
using System.Linq;
using System.Linq.Expressions;

using Microsoft.EntityFrameworkCore;

using Domain.Interfaces.Repositories;
using Domain.Models;

namespace Infra.Repositories {
  public class LinhaRepository : RepositoryBase<Linha>, ILinhaRepository {
    public LinhaRepository(DataContext context) : base(context) { }

    protected override IQueryable<Linha> Get(Expression<Func<Linha, bool>> condition = null, 
        Func<IQueryable<Linha>, IOrderedQueryable<Linha>> order = null) {
      try {
        return base.Get(condition, order)
                   .Include(l => l.Empresa).Include(l => l.EDominio.Dominio)
                   .Include(l => l.Operacao.OpLinha).Include(l => l.CLinha.ClassLinha);
      }
      catch (DbException ex) {
        throw new Exception(ex.Message);
      }
    }
  }
}
