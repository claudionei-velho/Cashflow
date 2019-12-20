using Domain.Interfaces.Repositories;
using Domain.Interfaces.Services;
using Domain.Models;

namespace Domain.Services {
  public class FrotaHoraService : ServiceBase<FrotaHora>, IFrotaHoraService {
    private readonly IFrotaHoraRepository _repository;

    public FrotaHoraService(IFrotaHoraRepository repository) : base(repository) {
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
