namespace Domain.Models {
  public class Depreciacao {
    public int ClasseId { get; private set; }
    public int EtariaId { get; private set; }
    public int Anos { get; private set; }
    public decimal? Coeficiente { get; set; }
    public decimal? Acumulado { get; set; }

    // Navigation Properties
    public ECVeiculo ECVeiculo { get; private set; }
    public FxEtaria FxEtaria { get; private set; }
  }
}
