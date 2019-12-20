using Domain.Interfaces.Repositories;
using Domain.Interfaces.Services;
using Domain.Models;

namespace Domain.Services {
  public class DepreciacaoService : ServiceBase<Depreciacao>, IDepreciacaoService {
    private readonly IDepreciacaoRepository _repository;

    public DepreciacaoService(IDepreciacaoRepository repository) : base(repository) {
      _repository = repository;
    }

    public decimal? GetCoeficiente(int classeId, int etariaId) {
      return _repository.GetCoeficiente(classeId, etariaId);
    }

    public decimal? GetAcumulado(int classeId, int idade) {
      return _repository.GetAcumulado(classeId, idade);
    }

    protected override void Dispose(bool disposing) {
      if (disposing) {
        _repository.Dispose();
      }
      base.Dispose(disposing);
    }
  }
}
