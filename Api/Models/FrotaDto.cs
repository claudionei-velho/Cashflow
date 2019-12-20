namespace Api.Models {
  public class FrotaDto {
    public int Id { get; set; }
    public int EmpresaId { get; set; }
    public int Ano { get; set; }
    public int Mes { get; set; }
    public int CategoriaId { get; set; }
    public int EtariaId { get; set; }
    public bool ArCondicionado { get; set; }
    public int Quantidade { get; set; }

    // Navigation Properties
    public CVeiculoDto CVeiculo { get; set; }
    public EmpresaDto Empresa { get; set; }
    public FxEtariaDto FxEtaria { get; set; }
  }
}
