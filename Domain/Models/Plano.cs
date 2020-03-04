using System;

namespace Domain.Models {
  public class Plano {
    public int Id { get; private set; }
    public int LinhaId { get; private set; }
    public int? AtendimentoId { get; private set; }
    public string Sentido { get; private set; }
    public int? ViagensUtil { get; private set; }
    public int? ViagensSab { get; private set; }
    public int? ViagensDom { get; private set; }
    public DateTime? Cadastro { get; private set; }

    // Navigation Properties
    public Atendimento Atendimento { get; private set; }
    public Linha Linha { get; private set; }
  }
}
