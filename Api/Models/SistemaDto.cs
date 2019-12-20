using System;

namespace Api.Models {
  public class SistemaDto {
    public int Id { get; set; }
    public string Codigo { get; set; }
    public string Denominacao { get; set; }
    public DateTime? Cadastro { get; set; }
  }
}
