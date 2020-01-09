using System;
using System.Text;

using Domain.Lists;

namespace Api.Models {
  public class AtendimentoDto {
    private readonly char[] _charsToTrim = { ' ', ';', '/' };

    public int Id { get; set; }
    public int LinhaId { get; set; }
    public string Prefixo { get; set; }
    public string Denominacao { get; set; }

    public string Descricao => $"{Prefixo} | {Denominacao}";

    public bool Uteis { get; set; }
    public bool Sabados { get; set; }
    public bool Domingos { get; set; }

    public string DiasOperacao {
      get {
        var aux = new StringBuilder();
        if (Uteis) {
          aux.Append($"{new Workday().Items[1]}; ");
        }
        if (Sabados) {
          aux.Append($"{new Workday().Items[2]}; ");
        }
        if (Domingos) {
          aux.Append(new Workday().Items[3]);
        }
        return aux.ToString().Trim(this._charsToTrim);
      }
    }

    public decimal? ExtensaoAB { get; set; }
    public decimal? ExtensaoBA { get; set; }

    public decimal? Extensao => (ExtensaoAB ?? 0) + (ExtensaoBA ?? 0);

    public DateTime? Cadastro { get; set; }

    // Navigation Properties
    public LinhaDto Linha { get; private set; }
  }
}
