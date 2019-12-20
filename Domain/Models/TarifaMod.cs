using System;

namespace Domain.Models {
  public class TarifaMod {
    public int Id { get; private set; }
    public int EmpresaId { get; private set; }
    public string Denominacao { get; private set; }
    public bool Gratuidade { get; private set; }
    public decimal? Rateio { get; private set; }
    public decimal? Tarifa { get; private set; }
    public DateTime? Cadastro { get; private set; }

    // Navigation Properties
    public Empresa Empresa { get; private set; }
  }
}
