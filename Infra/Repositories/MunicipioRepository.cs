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
        int[] cities = _context.Set<Empresa>().AsNoTracking()
                           .Select(e => e.MunicipioId).Distinct().ToArray();

        return (from city in _context.Municipios
                where cities.Contains(city.Id)
                orderby city.Nome
                select city).AsNoTracking().Include(m => m.Uf);
      }
      catch (DbException ex) {
        throw new Exception(ex.Message);
      }
    }

    protected override IQueryable<Municipio> Get(Expression<Func<Municipio, bool>> condition = null, 
        Func<IQueryable<Municipio>, IOrderedQueryable<Municipio>> order = null) {
      try {
        IQueryable<Municipio> query = _context.Set<Municipio>().AsNoTracking().Include(m => m.Uf);
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
  }
}
