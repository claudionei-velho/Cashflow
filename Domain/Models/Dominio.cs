using System.Collections.Generic;

namespace Domain.Models {
  public class Dominio {
    public int Id { get; private set; }
    public string Denominacao { get; private set; }
    public string Descricao { get; private set; }

    // Navigation Properties
    public ICollection<EDominio> EDominios { get; private set; }
  }
}
