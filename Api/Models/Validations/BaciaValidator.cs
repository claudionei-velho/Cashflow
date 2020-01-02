using FluentValidation;

using Api.Properties;

namespace Api.Models.Validations {
  public class BaciaValidator : AbstractValidator<BaciaDto> {
    public BaciaValidator() {
      RuleFor(c => c.MunicipioId)
          .NotNull().WithMessage(x => $"{Resources.MunicipioIdRequired} {Resources.BaciaDto}");

      RuleFor(c => c.Denominacao)
          .NotNull().WithMessage(x => $"{Resources.DenominacaoRequired} {Resources.BaciaDto}")
          .NotEmpty().WithMessage(x => $"{Resources.DenominacaoRequired} {Resources.BaciaDto}")
          .MaximumLength(64);

      RuleFor(c => c.Descricao).MaximumLength(256);     
    }
  }
}
