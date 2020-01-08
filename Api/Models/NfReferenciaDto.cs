using System;

namespace Api.Models {
  public class NfReferenciaDto {
    public int Id { get; private set; }
    public int NotaId { get; private set; }
    public string ChaveNfeRef { get; private set; }
    public int AnoMes { get; private set; }
    public string Emitente { get; private set; }
    public int? FornecedorId { get; private set; }
    public int Modelo { get; private set; }
    public int Serie { get; set; }
    public int Numero { get; private set; }
    public DateTime? Cadastro { get; private set; }

    // Navigation Properties
    public FornecedorDto Fornecedor { get; private set; }
    public NFiscalDto NFiscal { get; private set; }
  }
}
