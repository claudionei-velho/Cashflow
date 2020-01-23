using System;
using System.Linq.Expressions;

using Domain.Models;

namespace Domain.Interfaces.Services {
  public interface ICstCarroceriaService : IServiceBase<CstCarroceria> {
    Expression<Func<CstCarroceria, bool>> GetExpression(int? id);
  }
}
