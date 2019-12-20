using System;

using Domain.Lists;

namespace Domain.Models {
  public class Carroceria {
    public int VeiculoId { get; private set; }
    public string Fabricante { get; private set; }
    public string Modelo { get; private set; }
    public string Referencia { get; private set; }
    public int? Ano { get; private set; }
    public DateTime? Aquisicao { get; private set; }
    public string Fornecedor { get; private set; }
    public string NotaFiscal { get; private set; }
    public decimal? Valor { get; private set; }
    public string ChaveNfe { get; private set; }
    public DateTime? Encarrocamento { get; private set; }
    public string QuemEncarroca { get; private set; }
    public string NotaEncarroca { get; private set; }
    public decimal? ValorEncarroca { get; private set; }
    public byte Portas { get; private set; }
    public byte? Assentos { get; private set; }
    public byte? Capacidade { get; private set; }
    public string Piso { get; private set; }
    public bool EscapeV { get; private set; }
    public bool EscapeH { get; private set; }
    public int? Catraca { get; private set; }
    public string CatracaCap => new Posicao().Items[Catraca ?? 0];

    public int PortaIn { get; private set; }
    public string PortaInCap => new Posicao().Items[PortaIn];

    public bool SaidaFrente { get; private set; }
    public bool SaidaMeio { get; private set; }
    public bool SaidaTras { get; private set; }

    // Navigation Properties
    public Veiculo Veiculo { get; private set; }
  }
}
