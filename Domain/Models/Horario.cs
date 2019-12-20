using System;

namespace Domain.Models {
  public class Horario {
    public int Id { get; private set; }
    public int LinhaId { get; private set; }
    public int DiaId { get; private set; }
    public string Sentido { get; private set; }
    public TimeSpan Inicio { get; private set; }
    public int? AtendimentoId { get; private set; }
    public DateTime? Cadastro { get; private set; }

    // Navigation Properties
    public Atendimento Atendimento { get; private set; }
    public Linha Linha { get; private set; }
  }
}
