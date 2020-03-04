using System;
using System.Collections.Generic;

namespace Domain.Models {
  public class CLinha {
    public int Id { get; private set; }
    public int EmpresaId { get; private set; }
    public int ClassLinhaId { get; private set; }
    public DateTime? Cadastro { get; private set; }

    // Navigation Properties
    public ClassLinha ClassLinha { get; private set; }
    public Empresa Empresa { get; private set; }

    public ICollection<Linha> Linhas { get; private set; }
  }
}
