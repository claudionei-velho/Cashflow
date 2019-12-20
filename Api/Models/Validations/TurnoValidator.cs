using FluentValidation;

using Api.Properties;

namespace Api.Models.Validations {
  public class TurnoValidator : AbstractValidator<TurnoDto> {
    public TurnoValidator() {
      RuleFor(c => c.EmpresaId).NotNull().WithMessage(x => Resources.EmpresaIdRequired);
      RuleFor(c => c.Denominacao)
          .NotNull().WithMessage(x => $"{Resources.DenominacaoRequired} {Resources.TurnoDto}")
          .NotEmpty().WithMessage(x => $"{Resources.DenominacaoRequired} {Resources.TurnoDto}")
          .MaximumLength(32);
    }
  }
}
