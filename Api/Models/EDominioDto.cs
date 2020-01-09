using System;

namespace Api.Models {
  public class EDominioDto {
    public int Id { get; set; }
    public int EmpresaId { get; set; }
    public int DominioId { get; set; }
    public DateTime? Cadastro { get; set; }

    // Navigation Properties
    public DominioDto Dominio { get; private set; }
    public EmpresaDto Empresa { get; private set; }
  }
}
