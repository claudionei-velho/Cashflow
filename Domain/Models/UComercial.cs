using System.Collections.Generic;

namespace Domain.Models {
  public class UComercial {
    public string Id { get; private set; }
    public string Denominacao { get; private set; }

    // Navigation Properties
    public ICollection<Produto> Produtos { get; private set; }
  }
}
