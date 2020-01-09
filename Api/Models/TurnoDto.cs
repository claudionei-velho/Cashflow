using System;

namespace Api.Models {
  public class TurnoDto {
    public int Id { get; set; }
    public int EmpresaId { get; set; }
    public string Denominacao { get; set; }
    public TimeSpan? Inicio { get; set; }
    public TimeSpan? Termino { get; set; }
    public DateTime? Cadastro { get; set; }

    // Navigation Properties
    public EmpresaDto Empresa { get; private set; }
  }
}
