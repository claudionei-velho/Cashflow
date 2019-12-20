using Domain.Interfaces.Repositories;
using Domain.Interfaces.Services;
using Domain.Models;

namespace Domain.Services {
  public class SistDespesaService : ServiceBase<SistDespesa>, ISistDespesaService {
    private readonly ISistDespesaRepository _repository;

    public SistDespesaService(ISistDespesaRepository repository) : base(repository) {
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
