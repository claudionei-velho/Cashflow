using System;

namespace Api.Models {
  public class VCatalogoDto {
    public int Id { get; set; }
    public int EmpresaId { get; set; }
    public int Ano { get; set; }
    public int Mes { get; set; }
    public int ClasseId { get; set; }
    public int FornecedorId { get; set; }
    public decimal? Unitario { get; set; }
    public decimal? UnitarioAr { get; set; }
    public DateTime? Cadastro { get; set; }

    // Navigation Properties
    public EmpresaDto Empresa { get; set; }
    public CVeiculoDto CVeiculo { get; set; }
    public FornecedorDto Fornecedor { get; set; }
  }
}
