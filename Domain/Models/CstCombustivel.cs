using System;

namespace Domain.Models {
  public class CstCombustivel {
    public int Id { get; private set; }
    public int EmpresaId { get; private set; }
    public int Ano { get; private set; }
    public int Mes { get; private set; }
    public int CombustivelId { get; private set; }
    public decimal Unitario { get; private set; }
    public decimal? Frete { get; private set; }

    public decimal Custo {
      get {
        return Unitario * (1 + (Frete ?? 0));
      }
    }

    public DateTime? Cadastro { get; private set; }

    // Navigation Properties
    public Empresa Empresa { get; private set; }
    public AnpProduto Combustivel { get; private set; }
  }
}
