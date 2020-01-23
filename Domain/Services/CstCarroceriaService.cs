using System;
using System.Linq.Expressions;

using Domain.Interfaces.Repositories;
using Domain.Interfaces.Services;
using Domain.Models;

namespace Domain.Services {
  public class CstCarroceriaService : ServiceBase<CstCarroceria>, ICstCarroceriaService {
    private readonly ICstCarroceriaRepository _repository;

    public CstCarroceriaService(ICstCarroceriaRepository repository) : base(repository) {
      _repository = repository;
    }

    public Expression<Func<CstCarroceria, bool>> GetExpression(int? id) {
      if (id != null) {
        return cs => cs.EmpresaId == id;
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
