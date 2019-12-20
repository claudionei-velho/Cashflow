using System.Collections.Generic;

namespace Domain.Models {
  public class ClassLinha {
    public int Id { get; private set; }
    public string Denominacao { get; private set; }
    public string Descricao { get; private set; }

    // Navigation Properties
    public ICollection<CLinha> CLinhas { get; private set; }
  }
}
