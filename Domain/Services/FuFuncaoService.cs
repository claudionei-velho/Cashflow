using Domain.Interfaces.Repositories;
using Domain.Interfaces.Services;
using Domain.Models;

namespace Domain.Services {
  public class FuFuncaoService : ServiceBase<FuFuncao>, IFuFuncaoService {
    private readonly IFuFuncaoRepository _repository;

    public FuFuncaoService(IFuFuncaoRepository repository) : base(repository) {
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
