using Domain.Interfaces.Repositories;
using Domain.Interfaces.Services;
using Domain.Models;

namespace Domain.Services {
  public class ProducaoMediaService : ServiceBase<ProducaoMedia>, IProducaoMediaService {
    private readonly IProducaoMediaRepository _repository;

    public ProducaoMediaService(IProducaoMediaRepository repository) : base(repository) {
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
