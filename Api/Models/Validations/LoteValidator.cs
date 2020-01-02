using FluentValidation;

using Api.Properties;

namespace Api.Models.Validations {
  public class LoteValidator : AbstractValidator<LoteDto> {
    public LoteValidator() {
      RuleFor(c => c.BaciaId)
          .NotNull().WithMessage(x => Resources.BaciaIdRequired);

      RuleFor(c => c.Denominacao)
          .NotNull().WithMessage(x => $"{Resources.DenominacaoRequired} {Resources.LoteDto}")
          .NotEmpty().WithMessage(x => $"{Resources.DenominacaoRequired} {Resources.LoteDto}")
          .MaximumLength(32);
    }
  }
}
