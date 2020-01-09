using System;

namespace Api.Models {
  public class BaciaDto {
    public int Id { get; set; }
    public int MunicipioId { get; set; }
    public string Denominacao { get; set; }
    public string Descricao { get; set; }
    public DateTime? Cadastro { get; set; }

    // Navigation Properties
    public MunicipioDto Municipio { get; private set; }
  }
}
