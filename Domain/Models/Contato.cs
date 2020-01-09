using System;

namespace Domain.Models {
  public class Contato {
    public int Id { get; private set; }
    public int EmpresaId { get; private set; }
    public string Nome { get; private set; }
    public string Cargo { get; private set; }
    public string Telefone { get; private set; }
    public string Email { get; private set; }
    public DateTime? Cadastro { get; private set; }

    // Navigation Properties
    public Empresa Empresa { get; private set; }
  }
}
