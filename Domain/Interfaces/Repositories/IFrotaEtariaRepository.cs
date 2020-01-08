using System;
using System.Linq.Expressions;

using Domain.Models;

namespace Domain.Interfaces.Repositories {
  public interface IFrotaEtariaRepository : IRepositoryBase<FrotaEtaria> {
    decimal? FrotaIdade(Expression<Func<FrotaEtaria, bool>> condition = null);
  }
}
