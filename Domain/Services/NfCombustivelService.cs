using Domain.Interfaces.Repositories;
using Domain.Interfaces.Services;
using Domain.Models;

namespace Domain.Services {
  public class NfCombustivelService : ServiceBase<NfCombustivel>, INfCombustivelService {
    private readonly INfCombustivelRepository _repository;

    public NfCombustivelService(INfCombustivelRepository repository) : base(repository) {
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
