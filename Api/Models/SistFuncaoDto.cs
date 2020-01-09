using System;

namespace Api.Models {
  public class SistFuncaoDto {
    public int Id { get; set; }
    public int SistemaId { get; set; }
    public int Item { get; set; }
    public int FuncaoId { get; set; }
    public decimal Quantidade { get; set; }
    public decimal SalBase { get; set; }
    public decimal? Encargos { get; set; }
    public decimal? Beneficios { get; set; }
    public DateTime? Cadastro { get; set; }

    // Navigation Properties
    public ESistemaDto ESistema { get; private set; }
    public FuncaoDto Funcao { get; private set; }
  }
}
