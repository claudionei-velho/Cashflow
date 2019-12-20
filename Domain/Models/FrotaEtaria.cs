namespace Domain.Models {
  public class FrotaEtaria {
    public int EmpresaId { get; private set; }
    public int EtariaId { get; private set; }
    public int? Micro { get; private set; }
    public int? Mini { get; private set; }
    public int? Midi { get; private set; }
    public int? Basico { get; private set; }
    public int? Padron { get; private set; }
    public int? Especial { get; private set; }
    public int? Articulado { get; private set; }
    public int? BiArticulado { get; private set; }
    public int? Frota { get; private set; }
    public decimal? Ratio { get; private set; }
    public int? EqvIdade { get; private set; }

    // Navigation Properties
    public Empresa Empresa { get; private set; }
    public FxEtaria FxEtaria { get; private set; }
  }
}
