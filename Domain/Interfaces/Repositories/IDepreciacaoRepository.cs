using Domain.Models;

namespace Domain.Interfaces.Repositories {
  public interface IDepreciacaoRepository : IRepositoryBase<Depreciacao> {
    decimal? GetCoeficiente(int classeId, int etariaId);
    decimal? GetAcumulado(int classeId, int idade);
  }
}
