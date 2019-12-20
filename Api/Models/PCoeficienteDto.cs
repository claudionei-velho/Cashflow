using System;

namespace Api.Models {
  public class PCoeficienteDto {
    public int Id { get; set; }
    public int EmpresaId { get; set; }
    public int Ano { get; set; }
    public int Mes { get; set; }
    public decimal? Reserva { get; set; }
    public decimal? Improdutiva { get; set; }
    public decimal? Arla { get; set; }
    public decimal? Lubrificante { get; set; }
    public decimal? UtilPneus { get; set; }
    public decimal? Recapagens { get; set; }
    public decimal? Pecas { get; set; }
    public DateTime? Cadastro { get; set; }

    // Navigation Properties
    public EmpresaDto Empresa { get; set; }
  }
}
