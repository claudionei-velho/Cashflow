namespace Domain.Models {
  public class AAdmin {
    public int Id { get; private set; }
    public int InstalacaoId { get; private set; }
    public decimal? Requisitom2 { get; private set; }
    public decimal Disponivelm2 { get; private set; }

    // Navigation Properties
    public EInstalacao EInstalacao { get; private set; }
  }
}
