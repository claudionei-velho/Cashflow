using System;

namespace Domain.Models {
  public class SistDespesa {
    public int Id { get; private set; }
    public int SistemaId { get; private set; }
    public int Item { get; private set; }
    public int ContaId { get; private set; }
    public decimal Quantidade { get; private set; }
    public string Unidade { get; private set; }
    public decimal ValorBase { get; private set; }
    public DateTime? Cadastro { get; private set; }

    // Navigation Properties
    public Conta Conta { get; private set; }
    public ESistema ESistema { get; private set; }
  }
}
