using System.Collections.Generic;

namespace Domain.Models {
  public class Uf {
    public int Id { get; private set; }
    public string Sigla { get; private set; }
    public string Estado { get; private set; }
    public string Capital { get; private set; }
    public string Regiao { get; private set; }
    public int? Unidades { get; private set; }

    // Navigation Properties
    public ICollection<Municipio> Municipios { get; private set; }
  }
}
