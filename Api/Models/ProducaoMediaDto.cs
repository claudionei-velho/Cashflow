using System;

namespace Api.Models {
  public class ProducaoMediaDto {
    public int EmpresaId { get; set; }
    public int Ano { get; set; }
    public int TarifariaId { get; set; }
    public int? Passageiros { get; set; }
    public int? Mensal { get; set; }
    public decimal? Equivalente { get; set; }
    public int? MensalEqv { get; set; }

    public decimal? Equivalencia {
      get {
        try {
          if (this.Equivalente != null) {
            return (decimal)this.Equivalente / this.Passageiros;
          }
        }
        catch (DivideByZeroException) {
          return null;
        }
        return null;
      }
    }

    public decimal? Ratio { get; set; }

    // Navigation Properties
    public EmpresaDto Empresa { get; private set; }
    public TCategoriaDto TCategoria { get; private set; }
  }
}
