using System;
using System.Linq.Expressions;

using Domain.Interfaces.Repositories;
using Domain.Interfaces.Services;
using Domain.Models;

namespace Domain.Services {
  public class CentroService : ServiceBase<Centro>, ICentroService {
    private readonly ICentroRepository _repository;

    public CentroService(ICentroRepository repository) : base(repository) {
      _repository = repository;
    }

    public override Expression<Func<Centro, bool>> GetExpression(int? id = null) {
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
