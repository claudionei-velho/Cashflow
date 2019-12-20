using System;

namespace Domain.Models {
  public class PCombustivel {
    public int Id { get; private set; }
    public int EmpresaId { get; private set; }
    public int Ano { get; private set; }
    public int Mes { get; private set; }
    public int ClasseId { get; private set; }
    public int CombustivelId { get; private set; }
    public decimal? CoeficienteComAr { get; private set; }
    public decimal? CoeficienteSemAr { get; private set; }
    public DateTime? Cadastro { get; private set; }

    // Navigation Properties
    public Empresa Empresa { get; private set; }
    public CVeiculo CVeiculo { get; private set; }
    public AnpProduto Combustivel { get; private set; }
  }
}
