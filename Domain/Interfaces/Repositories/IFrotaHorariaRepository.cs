using System;
using System.Linq.Expressions;

using Domain.Models;

namespace Domain.Interfaces.Repositories {
  public interface IFrotaHorariaRepository : IRepositoryBase<FrotaHoraria> {
    decimal? MediaHoras(Expression<Func<FrotaHoraria, bool>> condition = null);
  }
}
