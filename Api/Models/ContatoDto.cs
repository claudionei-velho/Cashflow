using System;

namespace Api.Models {
  public class ContatoDto {
    public int Id { get; set; }
    public int EmpresaId { get; set; }
    public string Nome { get; set; }
    public string Cargo { get; set; }
    public string Telefone { get; set; }
    public string Email { get; set; }
    public DateTime? Cadastro { get; set; }

    // Navigation Properties
    public EmpresaDto Empresa { get; private set; }
  }
}
