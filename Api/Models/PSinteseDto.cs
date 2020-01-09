using System;

using Domain.Extensions;
using Domain.Lists;

namespace Api.Models {
  public class PSinteseDto {
    public int EmpresaId { get; set; }
    public int DiaId { get; set; }

    public string DiaIdCap => new Workday().Items[DiaId];

    public int Dias { get; set; }
    public int Viagens { get; set; }
    public decimal Percurso { get; set; }

    public int? ViagensSemana => (this.Dias * this.Viagens) / CustomCalendar.WeeksPerYear;

    public decimal? PercursoSemana => Math.Round((this.Dias * this.Percurso) / CustomCalendar.WeeksPerYear, 2);

    public int? ViagensMes => (this.Dias * this.Viagens) / CustomCalendar.MonthsPerYear;

    public decimal? PercursoMes => Math.Round((this.Dias * this.Percurso) / CustomCalendar.MonthsPerYear, 1);

    public int? ViagensAno => Dias * Viagens;

    public decimal? PercursoAno => Math.Round(Dias * Percurso, 1);

    // Navigation Properties
    public EmpresaDto Empresa { get; private set; }
  }
}
