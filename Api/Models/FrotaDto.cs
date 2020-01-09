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
    public CVeiculoDto CVeiculo { get; private set; }
    public EmpresaDto Empresa { get; private set; }
    public FxEtariaDto FxEtaria { get; private set; }
  }
}
