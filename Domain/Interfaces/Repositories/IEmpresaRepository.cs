using System.Collections.Generic;
using System.Threading.Tasks;

using Domain.Models;

namespace Domain.Interfaces.Repositories {
  public interface IEmpresaRepository : IRepositoryBase<Empresa> {
    // Task<IEnumerable<string>> ListCities();
  }
}
