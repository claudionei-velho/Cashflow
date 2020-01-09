using System;

namespace Api.Models {
  public class CstCombustivelDto {
    public int Id { get; set; }
    public int EmpresaId { get; set; }
    public int Ano { get; set; }
    public int Mes { get; set; }
    public int CombustivelId { get; set; }
    public decimal Unitario { get; set; }
    public DateTime? Cadastro { get; set; }

    // Navigation Properties
    public EmpresaDto Empresa { get; private set; }
    public AnpProdutoDto Combustivel { get; private set; }
  }
}
