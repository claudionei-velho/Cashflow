using Domain.Interfaces.Repositories;
using Domain.Interfaces.Services;
using Domain.Models;

namespace Domain.Services {
  public class DepartamentoService : ServiceBase<Departamento>, IDepartamentoService {
    private readonly IDepartamentoRepository _repository;

    public DepartamentoService(IDepartamentoRepository repository) : base(repository) {
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
