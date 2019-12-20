using System;

namespace Domain.Models {
  public class CstChassi {
    public int Id { get; private set; }
    public int EmpresaId { get; private set; }
    public int Ano { get; private set; }
    public int Mes { get; private set; }
    public int ClasseId { get; private set; }
    public string Marca { get; private set; }
    public string Modelo { get; private set; }
    public int Quantidade { get; private set; }
    public decimal Unitario { get; private set; }
    public DateTime? Cadastro { get; private set; }

    // Navigation Properties
    public Empresa Empresa { get; private set; }
    public CVeiculo CVeiculo { get; private set; }
  }
}
