namespace Api.Models {
  public class FrotaHorariaDto {
    public int EmpresaId { get; set; }
    public int Hora { get; set; }
    public string Faixa { get; set; }
    public int? Viagens { get; set; }
    public int? Veiculos { get; set; }
    public decimal? KmTotal { get; set; }

    // Navigation Properties
    public EmpresaDto Empresa { get; private set; }
  }
}
