using Domain.Interfaces.Repositories;
using Domain.Interfaces.Services;
using Domain.Models;

namespace Domain.Services {
  public class PCombustivelService : ServiceBase<PCombustivel>, IPCombustivelService {
    private readonly IPCombustivelRepository _repository;

    public PCombustivelService(IPCombustivelRepository repository) : base(repository) {
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
