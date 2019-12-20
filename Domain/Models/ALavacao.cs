namespace Domain.Models {
  public class ALavacao {
    public int Id { get; private set; }
    public int InstalacaoId { get; private set; }
    public int? Maquinas { get; private set; }
    public decimal Aguam3 { get; private set; }

    // Navigation Properties
    public EInstalacao EInstalacao { get; private set; }
  }
}
