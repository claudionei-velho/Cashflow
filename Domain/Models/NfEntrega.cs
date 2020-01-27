namespace Domain.Models {
  public class NfEntrega {
    public int Id { get; private set; }
    public int NotaId { get; private set; }
    public string Endereco { get; private set; }
    public string EnderecoNo { get; private set; }
    public string Complemento { get; private set; }
    public string Bairro { get; private set; }
    public int MunicipioId { get; private set; }

    // Navigation Properties
    public Municipio Municipio { get; private set; }
    public NFiscal NFiscal { get; private set; }
  }
}
