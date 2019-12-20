using System;
using System.Data.Common;
using System.Linq;
using System.Linq.Expressions;

using Microsoft.EntityFrameworkCore;

using Domain.Interfaces.Repositories;
using Domain.Models;

namespace Infra.Repositories {
  public class AtendimentoRepository : RepositoryBase<Atendimento>, IAtendimentoRepository {
    public AtendimentoRepository(DataContext context) : base(context) { }

    protected override IQueryable<Atendimento> Get(Expression<Func<Atendimento, bool>> condition = null, 
        Func<IQueryable<Atendimento>, IOrderedQueryable<Atendimento>> order = null) {
      try {
        IQueryable<Atendimento> query = _context.Set<Atendimento>().AsNoTracking()
                                            .Include(a => a.Linha.Empresa);
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
