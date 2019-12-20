using System;

namespace Api.Models {
  public class ProducaoDto {
    public int Id { get; set; }
    public int EmpresaId { get; set; }
    public int Ano { get; set; }
    public int Mes { get; set; }
    public int TarifariaId { get; set; }
    public int? LinhaId { get; set; }
    public int Passageiros { get; set; }
    public DateTime? Cadastro { get; set; }

    // Navigation Properties
    public EmpresaDto Empresa { get; set; }
    public LinhaDto Linha { get; set; }
    public TCategoriaDto TCategoria { get; set; }
  }
}
