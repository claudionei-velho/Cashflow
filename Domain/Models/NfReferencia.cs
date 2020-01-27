namespace Domain.Models {
  public class NfReferencia {
    public int Id { get; private set; }
    public int NotaId { get; private set; }
    public string ChaveNfeRef { get; private set; }
    public int AnoMes { get; private set; }
    public string Emitente { get; private set; }
    public int? FornecedorId { get; private set; }
    public int Modelo { get; private set; }
    public int Serie { get; set; }
    public int Numero { get; private set; }

    // Navigation Properties
    public Fornecedor Fornecedor { get; private set; }
    public NFiscal NFiscal { get; private set; }
  }
}
