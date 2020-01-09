using System;

namespace Api.Models {
  public class LoteDto {
    public int Id { get; set; }
    public int BaciaId { get; set; }
    public string Denominacao { get; set; }
    public DateTime? Cadastro { get; set; }

    // Navigation Properties
    public BaciaDto Bacia { get; private set; }
  }
}
