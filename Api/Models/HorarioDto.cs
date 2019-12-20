using System;

namespace Api.Models {
  public class HorarioDto {
    public int Id { get; set; }
    public int LinhaId { get; set; }
    public int DiaId { get; set; }
    public string Sentido { get; set; }
    public TimeSpan Inicio { get; set; }
    public int? AtendimentoId { get; set; }
    public DateTime? Cadastro { get; set; }

    // Navigation Properties
    public AtendimentoDto Atendimento { get; set; }
    public LinhaDto Linha { get; set; }
  }
}
