using Domain.Interfaces.Repositories;
using Domain.Interfaces.Services;
using Domain.Models;

namespace Domain.Services {
  public class EInstalacaoService : ServiceBase<EInstalacao>, IEInstalacaoService {
    private readonly IEInstalacaoRepository _repository;

    public EInstalacaoService(IEInstalacaoRepository repository) : base(repository) {
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
