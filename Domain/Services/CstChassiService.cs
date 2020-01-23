﻿using System;
using System.Linq.Expressions;

using Domain.Interfaces.Repositories;
using Domain.Interfaces.Services;
using Domain.Models;

namespace Domain.Services {
  public class CstChassiService : ServiceBase<CstChassi>, ICstChassiService {
    private readonly ICstChassiRepository _repository;

    public CstChassiService(ICstChassiRepository repository) : base(repository) {
      _repository = repository;
    }

    public Expression<Func<CstChassi, bool>> GetExpression(int? id) {
      if (id != null) {
        return cs => cs.EmpresaId == id;
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
