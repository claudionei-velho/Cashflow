using System;
using System.Data.Common;
using System.Linq;
using System.Linq.Expressions;

using Microsoft.EntityFrameworkCore;

using Domain.Interfaces.Repositories;
using Domain.Models;

namespace Infra.Repositories {
  public class MunicipioRepository : RepositoryBase<Municipio>, IMunicipioRepository {
    public MunicipioRepository(DataContext context) : base(context) { }

    public IQueryable<Municipio> GetExpertise() {
      try {
        int[] cities = _context.Empresas.Select(e => e.MunicipioId).Union(
                           _context.Consorcios.Select(c => c.MunicipioId)
                       ).Distinct().ToArray();

        return (from city in _context.Municipios
                where cities.Contains(city.Id)
                orderby city.Nome
                select city).Include(m => m.Uf).AsNoTracking();
      }
      catch (DbException ex) {
        throw new Exception(ex.Message);
      }
    }

    protected override IQueryable<Municipio> Get(Expression<Func<Municipio, bool>> condition = null, 
        Func<IQueryable<Municipio>, IOrderedQueryable<Municipio>> order = null) {
      try {
        return base.Get(condition, order).Include(m => m.Uf).AsNoTracking();
      }
      catch (DbException ex) {
        throw new Exception(ex.Message);
      }
    }
  }
}
