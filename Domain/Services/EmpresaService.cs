﻿using Domain.Interfaces.Repositories;
using Domain.Interfaces.Services;
using Domain.Models;

namespace Domain.Services {
  public class EmpresaService : ServiceBase<Empresa>, IEmpresaService {
    private readonly IEmpresaRepository _repository;

    public EmpresaService(IEmpresaRepository repository) : base(repository) {
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
