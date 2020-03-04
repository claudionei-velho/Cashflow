using System;

namespace Domain.Models {
  public class EConsorcio {
    public int Id { get; private set; }
    public int ConsorcioId { get; private set; }
    public int EmpresaId { get; private set; }
    public decimal Ratio { get; private set; }
    public DateTime Integracao { get; private set; }
    public string Documento { get; private set; }
    public DateTime? Desintegracao { get; private set; }

    public bool Ativo {
      get {
        return Desintegracao == null;
      }
    }

    public string Responsavel { get; private set; }
    public string CpfResponsavel { get; private set; }
    public DateTime? Cadastro { get; private set; }

    // Navigation Properties
    public Consorcio Consorcio { get; private set; }
    public Empresa Empresa { get; private set; }
  }
}
