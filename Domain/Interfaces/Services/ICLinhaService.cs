using System;
using System.Linq.Expressions;

using Domain.Models;

namespace Domain.Interfaces.Services {
  public interface ICLinhaService : IServiceBase<CLinha> {
    Expression<Func<CLinha, bool>> GetExpression(int? id);
  }
}
