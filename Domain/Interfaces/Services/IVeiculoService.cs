using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;

using Domain.Models;

namespace Domain.Interfaces.Services {
  public interface IVeiculoService : IServiceBase<Veiculo> {
    Task<IEnumerable<Veiculo>> GetNoChassi(Expression<Func<Veiculo, bool>> condition = null,
        Func<IQueryable<Veiculo>, IOrderedQueryable<Veiculo>> order = null);

    Task<IEnumerable<Veiculo>> GetNoCarroceria(Expression<Func<Veiculo, bool>> condition = null,
        Func<IQueryable<Veiculo>, IOrderedQueryable<Veiculo>> order = null);
  }
}
