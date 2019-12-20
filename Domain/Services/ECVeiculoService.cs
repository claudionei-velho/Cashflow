using Domain.Interfaces.Repositories;
using Domain.Interfaces.Services;
using Domain.Models;

namespace Domain.Services {
  public class ECVeiculoService : ServiceBase<ECVeiculo>, IECVeiculoService {
    private readonly IECVeiculoRepository _repository;

    public ECVeiculoService(IECVeiculoRepository repository) : base(repository) {
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
