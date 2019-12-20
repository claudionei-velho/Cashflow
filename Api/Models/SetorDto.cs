using System;

namespace Api.Models {
  public class SetorDto {
    public int Id { get; set; }
    public int EmpresaId { get; set; }
    public string Codigo { get; set; }
    public string Denominacao { get; set; }
    public string Descricao { get; set; }
    public int? ResponsavelId { get; set; }
    public int? VinculoId { get; set; }
    public DateTime? Cadastro { get; set; }

    // Navigation Properties
    public CargoDto Cargo { get; set; }
    public EmpresaDto Empresa { get; set; }
    public SetorDto Vinculo { get; set; }
  }
}
