using System;
using System.Linq.Expressions;

using Domain.Interfaces.Repositories;
using Domain.Interfaces.Services;
using Domain.Models;

namespace Domain.Services {
  public class CLinhaService : ServiceBase<CLinha>, ICLinhaService {
    private readonly ICLinhaRepository _repository;

    public CLinhaService(ICLinhaRepository repository) : base(repository) {
      _repository = repository;
    }

    public Expression<Func<CLinha, bool>> GetExpression(int? id) {
      if (id != null) {
        return cl => cl.EmpresaId == id;
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
