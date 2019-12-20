using Domain.Interfaces.Repositories;
using Domain.Interfaces.Services;
using Domain.Models;

namespace Domain.Services {
  public class CstChassiService : ServiceBase<CstChassi>, ICstChassiService {
    private readonly ICstChassiRepository _repository;

    public CstChassiService(ICstChassiRepository repository) : base(repository) {
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
