using System;

namespace Domain.Models {
  public class Turno {
    public int Id { get; private set; }
    public int EmpresaId { get; private set; }
    public string Denominacao { get; private set; }
    public TimeSpan? Inicio { get; private set; }
    public TimeSpan? Termino { get; private set; }
    public DateTime? Cadastro { get; private set; }

    // Navigation Properties
    public Empresa Empresa { get; private set; }
  }
}
