namespace Domain.Models {
  public class ATrafego {
    public int Id { get; private set; }
    public int InstalacaoId { get; private set; }
    public byte? Plantao { get; private set; }
    public byte? Reserva { get; private set; }
    public byte? Equipamentos { get; private set; }
    public byte? Mobiliario { get; private set; }

    // Navigation Properties
    public EInstalacao EInstalacao { get; private set; }
  }
}
