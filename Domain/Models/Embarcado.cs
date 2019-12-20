using System;

namespace Domain.Models {
  public class Embarcado {
    public int Id { get; private set; }
    public int VeiculoId { get; private set; }
    public int EquipamentoId { get; private set; }
    public string SerieNo { get; private set; }
    public string Fabricante { get; private set; }
    public DateTime? Aquisicao { get; private set; }
    public string ChaveNfe { get; private set; }
    public decimal Quantidade { get; private set; }
    public DateTime? Instalacao { get; private set; }
    public int? OSInstala { get; private set; }
    public string Localizacao { get; private set; }
    public DateTime? Desinstalacao { get; private set; }
    public int? OSDesinstala { get; private set; }

    // Navigation Properties
    public Veiculo Veiculo { get; private set; }
    public VEquipamento Equipamento { get; private set; }    
  }
}
