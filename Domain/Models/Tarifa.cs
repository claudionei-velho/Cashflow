using System;

namespace Domain.Models {
  public class Tarifa {
    public int Id { get; private set; }
    public int EmpresaId { get; private set; }
    public DateTime Referencia { get; private set; }
    public decimal Valor { get; private set; }
    public string Decreto { get; private set; }

    public Empresa Empresa { get; private set; }
  }
}
