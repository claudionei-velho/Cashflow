using System;

namespace Api.Models {
  public class NfReferenciaDto {
    public int Id { get; set; }
    public int NotaId { get; set; }
    public string ChaveNfeRef { get; set; }
    public int AnoMes { get; set; }
    public string Emitente { get; set; }
    public int? FornecedorId { get; set; }
    public int Modelo { get; set; }
    public int Serie { get; set; }
    public int Numero { get; set; }
    public DateTime? Cadastro { get; set; }

    // Navigation Properties
    public FornecedorDto Fornecedor { get; private set; }
    public NFiscalDto NFiscal { get; private set; }
  }
}
