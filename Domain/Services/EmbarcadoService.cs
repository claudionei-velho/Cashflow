using Domain.Interfaces.Repositories;
using Domain.Interfaces.Services;
using Domain.Models;

namespace Domain.Services {
  public class EmbarcadoService : ServiceBase<Embarcado>, IEmbarcadoService {
    private readonly IEmbarcadoRepository _repository;

    public EmbarcadoService(IEmbarcadoRepository repository) : base(repository) {
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
