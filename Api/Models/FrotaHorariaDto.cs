namespace Api.Models {
  public class FrotaHorariaDto {
    public int EmpresaId { get; private set; }
    public int Hora { get; private set; }
    public string Faixa { get; private set; }
    public int? Viagens { get; private set; }
    public int? Veiculos { get; private set; }
    public decimal? KmTotal { get; private set; }

    // Navigation Properties
    public EmpresaDto Empresa { get; private set; }
  }
}
