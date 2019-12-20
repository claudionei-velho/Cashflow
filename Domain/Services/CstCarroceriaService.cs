using Domain.Interfaces.Repositories;
using Domain.Interfaces.Services;
using Domain.Models;

namespace Domain.Services {
  public class CstCarroceriaService : ServiceBase<CstCarroceria>, ICstCarroceriaService {
    private readonly ICstCarroceriaRepository _repository;

    public CstCarroceriaService(ICstCarroceriaRepository repository) : base(repository) {
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
