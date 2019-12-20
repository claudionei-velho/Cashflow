using FluentValidation;

using Api.Properties;

namespace Api.Models.Validations {
  public class EmbarcadoValidator : AbstractValidator<EmbarcadoDto> {
    public EmbarcadoValidator() {
      RuleFor(e => e.VeiculoId).NotNull().WithMessage(x => Resources.VeiculoIdRequired);
      RuleFor(e => e.EquipamentoId).NotNull().WithMessage(x => Resources.EquipamentoIdRequired);
      RuleFor(e => e.Quantidade)
          .NotNull().WithMessage(x => Resources.QuantidadeRequired)
          .GreaterThan(0);
    }
  }
}
