using Domain.Interfaces.Repositories;
using Domain.Interfaces.Services;
using Domain.Models;

namespace Domain.Services {
  public class OperacionalService : ServiceBase<Operacional>, IOperacionalService {
    private readonly IOperacionalRepository _repository;

    public OperacionalService(IOperacionalRepository repository) : base(repository) {
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
