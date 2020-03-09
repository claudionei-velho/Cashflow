using System.Collections.Generic;
using System.Threading.Tasks;

using Domain.Models;

namespace Domain.Interfaces.Repositories {
  public interface IMunicipioRepository : IRepositoryBase<Municipio> {
    Task<IEnumerable<Municipio>> GetExpertise();
  }
}
