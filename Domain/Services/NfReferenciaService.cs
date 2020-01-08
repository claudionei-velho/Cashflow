using Domain.Interfaces.Repositories;
using Domain.Interfaces.Services;
using Domain.Models;

namespace Domain.Services {
  public class NfReferenciaService : ServiceBase<NfReferencia>, INfReferenciaService {
    private readonly INfReferenciaRepository _repository;

    public NfReferenciaService(INfReferenciaRepository repository) : base(repository) {
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
