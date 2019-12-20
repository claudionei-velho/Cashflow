using System;

namespace Domain.Models {
  public class SistFuncao {
    public int Id { get; private set; }
    public int SistemaId { get; private set; }
    public int Item { get; private set; }
    public int FuncaoId { get; private set; }
    public decimal Quantidade { get; private set; }
    public decimal SalBase { get; private set; }
    public decimal? Encargos { get; private set; }
    public decimal? Beneficios { get; private set; }
    public DateTime? Cadastro { get; private set; }

    // Navigation Properties
    public ESistema ESistema { get; private set; }
    public Funcao Funcao { get; private set; }
  }
}
