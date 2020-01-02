using Domain.Interfaces.Repositories;
using Domain.Interfaces.Services;
using Domain.Models;

namespace Domain.Services {
  public class LoteService : ServiceBase<Lote>, ILoteService {
    private readonly ILoteRepository _repository;

    public LoteService(ILoteRepository repository) : base(repository) {
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
