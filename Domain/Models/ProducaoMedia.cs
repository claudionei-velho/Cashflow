using System;

namespace Domain.Models {
  public class ProducaoMedia {
    public int EmpresaId { get; private set; }
    public int Ano { get; private set; }
    public int TarifariaId { get; private set; }
    public int? Passageiros { get; private set; }
    public int? Mensal { get; private set; }
    public decimal? Equivalente { get; private set; }
    public int? MensalEqv { get; private set; }

    public decimal? Equivalencia {
      get {
        try {
          if (Passageiros != null) {
            if (Equivalente != null) {
              return (decimal)Equivalente / Passageiros;
            }
          }
        }
        catch (DivideByZeroException) {
          return null;
        }
        return null;
      }
    }

    public decimal? Ratio { get; private set; }

    // Navigation Properties
    public Empresa Empresa { get; private set; }
    public TCategoria TCategoria { get; private set; }
  }
}
