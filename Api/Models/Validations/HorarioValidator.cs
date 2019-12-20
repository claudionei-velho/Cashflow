using FluentValidation;

using Api.Properties;

namespace Api.Models.Validations {
  public class HorarioValidator : AbstractValidator<HorarioDto> {
    public HorarioValidator() {
      RuleFor(h => h.LinhaId).NotNull().WithMessage(x => Resources.LinhaIdRequired);
      RuleFor(h => h.DiaId).NotNull().WithMessage(x => Resources.DiaIdRequired);
      RuleFor(h => h.Sentido)
          .NotNull().WithMessage(x => Resources.SentidoRequired)
          .NotEmpty().WithMessage(x => Resources.SentidoRequired)
          .MaximumLength(2);

      RuleFor(h => h.Inicio).NotNull().WithMessage(x => Resources.InicioRequired);
    }
  }
}
