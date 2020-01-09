namespace Api.Models {
  public class DepreciacaoDto {
    public int ClasseId { get; set; }
    public int EtariaId { get; set; }
    public int Anos { get; set; }
    public decimal? Coeficiente { get; set; }
    public decimal? Acumulado { get; set; }

    // Navigation Properties
    public ECVeiculoDto ECVeiculo { get; private set; }
    public FxEtariaDto FxEtaria { get; private set; }
  }
}
