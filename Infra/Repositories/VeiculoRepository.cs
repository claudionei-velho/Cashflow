using System;
using System.Data.Common;
using System.Linq;
using System.Linq.Expressions;

using Microsoft.EntityFrameworkCore;

using Domain.Interfaces.Repositories;
using Domain.Models;

namespace Infra.Repositories {
  public class VeiculoRepository : RepositoryBase<Veiculo>, IVeiculoRepository {
    public VeiculoRepository(DataContext context) : base(context) { }

    public IQueryable<Veiculo> GetNoChassi(Expression<Func<Veiculo, bool>> condition = null,
        Func<IQueryable<Veiculo>, IOrderedQueryable<Veiculo>> order = null) {
      try {
        int[] selected = _context.Set<Chassi>().AsNoTracking()
                             .Select(q => q.VeiculoId).Distinct().ToArray();

        IQueryable<Veiculo> query = GetListVeiculos(selected);
        if (condition != null) {
          query = query.Where(condition);
        }
        if (order != null) {
          query = order(query);
        }
        return query;
      }
      catch (DbException ex) {
        throw new Exception(ex.Message);
      }
    }

    public IQueryable<Veiculo> GetNoCarroceria(Expression<Func<Veiculo, bool>> condition = null,
        Func<IQueryable<Veiculo>, IOrderedQueryable<Veiculo>> order = null) {
      try {
        int[] selected = _context.Set<Carroceria>().AsNoTracking()
                             .Select(q => q.VeiculoId).Distinct().ToArray();

        IQueryable<Veiculo> query = GetListVeiculos(selected);
        if (condition != null) {
          query = query.Where(condition);
        }
        if (order != null) {
          query = order(query);
        }
        return query;
      }
      catch (DbException ex) {
        throw new Exception(ex.Message);
      }
    }

    protected override IQueryable<Veiculo> Get(Expression<Func<Veiculo, bool>> condition = null,
        Func<IQueryable<Veiculo>, IOrderedQueryable<Veiculo>> order = null) {
      try {
        if (condition == null) {
          condition = v => !v.Inativo;
        }
        return base.Get(condition, order)
                   .Include(v => v.Empresa).Include(v => v.CVeiculo)
                   .OrderBy(v => v.EmpresaId).ThenBy(v => v.Numero);
      }
      catch (DbException ex) {
        throw new Exception(ex.Message);
      }
    }

    private IQueryable<Veiculo> GetListVeiculos(int[] list) {
      return (from v in Get()
              where !list.Contains(v.Id)
              select v).AsNoTracking();
    }
  }
}
