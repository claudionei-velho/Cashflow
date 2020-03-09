using System.Collections.Generic;
using System.Threading.Tasks;

using Domain.Models;

namespace Domain.Interfaces.Services {
  public interface IMunicipioService : IServiceBase<Municipio> {
    Task<IEnumerable<Municipio>> GetExpertise();
  }
}
