﻿using Domain.Interfaces.Repositories;
using Domain.Interfaces.Services;
using Domain.Models;

namespace Domain.Services {
  public class ProducaoService : ServiceBase<Producao>, IProducaoService {
    private readonly IProducaoRepository _repository;

    public ProducaoService(IProducaoRepository repository) : base(repository) {
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
