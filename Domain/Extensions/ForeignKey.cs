using System;

namespace Domain.Extensions {
  [Flags]
  public enum ForeignKey : byte {
    MunicipioId = 0,
    EmpresaId = 1
  }
}
