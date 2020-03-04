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
        return base.Get(condition, order).Include(h => h.Linha.Empresa).Include(h => h.Atendimento);
      }
      catch (DbException ex) {
        throw new Exception(ex.Message);
      }
    }
  }
}
