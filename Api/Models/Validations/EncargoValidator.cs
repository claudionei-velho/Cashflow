using FluentValidation;

using Api.Properties;

namespace Api.Models.Validations {
  public class EncargoValidator : AbstractValidator<EncargoDto> {
    public EncargoValidator() {
      RuleFor(e => e.Grupo)
          .NotNull().WithMessage(x => $"{Resources.GrupoRequired} {Resources.EncargoDto}")
          .NotEmpty().WithMessage(x => $"{Resources.GrupoRequired} {Resources.EncargoDto}");

      RuleFor(e => e.Denominacao)
          .NotNull().WithMessage(x => $"{Resources.DenominacaoRequired} {Resources.EncargoDto}")
          .NotEmpty().WithMessage(x => $"{Resources.DenominacaoRequired} {Resources.EncargoDto}")
          .MaximumLength(64);
    }
  }
}
