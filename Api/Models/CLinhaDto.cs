using System;

namespace Api.Models {
  public class CLinhaDto {
    public int Id { get; set; }
    public int EmpresaId { get; set; }
    public int ClassLinhaId { get; set; }
    public DateTime? Cadastro { get; set; }

    // Navigation Properties
    public ClassLinhaDto ClassLinha { get; private set; }
    public EmpresaDto Empresa { get; private set; }
  }
}
