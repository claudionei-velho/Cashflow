using System;
using System.Linq.Expressions;

using Domain.Models;

namespace Domain.Interfaces.Services {
  public interface IEConsorcioService : IServiceBase<EConsorcio> {
    Expression<Func<EConsorcio, bool>> GetExpression(int? id);
    decimal? TotalRatio(Expression<Func<EConsorcio, bool>> condition = null);
  }
}
