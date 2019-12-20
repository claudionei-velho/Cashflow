using System;

namespace Api.Models {
  public class CstPneuDto {
    public int Id { get; set; }
    public int EmpresaId { get; set; }
    public int Ano { get; set; }
    public int Mes { get; set; }
    public int ClasseId { get; set; }
    public decimal UnitPneu { get; set; }
    public decimal UnitRecap { get; set; }
    public DateTime? Cadastro { get; set; }

    // Navigation Properties
    public EmpresaDto Empresa { get; set; }
    public CVeiculoDto CVeiculo { get; set; }
  }
}
