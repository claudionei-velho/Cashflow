using System;
using System.Collections.Generic;

namespace Domain.Models {
  public class VEquipamento {
    public int Id { get; private set; }
    public int EmpresaId { get; private set; }
    public string Denominacao { get; private set; }
    public string Unidade { get; private set; }
    public int? Depreciacao { get; private set; }

    public decimal? Coeficiente {
      get {
        try {
          if (Depreciacao != null) {
            return 1 / (decimal)Depreciacao;
          }
        }
        catch (DivideByZeroException) {
          return null;
        }
        return null;
      }
    }

    public DateTime? Cadastro { get; private set; }

    // Navigation Properties
    public Empresa Empresa { get; private set; }

    public ICollection<Embarcado> Embarcados { get; private set; }
  }
}
