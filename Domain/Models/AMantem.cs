namespace Domain.Models {
  public class AMantem {
    public int Id { get; private set; }
    public int InstalacaoId { get; private set; }
    public decimal? Area { get; private set; }
    public int? PontosAr { get; private set; }
    public byte Eletricidade { get; private set; }
    public int? Elevadores { get; private set; }

    // Navigation Properties
    public EInstalacao EInstalacao { get; private set; }
  }
}
