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

    public Expression<Func<Linha, bool>> SearchBy(ForeignKey key = 0, object id = null) {
      if (key == 0 || id == null) {
        return null;
      }

      return key switch {
        ForeignKey.MunicipioId => l => l.Empresa.MunicipioId == (int)id,
        ForeignKey.EmpresaId => l => l.EmpresaId == (int)id,
        _ => null,
      };
    }

    protected override void Dispose(bool disposing) {
      if (disposing) {
        _repository.Dispose();
      }
      base.Dispose(disposing);
    }
  }
}
