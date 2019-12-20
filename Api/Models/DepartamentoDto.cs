using System;

namespace Api.Models {
  public class DepartamentoDto {
    public int Id { get; set; }
    public int SetorId { get; set; }
    public string Codigo { get; set; }
    public string Denominacao { get; set; }
    public string Descricao { get; set; }
    public int? ResponsavelId { get; set; }
    public DateTime? Cadastro { get; set; }

    // Navigation Properties
    public CargoDto Cargo { get; set; }
    public SetorDto Setor { get; set; }
  }
}
