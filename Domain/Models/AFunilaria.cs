namespace Domain.Models {
  public class AFunilaria {
    public int Id { get; private set; }
    public int InstalacaoId { get; private set; }
    public decimal? Area { get; private set; }
    public bool Isolada { get; private set; }
    public byte? PPoluicao { get; private set; }
    public byte? Exaustao { get; private set; }

    // Navigation Properties
    public EInstalacao EInstalacao { get; private set; }
  }
}
