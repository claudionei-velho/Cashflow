using Domain.Interfaces.Repositories;
using Domain.Interfaces.Services;
using Domain.Models;

namespace Domain.Services {
  public class CstCombustivelService : ServiceBase<CstCombustivel>, ICstCombustivelService {
    private readonly ICstCombustivelRepository _repository;

    public CstCombustivelService(ICstCombustivelRepository repository) : base(repository) {
      _repository = repository;
    }

    protected override void Dispose(bool disposing) {
      if (disposing) {
        _repository.Dispose();
      }
      base.Dispose(disposing);
    }
  }
}
