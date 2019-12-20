using System;

namespace Api.Models {
  public class CargoDto {
    public int Id { get; set; }
    public int EmpresaId { get; set; }
    public string Codigo { get; set; }
    public string Denominacao { get; set; }
    public string Titulo { get; set; }
    public string Cbo { get; set; }
    public string Descricao { get; set; }
    public DateTime? Cadastro { get; set; }

    // Navigation Properties
    public EmpresaDto Empresa { get; set; }
  }
}
