﻿using System.Collections.Generic;
using System.Threading.Tasks;

using Domain.Interfaces.Repositories;
using Domain.Interfaces.Services;
using Domain.Models;

namespace Domain.Services {
  public class MunicipioService : ServiceBase<Municipio>, IMunicipioService {
    private readonly IMunicipioRepository _repository;

    public MunicipioService(IMunicipioRepository repository) : base(repository) {
      _repository = repository;
    }

    public async Task<IEnumerable<Municipio>> GetExpertise() {
      return await _repository.GetExpertise();
    }

    protected override void Dispose(bool disposing) {
      if (disposing) {
        _repository.Dispose();
      }
      base.Dispose(disposing);
    }
  }
}
