using System;

namespace Api.Models {
  public class CstChassiDto {
    public int Id { get; set; }
    public int EmpresaId { get; set; }
    public int Ano { get; set; }
    public int Mes { get; set; }
    public int ClasseId { get; set; }
    public string Marca { get; set; }
    public string Modelo { get; set; }
    public int Quantidade { get; set; }
    public decimal Unitario { get; set; }
    public DateTime? Cadastro { get; set; }

    // Navigation Properties
    public EmpresaDto Empresa { get; private set; }
    public CVeiculoDto CVeiculo { get; private set; }
  }
}
