using System;

namespace Domain.Extensions {
  [Flags]
  public enum ForeignKey {
    MunicipioId = 1,
    EmpresaId = 2
  }
}
