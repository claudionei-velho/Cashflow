using System;
using System.Linq.Expressions;

using Domain.Extensions;
using Domain.Models;

namespace Domain.Interfaces.Services {
  public interface ILinhaService : IServiceBase<Linha> {
    Expression<Func<Linha, bool>> GetExpression(int? id);
    Expression<Func<Linha, bool>> GetExpression(ForeignKey key, int? id);
  }
}
