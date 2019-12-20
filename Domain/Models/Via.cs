using System.Collections.Generic;

namespace Domain.Models {
  public class Via {
    public int Id { get; private set; }
    public string Denominacao { get; private set; }

    // Navigation Properties
    public ICollection<AAbastece> Abastecimentos { get; private set; }
  }
}
