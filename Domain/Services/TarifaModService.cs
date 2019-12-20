using Domain.Interfaces.Repositories;
using Domain.Interfaces.Services;
using Domain.Models;

namespace Domain.Services {
  public class TarifaModService : ServiceBase<TarifaMod>, ITarifaModService {
    private readonly ITarifaModRepository _repository;

    public TarifaModService(ITarifaModRepository repository) : base(repository) {
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
