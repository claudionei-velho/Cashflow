using System.Collections.Generic;

namespace Domain.Models {
  public class Encargo {
    public int Id { get; private set; }
    public char Grupo { get; private set; }
    public string Denominacao { get; private set; }
    public string Observacao { get; private set; }

    // Navigation Properties
    public ICollection<EEncargo> EEncargos { get; private set; }
  }
}
