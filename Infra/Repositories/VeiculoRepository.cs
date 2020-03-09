using System;
using System.Collections.Generic;
using System.Data.Common;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;

using Microsoft.EntityFrameworkCore;

using Domain.Extensions;
using Domain.Interfaces.Repositories;
using Domain.Models;

namespace Infra.Repositories {
  public class VeiculoRepository : RepositoryBase<Veiculo>, IVeiculoRepository {
    public VeiculoRepository(DataContext context) : base(context) { }

    public async Task<IEnumerable<Veiculo>> GetNoChassi(Expression<Func<Veiculo, bool>> condition = null,
        Func<IQueryable<Veiculo>, IOrderedQueryable<Veiculo>> order = null) {
      try {
        IQueryable<Veiculo> query = from v in Get()
                                    where !_context.Set<Chassi>()
                                               .Select(q => q.VeiculoId).ToArray().Contains(v.Id)
                                    select v;
        if (condition != null) {
          query = query.Where(condition);
        }
        if (order != null) {
          return await order(query).ToListAsync();
        }
        return await query.ToListAsync();
      }
      catch (DbException ex) {
        throw new Exception(ex.Message);
      }
    }

    public async Task<IEnumerable<Veiculo>> GetNoCarroceria(Expression<Func<Veiculo, bool>> condition = null,
        Func<IQueryable<Veiculo>, IOrderedQueryable<Veiculo>> order = null) {
      try {
        IQueryable<Veiculo> query = from v in Get()
                                    where !_context.Set<Carroceria>()
                                               .Select(q => q.VeiculoId).ToArray().Contains(v.Id)
                                    select v;
        if (condition != null) {
          query = query.Where(condition);
        }
        if (order != null) {
          return await order(query).ToListAsync();
        }
        return await query.ToListAsync();
      }
      catch (DbException ex) {
        throw new Exception(ex.Message);
      }
    }

    protected override IQueryable<Veiculo> Get(Expression<Func<Veiculo, bool>> condition = null,
        Func<IQueryable<Veiculo>, IOrderedQueryable<Veiculo>> order = null) {
      condition = Predicate.And(condition, v => !v.Inativo);
      try {        
        return base.Get(condition, order)
                   .Include(v => v.Empresa).Include(v => v.CVeiculo)
                   .OrderBy(v => v.EmpresaId).ThenBy(v => v.Numero);
      }
      catch (DbException ex) {
        throw new Exception(ex.Message);
      }
    }
  }
}
