using System;

using Domain.Extensions;
using Domain.Lists;

namespace Api.Models {
  public class PSinteseDto {
    public int EmpresaId { get; set; }
    public int DiaId { get; set; }

    public string DiaIdCap {
      get {
        return Workday.Items[DiaId];
      }
    }

    public int Dias { get; set; }
    public int Viagens { get; set; }
    public decimal Percurso { get; set; }

    public int? ViagensSemana {
      get {
        return (this.Dias * this.Viagens) / CustomCalendar.WeeksPerYear;
      }
    }

    public decimal? PercursoSemana {
      get {
        return Math.Round((this.Dias * this.Percurso) / CustomCalendar.WeeksPerYear, 2);
      }
    }

    public int? ViagensMes {
      get {
        return (this.Dias * this.Viagens) / CustomCalendar.MonthsPerYear;
      }
    }

    public decimal? PercursoMes {
      get {
        return Math.Round((this.Dias * this.Percurso) / CustomCalendar.MonthsPerYear, 1);
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
    public EmpresaDto Empresa { get; private set; }
  }
}
