using System;
using System.Linq.Expressions;

using Domain.Models;

namespace Domain.Interfaces.Services {
  public interface IContaService : IServiceBase<Conta> {
    Expression<Func<Conta, bool>> GetExpression(int? id);
  }
}
