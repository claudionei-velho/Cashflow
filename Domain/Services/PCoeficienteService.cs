using Domain.Interfaces.Repositories;
using Domain.Interfaces.Services;
using Domain.Models;

namespace Domain.Services {
  public class PCoeficienteService : ServiceBase<PCoeficiente>, IPCoeficienteService {
    private readonly IPCoeficienteRepository _repository;

    public PCoeficienteService(IPCoeficienteRepository repository) : base(repository) {
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
