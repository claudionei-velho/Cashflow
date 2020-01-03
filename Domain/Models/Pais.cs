using System.Collections.Generic;

namespace Domain.Models {
  public class Pais {
    public string Id { get; private set; }
    public string Nome { get; private set; }
    public string Capital { get; private set; }
    public string Continente { get; private set; }

    // Navigation Properties
    public ICollection<Consorcio> Consorcios { get; private set; }
    public ICollection<Empresa> Empresas { get; private set; }
    public ICollection<Fornecedor> Fornecedores { get; private set; }
  }
}
