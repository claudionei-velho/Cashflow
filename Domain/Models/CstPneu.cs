using System;

namespace Domain.Models {
  public class CstPneu {
    public int Id { get; private set; }
    public int EmpresaId { get; private set; }
    public int Ano { get; private set; }
    public int Mes { get; private set; }
    public int ClasseId { get; private set; }
    public decimal UnitPneu { get; private set; }
    public decimal UnitRecap { get; private set; }
    public DateTime? Cadastro { get; private set; }

    // Navigation Properties
    public Empresa Empresa { get; private set; }
    public CVeiculo CVeiculo { get; private set; }
  }
}
