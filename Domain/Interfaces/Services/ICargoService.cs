using System;
using System.Linq.Expressions;

using Domain.Models;

namespace Domain.Interfaces.Services {
  public interface ICargoService : IServiceBase<Cargo> {
    Expression<Func<Cargo, bool>> GetExpression(int? id);
  }
}
