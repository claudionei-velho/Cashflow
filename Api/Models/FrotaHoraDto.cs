using System;

namespace Api.Models {
  public class FrotaHoraDto {
    public int Id { get; set; }
    public int EmpresaId { get; set; }
    public int Ano { get; set; }
    public int Mes { get; set; }
    public int HorarioId { get; set; }
    public int Frota { get; set; }
    public DateTime? Cadastro { get; set; }

    // Navigation Properties
    public EmpresaDto Empresa { get; set; }
  }
}
