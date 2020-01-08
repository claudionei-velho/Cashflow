using System.Collections.Generic;

namespace Domain.Models {
  public class FxEtaria {
    public int Id { get; private set; }
    public string Denominacao { get; private set; }
    public int Minimo { get; private set; }
    public int Maximo { get; private set; }

    public decimal Media => (Minimo + Maximo) * 0.5m;

    // Navigation Properties
    public ICollection<Depreciacao> Depreciacoes { get; private set; }
    public ICollection<Frota> Frotas { get; private set; }
    public ICollection<FrotaEtaria> FrotaEtarias { get; private set; }
  }
}
