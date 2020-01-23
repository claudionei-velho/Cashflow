using System;
using System.Linq.Expressions;

using Domain.Models;

namespace Domain.Interfaces.Services {
  public interface ICentroService : IServiceBase<Centro> {
    Expression<Func<Centro, bool>> GetExpression(int? id);
  }
}
