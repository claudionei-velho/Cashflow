using System;
using System.Data.Common;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;

using Microsoft.EntityFrameworkCore;

using Domain.Interfaces.Repositories;
using Domain.Models;

namespace Infra.Repositories {
  public class EmpresaRepository : RepositoryBase<Empresa>, IEmpresaRepository {
    public EmpresaRepository(DataContext context) : base(context) { }

    public override Empresa GetFirst(Expression<Func<Empresa, bool>> condition) {
      Empresa entity = base.GetFirst(condition);
      try {
        if (!_context.Entry(entity).Collection(c => c.Contatos).IsLoaded) {
          _context.Entry(entity).Collection(c => c.Contatos).Load();
        }
        return entity;
      }
      catch (DbException ex) {
        throw new Exception(ex.Message);
      }
    }

    public override async Task<Empresa> GetFirstAsync(Expression<Func<Empresa, bool>> condition) {
      Empresa entity = await base.GetFirstAsync(condition);
      try {
        if (!_context.Entry(entity).Collection(c => c.Contatos).IsLoaded) {
          await _context.Entry(entity).Collection(c => c.Contatos).LoadAsync();
        }
        return entity;
      }
      catch (DbException ex) {
        throw new Exception(ex.Message);
      }
    }

    protected override IQueryable<Empresa> Get(Expression<Func<Empresa, bool>> condition = null,
        Func<IQueryable<Empresa>, IOrderedQueryable<Empresa>> order = null) {
      try {
        return base.Get(condition, order).Include(e => e.Cidade.Uf)
                   .Include(e => e.Pais).AsTracking();
      }
      catch (DbException ex) {
        throw new Exception(ex.Message);
      }
    }
  }
}
