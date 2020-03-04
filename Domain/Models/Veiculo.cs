using System;
using System.Collections.Generic;

using Domain.Lists;

namespace Domain.Models {
  public class Veiculo {
    public int Id { get; private set; }
    public int EmpresaId { get; private set; }
    public string Numero { get; private set; }
    public string Cor { get; private set; }
    public int Classe { get; private set; }
    public int? Categoria { get; private set; }

    public string CategoriaCap {
      get {
        return new Categoria().Items[Categoria ?? 0];
      }
    }

    public string Placa { get; private set; }
    public string Renavam { get; private set; }
    public string Antt { get; private set; }
    public DateTime? Inicio { get; private set; }
    public bool Inativo { get; private set; }
    public DateTime? Cadastro { get; private set; }

    // Navigation Properties
    public Empresa Empresa { get; private set; }
    public CVeiculo CVeiculo { get; private set; }

    public Chassi Chassis { get; private set; }
    public Carroceria Carrocerias { get; private set; }
    public ICollection<Embarcado> Embarcados { get; private set; }
  }
}
