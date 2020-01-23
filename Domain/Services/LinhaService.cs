using System;
using System.Linq.Expressions;

using Domain.Extensions;
using Domain.Interfaces.Repositories;
using Domain.Interfaces.Services;
using Domain.Models;

namespace Domain.Services {
  public class LinhaService : ServiceBase<Linha>, ILinhaService {
    private readonly ILinhaRepository _repository;

    public LinhaService(ILinhaRepository repository) : base(repository) {
      _repository = repository;
    }

    public Expression<Func<Linha, bool>> GetExpression(int? id) {
      if (id != null) {
        return l => l.EmpresaId == id;
      }
      return null;
    }

    public Expression<Func<Linha, bool>> GetExpression(ForeignKey key, int? id) {
      if (key > 0 && id != null) {
        return key switch {
          ForeignKey.MunicipioId => l => l.Empresa.MunicipioId == id,
          ForeignKey.EmpresaId => l => l.EmpresaId == id,
          _ => null,
        };
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
