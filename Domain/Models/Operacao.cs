using System;
using System.Collections.Generic;

namespace Domain.Models {
  public class Operacao {
    public int Id { get; private set; }
    public int EmpresaId { get; private set; }
    public int OpLinhaId { get; private set; }
    public DateTime? Cadastro { get; private set; }

    // Navigation Properties
    public Empresa Empresa { get; private set; }
    public OpLinha OpLinha { get; private set; }

    public ICollection<Linha> Linhas { get; private set; }
  }
}
