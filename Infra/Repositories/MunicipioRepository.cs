using System;
using System.Collections.Generic;
using System.Data.Common;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;

using Microsoft.EntityFrameworkCore;

using Domain.Interfaces.Repositories;
using Domain.Models;

namespace Infra.Repositories {
  public class MunicipioRepository : RepositoryBase<Municipio>, IMunicipioRepository {
    public MunicipioRepository(DataContext context) : base(context) { }

    public async Task<IEnumerable<Municipio>> GetExpertise() {
      try {
        return await (from city in _context.Municipios
                      where _context.Set<Empresa>().Select(e => e.MunicipioId).Union(
                                _context.Set<Consorcio>().Select(c => c.MunicipioId)
                            ).Distinct().ToArray().Contains(city.Id)
                      orderby city.Nome
                      select city).Include(m => m.Uf).ToListAsync();
      }
      catch (DbException ex) {
        throw new Exception(ex.Message);
      }
    }

    protected override IQueryable<Municipio> Get(Expression<Func<Municipio, bool>> condition = null,
        Func<IQueryable<Municipio>, IOrderedQueryable<Municipio>> order = null) {
      try {
        return base.Get(condition, order).Include(m => m.Uf);
      }
      catch (DbException ex) {
        throw new Exception(ex.Message);
      }
    }
  }
}
