using System;
using System.Linq.Expressions;

using Domain.Interfaces.Repositories;
using Domain.Interfaces.Services;
using Domain.Models;

namespace Domain.Services {
  public class CargoService : ServiceBase<Cargo>, ICargoService {
    private readonly ICargoRepository _repository;

    public CargoService(ICargoRepository repository) : base(repository) {
      _repository = repository;
    }

    public Expression<Func<Cargo, bool>> GetExpression(int? id) {
      if (id != null) {
        return c => c.EmpresaId == id;
      }
      return null;
    }

    protected override void Dispose(bool disposing) {
      if (disposing) {
        _repository.Dispose();
      }
      base.Dispose(disposing);
    }
  }
}
