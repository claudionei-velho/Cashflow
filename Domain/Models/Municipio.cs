using System.Collections.Generic;

namespace Domain.Models {
  public class Municipio {
    public int Id { get; private set; }
    public int UfId { get; private set; }
    public string Nome { get; private set; }
    public string Estado { get; private set; }

    // Navigation Properties
    public Uf Uf { get; set; }

    public ICollection<Empresa> Empresas { get; set; }
    public ICollection<Fornecedor> Fornecedores { get; set; }
  }
}
