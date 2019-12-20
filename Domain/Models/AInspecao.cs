namespace Domain.Models {
  public class AInspecao {
    public int Id { get; private set; }
    public int InstalacaoId { get; private set; }
    public int Rampas { get; private set; }

    // Navigation Properties
    public EInstalacao EInstalacao { get; private set; }
  }
}
