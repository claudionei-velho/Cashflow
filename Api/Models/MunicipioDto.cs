using System.Collections.Generic;

namespace Api.Models {
  public class MunicipioDto {
    public int Id { get; set; }
    public int UfId { get; set; }
    public string Nome { get; set; }
    public string Estado { get; set; }

    // Navigation Properties
    public UfDto Uf { get; set; }
  }
}
