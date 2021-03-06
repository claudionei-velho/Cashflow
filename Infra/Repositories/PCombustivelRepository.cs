﻿using System;
using System.Data.Common;
using System.Linq;
using System.Linq.Expressions;

using Microsoft.EntityFrameworkCore;

using Domain.Interfaces.Repositories;
using Domain.Models;

namespace Infra.Repositories {
  public class PCombustivelRepository : RepositoryBase<PCombustivel>, IPCombustivelRepository {
    public PCombustivelRepository(DataContext context) : base(context) { }

    protected override IQueryable<PCombustivel> Get(Expression<Func<PCombustivel, bool>> condition = null,
        Func<IQueryable<PCombustivel>, IOrderedQueryable<PCombustivel>> order = null) {
      try {
        return base.Get(condition, order).Include(p => p.Empresa)
                   .Include(p => p.CVeiculo).Include(p => p.Combustivel);
      }
      catch (DbException ex) {
        throw new Exception(ex.Message);
      }
    }
  }
}
