namespace Api.Models {
  public class CVeiculoDto {
    public int Id { get; set; }
    public string Categoria { get; set; }
    public string Classe { get; set; }
    public int? Minimo { get; set; }
    public int? Maximo { get; set; }
  }
}
