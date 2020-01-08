using System;

namespace Domain.Models {
  public class Premissa {
    public int Id { get; private set; }
    public int EmpresaId { get; private set; }
    public int Ano { get; private set; }
    public int Mes { get; private set; }
    public decimal KmProdutivo { get; private set; }
    public decimal? KmImprodutivo { get; private set; }

    public decimal KmProgramado => KmProdutivo + (KmImprodutivo ?? 0);

    public int FrotaOperacao { get; private set; }
    public int? FrotaReserva { get; private set; }

    public int FrotaTotal => FrotaOperacao + (FrotaReserva ?? 0);

    public decimal? PMm {
      get {
        try {
          return KmProgramado / FrotaOperacao;
        }
        catch (DivideByZeroException) {
          return null;
        }
      }
    }

    public decimal IdadeFrota { get; private set; }
    public DateTime? InicioContrato { get; private set; }
    public DateTime? TerminoContrato { get; private set; }
    public int Demanda { get; private set; }
    public int Equivalente { get; private set; }

    public decimal? Equivalencia {
      get {
        try {
          return (decimal)Equivalente / Demanda;
        }
        catch (DivideByZeroException) {
          return null;
        }
      }
    }

    public decimal? IPKe { get; private set; }
    public DateTime? Cadastro { get; private set; }

    // Navigation Properties
    public Empresa Empresa { get; private set; }
  }
}
