using System;

namespace Api.Models {
  public class ECVeiculoDto {
    public int Id { get; set; }
    public int EmpresaId { get; set; }
    public int ClasseId { get; set; }
    public int? Minimo { get; set; }
    public int? Maximo { get; set; }
    public byte Passageirom2 { get; set; }
    public byte Pneus { get; set; }
    public int? Util { get; set; }
    public decimal? Residual { get; set; }
    public DateTime? Cadastro { get; set; }

    // Navigation Properties
    public CVeiculoDto CVeiculo { get; private set; }
    public EmpresaDto Empresa { get; private set; }
  }
}
