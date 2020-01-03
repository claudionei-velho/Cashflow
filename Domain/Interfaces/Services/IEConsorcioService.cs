using System;
using System.Linq.Expressions;

using Domain.Models;

namespace Domain.Interfaces.Services {
  public interface IEConsorcioService : IServiceBase<EConsorcio> {
    decimal? TotalRatio(Expression<Func<EConsorcio, bool>> condition = null);
  }
}
