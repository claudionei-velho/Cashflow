using System;

namespace Api.Models {
  public class CentroDto {
    public int Id { get; set; }
    public int EmpresaId { get; set; }
    public string Classificacao { get; set; }
    public string Denominacao { get; set; }
    public int? VinculoId { get; set; }
    public DateTime? Cadastro { get; set; }

    // Navigation Properties
    public CentroDto Vinculo { get; set; }
    public EmpresaDto Empresa { get; set; }
  }
}
