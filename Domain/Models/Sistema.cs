using System;
using System.Collections.Generic;

namespace Domain.Models {
  public class Sistema {
    public int Id { get; private set; }
    public string Codigo { get; private set; }
    public string Denominacao { get; private set; }
    public DateTime? Cadastro { get; private set; }

    // Navigation Properties
    public ICollection<ESistema> ESistemas { get; private set; }
  }
}
