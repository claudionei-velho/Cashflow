using System;
using System.Data.Common;
using System.Linq;
using System.Linq.Expressions;

using Microsoft.EntityFrameworkCore;

using Domain.Interfaces.Repositories;
using Domain.Models;

namespace Infra.Repositories {
  public class InstalacaoRepository : RepositoryBase<Instalacao>, IInstalacaoRepository {
    public InstalacaoRepository(DataContext context) : base(context) { }

    protected override IQueryable<Instalacao> Get(Expression<Func<Instalacao, bool>> condition = null, 
        Func<IQueryable<Instalacao>, IOrderedQueryable<Instalacao>> order = null) {
      try {
        return base.Get(condition, order).Include(i => i.Empresa);
      }
      catch (DbException ex) {
        throw new Exception(ex.Message);
      }
    }
  }
}
