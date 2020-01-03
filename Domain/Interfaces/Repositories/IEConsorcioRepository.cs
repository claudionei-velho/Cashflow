using System;
using System.Linq.Expressions;

using Domain.Models;

namespace Domain.Interfaces.Repositories {
  public interface IEConsorcioRepository : IRepositoryBase<EConsorcio> {
    decimal? TotalRatio(Expression<Func<EConsorcio, bool>> condition = null);
  }
}
