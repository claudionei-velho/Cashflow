using System;

namespace Domain.Models {
  public class PCoeficiente {
    public int Id { get; private set; }
    public int EmpresaId { get; private set; }
    public int Ano { get; private set; }
    public int Mes { get; private set; }
    public decimal? Reserva { get; private set; }
    public decimal? Improdutiva { get; private set; }
    public decimal? Arla { get; private set; }
    public decimal? Lubrificante { get; private set; }
    public decimal? UtilPneus { get; private set; }
    public decimal? Recapagens { get; private set; }
    public decimal? Pecas { get; private set; }
    public DateTime? Cadastro { get; private set; }

    // Navigation Properties
    public Empresa Empresa { get; private set; }
  }
}
