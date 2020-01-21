using System;

namespace Domain.Extensions {
  [Flags]
  public enum ForeignKey : byte {
    MunicipioId = 1,
    EmpresaId = 2
  }
}
