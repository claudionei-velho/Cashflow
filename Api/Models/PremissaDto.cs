﻿using System;

namespace Api.Models {
  public class PremissaDto {
    public int Id { get; set; }
    public int EmpresaId { get; set; }
    public int Ano { get; set; }
    public int Mes { get; set; }
    public decimal KmProdutivo { get; set; }
    public decimal? KmImprodutivo { get; set; }

    public decimal KmProgramado {
      get {
        return KmProdutivo + (KmImprodutivo ?? 0);
      }
    }

    public int FrotaOperacao { get; set; }
    public int? FrotaReserva { get; set; }

    public int FrotaTotal {
      get {
        return FrotaOperacao + (FrotaReserva ?? 0);
      }
    }

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

    public decimal IdadeFrota { get; set; }
    public DateTime? InicioContrato { get; set; }
    public DateTime? TerminoContrato { get; set; }
    public int Demanda { get; set; }
    public int Equivalente { get; set; }

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

    public decimal? IPKe { get; set; }
    public DateTime? Cadastro { get; set; }

    // Navigation Properties
    public EmpresaDto Empresa { get; private set; }
  }
}
