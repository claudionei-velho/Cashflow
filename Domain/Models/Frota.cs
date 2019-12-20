namespace Domain.Models {
  public class Frota {
    public int Id { get; private set; }
    public int EmpresaId { get; private set; }
    public int Ano { get; private set; }
    public int Mes { get; private set; }
    public int CategoriaId { get; private set; }
    public int EtariaId { get; private set; }
    public bool ArCondicionado { get; private set; }
    public int Quantidade { get; private set; }

    // Navigation Properties
    public CVeiculo CVeiculo { get; private set; }
    public Empresa Empresa { get; private set; }
    public FxEtaria FxEtaria { get; private set; }
  }
}
