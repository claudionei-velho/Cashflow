using System;
using System.Linq.Expressions;

using Domain.Interfaces.Repositories;
using Domain.Interfaces.Services;
using Domain.Models;

namespace Domain.Services {
  public class FrotaEtariaService : ServiceBase<FrotaEtaria>, IFrotaEtariaService {
    private readonly IFrotaEtariaRepository _repository;

    public FrotaEtariaService(IFrotaEtariaRepository repository) : base(repository) {
      _repository = repository;
    }

    public decimal? IdadeFrota(Expression<Func<FrotaEtaria, bool>> condition = null) {
      return _repository.FrotaIdade(condition);
    }

    protected override void Dispose(bool disposing) {
      if (disposing) {
        _repository.Dispose();
      }
      base.Dispose(disposing);
    }
  }
}
