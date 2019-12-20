using Domain.Interfaces.Repositories;
using Domain.Interfaces.Services;
using Domain.Models;

namespace Domain.Services {
  public class CarroceriaService : ServiceBase<Carroceria>, ICarroceriaService {
    private readonly ICarroceriaRepository _repository;

    public CarroceriaService(ICarroceriaRepository repository) : base(repository) {
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
