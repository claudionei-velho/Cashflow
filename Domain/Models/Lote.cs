using System;
using System.Collections.Generic;

namespace Domain.Models {
  public class Lote {
    public int Id { get; private set; }
    public int BaciaId { get; private set; }
    public string Denominacao { get; private set; }
    public DateTime? Cadastro { get; private set; }

    // Navigation Properties
    public Bacia Bacia { get; private set; }

    public ICollection<Linha> Linhas { get; private set; }
  }
}
