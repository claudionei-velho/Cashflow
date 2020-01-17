using System;
using System.Linq.Expressions;

using Domain.Extensions;
using Domain.Models;

namespace Domain.Interfaces.Services {
  public interface ILinhaService : IServiceBase<Linha> {
    Expression<Func<Linha, bool>> SearchBy(ForeignKey key = 0, object id = null);
  }
}
