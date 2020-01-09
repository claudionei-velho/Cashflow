using System;

namespace Api.Models {
  public class NfEntregaDto {
    public int Id { get; set; }
    public int NotaId { get; set; }
    public string Endereco { get; set; }
    public string EnderecoNo { get; set; }
    public string Complemento { get; set; }
    public string Bairro { get; set; }
    public int MunicipioId { get; set; }
    public DateTime? Cadastro { get; set; }

    // Navigation Properties
    public MunicipioDto Municipio { get; private set; }
    public NFiscalDto NFiscal { get; private set; }
  }
}
