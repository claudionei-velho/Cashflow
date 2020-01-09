namespace Api.Models {
  public class NcmDto {
    public int Id { get; set; }
    public string Classificacao { get; set; }
    public string Descricao { get; set; }
    public decimal? Ipi { get; set; }
    public bool Vigente { get; set; }
    public int? GrupoId { get; set; }

    // Navigation Properties
    public NcmDto Agrupamento { get; private set; }
  }
}
