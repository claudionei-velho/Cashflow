using System;
using System.Linq.Expressions;

using Domain.Interfaces.Repositories;
using Domain.Interfaces.Services;
using Domain.Models;

namespace Domain.Services {
  public class EConsorcioService : ServiceBase<EConsorcio>, IEConsorcioService {
    private readonly IEConsorcioRepository _repository;

    public EConsorcioService(IEConsorcioRepository repository) : base(repository) {
      _repository = repository;
    }

    public Expression<Func<EConsorcio, bool>> GetExpression(int? id) {
      if (id != null) {
        return ec => ec.ConsorcioId == id;
      }
      return null;
    }

    public decimal? TotalRatio(Expression<Func<EConsorcio, bool>> condition = null) {
      return _repository.TotalRatio(condition);
    }

    protected override void Dispose(bool disposing) {
      if (disposing) {
        _repository.Dispose();
      }
      base.Dispose(disposing);
    }
  }
}
