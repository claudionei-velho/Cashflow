namespace Domain.Models {
  public class AAbastece {
    public int Id { get; private set; }
    public int InstalacaoId { get; private set; }
    public int PavimentoId { get; private set; }
    public int Bombas { get; private set; }

    // Navigation Properties
    public EInstalacao EInstalacao { get; private set; }
    public Via Via { get; private set; }
  }
}
