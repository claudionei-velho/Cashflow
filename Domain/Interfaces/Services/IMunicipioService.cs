using System.Linq;

using Domain.Models;

namespace Domain.Interfaces.Services {
  public interface IMunicipioService : IServiceBase<Municipio> {
    IQueryable<Municipio> GetExpertise();
  }
}
