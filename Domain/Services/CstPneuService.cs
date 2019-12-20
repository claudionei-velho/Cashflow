using Domain.Interfaces.Repositories;
using Domain.Interfaces.Services;
using Domain.Models;

namespace Domain.Services {
  public class CstPneuService : ServiceBase<CstPneu>, ICstPneuService {
    private readonly ICstPneuRepository _repository;

    public CstPneuService(ICstPneuRepository repository) : base(repository) {
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
