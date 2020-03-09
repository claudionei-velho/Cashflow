using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;

using Domain.Interfaces.Repositories;
using Domain.Interfaces.Services;
using Domain.Models;

namespace Domain.Services {
  public class VeiculoService : ServiceBase<Veiculo>, IVeiculoService {
    private readonly IVeiculoRepository _repository;

    public VeiculoService(IVeiculoRepository repository) : base(repository) {
      _repository = repository;
    }

    public async Task<IEnumerable<Veiculo>> GetNoChassi(Expression<Func<Veiculo, bool>> condition = null,
        Func<IQueryable<Veiculo>, IOrderedQueryable<Veiculo>> order = null) {
      return await _repository.GetNoChassi(condition, order);
    }

    public async Task<IEnumerable<Veiculo>> GetNoCarroceria(Expression<Func<Veiculo, bool>> condition = null,
        Func<IQueryable<Veiculo>, IOrderedQueryable<Veiculo>> order = null) {
      return await _repository.GetNoCarroceria(condition, order);
    }

    protected override void Dispose(bool disposing) {
      if (disposing) {
        _repository.Dispose();
      }
      base.Dispose(disposing);
    }
  }
}
