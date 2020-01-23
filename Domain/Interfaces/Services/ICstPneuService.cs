using System;
using System.Linq.Expressions;

using Domain.Models;

namespace Domain.Interfaces.Services {
  public interface ICstPneuService : IServiceBase<CstPneu> {
    Expression<Func<CstPneu, bool>> GetExpression(int? id);
  }
}
