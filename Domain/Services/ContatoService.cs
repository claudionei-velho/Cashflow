using Domain.Interfaces.Repositories;
using Domain.Interfaces.Services;
using Domain.Models;

namespace Domain.Services {
  public class ContatoService : ServiceBase<Contato>, IContatoService {
    private readonly IContatoRepository _repository;

    public ContatoService(IContatoRepository repository) : base(repository) {
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
