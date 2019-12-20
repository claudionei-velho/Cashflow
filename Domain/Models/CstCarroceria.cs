using System;

namespace Domain.Models {
  public class CstCarroceria {
    public int Id { get; private set; }
    public int EmpresaId { get; private set; }
    public int Ano { get; private set; }
    public int Mes { get; private set; }
    public int ClasseId { get; private set; }
    public string Marca { get; private set; }
    public string Modelo { get; private set; }
    public decimal? UnitAr { get; private set; }
    public bool ArCondicionado => UnitAr != null;

    public decimal? UnitElevatoria { get; private set; }
    public bool Elevatoria => UnitElevatoria != null;

    public decimal Unitario { get; private set; }
    public decimal Ponderado => (UnitAr ?? 0) + (UnitElevatoria ?? 0) + Unitario;

    public DateTime? Cadastro { get; private set; }

    // Navigation Properties
    public Empresa Empresa { get; private set; }
    public CVeiculo CVeiculo { get; private set; }
  }
}
