using System;

namespace Api.Models {
  public class VEquipamentoDto {
    public int Id { get; set; }
    public int EmpresaId { get; set; }
    public string Denominacao { get; set; }
    public string Unidade { get; set; }
    public int? Depreciacao { get; set; }

    public decimal? Coeficiente {
      get {
        try {
          if (this.Depreciacao != null) {
            return 1 / (decimal)this.Depreciacao.Value;
          }
        }
        catch (DivideByZeroException) {
          return null;
        }
        return null;
      }
    }

    public DateTime? Cadastro { get; set; }

    // Navigation Properties
    public EmpresaDto Empresa { get; private set; }
  }
}
