using System.Linq;

using Domain.Interfaces.Repositories;
using Domain.Interfaces.Services;
using Domain.Models;

namespace Domain.Services {
  public class MunicipioService : ServiceBase<Municipio>, IMunicipioService {
    private readonly IMunicipioRepository _repository;

    public MunicipioService(IMunicipioRepository repository) : base(repository) {
      _repository = repository;
    }

    public IQueryable<Municipio> GetExpertise() {
      return _repository.GetExpertise();
    }

    protected override void Dispose(bool disposing) {
      if (disposing) {
        _repository.Dispose();
      }
      base.Dispose(disposing);
    }
  }
}
