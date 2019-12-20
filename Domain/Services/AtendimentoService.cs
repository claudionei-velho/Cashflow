using Domain.Interfaces.Repositories;
using Domain.Interfaces.Services;
using Domain.Models;

namespace Domain.Services {
  public class AtendimentoService : ServiceBase<Atendimento>, IAtendimentoService {
    private readonly IAtendimentoRepository _repository;

    public AtendimentoService(IAtendimentoRepository repository) : base(repository) {
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
