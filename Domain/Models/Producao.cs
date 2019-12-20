using System;

namespace Domain.Models {
  public class Producao {
    public int Id { get; private set; }
    public int EmpresaId { get; private set; }
    public int Ano { get; private set; }
    public int Mes { get; private set; }
    public int TarifariaId { get; private set; }
    public int? LinhaId { get; private set; }
    public int Passageiros { get; private set; }
    public DateTime? Cadastro { get; private set; }

    // Navigation Properties
    public Empresa Empresa { get; private set; }
    public Linha Linha { get; private set; }
    public TCategoria TCategoria { get; private set; }
  }
}
