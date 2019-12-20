using System;

namespace Api.Models {
  public class PCombustivelDto {
    public int Id { get; set; }
    public int EmpresaId { get; set; }
    public int Ano { get; set; }
    public int Mes { get; set; }
    public int ClasseId { get; set; }
    public int CombustivelId { get; set; }
    public decimal? CoeficienteComAr { get; set; }
    public decimal? CoeficienteSemAr { get; set; }
    public DateTime? Cadastro { get; set; }

    // Navigation Properties
    public EmpresaDto Empresa { get; set; }
    public CVeiculoDto CVeiculo { get; set; }
    public AnpProdutoDto Combustivel { get; set; }
  }
}
