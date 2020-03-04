using System;

namespace Domain.Models {
  public class FuFuncao {
    public int Id { get; private set; }
    public int EmpresaId { get; private set; }
    public int Ano { get; private set; }
    public int Mes { get; private set; }
    public int FuncaoId { get; private set; }
    public int Titular { get; private set; }
    public int? Ferista { get; private set; }
    public int? Folguista { get; private set; }
    public int? Reserva { get; private set; }

    public int Soma {
      get {
        return Titular + (Ferista ?? 0) + (Folguista ?? 0) + (Reserva ?? 0);
      }
    }

    public DateTime? Cadastro { get; private set; }

    // Navigation Properties
    public Empresa Empresa { get; private set; }
    public Funcao Funcao { get; private set; }
  }
}