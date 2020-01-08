using System;
using System.Linq.Expressions;

using Domain.Models;

namespace Domain.Interfaces.Services {
  public interface IFrotaEtariaService : IServiceBase<FrotaEtaria> {
    decimal? IdadeFrota(Expression<Func<FrotaEtaria, bool>> condition = null);
  }
}
