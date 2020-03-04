using System;
using System.Data.Common;
using System.Linq;
using System.Linq.Expressions;

using Microsoft.EntityFrameworkCore;

using Domain.Interfaces.Repositories;
using Domain.Models;

namespace Infra.Repositories {
  public class OperacaoRepository : RepositoryBase<Operacao>, IOperacaoRepository {
    public OperacaoRepository(DataContext context) : base(context) { }

    protected override IQueryable<Operacao> Get(Expression<Func<Operacao, bool>> condition = null,
        Func<IQueryable<Operacao>, IOrderedQueryable<Operacao>> order = null) {
      try {
        return base.Get(condition, order).Include(o => o.Empresa).Include(o => o.OpLinha);
      }
      catch (DbException ex) {
        throw new Exception(ex.Message);
      }
    }
  }
}
