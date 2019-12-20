using FluentValidation;

using Api.Properties;

namespace Api.Models.Validations {
  public class PlanoValidator : AbstractValidator<PlanoDto> {
    public PlanoValidator() {
      RuleFor(p => p.LinhaId).NotNull().WithMessage(x => Resources.LinhaIdRequired);
      RuleFor(p => p.Sentido)
          .NotNull().WithMessage(x => Resources.SentidoRequired)
          .NotEmpty().WithMessage(x => Resources.SentidoRequired);
    }
  }
}
