using System;

namespace Api.Models {
  public class ContaDto {
    public int Id { get; set; }
    public int EmpresaId { get; set; }
    public string Classificacao { get; set; }
    public string Denominacao { get; set; }
    public int? VinculoId { get; set; }
    public DateTime? Cadastro { get; set; }

    // Navigation Properties
    public ContaDto Vinculo { get; private set; }
    public EmpresaDto Empresa { get; private set; }
  }
}
