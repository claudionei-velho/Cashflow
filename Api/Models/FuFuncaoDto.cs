using System;

namespace Api.Models {
  public class FuFuncaoDto {
    public int Id { get; set; }
    public int EmpresaId { get; set; }
    public int Ano { get; set; }
    public int Mes { get; set; }
    public int FuncaoId { get; set; }
    public int Titular { get; set; }
    public int? Ferista { get; set; }
    public int? Folguista { get; set; }
    public int? Reserva { get; set; }

    public int Soma => Titular + (Ferista ?? 0) + (Folguista ?? 0) + (Reserva ?? 0);

    public DateTime? Cadastro { get; set; }

    // Navigation Properties
    public EmpresaDto Empresa { get; set; }
    public FuncaoDto Funcao { get; set; }
  }
}
