using System;

namespace Api.Models {
  public class CstCarroceriaDto {
    public int Id { get; set; }
    public int EmpresaId { get; set; }
    public int Ano { get; set; }
    public int Mes { get; set; }
    public int ClasseId { get; set; }
    public string Marca { get; set; }
    public string Modelo { get; set; }
    public decimal? UnitAr { get; set; }
    public bool ArCondicionado => UnitAr != null;

    public decimal? UnitElevatoria { get; set; }
    public bool Elevatoria => UnitElevatoria != null;

    public decimal Unitario { get; set; }
    public decimal Ponderado => (UnitAr ?? 0) + (UnitElevatoria ?? 0) + Unitario;

    public DateTime? Cadastro { get; set; }

    // Navigation Properties
    public EmpresaDto Empresa { get; set; }
    public CVeiculoDto CVeiculo { get; set; }
  }
}
