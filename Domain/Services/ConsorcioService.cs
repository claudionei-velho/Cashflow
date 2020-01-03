using Domain.Interfaces.Repositories;
using Domain.Interfaces.Services;
using Domain.Models;

namespace Domain.Services {
  public class ConsorcioService : ServiceBase<Consorcio>, IConsorcioService {
    private readonly IConsorcioRepository _repository;

    public ConsorcioService(IConsorcioRepository repository) : base(repository) {
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
