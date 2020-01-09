using Domain.Interfaces.Repositories;
using Domain.Interfaces.Services;
using Domain.Models;

namespace Domain.Services {
  public class NfEntregaService : ServiceBase<NfEntrega>, INfEntregaService {
    private readonly INfEntregaRepository _repository;

    public NfEntregaService(INfEntregaRepository repository) : base(repository) {
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
