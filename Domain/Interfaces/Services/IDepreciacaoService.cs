using Domain.Models;

namespace Domain.Interfaces.Services {
  public interface IDepreciacaoService : IServiceBase<Depreciacao> {
    decimal? GetCoeficiente(int classeId, int etariaId);
    decimal? GetAcumulado(int classeId, int idade);
  }
}
