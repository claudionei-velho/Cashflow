using Domain.Interfaces.Repositories;
using Domain.Interfaces.Services;
using Domain.Models;

namespace Domain.Services {
  public class TCategoriaService : ServiceBase<TCategoria>, ITCategoriaService {
    private readonly ITCategoriaRepository _repository;

    public TCategoriaService(ITCategoriaRepository repository) : base(repository) {
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
