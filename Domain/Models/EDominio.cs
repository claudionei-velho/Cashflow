using System;
using System.Collections.Generic;

namespace Domain.Models {
  public class EDominio {
    public int Id { get; private set; }
    public int EmpresaId { get; private set; }
    public int DominioId { get; private set; }
    public DateTime? Cadastro { get; private set; }

    // Navigation Properties
    public Dominio Dominio { get; private set; }
    public Empresa Empresa { get; private set; }

    public ICollection<Linha> Linhas { get; private set; }
  }
}
