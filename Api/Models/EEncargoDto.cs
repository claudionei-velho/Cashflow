using System;

namespace Api.Models {
  public class EEncargoDto {
    public int Id { get; set; }
    public int EmpresaId { get; set; }
    public int EncargoId { get; set; }
    public string Formula { get; set; }
    public decimal Coeficiente { get; set; }
    public bool Vigente { get; set; }
    public DateTime? Cadastro { get; set; }

    // Navigation Properties
    public EmpresaDto Empresa { get; private set; }
    public EncargoDto Encargo { get; private set; }
  }
}
