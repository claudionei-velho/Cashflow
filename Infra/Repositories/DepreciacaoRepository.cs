using System;
using System.Data.Common;
using System.Linq;
using System.Linq.Expressions;

using Microsoft.EntityFrameworkCore;

using Domain.Interfaces.Repositories;
using Domain.Models;

namespace Infra.Repositories {
  public class DepreciacaoRepository : RepositoryBase<Depreciacao>, IDepreciacaoRepository {
    public DepreciacaoRepository(DataContext context) : base(context) { }

    public decimal? GetCoeficiente(int classeId, int etariaId) {
      Depreciacao dataset = GetFirst(
          d => (d.ClasseId == classeId) && (d.EtariaId == etariaId));

      try {        
        return (1 - dataset.ECVeiculo.Residual) * dataset.Anos / 
                        SomaIdade(d => d.ClasseId == classeId);
      }
      catch (DivideByZeroException) {
        return 1;
      }
    }

    public decimal? GetAcumulado(int classeId, int idade) {
      decimal? residual = GetData(d => d.ClasseId == classeId).Max(p => p.ECVeiculo.Residual);
      int[] idades = {
          SomaIdade(d => d.ClasseId == classeId && d.Anos >= idade),
          SomaIdade(d => d.ClasseId == classeId)
      };

      try {
        return (1 - residual) * idades[0] / idades[1];
      }
      catch (DivideByZeroException) {
        return 1 - residual;
      }
    }

    protected override IQueryable<Depreciacao> Get(Expression<Func<Depreciacao, bool>> condition = null,
        Func<IQueryable<Depreciacao>, IOrderedQueryable<Depreciacao>> order = null) {
      try {
        IQueryable<Depreciacao> query = _context.Set<Depreciacao>().AsNoTracking()
                                            .Include(d => d.ECVeiculo).Include(d => d.FxEtaria);
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

    private int SomaIdade(Expression<Func<Depreciacao, bool>> condition) {
      return _context.Set<Depreciacao>().AsNoTracking()
                 .Where(condition).Sum(d => d.Anos);
    }
  }
}
