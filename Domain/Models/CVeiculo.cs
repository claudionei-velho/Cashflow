using System.Collections.Generic;

namespace Domain.Models {
  public class CVeiculo {
    public int Id { get; private set; }
    public string Categoria { get; private set; }
    public string Classe { get; private set; }
    public int? Minimo { get; private set; }
    public int? Maximo { get; private set; }

    // Navigation Properties
    public ICollection<CstCarroceria> CstCarrocerias { get; private set; }
    public ICollection<CstChassi> CstChassis { get; private set; }
    public ICollection<CstPneu> CstPneus { get; private set; }
    public ICollection<ECVeiculo> ECVeiculos { get; private set; }
    public ICollection<Frota> Frotas { get; private set; }
    public ICollection<PCombustivel> PCombustiveis { get; private set; }
    public ICollection<VCatalogo> VCatalogos { get; private set; }
    public ICollection<Veiculo> Veiculos { get; private set; }
  }
}
