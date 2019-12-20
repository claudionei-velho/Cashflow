using Domain.Interfaces.Repositories;
using Domain.Interfaces.Services;
using Domain.Models;

namespace Domain.Services {
  public class PSinteseService : ServiceBase<PSintese>, IPSinteseService {
    private readonly IPSinteseRepository _repository;

    public PSinteseService(IPSinteseRepository repository) : base(repository) {
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
