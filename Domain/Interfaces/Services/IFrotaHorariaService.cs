using System;
using System.Linq.Expressions;

using Domain.Models;

namespace Domain.Interfaces.Services {
  public interface IFrotaHorariaService : IServiceBase<FrotaHoraria> {
    decimal? MediaHoras(Expression<Func<FrotaHoraria, bool>> condition = null);
  }
}
