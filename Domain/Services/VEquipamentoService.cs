using Domain.Interfaces.Repositories;
using Domain.Interfaces.Services;
using Domain.Models;

namespace Domain.Services {
  public class VEquipamentoService : ServiceBase<VEquipamento>, IVEquipamentoService {
    private readonly IVEquipamentoRepository _repository;

    public VEquipamentoService(IVEquipamentoRepository repository) : base(repository) {
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
