using System;
using System.Collections.Generic;
using System.Text;

using Domain.Lists;

namespace Domain.Models {
  public class Atendimento {
    private readonly char[] charsToTrim = { ' ', ';', '/' };

    public int Id { get; private set; }
    public int LinhaId { get; private set; }
    public string Prefixo { get; private set; }
    public string Denominacao { get; private set; }

    public string Descricao {
      get {
        return $"{Prefixo} | {Denominacao}";
      }
    }

    public bool Uteis { get; private set; }
    public bool Sabados { get; private set; }
    public bool Domingos { get; private set; }

    public string DiasOperacao {
      get {
        StringBuilder aux = new StringBuilder();
        if (Uteis) {
          aux.Append($"{Workday.Items[1]}; ");
        }
        if (Sabados) {
          aux.Append($"{Workday.Items[2]}; ");
        }
        if (Domingos) {
          aux.Append(Workday.Items[3]);
        }
        return aux.ToString().Trim(charsToTrim);
      }
    }

    public decimal? ExtensaoAB { get; private set; }
    public decimal? ExtensaoBA { get; private set; }

    public decimal? Extensao {
      get {
        return (ExtensaoAB ?? 0) + (ExtensaoBA ?? 0);
      }
    }

    public DateTime? Cadastro { get; private set; }

    // Navigation Properties
    public Linha Linha { get; private set; }

    public ICollection<Horario> Horarios { get; private set; }
    public ICollection<Operacional> Operacionais { get; private set; }
    public ICollection<Plano> Planos { get; private set; }
  }
}
