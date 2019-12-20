using Domain.Interfaces.Repositories;
using Domain.Interfaces.Services;
using Domain.Models;

namespace Domain.Services {
  public class FornecedorService : ServiceBase<Fornecedor>, IFornecedorService {
    private readonly IFornecedorRepository _repository;

    public FornecedorService(IFornecedorRepository repository) : base(repository) {
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
