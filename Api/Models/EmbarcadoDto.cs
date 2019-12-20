using System;

namespace Api.Models {
  public class EmbarcadoDto {
    public int Id { get; set; }
    public int VeiculoId { get; set; }
    public int EquipamentoId { get; set; }
    public string SerieNo { get; set; }
    public string Fabricante { get; set; }
    public DateTime? Aquisicao { get; set; }
    public string ChaveNfe { get; set; }
    public decimal Quantidade { get; set; }
    public DateTime? Instalacao { get; set; }
    public int? OSInstala { get; set; }
    public string Localizacao { get; set; }
    public DateTime? Desinstalacao { get; set; }
    public int? OSDesinstala { get; set; }

    // Navigation Properties
    public VeiculoDto Veiculo { get; set; }
    public VEquipamentoDto Equipamento { get; set; }
  }
}
