using System;

using Domain.Extensions;

namespace Api.Models {
  public class ESistemaDto {
    public int Id { get; set; }
    public int EmpresaId { get; set; }
    public int SistemaId { get; set; }
    public string Codigo { get; set; }
    public string Denominacao { get; set; }
    public int? ResponsavelId { get; set; }
    public int? Util { get; set; }
    public decimal? Depreciacao { get; set; }
    public decimal? Residual { get; set; }

    public decimal? Coeficiente {
      get {
        try {
          if (this.Util != null) {
            return 1 / (decimal)this.Util.Value;
          }
        }
        catch (DivideByZeroException) {
          return null;
        }
        return null;
      }
    }

    public decimal? CoeficienteAno => Coeficiente * CustomCalendar.MonthsPerYear;

    public DateTime? Cadastro { get; set; }

    // Navigation Properties
    public CargoDto Cargo { get; private set; }
    public EmpresaDto Empresa { get; private set; }
    public SistemaDto Sistema { get; private set; }
  }
}
