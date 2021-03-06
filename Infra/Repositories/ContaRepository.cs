﻿using System;
using System.Data.Common;
using System.Linq;
using System.Linq.Expressions;

using Microsoft.EntityFrameworkCore;

using Domain.Interfaces.Repositories;
using Domain.Models;

namespace Infra.Repositories {
  public class ContaRepository : RepositoryBase<Conta>, IContaRepository {
    public ContaRepository(DataContext context) : base(context) { }

    protected override IQueryable<Conta> Get(Expression<Func<Conta, bool>> condition = null,
        Func<IQueryable<Conta>, IOrderedQueryable<Conta>> order = null) {
      try {
        return base.Get(condition, order).Include(c => c.Empresa).Include(c => c.Vinculo);
      }
      catch (DbException ex) {
        throw new Exception(ex.Message);
      }
    }
  }
}
