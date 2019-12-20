using System;
using System.Collections.Generic;

namespace Domain.Models {
  public class ESistema {
    public int Id { get; private set; }
    public int EmpresaId { get; private set; }
    public int SistemaId { get; private set; }
    public string Codigo { get; private set; }
    public string Denominacao { get; private set; }
    public int? ResponsavelId { get; private set; }
    public int? Util { get; private set; }
    public decimal? Depreciacao { get; private set; }
    public decimal? Residual { get; private set; }

    public decimal? Coeficiente {
      get {
        try {
          if (Util != null) {
            return 1 / (decimal)Util.Value;
          }
        }
        catch (DivideByZeroException) {
          return null;
        }
        return null;
      }
    }

    public decimal? CoeficienteAno => Coeficiente * CustomCalendar.MonthsPerYear;

    public DateTime? Cadastro { get; private set; }

    // Navigation Properties
    public Cargo Cargo { get; private set; }
    public Empresa Empresa { get; private set; }
    public Sistema Sistema { get; private set; }

    public ICollection<SistDespesa> SistDespesas { get; private set; }
    public ICollection<SistFuncao> SistFuncoes { get; private set; }
  }
}
