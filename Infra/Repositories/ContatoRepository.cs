using System;
using System.Data.Common;
using System.Linq;
using System.Linq.Expressions;

using Microsoft.EntityFrameworkCore;

using Domain.Interfaces.Repositories;
using Domain.Models;

namespace Infra.Repositories {
  public class ContatoRepository : RepositoryBase<Contato>, IContatoRepository {
    public ContatoRepository(DataContext context) : base(context) { }

    protected override IQueryable<Contato> Get(Expression<Func<Contato, bool>> condition = null, 
        Func<IQueryable<Contato>, IOrderedQueryable<Contato>> order = null) {
      try {
        return base.Get(condition, order).Include(c => c.Empresa);
      }
      catch (DbException ex) {
        throw new Exception(ex.Message);
      }
    }
  }
}
