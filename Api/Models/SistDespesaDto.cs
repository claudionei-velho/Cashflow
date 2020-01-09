using System;

namespace Api.Models {
  public class SistDespesaDto {
    public int Id { get; set; }
    public int SistemaId { get; set; }
    public int Item { get; set; }
    public int ContaId { get; set; }
    public decimal Quantidade { get; set; }
    public string Unidade { get; set; }
    public decimal ValorBase { get; set; }
    public DateTime? Cadastro { get; set; }

    // Navigation Properties
    public ContaDto Conta { get; private set; }
    public ESistemaDto ESistema { get; private set; }
  }
}
