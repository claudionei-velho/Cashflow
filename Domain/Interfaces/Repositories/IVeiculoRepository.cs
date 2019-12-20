using System;
using System.Linq;
using System.Linq.Expressions;

using Domain.Models;

namespace Domain.Interfaces.Repositories {
  public interface IVeiculoRepository : IRepositoryBase<Veiculo> {
    IQueryable<Veiculo> GetNoChassi(Expression<Func<Veiculo, bool>> condition = null,
        Func<IQueryable<Veiculo>, IOrderedQueryable<Veiculo>> order = null);

    IQueryable<Veiculo> GetNoCarroceria(Expression<Func<Veiculo, bool>> condition = null,
        Func<IQueryable<Veiculo>, IOrderedQueryable<Veiculo>> order = null);
  }
}
