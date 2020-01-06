using System;
using System.Linq.Expressions;

using Domain.Interfaces.Repositories;
using Domain.Interfaces.Services;
using Domain.Models;

namespace Domain.Services {
  public class FrotaHorariaService : ServiceBase<FrotaHoraria>, IFrotaHorariaService {
    private readonly IFrotaHorariaRepository _repository;

    public FrotaHorariaService(IFrotaHorariaRepository repository) : base(repository) {
      _repository = repository;
    }

    public decimal? MediaHoras(Expression<Func<FrotaHoraria, bool>> condition = null) {
      return _repository.MediaHoras(condition);
    }

    protected override void Dispose(bool disposing) {
      if (disposing) {
        _repository.Dispose();
      }
      base.Dispose(disposing);
    }
  }
}
