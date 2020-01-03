using System;

namespace Api.Models {
  public class EConsorcioDto {
    public int Id { get; private set; }
    public int ConsorcioId { get; private set; }
    public int EmpresaId { get; private set; }
    public decimal Ratio { get; private set; }
    public DateTime Integracao { get; private set; }
    public string Documento { get; private set; }
    public DateTime? Desintegracao { get; private set; }

    public bool Ativo => Desintegracao == null;

    public string Responsavel { get; private set; }
    public string CpfResponsavel { get; private set; }
    public DateTime? Cadastro { get; private set; }

    // Navigation Properties
    public ConsorcioDto Consorcio { get; private set; }
    public EmpresaDto Empresa { get; private set; }
  }
}
