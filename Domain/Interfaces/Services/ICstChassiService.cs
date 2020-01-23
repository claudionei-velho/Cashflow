using System;
using System.Linq.Expressions;

using Domain.Models;

namespace Domain.Interfaces.Services {
  public interface ICstChassiService : IServiceBase<CstChassi> {
    Expression<Func<CstChassi, bool>> GetExpression(int? id);
  }
}
