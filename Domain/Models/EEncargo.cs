using System;

namespace Domain.Models {
  public class EEncargo {
    public int Id { get; private set; }
    public int EmpresaId { get; private set; }
    public int EncargoId { get; private set; }
    public string Formula { get; private set; }
    public decimal Coeficiente { get; private set; }
    public bool Vigente { get; private set; }
    public DateTime? Cadastro { get; private set; }

    // Navigation Properties
    public Empresa Empresa { get; private set; }
    public Encargo Encargo { get; private set; }
  }
}
