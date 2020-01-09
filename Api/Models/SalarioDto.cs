using System;

namespace Api.Models {
  public class SalarioDto {
    public int Id { get; set; }
    public int FuncaoId { get; set; }
    public int Ano { get; set; }
    public int Mes { get; set; }
    public decimal SalBase { get; set; }
    public decimal? Encargos { get; set; }
    public decimal? Beneficios { get; set; }
    public DateTime? Cadastro { get; set; }

    // Navigation Properties
    public FuncaoDto Funcao { get; private set; }
  }
}
