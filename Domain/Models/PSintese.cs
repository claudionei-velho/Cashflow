using System;

using Domain.Lists;

namespace Domain.Models {
  public class PSintese {
    public int EmpresaId { get; private set; }
    public int DiaId { get; private set; }

    public string DiaIdCap => new Workday().Items[DiaId];

    public int Dias { get; private set; }
    public int Viagens { get; private set; }
    public decimal Percurso { get; private set; }

    public int? ViagensSemana => Dias * Viagens / CustomCalendar.WeeksPerYear;

    public decimal? PercursoSemana => Math.Round(Dias * Percurso / CustomCalendar.WeeksPerYear, 2);

    public int? ViagensMes => Dias * Viagens / CustomCalendar.MonthsPerYear;

    public decimal? PercursoMes => Math.Round(Dias * Percurso / CustomCalendar.MonthsPerYear, 1);

    public int? ViagensAno => Dias * Viagens;

    public decimal? PercursoAno => Math.Round(Dias * Percurso, 1);

    // Navigation Properties
    public Empresa Empresa { get; private set; }
  }
}
