using System;
using System.Linq.Expressions;

using Domain.Models;

namespace Domain.Interfaces.Services {
  public interface IContatoService : IServiceBase<Contato> {
    Expression<Func<Contato, bool>> GetExpression(int? id);
  }
}
