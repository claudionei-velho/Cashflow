using Domain.Interfaces.Repositories;
using Domain.Interfaces.Services;
using Domain.Models;

namespace Domain.Services {
  public class InstalacaoService : ServiceBase<Instalacao>, IInstalacaoService {
    private readonly IInstalacaoRepository _repository;

    public InstalacaoService(IInstalacaoRepository repository) : base(repository) {
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
