using System;

namespace Api.Models {
  public class TarifaDto {
    public int Id { get; set; }
    public int EmpresaId { get; set; }
    public DateTime Referencia { get; set; }
    public decimal Valor { get; set; }
    public string Decreto { get; set; }

    public EmpresaDto Empresa { get; private set; }
  }
}
