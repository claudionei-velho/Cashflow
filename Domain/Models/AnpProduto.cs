using System.Collections.Generic;

namespace Domain.Models {
  public class AnpProduto {
    public int Id { get; private set; }
    public string Denominacao { get; private set; }
    public bool Informar { get; private set; }

    // Navigation Properties
    public ICollection<CstCombustivel> CstCombustiveis { get; private set; }
    public ICollection<PCombustivel> PCombustiveis { get; private set; }
  }
}
