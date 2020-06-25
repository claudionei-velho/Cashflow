using System;
using System.Text;

using Domain.Lists;

namespace Api.Models {
  public class LinhaDto {
    private readonly char[] charsToTrim = { ' ', ';', '/' };

    public int Id { get; set; }
    public int EmpresaId { get; set; }
    public string Prefixo { get; set; }
    public string Denominacao { get; set; }

    public string Descricao {
      get {
        return $"{Prefixo} | {Denominacao}";
      }
    }

    public bool Uteis { get; set; }
    public bool Sabados { get; set; }
    public bool Domingos { get; set; }

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

    public int DominioId { get; set; }
    public int OperacaoId { get; set; }
    public int Classificacao { get; set; }
    public bool Captacao { get; set; }
    public bool Transporte { get; set; }
    public bool Distribuicao { get; set; }

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

    public decimal? ExtensaoAB { get; set; }
    public decimal? ExtensaoBA { get; set; }

    public decimal? Extensao {
      get {
        return (ExtensaoAB ?? 0) + (ExtensaoBA ?? 0);
      }
    }

    public int? LoteId { get; set; }
    public DateTime? Cadastro { get; set; }

    // Navigation Properties
    public CLinhaDto CLinha { get; private set; }
    public EDominioDto EDominio { get; private set; }
    public EmpresaDto Empresa { get; private set; }
    public LoteDto Lote { get; private set; }
    public OperacaoDto Operacao { get; private set; }
  }
}
