using System;

namespace Api.Models {
  public class PlanoDto {
    public int Id { get; set; }
    public int LinhaId { get; set; }
    public int? AtendimentoId { get; set; }
    public string Sentido { get; set; }
    public int? ViagensUtil { get; set; }
    public int? ViagensSab { get; set; }
    public int? ViagensDom { get; set; }
    public DateTime? Cadastro { get; set; }

    // Navigation Properties
    public AtendimentoDto Atendimento { get; set; }
    public LinhaDto Linha { get; set; }    
  }
}
