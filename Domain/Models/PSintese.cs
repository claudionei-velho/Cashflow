using System;

using Domain.Extensions;
using Domain.Lists;

namespace Domain.Models {
  public class PSintese {
    public int EmpresaId { get; private set; }
    public int DiaId { get; private set; }

    public string DiaIdCap {
      get {
        return Workday.Items[DiaId];
      }
    }

    public int Dias { get; private set; }
    public int Viagens { get; private set; }
    public decimal Percurso { get; private set; }

    public int? ViagensSemana {
      get {
        return Dias * Viagens / CustomCalendar.WeeksPerYear;
      }
    }

    public decimal? PercursoSemana {
      get {
        return Math.Round(Dias * Percurso / CustomCalendar.WeeksPerYear, 2);
      }
    }

    public int? ViagensMes {
      get {
        return Dias * Viagens / CustomCalendar.MonthsPerYear;
      }
    }

    public decimal? PercursoMes {
      get {
        return Math.Round(Dias * Percurso / CustomCalendar.MonthsPerYear, 1);
      }
    }

    public int? ViagensAno {
      get {
        return Dias * Viagens;
      }
    }

    public decimal? PercursoAno {
      get {
        return Math.Round(Dias * Percurso, 1);
      }
    }

    // Navigation Properties
    public Empresa Empresa { get; private set; }
  }
}
