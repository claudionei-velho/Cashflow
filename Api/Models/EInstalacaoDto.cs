using System;

namespace Api.Models {
  public class EInstalacaoDto {
    public int Id { get; set; }
    public int InstalacaoId { get; set; }
    public int PropositoId { get; set; }
    public decimal? AreaCoberta { get; set; }
    public decimal? AreaTotal { get; set; }
    public int? Empregados { get; set; }
    public int? ContaId { get; set; }
    public TimeSpan? Inicio { get; set; }
    public TimeSpan? Termino { get; set; }
    public bool Efluentes { get; set; }
    public DateTime Cadastro { get; set; }

    // Navigation Properties
    public ContaDto Conta { get; private set; }
    public FInstalacaoDto FInstalacao { get; private set; }
    public InstalacaoDto Instalacao { get; private set; }
  }
}
