using System;

namespace Domain.Models {
  public class Salario {
    public int Id { get; private set; }
    public int FuncaoId { get; private set; }
    public int Ano { get; private set; }
    public int Mes { get; private set; }
    public decimal SalBase { get; private set; }
    public decimal? Encargos { get; private set; }
    public decimal? Beneficios { get; private set; }
    public DateTime? Cadastro { get; private set; }

    // Navigation Properties
    public Funcao Funcao { get; private set; }
  }
}
