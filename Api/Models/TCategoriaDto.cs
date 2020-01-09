using System;

namespace Api.Models {
  public class TCategoriaDto {
    public int Id { get; set; }
    public int EmpresaId { get; set; }
    public string Denominacao { get; set; }
    public bool Gratuidade { get; set; }
    public decimal? Rateio { get; set; }
    public DateTime? Cadastro { get; set; }

    // Navigation Properties
    public EmpresaDto Empresa { get; private set; }
  }
}
