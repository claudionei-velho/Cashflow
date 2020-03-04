using System;
using System.Collections.Generic;

namespace Domain.Models {
  public class Funcao {
    public int Id { get; private set; }
    public int CargoId { get; private set; }
    public int DepartamentoId { get; private set; }
    public string Titulo { get; private set; }
    public int? CentroId { get; private set; }
    public int? ContaId { get; private set; }
    public DateTime? Desvinculado { get; private set; }

    public bool Vigente {
      get {
        return Desvinculado == null;
      }
    }

    public DateTime? Cadastro { get; private set; }

    // Navigation Properties
    public Cargo Cargo { get; private set; }
    public Centro Centro { get; private set; }
    public Conta Conta { get; private set; }
    public Departamento Departamento { get; private set; }

    public ICollection<FuFuncao> FuFuncoes { get; private set; }
    public ICollection<Salario> Salarios { get; private set; }
    public ICollection<SistFuncao> SistFuncoes { get; private set; }
  }
}
