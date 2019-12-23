using System.Linq;

using Domain.Models;

namespace Domain.Interfaces.Repositories {
  public interface IMunicipioRepository : IRepositoryBase<Municipio> {
    IQueryable<Municipio> GetExpertise();
  }
}
