namespace Domain.Models {
  public class ALubrifica {
    public int Id { get; private set; }
    public int InstalacaoId { get; private set; }
    public byte? Lavacao { get; private set; }
    public bool Ceramico { get; private set; }
    public bool Protecao { get; private set; }

    // Navigation Properties
    public EInstalacao EInstalacao { get; private set; }
  }
}
