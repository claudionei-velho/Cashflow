using System;
using System.Collections.Generic;
using System.Data.Common;
using System.Linq;
using System.Linq.Expressions;

using Microsoft.EntityFrameworkCore;

using Domain.Interfaces.Repositories;
using Domain.Models;
using System.Threading.Tasks;

namespace Infra.Repositories {
  public class EmpresaRepository : RepositoryBase<Empresa>, IEmpresaRepository {
    public EmpresaRepository(DataContext context) : base(context) { }

    //public async Task<IEnumerable<string>> ListCities() {
    //  try {
    //    return await _context.Set<Empresa>().AsNoTracking()
    //                     .Select(e => e.Municipio).Distinct().ToListAsync();
    //  }
    //  catch (DbException ex) {
    //    throw new Exception(ex.Message);
    //  }
    //}

    protected override IQueryable<Empresa> Get(Expression<Func<Empresa, bool>> condition = null, 
        Func<IQueryable<Empresa>, IOrderedQueryable<Empresa>> order = null) {
      try {
        IQueryable<Empresa> query = _context.Set<Empresa>().AsNoTracking()
                                        .Include(e => e.Cidade).Include(e => e.Pais);
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
