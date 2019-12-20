using Domain.Interfaces.Repositories;
using Domain.Interfaces.Services;
using Domain.Models;

namespace Domain.Services {
  public class EEncargoService : ServiceBase<EEncargo>, IEEncargoService {
    private readonly IEEncargoRepository _repository;

    public EEncargoService(IEEncargoRepository repository) : base(repository) {
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
