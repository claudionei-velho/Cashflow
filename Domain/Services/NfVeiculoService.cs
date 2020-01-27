using Domain.Interfaces.Repositories;
using Domain.Interfaces.Services;
using Domain.Models;

namespace Domain.Services {
  public class NfVeiculoService : ServiceBase<NfVeiculo>, INfVeiculoService {
    private readonly INfVeiculoRepository _repository;

    public NfVeiculoService(INfVeiculoRepository repository) : base(repository) {
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
