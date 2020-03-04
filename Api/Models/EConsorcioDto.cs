using System;

namespace Api.Models {
  public class EConsorcioDto {
    public int Id { get; set; }
    public int ConsorcioId { get; set; }
    public int EmpresaId { get; set; }
    public decimal Ratio { get; set; }
    public DateTime Integracao { get; set; }
    public string Documento { get; set; }
    public DateTime? Desintegracao { get; set; }

    public bool Ativo {
      get {
        return Desintegracao == null;
      }
    }

    public string Responsavel { get; set; }
    public string CpfResponsavel { get; set; }
    public DateTime? Cadastro { get; set; }

    // Navigation Properties
    public ConsorcioDto Consorcio { get; private set; }
    public EmpresaDto Empresa { get; private set; }
  }
}
