using System;
using System.Data.Common;
using System.Linq;
using System.Linq.Expressions;

using Microsoft.EntityFrameworkCore;

using Domain.Interfaces.Repositories;
using Domain.Models;

namespace Infra.Repositories {
  public class HorarioRepository : RepositoryBase<Horario>, IHorarioRepository {
    public HorarioRepository(DataContext context) : base(context) { }

    protected override IQueryable<Horario> Get(Expression<Func<Horario, bool>> condition = null, 
        Func<IQueryable<Horario>, IOrderedQueryable<Horario>> order = null) {
      try {
        IQueryable<Horario> query = _context.Set<Horario>().AsNoTracking()
                                        .Include(h => h.Linha.Empresa).Include(h => h.Atendimento);
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
