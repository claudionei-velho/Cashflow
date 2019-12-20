namespace Domain.Models {
  public class AAlmox {
    public int Id { get; private set; }
    public int InstalacaoId { get; private set; }
    public decimal? Area { get; private set; }
    public bool Especifico { get; private set; }
    public bool Estoque { get; private set; }

    // Navigation Properties
    public EInstalacao EInstalacao { get; private set; }
  }
}
