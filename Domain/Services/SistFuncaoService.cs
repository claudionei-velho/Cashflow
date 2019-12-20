using Domain.Interfaces.Repositories;
using Domain.Interfaces.Services;
using Domain.Models;

namespace Domain.Services {
  public class SistFuncaoService : ServiceBase<SistFuncao>, ISistFuncaoService {
    private readonly ISistFuncaoRepository _repository;

    public SistFuncaoService(ISistFuncaoRepository repository) : base(repository) {
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
