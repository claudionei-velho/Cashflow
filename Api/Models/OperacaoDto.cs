using System;

namespace Api.Models {
  public class OperacaoDto {
    public int Id { get; set; }
    public int EmpresaId { get; set; }
    public int OpLinhaId { get; set; }
    public DateTime? Cadastro { get; set; }

    // Navigation Properties
    public EmpresaDto Empresa { get; set; }
    public OpLinhaDto OpLinha { get; set; }
  }
}
