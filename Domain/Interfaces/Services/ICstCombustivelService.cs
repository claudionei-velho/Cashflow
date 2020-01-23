using System;
using System.Linq.Expressions;

using Domain.Models;

namespace Domain.Interfaces.Services {
  public interface ICstCombustivelService : IServiceBase<CstCombustivel> {
    Expression<Func<CstCombustivel, bool>> GetExpression(int? id);
  }
}
