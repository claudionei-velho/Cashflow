using System;

namespace Api.Models {
  public class FuncaoDto {
    public int Id { get; set; }
    public int CargoId { get; set; }
    public int DepartamentoId { get; set; }
    public string Titulo { get; set; }
    public int? CentroId { get; set; }
    public int? ContaId { get; set; }
    public DateTime? Desvinculado { get; set; }

    public bool Vigente => Desvinculado == null;

    public DateTime? Cadastro { get; set; }

    // Navigation Properties
    public CargoDto Cargo { get; set; }
    public CentroDto Centro { get; set; }
    public ContaDto Conta { get; set; }
    public DepartamentoDto Departamento { get; set; }
  }
}
