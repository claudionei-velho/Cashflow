using System;
using System.Linq.Expressions;

using Domain.Interfaces.Repositories;
using Domain.Interfaces.Services;
using Domain.Models;

namespace Domain.Services {
  public class ContatoService : ServiceBase<Contato>, IContatoService {
    private readonly IContatoRepository _repository;

    public ContatoService(IContatoRepository repository) : base(repository) {
      _repository = repository;
    }

    public override Expression<Func<Contato, bool>> GetExpression(int? id) {
      if (id != null) {
        return c => c.EmpresaId == id;
      }
      return null;
    }

    protected override void Dispose(bool disposing) {
      if (disposing) {
        _repository.Dispose();
      }
      base.Dispose(disposing);
    }
  }
}
