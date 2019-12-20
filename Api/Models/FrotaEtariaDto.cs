namespace Api.Models {
  public class FrotaEtariaDto {
    public int EmpresaId { get; set; }
    public int EtariaId { get; set; }
    public int? Micro { get; set; }
    public int? Mini { get; set; }
    public int? Midi { get; set; }
    public int? Basico { get; set; }
    public int? Padron { get; set; }
    public int? Especial { get; set; }
    public int? Articulado { get; set; }
    public int? BiArticulado { get; set; }
    public int? Frota { get; set; }
    public decimal? Ratio { get; set; }
    public int? EqvIdade { get; set; }

    // Navigation Properties
    public EmpresaDto Empresa { get; set; }
    public FxEtariaDto FxEtaria { get; set; }
  }
}
