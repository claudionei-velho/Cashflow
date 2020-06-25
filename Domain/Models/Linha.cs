using System;
using System.Collections.Generic;
using System.Text;

using Domain.Extensions;
using Domain.Lists;

namespace Domain.Models {
  public class Linha {
    private readonly char[] charsToTrim = { ' ', ';', '/' };

    public int Id { get; private set; }
    public int EmpresaId { get; private set; }
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

    public int DominioId { get; private set; }
    public int OperacaoId { get; private set; }
    public int Classificacao { get; private set; }
    public bool Captacao { get; private set; }
    public bool Transporte { get; private set; }
    public bool Distribuicao { get; private set; }

    public string Funcao {
      get {
        StringBuilder aux = new StringBuilder();
        if (Captacao) {
          aux.Append("Captação; ");
        }
        if (Transporte) {
          aux.Append("Transporte; ");
        }
        if (Distribuicao) {
          aux.Append("Distribuição");
        }
        return aux.ToString().Trim(charsToTrim);
      }
    }

    public decimal? ExtensaoAB { get; private set; }
    public decimal? ExtensaoBA { get; private set; }

    public decimal? Extensao {
      get {
        return Handler.NullIf((ExtensaoAB ?? 0) + (ExtensaoBA ?? 0), 0);
      }
    }

    public int? LoteId { get; private set; }
    public DateTime? Cadastro { get; private set; }

    // Navigation Properties
    public CLinha CLinha { get; private set; }
    public EDominio EDominio { get; private set; }
    public Empresa Empresa { get; private set; }
    public Lote Lote { get; private set; }
    public Operacao Operacao { get; private set; }

    public ICollection<Atendimento> Atendimentos { get; private set; }
    public ICollection<Horario> Horarios { get; private set; }
    public ICollection<Operacional> Operacionais { get; private set; }
    public ICollection<Plano> Planos { get; private set; }
    public ICollection<Producao> Producoes { get; private set; }
  }
}
